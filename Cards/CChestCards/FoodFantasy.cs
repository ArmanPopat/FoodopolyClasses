using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class FoodFantasy:ICard
{
    public string CardMsg { get; }

    public FoodFantasy()
    {
        CardMsg = "You Won The Food Fantasty Sweepstake. Collect £10 From Each Player.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £10 from each player for the food fantasy sweepstake.";
        foreach (PlayerClass playerIt in game.PlayerList)
        {
            if (playerIt == player)
            {
                continue;
            }
            playerIt.DeductCash(20, game);
            player.Cash += 20;
        }

        return ("Nothing", msg);
    }
}
