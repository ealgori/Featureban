using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class BoardShould
    {
        [Fact]
        public void AllowMoveCardForward_IfWIPLimitForThisStateNotExceed()
        {
            var wipLimit = Create.WipLimit.WithLimit(2).Build();
            var card = Create.Card.InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();

            Assert.True(board.HasSlotsFor(CardState.InProgress));
        }

        [Fact]
        public void NowAllowMoveCardForward_IfWIPLimitForThisStateExceed()
        {
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var card = Create.Card.InProgressState().Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();

            Assert.False(board.HasSlotsFor(CardState.InProgress));
        }
    }
}
