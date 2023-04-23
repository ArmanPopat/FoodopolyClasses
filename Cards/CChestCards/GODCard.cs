using GameClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards.CChestCards;

public class GODCard:ICard
{
    public string CardMsg { get; }
    
    public GODCard()
    {
        CardMsg = "You Recieve A Get Out Dieting Card.";
    }
    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game)
    {
        player.NumOfGODCards += 1;
        string? msg = $"{player.Name} now has {player.NumOfGODCards} Get Out of Dieting Cards.";
        return ("Nothing", msg);
    }
}
