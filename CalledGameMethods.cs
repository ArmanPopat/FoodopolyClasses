using BoardClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses;

public class CalledGameMethods
{
    //public string StartTurnEvent(PlayerClass player)
    //{
    //    if (player.Dieting)
    //    {

    //    }
    //}

    

    public void AcceptTrade(PlayerClass proposer, PlayerClass proposee, List<BasePropertyClass> proposerGiveProps,
        List<BasePropertyClass> proposeeGiveProps, int proposerGiveCash, int proposeeGiveCash)
    {
        List<BasePropertyClass> proposerMortgagedProps = new List<BasePropertyClass>();
        List<BasePropertyClass> proposeeMortgagedProps = new List<BasePropertyClass>();
        //Validation
        foreach (var prop in proposerGiveProps)
        {
            if (prop.Owner != proposer)
            {
                throw new InvalidOperationException($"Proposee Does Not Own The {prop.Name} Property.");
            }
            //Add To Morgatged list here while iterating
            if (prop.Mortgaged)
            {
                proposerMortgagedProps.Add(prop);
            }
        }
        foreach (var prop in proposeeGiveProps)
        {
            if (prop.Owner != proposee)
            {
                throw new InvalidOperationException($"Proposee Does Not Own The {prop.Name} Property.");
            }
            //Add To Morgatged list here while iterating
            if (prop.Mortgaged)
            {
                proposeeMortgagedProps.Add(prop);
            }
        }

        int proposerCashNeeded = proposerGiveCash;

        foreach (var prop in proposerMortgagedProps)
        {
            proposerCashNeeded += ((prop.Price) / 2) / 10;
        }

        int proposeeCashNeeded = proposeeGiveCash;

        foreach (var prop in proposeeMortgagedProps)
        {
            proposeeCashNeeded += ((prop.Price) / 2) / 10;
        }

        if (proposerGiveCash > proposerCashNeeded)
        {
            throw new InvalidOperationException("Proposer Does Not Have Enough Cash. Recall 10% fee on mortgaged properties.");
        }
        if (proposeeGiveCash > proposeeCashNeeded)
        {
            throw new InvalidOperationException("Proposee Does Not Have Enough Cash. Recall 10% fee on mortgaged properties.");
        }



        //Implementation
        foreach(var prop in proposerGiveProps)
        {
            prop.Owner = proposee;
        }
        foreach (var prop in proposeeGiveProps)
        {
            prop.Owner = proposer;
        }

        
        proposer.Cash += proposeeGiveCash;
        proposer.Cash -= proposerGiveCash;

        proposee.Cash += proposerGiveCash;
        proposee.Cash -= proposeeGiveCash;

        //Run Unmortgage Props
    }
}
