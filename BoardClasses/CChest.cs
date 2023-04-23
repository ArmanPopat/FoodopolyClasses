using FoodopolyClasses.BoardClasses;
using FoodopolyClasses.Cards;
using FoodopolyClasses.Cards.CChestCards;
using GameClasses;
using PlayerClasses;
using System;

namespace BoardClasses;

/*
 * The Community Chest class, (todo) unique methods for receiving and enforcing chance cards.
*/
public class CChest : Card
{
    //private List<ICard> _cards;
    public CChest(string name, int boardPositon) : base(name, boardPositon)
    {
        _cards = new List<ICard>() { new CondimentsSale(), new FoodBlogging(), new FoodFantasy(), new FoodPoisoning(), new FoundMoney(), new Gifted()
            , new GOAD(), new GODCard(), new GoToStart(), new HealthInspection(), new ImprovementPay(), new PayWaterFees(), new PoorServiceRefund()
            ,new SellStakeRes(), new WineHit(), new WonFoodVoucher()};
        //Name = name;
        //BoardPosition = boardPositon;
    }
    
}

