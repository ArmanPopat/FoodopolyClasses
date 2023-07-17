using BoardClasses;
using FoodopolyClasses.Cards;
using GameClasses;
using FoodopolyClasses.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.BoardClasses;

public abstract class Card:BoardSpace
{
    protected List<ICard> _cards;
    public Card(string name, int boardPosition) : base(name, boardPosition)
    { }
    public async override Task<(string DoTask, string? Result)> LandEvent(PlayerClass player, GameClass game)
    {
        return await Task<string?>.Run(() =>
        {
            int numOfCards = _cards.Count;
            var random = new Random();
            var card = _cards[random.Next(numOfCards)];
            string msg = card.CardMsg;
            var methodReturn = card.Method(player, game);
            if (!string.IsNullOrEmpty(methodReturn.Result))
            {
                msg += methodReturn.Result;
            }
            return (methodReturn.DoTask, msg);
        });
    }
}
