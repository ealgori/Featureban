using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
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
            
            var player = Create.Player.Build();
            var game = Create.Game
                .WithTwoEagleCoin()
                .WithPlayers(new List<Player> {player})
                .Build();

            var coinSide = player.DropCoin(game.Coin);

            Assert.Equal(CoinSide.Eagle, coinSide);


        }

       
    }
}
