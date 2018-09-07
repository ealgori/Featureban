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


    }
}
