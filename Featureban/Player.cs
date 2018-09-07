using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly IPlayerBehaviour _behaviour;
        public Guid Id { get; }

        public Player(IPlayerBehaviour behaviour)
        {
            _behaviour = behaviour;
            Id = Guid.NewGuid();
        }

        public CoinSide DropCoin(ICoin coin)
        {
            return coin.Drop();
        }


        public Board Play(CoinSide coinSide, Board board)
        {
            if (_behaviour.CanApply(this, board, coinSide))
                return _behaviour.Apply(this, board);
            return board;

        }
    }
}
