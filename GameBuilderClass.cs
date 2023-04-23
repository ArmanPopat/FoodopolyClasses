using GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses;

public class GameBuilderClass
{
    //private GameClass _gameClass = new GameClass();

    public async Task<GameClass> BuildGameClass(int id, string password)
    {
        GameClass gameClass =  await Task<GameClass>.Run(() => new GameClass(id, password));
        return gameClass;
    }
}
