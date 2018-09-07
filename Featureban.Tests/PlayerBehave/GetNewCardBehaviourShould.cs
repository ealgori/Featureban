using System;
using System.Linq;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class GetNewCardBehaviourShould
    {
        [Fact]
        public void CanGetAdditionalCard_IfCoinDropEagle()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = getNewCardBehaviour.Apply(playerId, board);

            Assert.True(getNewCardBehaviour.CanApply(playerId, board, CoinSide.Eagle));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c=> Assert.Equal(playerId,c.PlayerId));
        }

       
    }
}
