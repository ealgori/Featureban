﻿using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Domain.PlayerBehave.Multi
{
    public class BlockOwnAndGetNewSticker:IPlayerBehaviour
    {
        readonly IPlayerBehaviour blockOwnBehaviour =  new BlockOwnCardBahaviour();
        readonly IPlayerBehaviour getNewCardBehaviour = new GetNewCardBahaviour();
        public BlockOwnAndGetNewSticker()
        {
            

        }
        public bool CanApply(Guid playerId, Board board, CoinSide coinSide)
        {
            return blockOwnBehaviour.CanApply(playerId, board, coinSide)
                   && getNewCardBehaviour.CanApply(playerId, board, coinSide);
        }

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            var newBoard = blockOwnBehaviour.Apply(playerId, board,coinSide);
            var newBoard2 = getNewCardBehaviour.Apply(playerId, board,coinSide);
            return newBoard2;
        }
    }
}
