using StandardInformation;
using BoardClasses;
using SetClasses;
using System.Diagnostics;
using GameClasses;
using FoodopolyClasses.SetClasses;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace FoodopolyClasses.PlayerClasses;

public class PlayerClass
{
    public int Cash { get; set; }
    public string Name { get; init; }
    public string Password { get; }
    public Icons? Icon { get; set; } //make this into a selection from a list
    //public int PlayingPos { get; init; }
    public int PlayerPos { get; set; }
    public bool Dieting { get; set; }
    public int NumOfDietRolls { get; set; }
    public int NumOfGODCards { get; set; } //Number of Get Out of Dieting Cards

    public bool Bankrupt { get; private set; }

    //public bool RolledYetThisTurn { get; private set; }


    public PlayerClass(string name, string password, int startingCash, Icons? icon, /*int playingPos,*/ int playerPos = 0)
    {
        //Taken out for now as test
        //if (startingCash > 0)
        //{
        //    Cash = startingCash;
        //}
        //else
        //{
        //    throw new ArgumentOutOfRangeException(nameof(startingCash), "Starting cash must be greater than 0");
        //}

        Cash = startingCash;

        name = name.Trim();  //Policy to trim all names
        if (name == "")
        {
            throw new InvalidCastException("Username Cannot Be Empty");
        }
        Name = name;

        password = password.Trim();  //Policy to trim all passwords
        if (password == "")
        {
            throw new InvalidCastException("User Password Cannot Be Empty");
        }
        Password = password;

        Icon = icon;

        //if ((playingPos >= 1) && (playingPos <= 4))
        //{
        //    PlayingPos = playingPos;
        //}
        //else
        //{
        //    throw new ArgumentOutOfRangeException(nameof(playingPos), "PlayingPos position must be between 1 and 4");
        //}

        PlayerPos = playerPos;
        Dieting = false;
        NumOfDietRolls = 0;
        NumOfGODCards = 0;
        Bankrupt = false;
    }

    Random random = new Random();  //add readonly???(vs recommends) unsure if it will cause problems

    //public int NumOfDoubleRolls { get; set ; }

    /*
     * A method that rolls dice once, this will be used in futher methods for the roll dice thingy at the start of a turn and for utilities and jail and such.
     * Returns the total and boolean whether a double was rolled in a tuple.
     */
    public (int Total, int Dice1, int Dice2, bool Double) RollDiceOnce()
    {
        int dice1 = random.Next(1, 7);
        int dice2 = random.Next(1, 7);
        int total = dice1 + dice2;
        //Console.WriteLine($"You rolled a {dice1} and a {dice2}, you move forward {total} spaces.");
        return (total, dice1, dice2, dice1 == dice2);
    }

    /*
     * Method that changes player postition for each roll, keeps track of doubles, doubles lead to extra roll except for 3 doubles in a row that send the player to jail.
     */
    public (string Msg, bool Double, bool Diet, int TotalRoll) StandardRollDiceEvent(GameClass game)
    {
        //int numOfDouble = 0;
        //var testCase = (Total : 12, Dice1: 6, Dice2: 6, Double: true);
        string msg;
        if (game.Turn.NumOfDoubles < 2)
        {
            var diceRoll = RollDiceOnce();
            msg = $"You rolled a {diceRoll.Dice1} and a {diceRoll.Dice2}, you move forward {diceRoll.Total} spaces.";
            //PlayerPos += diceRoll.Total;
            FullChangePlayerPos(diceRoll.Total);


            if (diceRoll.Double)
            {
                msg += "You rolled a double, roll again!";
                game.Turn.NumOfDoubles++;

            }
            else
            {
                //Ends roll Event
                game.Turn.RollEventDone = true;
            }
            return (msg, diceRoll.Double, false, diceRoll.Total);
        }
        var diceRoll2 = RollDiceOnce();
        if (diceRoll2.Double)
        {
            msg = $"You rolled a {diceRoll2.Dice1} and a {diceRoll2.Dice2}. \n";
            msg += "You rolled 3 doubles in a row. Go on a Diet!";
            //GOAD automatically ends rollevent
            game.goOnADiet.GoOnADietMethod(this, game);  //check
            return (msg, diceRoll2.Double, true, diceRoll2.Total);
        }
        else
        {
            msg = $"You rolled a {diceRoll2.Dice1} and a {diceRoll2.Dice2}, you move forward {diceRoll2.Total} spaces.";
            PlayerPos += diceRoll2.Total;
            //Ends roll Event
            game.Turn.RollEventDone = true;
            return (msg, diceRoll2.Double, false, diceRoll2.Total);

        }

    }

