using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void CanGetAdditionalCard_IfCoinDropTails()
        {
            var player = Create.Player.WithGetNewCardBehaviour().Build();
            var game = Create.Game
                .WithTwoTailsCoin()
                .WithPlayers(new List<Player> { player })
                .Build();
            
            game.PlayerIterate(player);


            Assert.Equal(2,game.Board.Cards.Count());
            Assert.True(game.Board.Cards.All(c=>c.Owner.Id==player.Id));
        }


        [Fact]
        public void CanMoveOwnCardForward_IfCoinDropTails()
        {
            var player = Create.Player.WithMoveOwnCardForwardBehaviour().Build();
            var card = Create.Card.OwnedTo(player).Build();
            var board = Create.Board.WithCards(card).Build();
            var game = Create.Game
                .WithTwoTailsCoin()
                .WithBoard(board)
                .WithPlayers(new List<Player> { player })
                .Build();

            game.PlayerIterate(player);

            Assert.True(game.Board.Cards.Single().Owner.Id == player.Id);
            Assert.True(game.Board.Cards.Single().State == CardState.InTesting);
        }

       





    }
}
