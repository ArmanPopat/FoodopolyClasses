using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class Gifted: ICard
{
    public string CardMsg { get; }

    public Gifted()
    {
        CardMsg = "A Relative gifts you £100.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £100 as a gift.";
        player.Cash += 100;

        return ("Nothing", msg);
    }
}
