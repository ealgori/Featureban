using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;
using Moq;

namespace Featureban.Tests.DSL
{
    public class CoinBuilder
    {
        private readonly Mock<ICoin> _coinStub;
        public CoinBuilder()
        {
             _coinStub = new Mock<ICoin>();
        }

        public CoinBuilder WhichAlwaysDropOn(CoinSide side)
        {
            _coinStub.Setup(coin => coin.Drop()).Returns(side);
            return this;
        }

        public ICoin Build()
        {
            return _coinStub.Object;
        }


    }
}
