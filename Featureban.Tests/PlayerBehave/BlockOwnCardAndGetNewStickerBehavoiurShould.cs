using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class BlockOwnCardAndGetNewStickerBehavoiurShould
    {
        [Fact]
        public void BlockOwnUnBlockedCardAndGetNewCard_IfOwnCardUnblocked()
        {
            var blockOwnAndGetNewBehave = new BlockOwnAndGetNewSticker();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);


            var newBoard = blockOwnAndGetNewBehave.Apply("Ivan", board, CoinSide.Eagle);

            Assert.True(blockOwnAndGetNewBehave.CanApply("Ivan", board, CoinSide.Eagle));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +Ivan*     |          |         +
                                  +-------------------------------+", newBoard);
        }
   
        [Fact]
        public void GetNewCardAndNotBlockBlockedCard_IfNoOwnUnblockedCard()
        {
            var blockOwnAndGetNewBehave = new BlockOwnAndGetNewSticker();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan*     |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = blockOwnAndGetNewBehave.Apply("Ivan", board, CoinSide.Eagle);

            Assert.True(blockOwnAndGetNewBehave.CanApply("Ivan", board, CoinSide.Eagle));
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +Ivan*     |          |         +
                                  +-------------------------------+", newBoard);

        }

    }
}
