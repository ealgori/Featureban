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
        private readonly Func<Card, Guid, Board , bool> _selector = ((c, id, b) => 
            !c.IsBlocked 
            && c.CanMoveForward() 
            && b.HasSlotsFor(c.State+1)
            && c.PlayerId ==id);
        public bool CanApply(Guid playerId, Board board, CoinSide coinSide)
        {
            return coinSide == CoinSide.Tails 
                   && board.Cards.Any(c => _selector(c, playerId,board));
               
        }

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            var card = board.Cards.First(c=>_selector(c,playerId,board));
            var newCard = card.MoveForward();

            return board.ReplaceCard(card, newCard);
        }
    }
}
