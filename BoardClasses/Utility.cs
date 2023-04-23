using PlayerClasses;
using System;
using SetClasses;
using Microsoft.VisualBasic;
using GameClasses;
using FoodopolyClasses.SetClasses;

namespace BoardClasses;

public class Utility : BasePropertyClass
{
    public Utility(string name, int boardPositon, int price, int rentL1, int rentL2, string setName,
        bool owned = false, PlayerClass? owner = null) : base(name, boardPositon, price, setName, rentL1, rentL2, owned, owner)
    {

    }

    public new Utilities GetSet(GameClass game)
    {
        Utilities set = game.utilities;
        return set;
    }

    public override string? Cost(PlayerClass player, GameClass game)
    {
        if (IsUnOwnedOrOwner(player))
        {
            return string.Empty;
        }
        int curRent = RentL1 * player.RollDiceOnce().Total;
        if (GetSet(game).SetExclusivelyOwned)
        {
            curRent = RentL2 * player.RollDiceOnce().Total;
        }

        string msg = $"You landed on {Name}. You are charged {curRent} to eat.";
        player.DeductCash(curRent, game);
        Owner.Cash += curRent;  //ignore warning, Owner cannot be null
        return msg;

    }
}