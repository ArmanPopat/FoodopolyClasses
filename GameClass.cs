using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using BoardClasses;
using FoodopolyClasses.PlayerClasses;
using StandardInformation;
using SetClasses;
using FoodopolyClasses.MultiplayerClasses;
using FoodopolyClasses.SetClasses;
using FoodopolyClasses.TurnClasses;
using FoodopolyClasses.Records;

namespace GameClasses;


//Needs to be split up SOLID/ interfaces and abstraction etc.
public class GameClass
{
    public PlayerClass? CurrentTurnPlayer { get 
        {
            if (PlayerList.Count == 0)
                return null;

            return PlayerList[CurrentTurnPos - 1];
        } }


    public TurnMultiplayerClass TurnMultiplayer { get; set; }
    public TurnClass Turn { get;set; }
    public List<PlayerClass> PlayerList { get; set; }
    public string Password { get; }
    public int Id { get; }

    public Dictionary<int, InitiateTradeRecord> TradeRecords { get; set; } = new Dictionary<int, InitiateTradeRecord>();


    public int CurrentTurnPos { get; internal set; }

    //private ChanceCards chanceCards;
    public GoOnADiet goOnADiet;
    public Dictionary<string, SetProp> setsPropDict;
    public Stations stations;
    public Utilities utilities;
    public List<CChest> cChests;
    public List<Chance> chances;

    //used just for land event-should find a better solution later
    //public List<int> FineBoardPoses { get; }
    //public List<int> BasePropBoardposes { get; }
    //public List<int> CChestBoardPoses { get; }
    //public List<int> ChanceBoardPoses { get; }


    public GameClass(int id, string password)
    {
        stations = new Stations(new List<Station>());
        utilities = new Utilities(new List<Utility>());

        cChests = new List<CChest>() {new CChest("CChest1", 2), new CChest("CChest2", 17), new CChest("CChest3", 33) };
        chances = new List<Chance>() {new Chance("Chance1", 7), new Chance("Chance2", 22), new Chance("Chance3", 36) };

        Id = id;

        TurnMultiplayer = new TurnMultiplayerClass();
        Turn = new TurnClass();

        password = password.Trim();  //Policy to trim all passwords
        if (password == "")
        {
            throw new InvalidCastException("Game Password Cannot Be Empty");
        }
        Password = password;

        PlayerList = new List<PlayerClass>();

        goOnADiet = new GoOnADiet();

        CurrentTurnPos = 1;

        //chanceCards = new ChanceCards();
        //foreach (int i in chanceCards.numListChanceCards)
        //{
        //    Console.WriteLine(i);
        //}

        //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"StandardInformation", "Properties.xml");
        //XElement root = XElement.Load(path);
        XElement root = XElement.Load("Properties.xml");
        //XElement root = XElement.Load("Properties.xml");  //Temp Solution, Will fix

        IEnumerable<XElement> sets = root.Elements("set");



        //Create a dictionary of set classes to hold sets
        setsPropDict = new Dictionary<string, SetProp>();


        foreach (XElement set in sets)
        {


            //This list will later be initialised into the Set Class
            List<Property> propertiesInSet = new List<Property>();
            List<Station> stationsInSet = new List<Station>();
            List<Utility> utilitiesInSet = new List<Utility>();

            //declared outside to access outside scope of try catch
            string setName;
            try
            {
                setName = (string)set.Attribute("set");  //force casr works, to string does not
            }
            catch (Exception)
            {
                Console.WriteLine("Set Element doest not have set attribute for name");
                throw;
            }

            //declare set instance beforehand
            SetProp setClassInstance = new SetProp(setName, new List<Property>());
            
            

            string[] propertyAttrsArray = { "name", "boardPosition", "price", "rentL1", "rentL2" };
            List<string> propertyAttrs = new List<string>(propertyAttrsArray);

            if (setName != "Utilities")
            {
                propertyAttrs.Add("rentL3");
                propertyAttrs.Add("rentL4");
                if (setName != "Stations")
                {
                    propertyAttrs.Add("rentL5");
                    propertyAttrs.Add("rent");
                    propertyAttrs.Add("upgradeCost");
                }
            }

            Console.WriteLine(set);
            IEnumerable<XElement> properties = set.Elements("property");
            foreach (XElement property in properties)
            {

                List<XElement> propertyParams = new List<XElement>();

                foreach (var attr in propertyAttrs)
                {
                    try
                    {
                        propertyParams.Add(property.Element(attr));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(attr + "may be null");
                        throw;
                    }
                }






                //ToDo Make this constructing code clearer, dict? //let's use something instead of force cast
                if (setName == "Stations")
                {
                    Station stationClassInst = new Station((string)propertyParams[0], (int)propertyParams[1], (int)propertyParams[2], (int)propertyParams[3],
                    (int)propertyParams[4], (int)propertyParams[5], (int)propertyParams[6], setClassInstance.Name);
                    stationsInSet.Add(stationClassInst);
                }
                else if (setName == "Utilities")
                {
                    Utility utilityClassInst = new Utility((string)propertyParams[0], (int)propertyParams[1], (int)propertyParams[2], (int)propertyParams[3],
                    (int)propertyParams[4], setClassInstance.Name);
                    utilitiesInSet.Add(utilityClassInst);
                }
                else
                {
                    Property propertyClassInst = new Property((string)propertyParams[0], (int)propertyParams[1], (int)propertyParams[2], (int)propertyParams[3],
                    (int)propertyParams[4], (int)propertyParams[5], (int)propertyParams[6], (int)propertyParams[7], (int)propertyParams[8], setClassInstance.Name, (int)propertyParams[9]);
                    propertiesInSet.Add(propertyClassInst);
                }

                //BoardSpace propertyClass = new Property((string));
                //propertiesInSet.Add(propertyClass);
                //Console.WriteLine(propertyClass.BoardPosition.GetType()); //test


            }

            //setClassInstance.Properties = propertiesInSet;

            if (setName == "Stations")
            {
                stations.Properties = stationsInSet;
            }
            else if (setName == "Utilities")
            {
                utilities.Properties = utilitiesInSet;
            }
            else
            {
                setClassInstance.Properties = propertiesInSet;
                setsPropDict.Add(setName, setClassInstance);
            }
            

        }
    }

    //method to add players to game
    public void AddPlayer(PlayerClass player)
    {
        if (PlayerList.Count>=4)
        {
            throw new InvalidOperationException("The Game is Full");
        }
        PlayerList.Add(player);
    }
}