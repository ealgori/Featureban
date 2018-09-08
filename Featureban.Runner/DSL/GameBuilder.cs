using Featureban.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;

namespace Featureban.Runner.DSL
{
   public class GameBuilder
    {
        private ICoin coin = new Coin();
        private int _playersCount;
        private int _stagesLimit=1;
        private int _wipLimit;
        private bool withTracing;


       

        public GameBuilder WithStagesLimit(int stagesLimit)
        {
            this._stagesLimit = stagesLimit;
            return this;
        }

        public GameBuilder WithPlayersCount(int playersCount)
        {
            _playersCount = playersCount;
            return this;
        }

        public GameBuilder WithWipLimit(int wipLimit)
        {
            _wipLimit = wipLimit;
            return this;
        }

        public GameBuilder WithOnBoardChangedTracing()
        {
            withTracing = true;
            return this;
        }


        

        public Game Build()
        {
            var players = Enumerable.Range(1, _playersCount).Select(i=> new PlayerBuilder().Build()).ToList();

            var game = new Game(
                players, 
                coin, 
                new BoardBuilder().WithWipLimit(_wipLimit).Build(), 
                _stagesLimit);

            if (withTracing)
                game.OnBoardChanged += (o, e) =>
                {

                    var cards = e.Board.Cards;
                    var progress = cards.Count(c => c.State == CardState.InProgress);
                    var progressUn = cards.Count(c => c.State == CardState.InProgress && !c.IsBlocked);

                    var testing = cards.Count(c => c.State == CardState.InTesting);
                    var testingUn = cards.Count(c => c.State == CardState.InTesting && !c.IsBlocked);

                    var completed = cards.Count(c => c.State == CardState.Completed);
                    var completedUn = cards.Count(c => c.State == CardState.Completed && !c.IsBlocked);

                    Console.WriteLine(e.CoinSide);
                    Console.WriteLine(
                        $"Progress:{progress}({progressUn}) Testing:{testing}({testingUn}) Completed: {completed}({completedUn})");
                };

            return game;
        }
    }
}
