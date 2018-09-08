using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Model;
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

        [Fact]
        public void ApplyHigherPriorityBehavior_IfTheyAllApplicable()
        {
            var getNewCardBehaviour = new GetNewCardBahaviour();
            var moveOwnCardBehaviour = new MoveOwnCardForwardBehaviour();
            var behaviours = new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(priority: 1, behaviour: getNewCardBehaviour),
                new PlayerBehaviourContainer(priority: 2, behaviour: moveOwnCardBehaviour),

            };
            var playerBehaviour = Create.PlayerBehavoiur
                .WithTailsBehaviours(behaviours)
                .Build();
            var playerId = Guid.NewGuid();
            var card = Create.Card.OwnedTo(playerId).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = playerBehaviour.Apply(playerId, board, CoinSide.Tails);


            Assert.True(getNewCardBehaviour.CanApply(playerId,board,CoinSide.Tails));
            Assert.True(moveOwnCardBehaviour.CanApply(playerId, board, CoinSide.Tails));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c=> Assert.Equal(playerId, c.PlayerId));

        }

    }
}
