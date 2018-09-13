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
        public void CreatePlayerWhichMoveOwnCardFirstly_IfDropTails()
        {
            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +Ivan*     |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Tails, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan*     |Ivan      |         +
                                  +          |          |         +
                                  +-------------------------------+", newBoard);
        }

        [Fact]
        public void CreatePlayerWhichUnblockOtherPlayerCard_IfDropTailsAndOwnCardsUnacessible()
        {

            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Vova*     |          |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:1   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Tails, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Vova      |          |         +
                                  +          |          |         +
                                  #-------------------------------#
                                  +Limit:1   |Limit:1   |         +", newBoard);
        }   
        

        [Fact]
        public void CreatePlayerWhichBlockOwnAndGetNewCard_IfDropEagle()
        {
            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |          |         +
                                +          |          |         +
                                +-------------------------------+";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Eagle, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +Ivan*     |          |         +
                                  +-------------------------------+", newBoard);
        }


        [Fact]
        public void CreatePlayerWhichUnblockOwnCard_IfDropTailsAndCantGetNewOrMoveOwnCard()
        {
            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan*     |          |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:1   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Tails, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |         +
                                  +          |          |         +
                                  #-------------------------------#
                                  +Limit:1   |Limit:1   |         +", newBoard);
        }


        [Fact]
        public void CreatePlayerWhichGetNewCard_IfDropTailsAndCantUnblockOrMoveOwnCard()
        {
            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |Vova      |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:2   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Tails, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |Vova      |         +
                                  +Ivan      |          |         +
                                  #-------------------------------#
                                  +Limit:2   |Limit:1   |         +", newBoard);
        }



        [Fact]
        public void CreatePlayerWhichMoveOtherPlayerCard_IfDropTailsAndCantGetNewUnblockOrMoveOwnCard()
        {
            var player = new Runner.DSL.PlayerBuilder()
                .WithName("Ivan")
                .Build();
            var boardMap = $@"  +-------------------------------+
                                +InProgress|InTesting |Completed+
                                +-------------------------------+
                                +Ivan      |Vova      |         +
                                +          |          |         +
                                #-------------------------------#
                                +Limit:1   |Limit:1   |         +";
            var board = Create.Board.FromMap(boardMap);

            var newBoard = player.Play(CoinSide.Tails, board);

            AssertBoard.Equals($@"+-------------------------------+
                                  +InProgress|InTesting |Completed+
                                  +-------------------------------+
                                  +Ivan      |          |Vova     +
                                  +          |          |         +
                                  #-------------------------------#
                                  +Limit:1   |Limit:1   |         +", newBoard);
        }
    }
}
