using System;
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
            return blockOwnBehaviour.CanApply(playerId, board, coinSide);

        }

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            Board newBoard = board;
            if (blockOwnBehaviour.CanApply(playerId, board, coinSide))
                newBoard = blockOwnBehaviour.Apply(playerId, board, coinSide);
            if (getNewCardBehaviour.CanApply(playerId, board, coinSide))
                newBoard = getNewCardBehaviour.Apply(playerId, newBoard, coinSide);
            return newBoard;
        }
    }
}
