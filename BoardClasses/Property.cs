using System;
using GameClasses;
using FoodopolyClasses.PlayerClasses;
using SetClasses;

namespace BoardClasses;

public class Property : BasePropertyClass
{
    public int Rent { get; init; }
    public int RentL3 { get; init; }
    public int RentL4 { get; init; }
    public int RentL5 { get; init; }
    public int NumOfUpgrades { get;protected set; }
    public int UpgradeCost { get; }

    public Property(string name, int boardPositon, int price, int rentL1, int rentL2, int rentL3, int rentL4, int rentL5, int rent, string setName, int upgradeCost, bool owned = false, PlayerClass? owner = null, int numOfUpgrades = 0) 
        : base(name, boardPositon, price, setName, rentL1, rentL2, owned, owner)
    {
        Rent = rent;
        RentL3 = rentL3;
        RentL4 = rentL4;
        RentL5 = rentL5;
        NumOfUpgrades = numOfUpgrades;
        UpgradeCost = upgradeCost;
        
    }
    
    //public void DoesOwnSet()
    //{

    //}

    public override string? Cost(PlayerClass player, GameClass game)
    {
        if (IsUnOwnedOrOwner(player))
        {
            return string.Empty;
        }
        int curRent = Rent;
        if (GetSet(game).SetExclusivelyOwned)
        {
            int[] rents = { Rent * 2, RentL1, RentL2, RentL3, RentL4, RentL5 };
            curRent = rents[NumOfUpgrades];
        }
        Owner.Cash += curRent;  //ignore warning, Owner cannot be null
        string? msgAdd = player.DeductCash(curRent,game);
        
        string msg = $"You landed on {Name}. You pay {curRent} to {Owner.Name} to eat here.";
        if (!string.IsNullOrEmpty(msgAdd))
        {
            msg += msgAdd;
        }
        return msg;
    }

    //Must First Validate to see if owner exists, i.e. is owned
    //Called To Upgrade Property
    public string? Upgrade() 
    { 
        if (Owner == null)
        {
            throw new InvalidOperationException("Property has no owner, cannot be upgraded");
        }
        NumOfUpgrades++;
        Owner.Cash -= UpgradeCost;
        return $"{Owner.Name} has Upgraded {Name} to level {NumOfUpgrades}.";
    }
    public override void ResetProperty()
    {
        NumOfUpgrades = 0;
        base.ResetProperty();
    }
}

