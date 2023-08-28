using BoardClasses;
using FoodopolyClasses.PlayerClasses;
using FoodopolyClasses.Records;
using GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.TradeClasses;

public static class Trade
{
    //public InitiateTradeRecord TradeRecord { get; }
    //public Trade(string playerUsername, InitiateTradeRecord tradeRecord, GameClass game)
    //{
        
    //}

    //Validates that the trade prop and cash is all in order
    public static bool ValidateTrade(GameClass game, string playerUsername, InitiateTradeRecord tradeRecord)
    {
        PlayerClass initee = game.PlayerList.First(o => o.Name == playerUsername);
        //List<Station> initeeStations = new List<Station>();
        //List<Utility> initeeUtilities = new List<Utility>();
        //List<Property> initeeProperties = new List<Property>();

        if (initee.Cash < tradeRecord.mySelectedCash || initee.NumOfGODCards < tradeRecord.myGODCards)
        {
            return false;
        }

        //Gets owned for initee
        (List<Station> initeeStations, List<Utility> initeeUtilities, List<Property> initeeProperties) = initee.GetOwnedPropsAndStuff(game);
        //Check these owned by initee
        foreach (int boardPos in tradeRecord.mySelectedStationsBoardPos)
        {
            if (initeeStations.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        foreach (int boardPos in tradeRecord.mySelectedUtilitiesBoardPos)
        {
            if (initeeUtilities.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        foreach (int boardPos in tradeRecord.mySelectedPropertiesBoardPos)
        {
            if (initeeProperties.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        
        //Check none of these properties have upgrades
        if (initeeProperties.Any(o => o.NumOfUpgrades > 0))
        {
            return false;
        }



        //Same for other player
        PlayerClass otherPlayer = game.PlayerList.First(p => p.Name == tradeRecord.otherPlayerName);

        if (otherPlayer.Cash < tradeRecord.theirSelectedCash || otherPlayer.NumOfGODCards < tradeRecord.theirGODCards)
        {
            return false;
        }
        //Gets owned for otherPlayer
        (List<Station> otherPlayerStations, List<Utility> otherPlayerUtilities, List<Property> otherPlayerProperties) = otherPlayer.GetOwnedPropsAndStuff(game);
        //Check these owned by otherPlayer
        foreach (int boardPos in tradeRecord.theirSelectedStationsBoardPos)
        {
            if (otherPlayerStations.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        foreach (int boardPos in tradeRecord.theirSelectedUtilitiesBoardPos)
        {
            if (otherPlayerUtilities.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        foreach (int boardPos in tradeRecord.theirSelectedPropertiesBoardPos)
        {
            if (otherPlayerProperties.Any(o => o.BoardPosition == boardPos))
                continue;
            return false;
        }
        //Check none of these properties have upgrades
        if (otherPlayerProperties.Any(o => o.NumOfUpgrades > 0))
        {
            return false;
        }
        return true;

        //MAYBE SHOULD CHECK IF HAVE ENOUGH MONEY FOR UNMORTGAGING
    }

    public static async Task<(List<Station> stations, List<Utility> utilities, List<Property> properties)> IdentifyTheOwnedStuff(List<int> stationsBoardPos, List<int> utilitiesBoardPos,
        List<int> propertiesBoardPos, GameClass gameClass)
    {
        return await Task<(List<Station> stations, List<Utility> utilities, List<Property> properties)>.Run(() => {
            List<Station> stations = new List<Station>();
            List<Utility> utilities = new List<Utility>();
            List<Property> properties = new List<Property>();
            foreach (int boardPos in stationsBoardPos)
            {
                stations.Add(gameClass.stations.Properties.First(o => o.BoardPosition == boardPos));
            }
            foreach (int boardPos in utilitiesBoardPos)
            {
                utilities.Add(gameClass.utilities.Properties.First(o => o.BoardPosition == boardPos));
            }
            foreach (int boardPos in propertiesBoardPos)
            {
                List<Property> propertyList = gameClass.setsPropDict.Values.Select(o => o.Properties).SelectMany(l => l).ToList();
                properties.Add(propertyList.First(o => o.BoardPosition == boardPos));
            }
            return (stations, utilities, properties);
        });
       
        
        
    }

    public async static Task<(List<Station> initeeMortgagedStations, List<Utility> initeeMortgagedUtilities, List<Property> initeeMortgagedProperties, 
        List<Station> recieverMortgagedStations, List<Utility> receiverMortgagedUtilities, List<Property> receiverMortgagedProperties)> AcceptTradeAsync(int tradeRecordKey,
        InitiateTradeRecord tradeRecord, GameClass game, List<(int BoardPosition, bool ToUnmortgage)> mortPropsToDo)
    {
        (List<Station> initeeSelectedStations, List<Utility> initeeSelectedUtilities, List<Property> initeeSelectedProperties) = await Trade.IdentifyTheOwnedStuff(tradeRecord.mySelectedStationsBoardPos,
            tradeRecord.mySelectedUtilitiesBoardPos, tradeRecord.mySelectedPropertiesBoardPos, game);
        //I.e stuff localplayer is gwtting
        (List<Station> receiverSelectedStations, List<Utility> receiverSelectedUtilities, List<Property> receiverSelectedProperties) = await Trade.IdentifyTheOwnedStuff(tradeRecord.theirSelectedStationsBoardPos,
            tradeRecord.theirSelectedUtilitiesBoardPos, tradeRecord.theirSelectedPropertiesBoardPos, game);

        PlayerClass initeePlayer = game.PlayerList.First(o => o.Name == tradeRecord.myName);
        PlayerClass receiverPlayer = game.PlayerList.First(o => o.Name == tradeRecord.otherPlayerName);

        //implementation
        
        //Cash done this way for simplification and not to accidentally trigger a bankruptcy thing
        int diffInCashExchange = tradeRecord.mySelectedCash - tradeRecord.theirSelectedCash;
        if (diffInCashExchange <= 0)
        {
            initeePlayer.Cash += (-diffInCashExchange);
        }
        else
        {
            receiverPlayer.Cash += diffInCashExchange;
        }

        //Change Prop Owners
        foreach (Station station in initeeSelectedStations)
        {
            station.Owner = receiverPlayer;
        }
        foreach(Utility utility in initeeSelectedUtilities)
        {
            utility.Owner = receiverPlayer;
        }
        foreach (Property property in initeeSelectedProperties)
        {
            property.Owner = receiverPlayer;
        }

        foreach (Station station in receiverSelectedStations)
        {
            station.Owner = initeePlayer;
        }
        foreach (Utility utility in receiverSelectedUtilities)
        {
            utility.Owner = initeePlayer;
        }
        foreach (Property property in receiverSelectedProperties)
        {
            property.Owner = initeePlayer;
        }

        //Change GOD Card Possession
        int diffInGODCards = tradeRecord.myGODCards - tradeRecord.theirGODCards;
        if (diffInGODCards <= 0)
        {
            initeePlayer.NumOfGODCards += (-diffInGODCards);
        }
        else
        {
            receiverPlayer.NumOfGODCards += diffInGODCards;
        }
        var returnVar = (initeeSelectedStations.Where(o=>o.Mortgaged ==true).ToList(), initeeSelectedUtilities.Where(o=>o.Mortgaged==true).ToList(), initeeSelectedProperties.Where(o=>o.Mortgaged==true).ToList(),
            receiverSelectedStations.Where(o=>o.Mortgaged == true).ToList(), receiverSelectedUtilities.Where(o => o.Mortgaged == true).ToList(), receiverSelectedProperties.Where(o => o.Mortgaged == true).ToList());

        //Unmortgage props to be recieved by confirmee
        await UnMortgageOrPayFee(receiverPlayer, initeeSelectedStations, initeeSelectedUtilities, initeeSelectedProperties, mortPropsToDo);

        //Remove the specific Trade from dict
        game.TradeRecords.Remove(tradeRecordKey);


        //add to morgatgetradefees list in player
        //initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged = initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(receiverSelectedStations).ToList();
        //initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged = initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(receiverSelectedUtilities).ToList();
        //initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged = initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(receiverSelectedProperties).ToList();

        initeePlayer.MorgatgeFeesNotPaidOrUnMortgaged.Add(tradeRecord);

        //receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged = receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(initeeSelectedStations).ToList();
        //receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged = receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(initeeSelectedUtilities).ToList();
        //receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged = receiverPlayer.MorgatgeFeesNotPaidOrUnMortgaged.Concat(initeeSelectedProperties).ToList();

        return returnVar;
    }

    public async static Task UnMortgageOrPayFee(PlayerClass thisGettingPlayer, List<Station> otherStationList, List<Utility> otherUtilityList, List<Property> otherPropertyList,
        List<(int BoardPosition, bool ToUnmortgage)> mortPropsToDo)
    {
        foreach (Station station in otherStationList)
        {
            if (mortPropsToDo.Find(o => o.BoardPosition == station.BoardPosition).ToUnmortgage)
            {
                station.UnMortgage();
            }
            else
            {
                thisGettingPlayer.Cash -= (station.Price / 20);
            }
        }
        foreach (Utility utility in otherUtilityList)
        {
            if (mortPropsToDo.Find(o => o.BoardPosition == utility.BoardPosition).ToUnmortgage)
            {
                utility.UnMortgage();
            }
            else
            {
                thisGettingPlayer.Cash -= (utility.Price / 20);
            }
        }
        foreach (Property property in otherPropertyList)
        {
            if (mortPropsToDo.Find(o => o.BoardPosition == property.BoardPosition).ToUnmortgage)
            {
                property.UnMortgage();
            }
            else
            {
                thisGettingPlayer.Cash -= (property.Price / 20);
            }
        }
    }
}
