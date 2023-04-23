using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.MultiplayerClasses;


//Class to keep track of turn multiplayer things such as methods sent etc
public class TurnMultiplayerClass
{

    //0th method will be the sending of the game class at the end of each turn
    public int turnMethodCount { get; set; }
    public int turnMsgCount { get; set; }

    public TurnMultiplayerClass()
    {
        turnMethodCount = 0;
        turnMsgCount = 0;
    }
    

    public void NewTurn()
    {
        turnMethodCount = 0;
        turnMsgCount = 0;
    }
}
