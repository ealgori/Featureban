using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;

namespace Featureban.Tests.DSL
{
    public class CardBuilder
    {
        private string _playerName  = Guid.NewGuid().ToString();
        private CardState _cardState = CardState.InProgress;
        private bool _blocked;

        public CardBuilder WhichBlocked()
        {
            _blocked = true;
            return this;
        }

        public CardBuilder InProgressState()
        {
            _cardState = CardState.InProgress;
            return this;
        }

        public CardBuilder InState(int stateNum)
        {
            if (!Enum.IsDefined(typeof(CardState), stateNum))
                throw new ArgumentException($"State with number {stateNum} not defined");
            _cardState = (CardState)stateNum;
            return this;
        }
        public CardBuilder InTestingState()
        {
            _cardState = CardState.InTesting;
            return this;
        }

        public CardBuilder InCompletedState()
        {
            _cardState = CardState.Completed;
            return this;
        }

        public CardBuilder OwnedTo(string playerName)
        {
            _playerName = playerName;
            return this;
        }
        public Card Build()
        {
            return new Card(_playerName,_cardState, _blocked);
        }
    }
}
