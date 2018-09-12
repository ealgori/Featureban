using Featureban.Domain;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Tests.DSL
{
    public static class AssertBahaviour
    {
        public static void CanApplyInvokedOnce(Mock<IPlayerBehaviour> playerBehaviourMock)
        {
            playerBehaviourMock.Verify(b => b.CanApply(It.IsAny<string>(),
                    It.IsAny<Board>(),
                    It.IsAny<CoinSide>()), 
                    Times.Once);

        }

        public static void ApplyInvokedOnce(Mock<IPlayerBehaviour> playerBehaviourMock)
        {
            playerBehaviourMock.Verify(b => b.Apply(It.IsAny<string>(),
                    It.IsAny<Board>(),
                    It.IsAny<CoinSide>()),
                    Times.Once);

        }
    }
}
