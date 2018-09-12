using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
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
        public void InvokeCanApply1Times_WhenPlay()
        {

            var bahaviourMock = Create.PlayerBehavoiurMock.Build();
            var player = Create.Player.WithBehaviour(bahaviourMock.Object).Build();

            var coinSide = player.Play(CoinSide.Eagle, Create.Board.Build());

            AssertBahaviour.CanApplyInvokedOnce(bahaviourMock);
        }













    }
}
