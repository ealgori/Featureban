using System;
using System.Collections.Generic;
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
    }
}
