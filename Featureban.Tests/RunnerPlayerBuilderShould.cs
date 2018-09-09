using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Model;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class RunnerPlayerBuilderShould
    {
        [Fact]
        public void UseDefaultTailsBehaviorsSettings_OnCreate()
        {
            var expectedTailsBehavioursSequence = new(int priority, Type type )[]
            {
                (1,typeof(MoveOwnCardForwardBehaviour)),
                (2,typeof(GetNewCardBahaviour)),
                (2,typeof(UnblockOwnCardBahaviour)),
                (3,typeof(MoveAnotherPlayerCardForwardBehaviour)),
                (4,typeof(UnblockAnotherPlayerCardBehaviour)),
            };
           
            var playerBuilder = new Runner.DSL.PlayerBuilder();
            var actualTailsBehavioursSequence = playerBuilder
                .TailsBehaviours
                .Select(c=> (c.Priority, c.Behaviour.GetType())
               );

            Assert.Equal(expectedTailsBehavioursSequence , actualTailsBehavioursSequence);

        }

        [Fact]
        public void UseDefaultEagleBehaviorsSettings_OnCreate()
        {
            var expectedEagleBehavioursSequence = new(int priority, Type type)[]
            {
                (1,typeof(BlockOwnAndGetNewSticker)),
               
            };

            var playerBuilder = new Runner.DSL.PlayerBuilder();
            var actualEagleBehavioursSequence = playerBuilder
                .EagleBehaviours
                .Select(c => (c.Priority, c.Behaviour.GetType())
                );

            Assert.Equal(expectedEagleBehavioursSequence, actualEagleBehavioursSequence);

        }

        [Fact]
        public void CreatePlayerWhichMoveOwnCardFirstly_IfDropTails()
        {

            var playerName = "Ivan";
            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName)
                .Build();
            var card1 = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var card2 = Create.Card.OwnedTo(playerName).WhichBlocked().InProgressState().Build();
            var board = Create.Board.WithCards(card1,card2).Build();

            var newBoard = player.Play(CoinSide.Tails, board);

            Assert.Single(newBoard.Cards, c=>c.State==CardState.InProgress&& c.IsBlocked);
            Assert.Single(newBoard.Cards, c => c.State == CardState.InTesting);
        }

        [Fact]
        public void CreatePlayerWhichUnblockOtherPlayerCard_IfDropTailsAndOwnCardsUnacessible()
        {

            var playerName1 = "Ivan";
            var playerName2 = "Vova";
            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName1)
                .Build();
            var card = Create.Card.OwnedTo(playerName2).InProgressState().WhichBlocked().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();

            var newBoard = player.Play(CoinSide.Tails, board);

            Assert.Single(newBoard.Cards);
            Assert.Single(newBoard.Cards, c => c.State == CardState.InProgress && !c.IsBlocked);
            Assert.Single(newBoard.Cards, c => c.PlayerName == playerName2);
        }


        [Fact]
        public void CreatePlayerWhichBlockOwnAndGetNewCard_IfDropEagle()
        {

            var playerName = "Ivan";
            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName)
                .Build();
            var card = Create.Card.OwnedTo(playerName).InProgressState().Build();
            var board = Create.Board.WithCards(card).Build();

            var newBoard = player.Play(CoinSide.Eagle, board);

            Assert.Equal(2, newBoard.Cards.Count());
            Assert.Single(newBoard.Cards, c => 
                c.State == CardState.InProgress 
                && c.IsBlocked
                && c.PlayerName==playerName);
            Assert.Single(newBoard.Cards, c =>
                c.State == CardState.InProgress
                && !c.IsBlocked
                && c.PlayerName == playerName);
        }


        [Fact]
        public void CreatePlayerWhichUnblockOwnCard_IfDropTailsAndCantGetNewOrMoveOwnCard()
        {

            var playerName = "Ivan";
            
            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName)
                .Build();
            var card = Create.Card.OwnedTo(playerName).InProgressState().WhichBlocked().Build();
            var wipLimit = Create.WipLimit.WithLimit(1).Build();
            var board = Create.Board.WithCards(card).WithWipLimit(wipLimit).Build();

            var newBoard = player.Play(CoinSide.Tails, board);

            Assert.Single(newBoard.Cards);
            Assert.Single(newBoard.Cards, c => c.State == CardState.InProgress && !c.IsBlocked);
            Assert.Single(newBoard.Cards, c => c.PlayerName == playerName);
        }


        [Fact]
        public void CreatePlayerWhichGetNewCard_IfDropTailsAndCantUnblockOrMoveOwnCard()
        {

            var playerName1 = "Ivan";
            var playerName2 = "Vova";

            var player = new Runner.DSL.PlayerBuilder()
                .WithName(playerName1)
                .Build();
            var card1 = Create.Card.OwnedTo(playerName1).InProgressState().Build();
            var card2 = Create.Card.OwnedTo(playerName2).InTestingState().Build();
            var wipLimit = Create.WipLimit.WithInProgressLimit(2).WithInTestingLimit(1).Build();
            var board = Create.Board.WithCards(card1,card2).WithWipLimit(wipLimit).Build();

            var newBoard = player.Play(CoinSide.Tails, board);

            Assert.Equal(3,newBoard.Cards.Count());
            Assert.Equal(2, newBoard.Cards.Count(c=> 
                c.State == CardState.InProgress
                && !c.IsBlocked
                && c.PlayerName == playerName1));
        }
    }
}
