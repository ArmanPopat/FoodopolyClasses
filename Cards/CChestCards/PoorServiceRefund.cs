using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class PoorServiceRefund:ICard
{
    public string CardMsg { get; }

    public PoorServiceRefund()
    {
        CardMsg = "You Recieved Poor Service At A Restaurant, You Recieve A Partial Refund. Collect £20.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} recieves £20 as a refund for poor service.";
        player.Cash += 20;

        return ("Nothing", msg);
    }
}
