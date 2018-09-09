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
        public string Name { get; }

        public Player(string name,IPlayerBehaviour behaviour)
        {
            _behaviour = behaviour;
            Name = name;
        }

        public Player(IPlayerBehaviour behaviour):this(Guid.NewGuid().ToString(), behaviour)
        {
           
        }

        public CoinSide DropCoin(ICoin coin)
        {
            return coin.Drop();
        }


        public Board Play(CoinSide coinSide, Board board)
        {
            if (_behaviour.CanApply(Name, board, coinSide))
                return _behaviour.Apply(Name, board, coinSide);
            return board;

        }
    }
}
