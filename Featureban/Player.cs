using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Player
    {
        public Guid Id { get; }

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public CoinSide DropCoin(ICoin coin)
        {
            return coin.Drop();
        }


        public Board Play(CoinSide coinSide, Board board)
        {
            var newBoard = board;
            if (coinSide == CoinSide.Tails)
            {
                var cardsList = new List<Card>(board.Cards)
                {
                    new Card(this, CardState.InProgress)
                };

                newBoard =  new Board(cardsList);
            }

            return newBoard;
        }
    }
}
