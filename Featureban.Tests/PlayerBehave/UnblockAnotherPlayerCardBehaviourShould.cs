using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class UnblockAnotherPlayerCardBehaviourShould
    {
        [Fact]
        public void UnblockAnotherPlayerBlockedCard_IfDropTails()
        {
            var unblockAnotherPlayerCardBehaviour = new UnblockAnotherPlayerCardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.WhichBlocked().Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = unblockAnotherPlayerCardBehaviour.Apply(playerId, board, CoinSide.Tails);

            Assert.True(unblockAnotherPlayerCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.False(newBoard.Cards.Single().IsBlocked);
            Assert.NotEqual(playerId, newBoard.Cards.Single().PlayerId);
        }

      

        [Fact]
        public void NotAllowUnblockAnotherPlayerBlockedCard_IfDropTailsAndNoBlockedCards()
        {
            var unblockAnotherPlayerCardBehaviour = new UnblockAnotherPlayerCardBehaviour(); ;
            var playerId = Guid.NewGuid();
            var card = Create.Card.Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(unblockAnotherPlayerCardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }
    }
}
