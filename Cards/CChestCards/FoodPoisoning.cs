using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class FoodPoisoning:ICard
{
    public string CardMsg { get; }

    public FoodPoisoning()
    {
        CardMsg = "You Have Food Poisoning, Pay £50 for Treatment";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        string msg = $"{player.Name} pays 50 £for food poisoning treatment.";
        string? msgBRupt = player.DeductCash(50, game);
        if (!string.IsNullOrEmpty(msgBRupt)) 
        {
            msg = msg + msgBRupt;
        }
        return ("Nothing", msg);
    }
}
