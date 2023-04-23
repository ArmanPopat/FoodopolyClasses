using BoardClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.SetClasses;

public abstract class SetBase
{
    public string Name { get; init; }
    //public abstract List<> Properties { get; set; }  //turn into set only once after constructor somehow -- look into   maybe change into Ilist
    
    public SetBase(string name)
    {
        Name = name;
        //Properties = properties;
        //SetExclusivelyOwned = false;
        //Owner = null;
    }

    public abstract bool SetExclusivelyOwned { get; }
    
    public abstract PlayerClass? Owner { get; }
    
}
