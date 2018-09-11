using Featureban.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featureban.Tests.DSL
{
    internal static class BoardParser
    {
        public static Board Parse(string boardMap)
        {
            var cards = new List<Card>();
            var collapsedBoard = boardMap.Replace(" ", "").Replace("+", "").Replace("-", "");

            var rows = collapsedBoard.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var mapHasLimitsRow = false;
            for (int rowNum = 1; rowNum < rows.Length; rowNum++)
            {
                var row = rows[rowNum];
                if (row.Contains("#") && string.IsNullOrEmpty(row.Replace("+", "").Replace("-", "").Replace("|","").Replace("#","")))
                {
                    if(rowNum!=(rows.Length-1))
                    {
                        mapHasLimitsRow = true;
                    }
                    break;
                }
                var columns = rows[rowNum].Split('|');
                for (int colNum = 0; colNum < columns.Length; colNum++)
                {
                    var column = columns[colNum];
                    if (!string.IsNullOrEmpty(column.Replace(" ", "")))
                    {
                        var card = Create.Card.OwnedTo(column.Trim(new char[] { ' ', '*' }));
                        card = card.InState(colNum);
                        if (column.Contains('*'))
                        {
                            card = card.WhichBlocked();
                        }
                        cards.Add(card.Build());
                    }
                }
            }

            var boardBuilder = Create.Board.WithCards(cards);

            if (mapHasLimitsRow)
            {
                var wipLimitBuilder = Create.WipLimit;
                var limitCols = rows.Last().Split("|").Select(col => new string(col.Where(c => Char.IsDigit(c)).ToArray())).ToList();
                for (int col = 0; col < limitCols.Count(); col++)
                {

                    if (int.TryParse(limitCols[col], out int limit))
                    {
                        if (col == 0)
                            wipLimitBuilder.WithInProgressLimit(limit);
                        if (col == 1)
                            wipLimitBuilder.WithInTestingLimit(limit);
                    }
                }
                boardBuilder.WithWipLimit(wipLimitBuilder.Build());
            }
            return boardBuilder.Build();
        }
    }
}