    /*
     * A method to call to change player position. Called by FullChangePlayerPos. This is internal to not accidentally call this method
     */
    internal (bool Forward, bool PassGo) ChangePlayerPos(int movement)
    {
        bool forward;
        if (movement >= 0)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }

        bool passGo;
        if (movement + PlayerPos >= 40)
        {
            passGo = true;
        }
        else
        {
            passGo = false;
        }

        PlayerPos = (movement + PlayerPos) % 40;

        return (forward, passGo);
    }

    public string? PassGo()
    {
        Cash += 200; //changeable
        return $"{Name} has passed Start and recieves 200.";
    }

    /*
     * Will return a bool for forward, added to increase customisation later.
     */
    public (bool, string?) FullChangePlayerPos(int movement)
    {
        var change = ChangePlayerPos(movement);
        string? msg = null;
        if (change.PassGo)
        {
            msg = PassGo();
        }
        return (change.Forward, msg);
    }


    public int GetNetWorth(GameClass game)
    {
        //int cash = Cash;
        int totalCashFromProperties = 0;
        foreach (KeyValuePair<string, SetProp> valuePair in game.setsPropDict)
        {

            foreach (Property property in valuePair.Value.Properties)
            {


                if (property.Owner == this)
                {
                    if (property.Mortgaged)
                    {
                        continue;
                    }
                    totalCashFromProperties += property.UpgradeCost / 2 * property.NumOfUpgrades;
                    totalCashFromProperties += property.Price / 2;
                }



            }

        }
        foreach (Station station in game.stations.Properties)
        {
            if (station.Owner == this)
            {
                if (station.Mortgaged)
                {
                    continue;
                }
                totalCashFromProperties += station.Price / 2;
            }
        }
        foreach (Utility utility in game.utilities.Properties)
        {
            if (utility.Owner == this)
            {
                if (utility.Mortgaged)
                {
                    continue;
                }
                totalCashFromProperties += utility.Price / 2;
            }
        }
        return (Cash + totalCashFromProperties);
    }

    //Figure out--think I have figured out
    public string? CheckIfBankrupt(GameClass game, PlayerClass player)
    {
        if (Cash >= 0)
        {
            return string.Empty;
        }
        int totalCashFromProperties = 0;
        foreach (KeyValuePair<string, SetProp> valuePair in game.setsPropDict)//Not Changed to GetNetWorth as optimised here (escapes as soon confirmed not bankrupt)
        {

            foreach (Property property in valuePair.Value.Properties)
            {


                if (property.Owner == this)
                {
                    if (property.Mortgaged)
                    {
                        continue;
                    }
                    totalCashFromProperties += property.UpgradeCost / 2 * property.NumOfUpgrades;
                    totalCashFromProperties += property.Price / 2;
                }

                if (totalCashFromProperties > -Cash)
                {
                    return string.Empty;
                }

            }

        }
        foreach (Station station in game.stations.Properties)
        {
            if (station.Owner == this)
            {
                if (station.Mortgaged)
                {
                    continue;
                }
                totalCashFromProperties += station.Price / 2;
            }
            if (totalCashFromProperties > -Cash)
            {
                return string.Empty;
            }
        }
        foreach (Utility utility in game.utilities.Properties)
        {
            if (utility.Owner == this)
            {
                if (utility.Mortgaged)
                {
                    continue;
                }
                totalCashFromProperties += utility.Price / 2;
            }
            if (totalCashFromProperties > -Cash)
            {
                return string.Empty;
            }
        }
        GoneBankrupt(player, game);
        return $"{player.Name} Has Gone Bankrupt. All their possesions have been returned to the bank.";
    }

    //method to give properties back to bank etc if bankrupcy is detected
    private void GoneBankrupt(PlayerClass player, GameClass game)
    {
        foreach (KeyValuePair<string, SetProp> keyValuePair in game.setsPropDict)
        {
            foreach (Property property in keyValuePair.Value.Properties)
            {
                if (property.Owner != player)
                {
                    continue;
                }
                property.ResetProperty();
            }
        }
        foreach (Station station in game.stations.Properties)
        {
            if (station.Owner != player)
            {
                continue;
            }
            station.ResetProperty();
        }
        foreach (Utility utility in game.utilities.Properties)
        {
            if (utility.Owner != player)
            {
                continue;
            }
            utility.ResetProperty();
        }
    }

    public string? DeductCash(int cash, GameClass game)
    {
        Cash -= cash;
        string? msg = CheckIfBankrupt(game, this);
        //string msg = string.Empty;   ///Only for testing
        return msg;
    }

    //Triggers Land Event, may wanna try simplify this later
    public async Task<(string DoTask, string? Result)> LandEventAsync(GameClass game)
    {
        if (PlayerPos >= 40)
        {
            PlayerPos = 0;
            throw new InvalidProgramException("Player Cannot Have A Board Position higher than 39");

        }
        foreach (KeyValuePair<string, SetProp> keyValue in game.setsPropDict)
        {
            foreach (Property property in keyValue.Value.Properties)
            {
                if (property.BoardPosition == this.PlayerPos)
                {
                    return await property.LandEvent(this, game);
                }
            }
        }
        foreach (Station station in game.stations.Properties)
        {
            if (station.BoardPosition == this.PlayerPos)
            {
                return await station.LandEvent(this, game);
            }
        }
        foreach (Utility utility in game.utilities.Properties)
        {
            if (utility.BoardPosition == this.PlayerPos)
            {
                return await utility.LandEvent(this, game);
            }
        }

        foreach (Chance chance in game.chances)
        {
            if (chance.BoardPosition == this.PlayerPos)
            {
                return await chance.LandEvent(this, game);
            }
        }
        foreach (CChest cChest in game.cChests)
        {
            if (cChest.BoardPosition == this.PlayerPos)
            {
                return await cChest.LandEvent(this, game);
            }
        }


        //JUST FOR TESTING
        return (string.Empty, string.Empty);
    }

    //method for player to buy, calls the specific buy method on property, MUST VALIDATE CURRENT TURN BEFORE CALLING
    public string BuyPlayer(GameClass thisGame)
    {
        string baseMsg = $"{Name} bought ";
        if (PlayerPos % 10 == 5)
        {
            Station station = thisGame.stations.Properties.First(station => station.BoardPosition == PlayerPos);

            station.Buy(this);
            return (baseMsg + $"{station.Name} for {station.Price}.");
        }
        if (PlayerPos == 12 || PlayerPos == 28)
        {
            Utility utility = thisGame.utilities.Properties.First(utility => utility.BoardPosition == PlayerPos);
            utility.Buy(this);
            return (baseMsg + $"{utility.Name} for {utility.Price}.");
        }
        else
        {
            try
            {
                Property? selectedProp = null;
                foreach (KeyValuePair<string, SetProp> keyValuePair in thisGame.setsPropDict)
                {

                    foreach (Property property in keyValuePair.Value.Properties)
                    {
                        if (property.BoardPosition == PlayerPos)
                        {
                            selectedProp = property;

                            selectedProp.Buy(this);

                        }

                    }

                }
                if (selectedProp == null)
                {
                    throw new InvalidDataException("Not Currently On A Property!");
                }
                return (baseMsg + $"{selectedProp.Name} for {selectedProp.Price}.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
    public (List<Station> Stations, List<Utility> Utilities, List<Property> Properties) GetOwnedPropsAndStuff(GameClass game)
    {
        List<Station> stations = new List<Station>();
        List<Utility> utilities = new List<Utility>();
        List<Property> properties = new List<Property>();
        foreach (Station station in game.stations.Properties)
        {
            if (!station.Owned)
            {
                continue;
            }
            if (station.Owner.Name == Name)
            {
                stations.Add(station);
            }
        }
        foreach (Utility utility in game.utilities.Properties)
        {
            if (!utility.Owned)
            {
                continue;
            }
            if (utility.Owner.Name == Name)
            {
                utilities.Add(utility);
            }
        }
        foreach (KeyValuePair<string, SetProp> keyValue in game.setsPropDict)
        {
            foreach (Property property in keyValue.Value.Properties)
            {
                if (!property.Owned)
                {
                    continue;
                }
                if (property.Owner.Name == Name)
                {
                    properties.Add(property);
                }
            }
        }

        return (stations, utilities, properties);
    }
}
