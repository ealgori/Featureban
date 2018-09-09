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
            var playerName = "Ivan";
            var card1 = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card1).Build();

            var newBoard = playerBehaviour.Apply(playerName, board, CoinSide.Tails);


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
            var playerName = "Ivan";
            var card = Create.Card.OwnedTo(playerName).Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = playerBehaviour.Apply(playerName, board, CoinSide.Tails);


            Assert.True(getNewCardBehaviour.CanApply(playerName,board,CoinSide.Tails));
            Assert.True(moveOwnCardBehaviour.CanApply(playerName, board, CoinSide.Tails));
            Assert.Equal(2, newBoard.Cards.Count());
            Assert.All(newBoard.Cards, c=> Assert.Equal(playerName, c.PlayerName));

        }

    }
}
