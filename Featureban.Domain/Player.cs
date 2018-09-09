using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain
{
    public class Player: IEquatable<Player>
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


        public override bool Equals(object obj)
        {
            return Equals(obj as Card);
        }

        public bool Equals(Player obj)
        {
            return
                obj != null
                && obj.Name == this.Name
                && obj._behaviour.GetType() == this._behaviour.GetType();
        }


        public static bool operator ==(Player item1, Player item2)
        {
            if (object.ReferenceEquals(item1, item2)) { return true; }
            if ((object)item1 == null || (object)item2 == null) { return false; }

            return item1.Name == item2.Name
                   && item1.Name == item2.Name;
        }

        public static bool operator !=(Player item1, Player item2)
        {
            return !(item1 == item2);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() & _behaviour.GetHashCode();
        }


    }
}
