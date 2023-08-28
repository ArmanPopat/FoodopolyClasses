using System;
using GameClasses;
using FoodopolyClasses.PlayerClasses;
using SetClasses;

namespace BoardClasses;

/*
 * The base class for any specific property class to be derived from, contains some unique methods and properties that will be inherited
 */
public abstract class BasePropertyClass : BoardSpace
{
    public int Price { get; init; }
    public bool Owned { get; set; }
    public bool Mortgaged { get; set; }
    public PlayerClass? Owner { get; set; }
    public string SetName { get; init; }
    
    //public int NumOfUpgrades { get; protected set; }
    public int RentL1 { get; init; }
    public int RentL2 { get; init; }
    //public Set Set { get; init; }
    public BasePropertyClass(string name, int boardPositon, int price,  string setName, int rentL1, int rentL2, bool owned = false, PlayerClass? owner = null, int numOfUpgrades = 0) : base(name, boardPositon)
    {
        //Name = name;
        //BoardPosition = boardPositon;
        Price = price;
        Owned = owned;
        Mortgaged = false;
        if (!(owned) && owner != null)
        {
            throw new ArgumentException("If property is not owned, owner must be null");
        }
        else if (owned && owner == null)
        {
            throw new ArgumentException("If property is owned, owner cannot be null");
        }
        else
        {
            Owner = owner; //ToDo Change this to a selection list if best solution
        }

        RentL1 = rentL1;
        RentL2 = rentL2;

        SetName = setName;
    }

    public override async Task<(string DoTask, string? Result)> LandEvent(PlayerClass player, GameClass game)
    {
        //DELETE, Just to see if working
        //return string.Empty;


        if (!Owned)
        {
            return ("CanBuy", string.Empty);
        }
        else 
        {
            if (!Mortgaged)
            {
                string? msg = Cost(player, game);
                return ("Nothing",msg);
            }
            return ("Nothing",string.Empty);
        }
    }

    public virtual SetProp GetSet(GameClass game)
    {
        if (string.IsNullOrEmpty(SetName))
        {
            throw new InvalidDataException("No Set Name");
        }
        string uncapitalisedSetName = char.ToLower(SetName[0]) + SetName[1..];
        SetProp set = game.setsPropDict[uncapitalisedSetName];
        return set;
    }

    //Both buy and sell cannot be called independently without checks beforehand, there is double verification here, with checks here sending out excep and crashing the program
    //Overriding previous, using try catch in hub
    public void Buy(PlayerClass player)
    {
        if (Owner != null)
        {
            //Console.WriteLine("Cannot buy an owned property.");
            throw new InvalidOperationException("Cannot buy an owned property."); //exception because should never happen
        }
        else
        {
            if (player.Cash < Price)
            {
                //Console.WriteLine("Cannot buy a property with a higher price than the cash you have now.");
                throw new InvalidOperationException("Cannot buy a property with a higher price than the cash you have now.");
            }
            else
            {
                Owned = true;
                Owner = player;
                player.Cash -= Price;
            }
        }
    }

    //Removed Validation
    public string Mortgage()
    {
        if (Owned == false || Owner == null)
        {
            //Console.WriteLine("Cannot mortgage an unowned property.");
            throw new InvalidOperationException("Cannot mortgage an unowned property.");
        }
        else
        {
            
            Mortgaged = true;
            Owner.Cash += (Price / 2);
            return $"{Owner.Name} mortgaged {Name}.";
            
        }
    }

    public string UnMortgage()
    {
        if (Owned == false || Owner == null || Mortgaged == false)
        {
            //Console.WriteLine("Cannot mortgage an unowned property.");
            throw new InvalidOperationException("Cannot unmortgage.");
        }
        else
        {

            Mortgaged = false;
            Owner.Cash -= (Price / 2);
            return $"{Owner.Name} unmortgaged {Name}.";

        }
    }

    public bool IsUnOwnedOrOwner(PlayerClass player)
    {
        if ((Owned == false) || (Owner == player))
        {
            return true;
        }
        return false;
    }

    public virtual void ResetProperty() 
    {
        Owner = null;
        Owned = false;
        Mortgaged = false;
    }

    public abstract string? Cost(PlayerClass player, GameClass game);

}