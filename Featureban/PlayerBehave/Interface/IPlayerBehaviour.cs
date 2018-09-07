using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain.PlayerBehave.Interface
{
    public interface IPlayerBehaviour
    {
        bool CanApply(Guid playerId, Board board, CoinSide coinSide);
        Board Apply(Guid playerId, Board board);
    }
}
