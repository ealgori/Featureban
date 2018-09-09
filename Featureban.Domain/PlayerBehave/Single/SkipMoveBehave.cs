using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class SkipMoveBehave:IPlayerBehaviour
    {
        public bool CanApply(string playerName, Board board, CoinSide coinSide) => true;

        public Board Apply(string playerName, Board board, CoinSide coinSide)
        {
            return board;
        }
    }
}
