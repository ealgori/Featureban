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
        public Player Owner { get;}

        public Card(Player owner, CardState state, bool isBlocked = false)
        {
            this.State = state;
            this.IsBlocked = isBlocked;
            this.Owner = owner;
        }

        public Card MoveForward()
        {
            if (!CanMoveForward())
            {
                throw new InvalidOperationException($"Cannot move card with state '{State}' forward");
            }
            var nextState = State + 1;
            return new Card(Owner,nextState);
        }

        public bool CanMoveForward()
        {
            var nextState = State + 1;
            return true;
        }

        public Card Block()
        {
            if(IsBlocked)
                throw new NotSupportedException("Card is already blocked");
            return new Card(this.Owner, this.State, true);

        }

        public Card Unblock()
        {
            if(!IsBlocked)
                throw new NotSupportedException("Card is not blocked");
            return new Card(this.Owner, this.State, false);
        }
    }
}
