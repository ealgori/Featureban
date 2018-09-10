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
    public class BlockOwnCardBehaviourShould
    {
        [Fact]
        public void BlockOwnUnBlockedCard_IfOwnCardUnblocked()
        {
            var blockCardBehaviour = new BlockOwnCardBahaviour();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board
              .FromMap(boardMap)
              .Build();


            var newBoard = blockCardBehaviour.Apply("Ivan", board, CoinSide.Eagle);

            Assert.True(blockCardBehaviour.CanApply("Ivan", board, CoinSide.Eagle));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan*     |          |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
        }


        [Fact]
        public void NotAllowBlockOwnUnBlockedCard_IfOwnCardBlocked()
        {
            var blockCardBehaviour = new BlockOwnCardBahaviour();
            var playerName = "Ivan";
            var card = Create.Card.WhichBlocked().OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(blockCardBehaviour.CanApply(playerName, board, CoinSide.Eagle));

        }
    }
}
