using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Tests.DSL
{
    public class PlayerBuilder
    {
        private ICoin coin;


        public PlayerBuilder WithTwoEagleCoin()
        {
            this.coin = Create.Coin.WhichAlwaysDropOn(CoinSide.Eagle).Build();
            return this;
        }

        public PlayerBuilder WithTwoTailsCoin()
        {
            this.coin = Create.Coin.WhichAlwaysDropOn(CoinSide.Tails).Build();
            return this;
        }



        public Player Build()
        {
            return new Player(coin);
        }
    }
}
