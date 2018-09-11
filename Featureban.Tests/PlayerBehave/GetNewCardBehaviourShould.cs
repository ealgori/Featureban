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
            var board = Create.Board
              .FromMap(boardMap)
              .Build();
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
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board
              .FromMap(boardMap)
              .WithWipLimit(wipLimit)
              .Build();

            Assert.False(getNewCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }

        [Fact]
        public void AllowGetNewCard_IfWipLimitNotExceed()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var wipLimit = Create.WipLimit.WithLimit(2).Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board
              .FromMap(boardMap)
              .WithWipLimit(wipLimit)
              .Build();


            Assert.True(getNewCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
        }


    }
}
