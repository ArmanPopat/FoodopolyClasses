using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class PayWaterFees:ICard
{
    public string CardMsg { get; }

    public PayWaterFees()
    {
        CardMsg = "Time To Pay Your Water Bill. Pay £100.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} pays £100 for their water bill.";
        string? msgBRupt = player.DeductCash(100, game);
        if (!string.IsNullOrEmpty(msgBRupt))
        {
            msg = msg + msgBRupt;
        }
        return ("Nothing", msg);
    }
}
