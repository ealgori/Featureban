using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Featureban.Domain.PlayerBehave.Model;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;
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
    }
}
