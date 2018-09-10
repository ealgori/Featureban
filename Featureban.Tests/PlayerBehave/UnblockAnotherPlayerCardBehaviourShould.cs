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
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Vova*     |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board
              .FromMap(boardMap)
              .Build();


            var newBoard = unblockAnotherPlayerCardBehaviour.Apply("Ivan", board, CoinSide.Tails);

            Assert.True(unblockAnotherPlayerCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Vova      |          |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
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
