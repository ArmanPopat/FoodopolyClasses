using System;
using System.Threading.Tasks;
using GameClasses;
using PlayerClasses;

namespace BoardClasses;

/*
 * The class from which any class that represents a space on the board is derived from with basic properties of name and board position.
*/
public abstract class BoardSpace
{
    public string Name { get; init; }
    public int BoardPosition { get; init; }
    public BoardSpace(string name, int boardPositon)
    {
        Name = name;
        BoardPosition = boardPositon;
    }

    public abstract Task<(string DoTask, string? Result)> LandEvent(PlayerClass player, GameClass game);
}
