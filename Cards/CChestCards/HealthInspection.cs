using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class HealthInspection:ICard
{
    public string CardMsg { get; }

    public HealthInspection()
    {
        CardMsg = "Time For A Health Inspection. Pay £50.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} pays £50 for a health inspection.";
        string? msgBRupt = player.DeductCash(50, game);
        if (!string.IsNullOrEmpty(msgBRupt))
        {
            msg = msg + msgBRupt;
        }
        return ("Nothing", msg);
    }
}
