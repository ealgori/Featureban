using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;

namespace Featureban.Tests.DSL
{
    public class CardBuilder
    {
        private Player _player  = new Player();
        private CardState _cardState = CardState.InProgress;
        private bool _blocked;

        public CardBuilder WhichBlocked()
        {
            _blocked = true;
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
        public Card Build()
        {
            return new Card(_player,_cardState, _blocked);
        }
    }
}
