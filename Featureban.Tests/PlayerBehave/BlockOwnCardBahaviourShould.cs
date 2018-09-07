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
    public class BlockOwnCardBehaviourShould
    {
        [Fact]
        public void BlockOwnUnBlockedCard_IfDropEagle()
        {
            var blockCardBehaviour = new BlockOwnCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = blockCardBehaviour.Apply(playerId, board);

            Assert.True(blockCardBehaviour.CanApply(playerId, board, CoinSide.Eagle));
            Assert.True(newBoard.Cards.Single().IsBlocked);
            Assert.Equal(playerId, newBoard.Cards.Single().PlayerId);
        }
    }
}
