using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class CondimentsSale:ICard
{
    public string CardMsg { get; }

    public CondimentsSale()
    {
        CardMsg = "Your Condiment Business Takes Off. Recieve £50.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £50 for his stellar condiment business.";
        player.Cash += 50;

        return ("Nothing",msg);
    }
}
