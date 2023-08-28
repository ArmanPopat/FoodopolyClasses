using BoardClasses;
using GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.BoardClasses;


//Unused
public static class PropertyIdentificationClass
{
    public static Station StationIdentificationMethodFromBoardPos(int boardPos, GameClass game)
    {
        if(!(boardPos == 5 || boardPos == 15 || boardPos == 25 || boardPos ==35))
        {
            throw new ArgumentException("Not A Station BoardPos");
        }
        return game.stations.Properties.First(o => o.BoardPosition == boardPos);
    }
    public static Utility UtilityIdentificationMethodFromBoardPos(int boardPos, GameClass game)
    {
        if (!(boardPos == 12 || boardPos == 28))
        {
            throw new ArgumentException("Not A Utility BoardPos");
        }
        return game.utilities.Properties.First(o => o.BoardPosition == boardPos);
    }

    //Unfinished
//    public static Property PropertyIdentificationMethodFromBoardPos(int boardPos, GameClass game)
//    {
//        //Not enough
//        if ((boardPos == 5 || boardPos == 15 || boardPos == 25 || boardPos == 35 || boardPos == 12 || boardPos == 28))
//        {
//            throw new ArgumentException("Not A Property BoardPos");
//        }
//        return game.setsPropDict.Values.Select(o=> o.Properties).ToList().First(o => o.BoardPosition == boardPos);
//    }

    //public static (List<Station> stations, List<Utility> utilities, List<Property> properties) BoardPosOwnablePropsIdentification(List<int> boardPosOwnableProps, GameClass game)
    //{

    //}

}
