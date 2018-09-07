using Featureban.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;

namespace Featureban.Runner.DSL
{
   public class GameBuilder
    {
        private ICoin coin = new Coin();
        private int _playersCount;
        private int _movesLimit=1;
        private int _wipLimit;


       

        public GameBuilder WithMovesLimit(int movesLimit)
        {
            this._movesLimit = movesLimit;
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


        public Game Build()
        {
            var players = Enumerable.Range(1, _playersCount).Select(i=> new PlayerBuilder().Build()).ToList();

            return new Game(
                players, 
                coin, 
                new BoardBuilder().WithWipLimit(_wipLimit).Build(), 
                _movesLimit);
        }
    }
}
