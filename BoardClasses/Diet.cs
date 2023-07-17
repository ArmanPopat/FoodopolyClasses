using System;
using GameClasses;
using FoodopolyClasses.PlayerClasses;


namespace BoardClasses;

public class Diet : BoardSpace
{
    public Diet() : base("Dieting", 10)
    {
        //Name = "Dieting";
        //BoardPosition = 10;
    }

    public async override Task<(string DoTask, string? Result)> LandEvent(PlayerClass player, GameClass game)
    {
        //string msg = string.Empty;
        //if (player.Dieting)
        //{
        //    //Do sruff here
        //}
        //else
        //{
        //    msg += "Don't Worry, Just Visiting The Dieting Centre";
        //}
        //return msg;
        string msg = "Don't Worry, Just Visiting The Dieting Centre";
        return  ("Nothing", msg);
    }


    /*
     * Method to allow play to get out by either using a card or pay
     */
    public bool GetOutOfDieting(PlayerClass player)
    {
        if (player.NumOfGODCards >= 1)
        {
            //ToDo Allow player to use Get out of Jail Card
            return true;
        }
        //ToDo Ask if player would like to pay 50 to get out

        return false;
    }
    
    /*
     * Method for roll if in Jail, will force player to use card or pay on third roll
     */
    public void DietingRoll(PlayerClass player)
    {

        if (GetOutOfDieting(player))
        {

        }
        else
        {
            var diceRoll = player.RollDiceOnce();
            if (player.NumOfDietRolls < 2)
            {

            }
            //stuffff carry on if double, if 3 injaillrolls, money to free parking
            else 
            {
                //ToDo Ask them to use card if they have one, if not force them to pay
            }        
        
        }
    }
}
