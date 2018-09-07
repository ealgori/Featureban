using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;

namespace Featureban.Tests.DSL
{
    public class BoardBuilder
    {
        private IEnumerable<Card> cards = new List<Card>();
        private WipLimit _wipLimit = Create.WipLimit.Build();

        public BoardBuilder WithCards(IEnumerable<Card> cards)
        {
            this.cards = cards;
            return this;
        }

        public BoardBuilder WithWipLimit(WipLimit wipLimit)
        {
            this._wipLimit = wipLimit;
            return this;
        }

        public BoardBuilder WithCards(params Card[] cards)
        {
            return WithCards(cards.AsEnumerable());
        }

        public Board Build()
        {
            return new Board(cards, _wipLimit);
        }
}
}
