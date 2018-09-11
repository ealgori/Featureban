using System;
using System.Linq;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class GetNewCardBehaviourShould
    {
        [Fact]
        public void CanGetAdditionalCard_OnApply()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);
            var newBoard = getNewCardBehaviour.Apply("Ivan", board,CoinSide.Eagle);

            Assert.True(getNewCardBehaviour.CanApply("Ivan", board, CoinSide.Eagle));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +Ivan      |          |         +
                                  +-------------------------------+", newBoard);
        }

    

        [Fact]
        public void NotAllowGetNewCard_IfWipLimitExceed()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:1   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            Assert.False(getNewCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }

        [Fact]
        public void AllowGetNewCard_IfWipLimitNotExceed()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:2   |Limit:2   |         +";
            var board = Create.Board.FromMap(boardMap);


            Assert.True(getNewCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }


    }
}
