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
    public class BlockOwnCardAndGetNewStickerBehavoiurShould
    {
        public void BlockOwnUnBlockedCardAndGetNewCard_IfDropEagle()
        {
            var blockOwnAndGetNewBehave = new BlockOwnCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = blockOwnAndGetNewBehave.Apply(playerId, board);

            Assert.True(blockOwnAndGetNewBehave.CanApply(playerId, board, CoinSide.Eagle));
            Assert.Single(newBoard.Cards, c=>c.IsBlocked);
            Assert.Single(newBoard.Cards, c=>!c.IsBlocked);
        }

        public void NotAllowedBlockOwnUnBlockedCardAndGetNewCard_IfDropTails()
        {
            var blockOwnAndGetNewBehave = new BlockOwnCardBahaviour();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            Assert.False(blockOwnAndGetNewBehave.CanApply(playerId, board, CoinSide.Tails));
        }

    }
}
