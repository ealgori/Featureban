using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class PlayerShould
    {
        [Fact]
        public void CanDropCoin()
        {
            var coin = Create.Coin.WhichAlwaysDropOn(CoinSide.Eagle).Build();
            var player = Create.Player.WithCoin(coin).Build();

            var coinSide = player.DropCoin();

            Assert.Equal(CoinSide.Eagle, coinSide);


        }
    }
}
