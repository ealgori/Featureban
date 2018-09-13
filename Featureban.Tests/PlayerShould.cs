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
            var coinMock = Create.CoinMock.Build();

            player.DropCoin(coinMock.Object);

            coinMock.Verify(c => c.Drop(), Times.Once);
        }

        [Fact]
        public void InvokeCanApplyOnce_WhenPlay()
        {
            var bahaviourMock = Create.PlayerBehavoiurMock.Build();
            var player = Create.Player.WithBehaviour(bahaviourMock.Object).Build();

            player.Play(CoinSide.Eagle, Create.Board.Build());

            AssertBahaviour.CanApplyInvokedOnce(bahaviourMock);
        }

        [Fact]
        public void InvokeApplyOnce_WhenPlay()
        {
            var bahaviourMock = Create.PlayerBehavoiurMock.Build();
            var player = Create.Player.WithBehaviour(bahaviourMock.Object).Build();

            player.Play(CoinSide.Eagle, Create.Board.Build());

            AssertBahaviour.ApplyInvokedOnce(bahaviourMock);
        }
    }
}
