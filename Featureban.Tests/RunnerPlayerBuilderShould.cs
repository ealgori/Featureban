using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Model;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class RunnerPlayerBuilderShould
    {
        [Fact]
        public void UseDefaultTailsBehaviorsSettings_OnCreate()
        {
            var expectedTailsBehavioursSequence = new(int priority, Type type )[]
            {
                (1,typeof(MoveOwnCardForwardBehaviour)),
                (2,typeof(GetNewCardBahaviour)),
                (2,typeof(UnblockOwnCardBahaviour)),
                (3,typeof(MoveAnotherPlayerCardForwardBehaviour)),
                (4,typeof(UnblockAnotherPlayerCardBehaviour)),
            };
           
            var playerBuilder = new Runner.DSL.PlayerBuilder();
            var actualTailsBehavioursSequence = playerBuilder
                .TailsBehaviours
                .Select(c=> (c.Priority, c.Behaviour.GetType())
               );

            Assert.Equal(expectedTailsBehavioursSequence , actualTailsBehavioursSequence);

        }

        [Fact]
        public void UseDefaultEagleBehaviorsSettings_OnCreate()
        {
            var expectedEagleBehavioursSequence = new(int priority, Type type)[]
            {
                (1,typeof(BlockOwnAndGetNewSticker)),
               
            };

            var playerBuilder = new Runner.DSL.PlayerBuilder();
            var actualEagleBehavioursSequence = playerBuilder
                .EagleBehaviours
                .Select(c => (c.Priority, c.Behaviour.GetType())
                );

            Assert.Equal(expectedEagleBehavioursSequence, actualEagleBehavioursSequence);

        }

        [Fact]
        public void CreatePlayerWhichMoveOwnCardFirstly_IfDropTails()
        {

            var playerName = "Ivan";
            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName)
                .Build();
            var card1 = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var card2 = Create.Card.OwnedTo(playerName).WhichBlocked().InProgressState().Build();
            var board = Create.Board.WithCards(card1,card2).Build();

            var newBoard = player.Play(CoinSide.Tails, board);

            Assert.Single(newBoard.Cards, c=>c.State==CardState.InProgress&& c.IsBlocked);
            Assert.Single(newBoard.Cards, c => c.State == CardState.InTesting);

        }

    }
}
