using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class UnblockAnotherPlayerCardBehaviour:IPlayerBehaviour
    {
        private readonly Func<Card, Guid, bool> _selector = ((c, id) =>
            c.IsBlocked
            && c.PlayerId != id);
        public bool CanApply(Guid playerId, Board board, CoinSide coinSide)
        {
            return coinSide == CoinSide.Tails && board.Cards.Any(c => _selector(c, playerId));
        }

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            var card = board.Cards.Where(c => _selector(c, playerId)).OrderBy(_ => Guid.NewGuid()).First();
            var newCard = card.Unblock();

            return board.ReplaceCard(card, newCard);
        }
    }
}
