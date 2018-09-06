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
            
            var player = Create.Player.WithTwoEagleCoin().Build();

            var coinSide = player.DropCoin();

            Assert.Equal(CoinSide.Eagle, coinSide);


        }

       
    }
}
