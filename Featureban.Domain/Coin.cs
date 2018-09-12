using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Coin:ICoin
    {
        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        public virtual CoinSide Drop()
        {
            return _rnd.Next(1, 10) % 2 > 0
                ? CoinSide.Eagle
                : CoinSide.Tails;
        }
    }
}
