using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly ICoin _coin;
        public Guid Id { get; }

        public Player(ICoin coin)
        {
            _coin = coin;
            Id = Guid.NewGuid();
        }

        public CoinSide DropCoin()
        {
            return _coin.Drop();
        }


    }
}
