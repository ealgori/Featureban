using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;

namespace Featureban.Tests.DSL
{
    public class BoardBuilder
    {
        private IEnumerable<Card> cards = new List<Card>();

        public BoardBuilder WithCards(IEnumerable<Card> cards)
        {
            this.cards = cards;
            return this;
        }

        public Board Build()
        {
            return new Board(cards);
        }
}
}
