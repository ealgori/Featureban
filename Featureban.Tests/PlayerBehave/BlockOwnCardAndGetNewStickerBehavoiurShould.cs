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
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = blockOwnAndGetNewBehave.Apply(playerName, board, CoinSide.Eagle);

            Assert.True(blockOwnAndGetNewBehave.CanApply(playerName, board, CoinSide.Eagle));
            Assert.Single(newBoard.Cards, c=>c.IsBlocked);
            Assert.Single(newBoard.Cards, c=>!c.IsBlocked);
        }
   
        [Fact]
        public void GetNewCardAndNotBlockBlockedCard_IfNoOwnUnblockedCard()
        {
            var blockOwnAndGetNewBehave = new BlockOwnAndGetNewSticker();
            var playerName = "Ivan";
            var card = Create.Card.WhichBlocked().OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = blockOwnAndGetNewBehave.Apply(playerName, board, CoinSide.Eagle);

            Assert.True(blockOwnAndGetNewBehave.CanApply(playerName, board, CoinSide.Eagle));
            Assert.Single(newBoard.Cards, c => c.IsBlocked);
            Assert.Single(newBoard.Cards, c => !c.IsBlocked);
        }

    }
}
