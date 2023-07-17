using System;
using System.Linq;
using System.Web;
using FoodopolyClasses.BoardClasses;
using FoodopolyClasses.Cards.CChestCards;
using FoodopolyClasses.Cards;
using FoodopolyClasses.PlayerClasses;

namespace BoardClasses;

/*
 * The Chance class, (todo) unique methods for receiving and enforcing chance cards.
*/
public class Chance : Card
{
    public Chance(string name, int boardPositon) : base(name , boardPositon)
    {
        //Name = name;
        //BoardPosition = boardPositon;
        _cards = new List<ICard>() { new CondimentsSale(), new FoodBlogging(), new FoodFantasy(), new FoodPoisoning(), new FoundMoney(), new Gifted()
            , new GOAD(), new GODCard(), new GoToStart(), new HealthInspection(), new ImprovementPay(), new PayWaterFees(), new PoorServiceRefund()
            ,new SellStakeRes(), new WineHit(), new WonFoodVoucher()};
    }


}

//link these two classes using static???
public class ChanceCards
{
    //public Range numListChanceCards = 1..10;
    public int[] numListChanceCards = Enumerable.Range(1, 10).ToArray();

    //static void AdvanceToStart(PlayerClass player)
    //{
    //    Console.WriteLine("Advance to Start. Collect $200!");
    //    player.PlayerPos = 0;
    //}
    static void AdvanceToStandardProperty(Property property, PlayerClass player)
    {
        player.PlayerPos = property.BoardPosition;
        Console.WriteLine($"Advance to {property.Name}. If you pass Start collect $200.");
        //Call to rent
    }
    static void AdvanceToUtility(Utility utility, PlayerClass player)
    {
        player.PlayerPos = utility.BoardPosition;
        Console.WriteLine($"Advance to {utility.Name}. If unowned, you may purchase it. If owned, roll the dice and pay the owner 15 times the amount rolled.");
        //Call to utility rent
    }
    static void AdvanceToNearestStation()
    {

    }
    static void AdvanceToStation(PlayerClass player)
    {
        //made this random.
        int[] stationPos = new int[] { 5, 15, 25, 35 };

    }
    static void GetOutOfDietingCard(PlayerClass player)
    {
        player.NumOfGODCards += 1;
    }
    static void GoBack3Spaces(PlayerClass player)
    {
        //player.PlayerPos -= 3; Should call the method instead
        player.FullChangePlayerPos(-3);
    }


}