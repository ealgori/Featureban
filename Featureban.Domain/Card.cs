using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;

namespace Featureban.Domain
{
    public class Card
    {
        public CardState State { get;}
        public bool IsBlocked { get;}
        public string PlayerName { get;}

        public Card(string playerName, CardState state, bool isBlocked = false)
        {
            this.State = state;
            this.IsBlocked = isBlocked;
            this.PlayerName = playerName;
        }

        public Card MoveForward()
        {
            if (!CanMoveForward())
            {
                throw new InvalidOperationException($"Cannot move card with state '{State}' forward");
            }
            var nextState = State + 1;
            return new Card(PlayerName, nextState);
        }

        public bool CanMoveForward()
        {
            return Enum.IsDefined(typeof(CardState),State + 1);
           
        }

        public Card Block()
        {
            if(IsBlocked)
                throw new NotSupportedException("Card is already blocked");
            return new Card(this.PlayerName, this.State, true);

        }

        public Card Unblock()
        {
            if(!IsBlocked)
                throw new NotSupportedException("Card is not blocked");
            return new Card(this.PlayerName, this.State, false);
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Card);
        }

        public bool Equals(Card obj)
        {
            return
                obj != null
                && obj.PlayerName == this.PlayerName
                && obj.IsBlocked == this.IsBlocked
                && obj.State == this.State;
        }


        public static bool operator ==(Card item1, Card item2)
        {
            if (object.ReferenceEquals(item1, item2)) { return true; }
            if ((object)item1 == null || (object)item2 == null) { return false; }
            return item1.PlayerName == item2.PlayerName
                    && item1.IsBlocked == item2.IsBlocked
                    && item1.State == item2.State;
        }

        public static bool operator !=(Card item1, Card item2)
        {
            return !(item1 == item2);
        }

        public override int GetHashCode()
        {
            return PlayerName.GetHashCode()&IsBlocked.GetHashCode()&State.GetHashCode();
        }
    }
}
