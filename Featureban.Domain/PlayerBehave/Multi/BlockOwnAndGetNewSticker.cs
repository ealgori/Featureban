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
        public bool CanApply(string playerName, Board board, CoinSide coinSide)
        {
            return  (blockOwnBehaviour.CanApply(playerName, board, coinSide)
                       || getNewCardBehaviour.CanApply(playerName, board, coinSide));

        }

        public Board Apply(string playerName, Board board, CoinSide coinSide)
        {
            Board newBoard = board;
            if (blockOwnBehaviour.CanApply(playerName, board, coinSide))
                newBoard = blockOwnBehaviour.Apply(playerName, board, coinSide);
            if (getNewCardBehaviour.CanApply(playerName, board, coinSide))
                newBoard = getNewCardBehaviour.Apply(playerName, newBoard, coinSide);
            return newBoard;
        }
    }
}
