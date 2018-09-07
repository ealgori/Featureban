using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class MoveOwnCardForwardBehaviour:IPlayerBehaviour
    {
        private readonly Func<Card, Player, bool> _selector = ((c, p) => 
            !c.IsBlocked 
            && c.CanMoveForward() 
            && c.Owner.Id == p.Id);
        public bool CanApply(Player player, Board board, CoinSide coinSide)
        {
            return coinSide == CoinSide.Tails && board.Cards.Any(c => _selector(c, player));
               
        }

        public Board Apply(Player player, Board board)
        {
            var card = board.Cards.First(c=>_selector(c,player));
            var newCard = card.MoveForward();

            var cards = board.Cards.ToList();
            cards.Remove(card);
            cards.Add(newCard);
            return new Board(cards);
        }
    }
}
