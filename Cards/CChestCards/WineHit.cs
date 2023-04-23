using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class WineHit:ICard
{
    public string CardMsg { get; }

    public WineHit()
    {
        CardMsg = "Your Home Brewed Wine Is An Instant Hit. Recieve £100.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £100 for his stellar wine.";
        player.Cash += 100;

        return ("Nothing", msg);
    }
}
