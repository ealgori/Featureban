using System;
using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Enums;
using Featureban.Runner.DSL;
using Featureban.Runner.Models;

namespace Featureban.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var stages = new List<Stage>
            {
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 0},
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 1},
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 2},
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 3},
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 4},
                new Stage() {PlayersCount = 3, StagesLimit = 15, GamesCount = 1000, WipLimit = 5},

                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 0},
                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 1},
                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 2},
                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 3},
                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 4},
                new Stage() {PlayersCount = 3, StagesLimit = 20, GamesCount = 1000, WipLimit = 5},

                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 0},
                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 1},
                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 2},
                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 3},
                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 4},
                new Stage() {PlayersCount = 5, StagesLimit = 15, GamesCount = 1000, WipLimit = 5},

                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 0},
                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 1},
                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 2},
                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 3},
                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 4},
                new Stage() {PlayersCount = 5, StagesLimit = 20, GamesCount = 1000, WipLimit = 5},

            };

            List<StageResult> stageResults = new List<StageResult>();
            foreach (var stage in stages)
            {
                var completedList = new List<int>();
                for (int i = 0; i < stage.GamesCount; i++)
                {
                    var game = Create.Game
                        .WithStagesLimit(stage.StagesLimit)
                        .WithPlayersCount(stage.PlayersCount)
                        .WithWipLimit(stage.WipLimit).Build();

                    game.Play();
                    completedList.Add(game.Board.Cards.Count(c => c.State== CardState.Completed));

                }

                stageResults.Add(new StageResult(stage,completedList));
            }

            foreach (var stageResult in stageResults)
            {
                Console.WriteLine(stageResult);
            }

            foreach (var stageResult in stageResults)
            {
                Console.WriteLine(stageResult.ToCsvRow());
            }

            Console.ReadLine();
        }
    }
}
