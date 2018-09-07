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

            var newBoard = getNewCardBehaviour.Apply(playerId, board,CoinSide.Eagle);

            Assert.True(getNewCardBehaviour.CanApply(playerId, board, CoinSide.Eagle));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c=> Assert.Equal(playerId,c.PlayerId));
        }

        [Fact]
        public void CanGetAdditionalCard_IfCoinDropTails()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = getNewCardBehaviour.Apply(playerId, board,CoinSide.Tails);

            Assert.True(getNewCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c => Assert.Equal(playerId, c.PlayerId));
        }


        [Fact]
        public void NotAllowGetNewCard_IfWipLimitExceedAndCoinDropTails()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.False(getNewCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
        }

        [Fact]
        public void AllowGetNewCard_IfWipLimitNotExceedAndCoinDropTails()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var wipLimit = Create.WipLimit.WithLimit(2).Build();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.True(getNewCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
        }


    }
}
