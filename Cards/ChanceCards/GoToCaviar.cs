using BoardClasses;
using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.ChanceCards;


//Need to change tyhis to be customisable
public class GoToCaviar:ICard
{
    public string CardMsg { get; }

    public GoToCaviar()
    {
        CardMsg = "Advance To Caviar.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        player.PlayerPos = 39;
        //Console.WriteLine($"Advance to Caviar. If you pass Start collect $200.");
        //Call to rent
        string msg = $"{player.Name} advances to Caviar.";
        var eventReturn = player.LandEventAsync(game).Result; //I reckon this will run sync
        string? msgAddition = eventReturn.Result;
        if (!string.IsNullOrEmpty(msgAddition))
        {
            msg += msgAddition;
        }
        return (eventReturn.DoTask, msg);
    }
}
