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
        public void MoveOwnUnblockedCard_IfDropTails()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = moveOwnCardBehaviour.Apply(playerId, board,CoinSide.Tails);

            Assert.True(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.Equal(CardState.InTesting, newBoard.Cards.Single().State);
            Assert.Equal(playerId, newBoard.Cards.Single().PlayerId);
        }

        

        [Fact]
        public void NotAllowMoveOwnUnblockedCard_IfDropTailsAndNoOwnUnblockdCards()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.WhichBlocked().OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }

        [Fact]
        public void NotAllowMoveOwnUnblockedCard_IfDropTailsAndWipLimitExceed()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).InProgressState().Build();
            var card2= Create.Card.OwnedTo(playerId).InTestingState().WhichBlocked().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card,card2).WithWipLimit(wipLimit).Build();


            Assert.False(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }

        [Fact]
        public void AllowMoveOwnUnblockedCard_IfDropTailsAndWipLimitNotExceed()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).InProgressState().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.True(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }



    }
}
