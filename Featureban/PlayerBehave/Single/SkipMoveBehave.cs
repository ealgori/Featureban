using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Single
{
    public class SkipMoveBehave:IPlayerBehaviour
    {
        public bool CanApply(Player player, Board board, CoinSide coinSide) => true;

        public Board Apply(Player player, Board board)
        {
            return board;
        }
    }
}
