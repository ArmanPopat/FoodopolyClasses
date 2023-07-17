using FoodopolyClasses.SetClasses;
using GameClasses;
using FoodopolyClasses.PlayerClasses;
using SetClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardClasses;

public class Station : BasePropertyClass
{
    public int RentL3 { get; init; }
    public int RentL4 { get; init; }

    public Station(string name, int boardPositon, int price, int rentL1, int rentL2, int rentL3, int rentL4, string setName,
        bool owned = false, PlayerClass? owner = null) : base(name, boardPositon, price, setName, rentL1, rentL2, owned, owner)
    {
        RentL3 = rentL3;
        RentL4 = rentL4;
    }

    public new Stations GetSet(GameClass game)
    {
        Stations set = game.stations;
        return set;
    }
    public override string? Cost(PlayerClass player, GameClass game)
    {
        if (IsUnOwnedOrOwner(player)) //probably uneeded validation
        {
            return string.Empty;
        }
        int[] rents = { RentL1, RentL2, RentL3, RentL4 };
        var numOfOwned = GetSet(game).Properties.Where(o => o.Owner == Owner).Count();
        int curRent = rents[numOfOwned - 1];

        string msg = $"You landed on {Name}. You are charged {curRent} to eat.";
        player.DeductCash(curRent,game);
        Owner.Cash += curRent;  //ignore warning, Owner cannot be null
        return msg;
    }
}