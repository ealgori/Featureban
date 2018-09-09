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

            Assert.Equal(playerList[0].Name, game.Players.First().Name);
            Assert.Equal(playerList[1].Name, game.Players.Last().Name);


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

            Assert.Equal(game.Board.Cards.First().PlayerName, game.Players.First().Name);
            


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

        [Fact]
        public void AllowPlayerGetCardNotMoreThanWipLimit_BeforeGameStarts()
        {
            var playerList = new List<Player>
            {
                Create.Player.Build(),
                Create.Player.Build()
            };
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithWipLimit(wipLimit).Build();

            var game = Create.Game
                .WithBoard(board)
                .WithPlayers(playerList).Build();

            Assert.Single(game.Board.Cards, 
                card=> playerList.Any(
                    player=> player.Name == card.PlayerName
                )
            );
            



        }

        [Fact]
        public void PerformExact5Stages_WhenPlayAndStagesLimitIs5()
        {
            var playerList = new List<Player>
            {
                Create.Player.Build(),
                Create.Player.Build()
            };
            var board = Create.Board.Build();
            var game = Create.Game
                .WithBoard(board)
                .WithStagesLimit(5)
                .WithPlayers(playerList).Build();

            game.Play();

            Assert.Equal(5, game.StagesDone);
        }
    }
}
