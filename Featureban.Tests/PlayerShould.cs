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
        public void CanGetAdditionalCard_WhenPlayerWithGetNewCardBehaviour()
        {
            var player = Create.Player.WithGetNewCardBehaviour().Build();
            var game = Create.Game
                .WithPlayers(new List<Player> { player })
                .Build();
            
            game.PlayerIterate(player);


            Assert.Equal(2,game.Board.Cards.Count());
            Assert.True(game.Board.Cards.All(c=>c.PlayerName==player.Name));
        }


        [Fact]
        public void CanMoveOwnCardForward_WhenPlayerWithMoveOwnCardForwardBehaviour()
        {
            var player = Create.Player.WithMoveOwnCardForwardBehaviour().Build();
            var card = Create.Card.OwnedTo(player.Name).Build();
            var board = Create.Board.WithCards(card).Build();
            var game = Create.Game
                .WithBoard(board)
                .WithPlayers(new List<Player> { player })
                .Build();

            game.PlayerIterate(player);

            Assert.True(game.Board.Cards.Single().PlayerName == player.Name);
            Assert.True(game.Board.Cards.Single().State == CardState.InTesting);
        }

        [Fact]
        public void CanMoveAnotherPlayerCardForward_WithWithMoveAnotherPlayerCardForwardBehaviour()
        {
            var player = Create.Player.WithMoveAnotherPlayerCardForwardBehaviour().Build();
            var card = Create.Card.Build();
            var board = Create.Board.WithCards(card).Build();
            var game = Create.Game
                .WithBoard(board)
                .WithPlayers(new List<Player> { player })
                .Build();

            game.PlayerIterate(player);

            Assert.True(game.Board.Cards.Single().PlayerName != player.Name);
            Assert.True(game.Board.Cards.Single().State == CardState.InTesting);
        }







    }
}
