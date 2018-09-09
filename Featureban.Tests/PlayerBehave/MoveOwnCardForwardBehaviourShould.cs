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
    public class MoveOwnCardForwardBehaviourShould
    {
        [Fact]
        public void MoveOwnUnblockedCard_IfOwnCardUnblocked()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = moveOwnCardBehaviour.Apply(playerName, board,CoinSide.Tails);

            Assert.True(moveOwnCardBehaviour.CanApply(playerName, board, CoinSide.Tails));
            Assert.Equal(CardState.InTesting, newBoard.Cards.Single().State);
            Assert.Equal(playerName, newBoard.Cards.Single().PlayerName);
        }

        

        [Fact]
        public void NotAllowMoveOwnUnblockedCard_IfNoOwnUnblockdCards()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerName = "Ivan";
            var card = Create.Card.WhichBlocked().OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(moveOwnCardBehaviour.CanApply(playerName, board, CoinSide.Tails));

        }

        [Fact]
        public void NotAllowMoveOwnUnblockedCard_IfWipLimitExceed()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var card2= Create.Card.OwnedTo(playerName).InTestingState().WhichBlocked().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card,card2).WithWipLimit(wipLimit).Build();


            Assert.False(moveOwnCardBehaviour.CanApply(playerName, board, CoinSide.Tails));

        }

        [Fact]
        public void AllowMoveOwnUnblockedCard_WipLimitNotExceed()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.True(moveOwnCardBehaviour.CanApply(playerName, board, CoinSide.Tails));

        }



    }
}
