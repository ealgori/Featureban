using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Player
    {
        public Guid Id { get; }

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public CoinSide DropCoin(ICoin coin)
        {
            return coin.Drop();
        }


    }
}
