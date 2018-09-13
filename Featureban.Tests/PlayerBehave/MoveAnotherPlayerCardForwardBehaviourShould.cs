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
        public void MoveAnotherPlayerUnblockedCard_IfCardUnblocked()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Vova      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = moveAnotherPlayerCardForwardBehaviour.Apply("Ivan", board,CoinSide.Tails);

            Assert.True(moveAnotherPlayerCardForwardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +          |Vova      |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
        }


        [Fact]
        public void NotAllowsMoveAnotherPlayerUnblockedCard_IfCardBlocked()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan*     |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            Assert.False(moveAnotherPlayerCardForwardBehaviour.CanApply("Ivan", board, CoinSide.Tails));

        }

        [Fact]
        public void NotAllowsMoveAnotherPlayerUnblockedCard_IfDropTailsAndWipLimitExceed()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Vova      |Ivan      |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:1   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            Assert.False(moveAnotherPlayerCardForwardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }

        [Fact]
        public void AllowsMoveAnotherPlayerUnblockedCard_IfDropTailsAndWipLimitNotExceed()
        {
            var moveAnotherPlayerCardForwardBehaviour = new MoveAnotherPlayerCardForwardBehaviour();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Vova      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            Assert.True(moveAnotherPlayerCardForwardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }


    }
}
