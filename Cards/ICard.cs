using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Cards;

public interface ICard
{
    public string CardMsg { get; }

    public (string DoTask, string? Result) Method(PlayerClass player, GameClass game);
}
