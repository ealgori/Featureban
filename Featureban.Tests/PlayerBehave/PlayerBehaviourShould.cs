using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class PlayerBehaviourShould
    {
        [Fact]
        public void SkipMove_IfNoBehavioursAcceptable()
        {
            var playerBehaviour = Create.PlayerBehavoiur
                .WithTailBehaviours(new UnblockOwnCardBahaviour())
                .Build();
            var playerId = Guid.NewGuid();
            var card1 = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card1).Build();

            var newBoard = playerBehaviour.Apply(playerId, board, CoinSide.Tails);


            Assert.Equal(board,newBoard);
        }

    }
}
