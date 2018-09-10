using Featureban.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Tests.DSL
{
    internal static class BoardParser
    {
        public static Board Parse(string board)
        {
            var cards = new List<Card>();
            var collapsedBoard = board.Replace(" ", "").Replace("+", "").Replace("-", "");

            var rows = collapsedBoard.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int row = 1; row < rows.Length; row++)
            {
                var columns = rows[row].Split('|');
                for (int col = 0; col < columns.Length; col++)
                {
                    var column = columns[col];
                    if (!string.IsNullOrEmpty(column.Replace(" ", "")))
                    {
                        var card = Create.Card.OwnedTo(column.Trim(new char[] { ' ', '*' }));
                        card = card.InState(col);
                        if (column.Contains('*'))
                        {
                            card = card.WhichBlocked();
                        }
                        cards.Add(card.Build());
                    }
                }
            }
            return Create.Board.WithCards(cards).Build();
        }
    }
}
