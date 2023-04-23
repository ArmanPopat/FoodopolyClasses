using System;
using GameClasses;
using PlayerClasses;

namespace BoardClasses;

public class GoOnADiet:BoardSpace
{
    public GoOnADiet() : base("Go On A Diet", 30)
    {
        //Name = "Go On A Diet";
        //BoardPosition = 30;
    }

    /*
     * A method to send the player to jail.
     */
    public void GoOnADietMethod(PlayerClass player, GameClass game)  //could be static???
    {
        player.PlayerPos = 10;
        player.Dieting = true;
        player.NumOfDietRolls = 0;
        game.Turn.RollEventDone = true;
    }
    //Add jail stuff to when you roll dice

    public override async Task<(string DoTask, string? Result)> LandEvent(PlayerClass player, GameClass game)
    {
        string msg = "You Are Straight Off To A Diet, Do Not Pass Start";
        GoOnADietMethod(player, game);
        return ("GoOnADiet", msg);
        
    }
}
