using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain.PlayerBehave.Interface
{
    public interface IPlayerBehaviour
    {
        bool CanApply(string playerName, Board board, CoinSide coinSide);
        Board Apply(string playerName, Board board, CoinSide coinSide);
    }
}
