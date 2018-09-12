using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;
using Moq;

namespace Featureban.Tests.DSL
{
    public class CoinMockBuilder 
    {
        private readonly Mock<ICoin> _coinMock = new Mock<ICoin>();
        public CoinMockBuilder()
        {
            _coinMock.Setup(c => c.Drop()).Returns(CoinSide.Eagle);
        }
        public Mock<ICoin> Build()
        {
            return _coinMock;
        }
    }
}