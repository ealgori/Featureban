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

            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = playerBehaviour.Apply("Ivan", board, CoinSide.Tails);
            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
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

            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);


            var newBoard = playerBehaviour.Apply("Ivan", board, CoinSide.Tails);


            Assert.True(getNewCardBehaviour.CanApply("Ivan",board,CoinSide.Tails));
            Assert.True(moveOwnCardBehaviour.CanApply("Ivan", board, CoinSide.Tails));
          

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +Ivan      |          |         +
                                  +-------------------------------+", newBoard);
        }

    }
}
