using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featureban.Domain
{
    public class Board
    {
        public IEnumerable<Card> Cards { get;}

        public Board(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        public Board ReplaceCard(Card card, Card newCard)
        {
            var cards = Cards.ToList();
            cards.Remove(card);
            cards.Add(newCard);
            return new Board(cards);
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
