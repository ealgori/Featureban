using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;

namespace Featureban.Domain.EventArguments
{
    public class BoardChangedEventArgs : EventArgs
    {
        public Board Board { get; }
        public CoinSide CoinSide { get; }

        public BoardChangedEventArgs(Board board, CoinSide coinSide)
        {
            Board = board;
            CoinSide = coinSide;
        }
    }
}
