using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class SkipMoveBehave:IPlayerBehaviour
    {
        public bool CanApply(Guid playerId, Board board, CoinSide coinSide) => true;

        public Board Apply(Guid playerId, Board board)
        {
            return board;
        }
    }
}
