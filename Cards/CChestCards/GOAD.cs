using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class GOAD:ICard
{
    public string CardMsg { get; }

    public GOAD()
    {
        CardMsg = "Go Straight On A Diet. Do Not Pass Start.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} goes straight on a Diet.";
        player.PlayerPos = 20;

        return ("GonOnADiet",msg);
    }
}
