using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class FoodBlogging:ICard
{
    public string CardMsg { get; }

    public FoodBlogging()
    {
        CardMsg = "Your Recieve Money From Your Food Blog. Recieve £25.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £25 from their food blog.";
        player.Cash += 25;

        return ("Nothing", msg);
    }
}
