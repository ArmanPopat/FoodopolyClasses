using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.Records;

public record InitiateTradeRecord (string myName, string otherPlayerName,int mySelectedCash,List<int> mySelectedStationsBoardPos, List<int> mySelectedUtilitiesBoardPos,
           List<int> mySelectedPropertiesBoardPos,int myGODCards,int theirSelectedCash, List<int> theirSelectedStationsBoardPos, List<int> theirSelectedUtilitiesBoardPos,
           List<int> theirSelectedPropertiesBoardPos,int theirGODCards)
{
}
