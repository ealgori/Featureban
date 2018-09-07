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
    public class MoveAnotherPlayerCardForwardBehaviourShould
    {
        [Fact]
        public void MoveAnotherPlayerUnblockedCard_IfDropTails()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = moveAnotherPlayerCardForwardBehaviour.Apply(playerId, board,CoinSide.Tails);

            Assert.True(moveAnotherPlayerCardForwardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.Equal(CardState.InTesting, newBoard.Cards.Single().State);
            Assert.NotEqual(playerId, newBoard.Cards.Single().PlayerId);
        }

        [Fact]
        public void NotAllowsMoveAnotherPlayerUnblockedCard_IfDropEagle()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.Build();
            var board = Create.Board.WithCards(card).Build();


            Assert.False(moveAnotherPlayerCardForwardBehaviour.CanApply(playerId, board, CoinSide.Eagle));
           
        }

        [Fact]
        public void NotAllowsMoveAnotherPlayerUnblockedCard_IfDropTailsAndNotUnblockedCards()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.WhichBlocked().Build();
            var board = Create.Board.WithCards(card).Build();


            Assert.False(moveAnotherPlayerCardForwardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }

        [Fact]
        public void NotAllowsMoveAnotherPlayerUnblockedCard_IfDropTailsAndWipLimitExceed()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.InProgressState().Build();
            var card2 = Create.Card.InTestingState().WhichBlocked().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card,card2).WithWipLimit(wipLimit).Build();


            Assert.False(moveAnotherPlayerCardForwardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }

        [Fact]
        public void AllowsMoveAnotherPlayerUnblockedCard_IfDropTailsAndWipLimitNotExceed()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.InProgressState().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();


            Assert.True(moveAnotherPlayerCardForwardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }


    }
}
