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
        public bool CanApply(Guid playerId, Board board, CoinSide coinSide) 
            => board.HasSlotsFor(CardState.InProgress);
       

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            var cardsList = new List<Card>(board.Cards)
            {
                new Card(playerId, CardState.InProgress)
            };

            return new Board(cardsList, board.WipLimit);
          
        }
    }
}
