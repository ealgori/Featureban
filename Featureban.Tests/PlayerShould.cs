using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;
using Featureban.Tests.DSL;
using Moq;
using Xunit;

namespace Featureban.Tests
{
    public class PlayerShould
    {
        [Fact]
        public void CanDropCoin()
        {
            
            var player = Create.Player.Build();
            var game = Create.Game
                .WithTwoEagleCoin()
                .WithPlayers(new List<Player> {player})
                .Build();

            var coinSide = player.DropCoin(game.Coin);

            Assert.Equal(CoinSide.Eagle, coinSide);


        }

        [Fact]
        public void DropCoin1Times_WhenDropCoin()
        {
            var player = Create.Player.WithGetNewCardBehaviour().Build();
            var coinMock = new Mock<Coin>() { CallBase = true };
            var game = Create.Game
                .WithPlayers(new List<Player> { player })
                .WithCoin(coinMock.Object)
                .Build();

            game.PlayerIterate(player);

            coinMock.Verify(c => c.Drop(), Times.Once);
        }
    }
}
