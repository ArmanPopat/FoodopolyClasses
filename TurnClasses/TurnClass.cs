using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.TurnClasses;

//tracks turn info-Distinct from multiplayer turn class
public class TurnClass
{
    public int NumOfDoubles { get; set; }
    public bool RollEventDone { get; set; }

    public TurnClass() 
    {
        NumOfDoubles = 0;
        RollEventDone = false;
    }

    public void Reset()
    {
        NumOfDoubles = 0;
        RollEventDone = false;
    }


    
}
