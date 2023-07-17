using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.ChanceCards;

public class GoToPizza:ICard
{
    public string CardMsg { get; }

    public GoToPizza()
    {
        CardMsg = "Advance Pizza. If You Pass Start Collect £200.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} advances to Pizza.";
        if (player.PlayerPos >= 14)
        {
            msg = msg + "\n" + player.PassGo();
        }

        player.PlayerPos = 14;
        //Console.WriteLine($"Advance to Caviar. If you pass Start collect $200.");
        //Call to rent
        var eventReturn = player.LandEventAsync(game).Result; //I reckon this will run sync
        string? msgAddition = eventReturn.Result;
        if (!string.IsNullOrEmpty(msgAddition))
        {
            msg += msgAddition;
        }
        return (eventReturn.DoTask, msg);
    }
}

