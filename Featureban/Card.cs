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
        public Guid PlayerId { get;}

        public Card(Guid playerId, CardState state, bool isBlocked = false)
        {
            this.State = state;
            this.IsBlocked = isBlocked;
            this.PlayerId = playerId;
        }

        public Card MoveForward()
        {
            if (!CanMoveForward())
            {
                throw new InvalidOperationException($"Cannot move card with state '{State}' forward");
            }
            var nextState = State + 1;
            return new Card(PlayerId,nextState);
        }

        public bool CanMoveForward()
        {
            return Enum.IsDefined(typeof(CardState),State + 1);
           
        }

        public Card Block()
        {
            if(IsBlocked)
                throw new NotSupportedException("Card is already blocked");
            return new Card(this.PlayerId, this.State, true);

        }

        public Card Unblock()
        {
            if(!IsBlocked)
                throw new NotSupportedException("Card is not blocked");
            return new Card(this.PlayerId, this.State, false);
        }
    }
}
