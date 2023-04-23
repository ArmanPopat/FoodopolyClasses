using BoardClasses;
using GameClasses;
using PlayerClasses;
using SetClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class ImprovementPay:ICard
{
    public string CardMsg { get; }

    public ImprovementPay()
    {
        CardMsg = "Building Tax Time. Pay £60 per improvement.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        int improvementCount = 0;
        foreach (KeyValuePair<string,SetProp> valuePair in game.setsPropDict)
        {
            //if (valuePair.Key == "Utilities"||  valuePair.Key == "Stations")
            //{
            //    continue;
            //}
            foreach (BasePropertyClass propertyTemp in valuePair.Value.Properties)
            {
                Property property;
                try
                {
                    property = (Property)propertyTemp;
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
                if (property.Owner == player)
                {
                    improvementCount = improvementCount + property.NumOfUpgrades;
                }
            }
        }
        string msg = $"{player.Name} pays £{improvementCount*60} for their {improvementCount} improvements as building tax.";
        string? msgBRupt = player.DeductCash(improvementCount*60, game);
        if (!string.IsNullOrEmpty(msgBRupt))
        {
            msg = msg + msgBRupt;
        }
        return ("Nothing", msg);

    }
}
