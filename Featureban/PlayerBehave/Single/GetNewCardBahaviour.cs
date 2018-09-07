using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class GetNewCardBahaviour:IPlayerBehaviour
    {
        public bool CanApply(Player player, Board board, CoinSide coinSide) => coinSide == CoinSide.Tails;
       

        public Board Apply(Player player, Board board)
        {
            var cardsList = new List<Card>(board.Cards)
            {
                new Card(player, CardState.InProgress)
            };

            return new Board(cardsList);
        }
    }
}
