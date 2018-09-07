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

            var newBoard = moveOwnCardBehaviour.Apply(playerId, board);

            Assert.True(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.Equal(CardState.InTesting, newBoard.Cards.Single().State);
            Assert.Equal(playerId, newBoard.Cards.Single().PlayerId);
        }

        [Fact]
        public void NotAllowdMoveOwnUnblockedCard_IfDropEagle()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = moveOwnCardBehaviour.Apply(playerId, board);

            Assert.False(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Eagle));
         
        }

        [Fact]
        public void NotAllowdMoveOwnUnblockedCard_IfDropTailsAndNoOwnUnblockdCards()
        {
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.WhichBlocked().OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));

        }


       
    }
}
