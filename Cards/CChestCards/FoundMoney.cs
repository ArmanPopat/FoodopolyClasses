using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class FoundMoney:ICard
{
    public string CardMsg { get; }

    public FoundMoney()
    {
        CardMsg = "You Found £10 On The Street.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} finds £10.";
        player.Cash += 10;

        return ("Nothing", msg);
    }
}
