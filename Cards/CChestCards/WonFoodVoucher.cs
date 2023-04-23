using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class WonFoodVoucher:ICard
{
    public string CardMsg { get; }

    public WonFoodVoucher()
    {
        CardMsg = "You Won A Food Voucher In The Raffle. Collect £200";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        player.Cash += 200;
        string? msg = $"{player.Name} has gained £200.";
        return ("Nothing", msg);
    }
}
