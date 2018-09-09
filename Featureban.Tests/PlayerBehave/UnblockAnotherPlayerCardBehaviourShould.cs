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
        public void UnblockAnotherPlayerBlockedCard_IfCardBlocked()
        {
            var unblockAnotherPlayerCardBehaviour = new UnblockAnotherPlayerCardBehaviour();
            var playerName = "Iavn";
            var card = Create.Card.WhichBlocked().Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = unblockAnotherPlayerCardBehaviour.Apply(playerName, board, CoinSide.Tails);

            Assert.True(unblockAnotherPlayerCardBehaviour.CanApply(playerName, board, CoinSide.Tails));
            Assert.False(newBoard.Cards.Single().IsBlocked);
            Assert.NotEqual(playerName, newBoard.Cards.Single().PlayerName);
        }

      

        [Fact]
        public void NotAllowUnblockAnotherPlayerBlockedCard_IfNoBlockedCards()
        {
            var unblockAnotherPlayerCardBehaviour = new UnblockAnotherPlayerCardBehaviour(); ;
            var playerName = "Ivan";
            var card = Create.Card.Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(unblockAnotherPlayerCardBehaviour.CanApply(playerName, board, CoinSide.Tails));

        }
    }
}
