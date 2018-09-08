using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;

namespace Featureban.Domain
{
    public class Board
    {
        public WipLimit WipLimit { get;}
        public IEnumerable<Card> Cards { get;}

        public Board(IEnumerable<Card> cards, WipLimit wipLimit)
        {
            WipLimit = wipLimit;
            Cards = cards;
        }

        public Board ReplaceCard(Card card, Card newCard)
        {
            var cards = Cards.ToList();
            cards.Remove(card);
            cards.Add(newCard);
            return new Board(cards, WipLimit);
        }

        public bool HasSlotsFor(CardState state)
        {
            if (!WipLimit.Limits.ContainsKey(state))
                return true;
            var limit = WipLimit.Limits[state];
            if (limit == 0)
                return true;
            return Cards.Count(c => c.State == state) < limit;
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Board);
        }

        public bool Equals(Board obj)
        {
            return
                obj != null
                &&(obj.Cards.Count() == Cards.Count()) 
                && !obj.Cards.Except(Cards).Any();
        }

        public override int GetHashCode()
        {
            return Cards.GetHashCode();
        }

    }
}
