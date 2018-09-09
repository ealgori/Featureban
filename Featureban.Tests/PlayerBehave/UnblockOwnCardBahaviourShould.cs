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
    public class UnblockOwnCardBahaviourShould
    {

        [Fact]
        public void UnblockOwnBlockedCard_IfOwnCardBlocked()
        {
            var unblockCardBehaviour = new UnblockOwnCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.WhichBlocked().OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = unblockCardBehaviour.Apply(playerName, board,CoinSide.Tails);

            Assert.True(unblockCardBehaviour.CanApply(playerName,board,CoinSide.Tails));
            Assert.False(newBoard.Cards.Single().IsBlocked);
            Assert.Equal(playerName, newBoard.Cards.Single().PlayerName);
        }


        [Fact]
        public void NotAllowUnblockOwnBlockedCard_IfNoOwnBlockedCards()
        {
            var unblockCardBehaviour = new UnblockOwnCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(unblockCardBehaviour.CanApply(playerName, board, CoinSide.Tails));

        }



    }
}
