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
        public void CanGetAdditionalCard_OnApply()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = getNewCardBehaviour.Apply(playerName, board,CoinSide.Eagle);

            Assert.True(getNewCardBehaviour.CanApply(playerName, board, CoinSide.Eagle));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c=> Assert.Equal(playerName,c.PlayerName));
        }

    

        [Fact]
        public void NotAllowGetNewCard_IfWipLimitExceed()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.False(getNewCardBehaviour.CanApply(playerName, board, CoinSide.Tails));
        }

        [Fact]
        public void AllowGetNewCard_IfWipLimitNotExceed()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var wipLimit = Create.WipLimit.WithLimit(2).Build();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.True(getNewCardBehaviour.CanApply(playerName, board, CoinSide.Tails));
        }


    }
}
