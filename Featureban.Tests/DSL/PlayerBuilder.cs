using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Interfaces;

namespace Featureban.Tests.DSL
{
    public class PlayerBuilder
    {
        private ICoin coin;

        public PlayerBuilder WithCoin(ICoin coin)
        {
            this.coin = coin;
            return this;
        }

        public Player Build()
        {
            return new Player(coin);
        }
    }
}
