using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class PlayerBehaviourShould
    {
        [Fact]
        public void SkipMove_IfNoBehavioursAcceptable()
        {
            var playerBehaviour = Create.PlayerBehavoiur.Build();
            var playerId = Guid.NewGuid();
            var board = Create.Board.Build();

            var newBoard = playerBehaviour.Apply(playerId, board, CoinSide.Eagle);

            Assert.Equal(board,newBoard);
        }
    }
}
