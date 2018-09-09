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
        public bool CanApply(string playerName, Board board, CoinSide coinSide) 
            => board.HasSlotsFor(CardState.InProgress);
       

        public Board Apply(string playerName, Board board, CoinSide coinSide)
        {
            var cardsList = new List<Card>(board.Cards)
            {
                new Card(playerName, CardState.InProgress)
            };

            return new Board(cardsList, board.WipLimit);
          
        }
    }
}
