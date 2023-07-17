using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.ChanceCards;

public class GoToStart
{
    public string CardMsg { get; }
    
    public GoToStart()
    {
        CardMsg = "Advance To Start. Collect £200.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player)
    {
        player.PlayerPos = 0;
        string? msg = player.PassGo();
        return ("Nothing", msg);
    }
}
