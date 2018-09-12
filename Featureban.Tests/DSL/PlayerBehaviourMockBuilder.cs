using Featureban.Domain;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
using Moq;

namespace Featureban.Tests.DSL
{
    public class PlayerBehaviourMockBuilder
    {
        private readonly Mock<IPlayerBehaviour> playerBehaviourMock = new Mock<IPlayerBehaviour>();
        public PlayerBehaviourMockBuilder()
        {
            playerBehaviourMock.Setup(p => p.CanApply(
                   It.IsAny<string>(),
                   It.IsAny<Board>(),
                   It.IsAny<CoinSide>())
                   ).Returns(true);

            playerBehaviourMock.Setup(p => p.Apply(
                   It.IsAny<string>(),
                   It.IsAny<Board>(),
                   It.IsAny<CoinSide>())
                   ).Returns(Create.Board.Build());
        }
        public Mock<IPlayerBehaviour> Build()
        {
            return playerBehaviourMock;
        }
    }
}