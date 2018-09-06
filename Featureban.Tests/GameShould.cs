using System;
using System.Collections.Generic;
using System.Linq;
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


            var game = Create.Game
                .WithTwoEagleCoin()
                .WithPlayers(playerList)
                .Build();

            Assert.Equal(playerList[0].Id, game.Players[0].Id);
            Assert.Equal(playerList[1].Id, game.Players[1].Id);


        }

        [Fact]
        public void AllowPlayerGetCard_BeforeGameStars()
        {
            var playerList = new List<Player>
            {
                Create.Player.Build()
            };


            var game = Create.Game
                .WithTwoEagleCoin()
                .WithPlayers(playerList).Build();

            Assert.Equal(game.Board.Cards.First().Owner.Id, game.Players[0].Id);
            


        }

        [Fact]
        public void NotAllowPlayersGetCardsBeforeGameStarts_IfCardsAlreadyTaken()
        {
            var playerList = new List<Player>
            {
                Create.Player.Build()
            };

            var card = Create.Card.Build();
            var board = Create.Board.WithCards(new List<Card> {card}).Build();
            var game = Create.Game
                .WithTwoEagleCoin()
                .WithBoard(board)
                .WithPlayers(playerList).Build();

            Assert.Equal(card, board.Cards.Single());



        }
    }
}
