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
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan*     |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = unblockCardBehaviour.Apply("Ivan", board,CoinSide.Tails);

            Assert.True(unblockCardBehaviour.CanApply("Ivan",board,CoinSide.Tails));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
        }


        [Fact]
        public void NotAllowUnblockOwnBlockedCard_IfNoOwnBlockedCards()
        {
            var unblockCardBehaviour = new UnblockOwnCardBahaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            Assert.False(unblockCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }



    }
}
