using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class BlockOwnCardBahaviour : IPlayerBehaviour
    {
        private readonly Func<Card, string, bool> _selector = ((c, id) => 
            !c.IsBlocked 
            && c.State!= CardState.Completed
            && c.PlayerName == id);
        public bool CanApply(string playerName, Board board, CoinSide coinSide)
        {
            return board.Cards.Any(c => _selector(c, playerName));
               
        }

        public Board Apply(string playerName, Board board, CoinSide coinSide)
        {
            var card = board.Cards.Where(c => _selector(c, playerName)).OrderBy(_ => Guid.NewGuid()).First(); ;
            var newCard = card.Block();

            return board.ReplaceCard(card, newCard);
        }
    }
}
