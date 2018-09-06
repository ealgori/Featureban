using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class GameShould
    {
        [Fact]
        public void JoinGroupOfPlayer_OnCreate()
        {
            var playerList = new List<Player>
            {
                Create.Player.Build(),
                Create.Player.Build()
            };


            var game = Create.Game.WithTwoEagleCoin().WithPlayers(playerList).Build();

            Assert.Equal(playerList[0].Id, game.Players[0].Id);
            Assert.Equal(playerList[1].Id, game.Players[1].Id);


        }
    }
}
