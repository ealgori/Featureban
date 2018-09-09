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

            Assert.Equal(playerList[0].Id, game.Players.First().Id);
            Assert.Equal(playerList[1].Id, game.Players.Last().Id);


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

            Assert.Equal(game.Board.Cards.First().PlayerId, game.Players.First().Id);
            


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
                    player=> player.Id == card.PlayerId
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
