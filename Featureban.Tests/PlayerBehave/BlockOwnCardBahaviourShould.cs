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
        public void BlockOwnUnBlockedCard_IfOwnCardUnblocked()
        {
            var blockCardBehaviour = new BlockOwnCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = blockCardBehaviour.Apply(playerName, board, CoinSide.Eagle);

            Assert.True(blockCardBehaviour.CanApply(playerName, board, CoinSide.Eagle));
            Assert.True(newBoard.Cards.Single().IsBlocked);
            Assert.Equal(playerName, newBoard.Cards.Single().PlayerName);
        }


        [Fact]
        public void NotAllowBlockOwnUnBlockedCard_IfOwnCardBlocked()
        {
            var blockCardBehaviour = new BlockOwnCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.WhichBlocked().OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(blockCardBehaviour.CanApply(playerName, board, CoinSide.Eagle));

        }
    }
}
