using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class PlayerBehaveShould
    {
        [Fact]
        public void UnblockOwnBlockedCard_IfDropTails()
        {
            var unblockCardBehaviour = new UnblockOwnCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.WhichBlocked().OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = unblockCardBehaviour.Apply(playerId, board);

            Assert.True(unblockCardBehaviour.CanApply(playerId,board,CoinSide.Tails));
            Assert.False(newBoard.Cards.Single().IsBlocked);
            Assert.Equal(playerId, newBoard.Cards.Single().PlayerId);
        }


       
    }
}
