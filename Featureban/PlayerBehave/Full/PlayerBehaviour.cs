using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Domain.PlayerBehave.Full
{
    public class PlayerBehaviour:IPlayerBehaviour
    {
        private IEnumerable<IPlayerBehaviour> tailsBehaviours;
        private IEnumerable<IPlayerBehaviour> eagleBehaviours;
        private readonly IPlayerBehaviour skipMoveBehaviour = new SkipMoveBehave();
        private readonly Random rnd = new Random();

        public PlayerBehaviour(IEnumerable<IPlayerBehaviour> tailsBehaviours, IEnumerable<IPlayerBehaviour> eagleBehaviours)
        {
            this.tailsBehaviours = tailsBehaviours;
            this.eagleBehaviours = eagleBehaviours;
        }

        public bool CanApply(Guid playerId, Board board, CoinSide coinSide) => true;
        

        public Board Apply(Guid playerId, Board board, CoinSide coinSide)
        {
            if (coinSide == CoinSide.Tails)
            {
                var satisfidedBehaves = tailsBehaviours.Where(b => b.CanApply(playerId, board, coinSide)).ToList();
                if (!satisfidedBehaves.Any())
                    return skipMoveBehaviour.Apply(playerId, board, coinSide);
                var randombehave = satisfidedBehaves[rnd.Next(satisfidedBehaves.Count())];
                return randombehave.Apply(playerId, board, coinSide);
            }

            if (coinSide == CoinSide.Eagle)
            {
                var satisfidedBehaves =eagleBehaviours.Where(b => b.CanApply(playerId, board, coinSide)).ToList();
                if (!satisfidedBehaves.Any())
                    return skipMoveBehaviour.Apply(playerId, board, coinSide);
                var randombehave = satisfidedBehaves[rnd.Next(satisfidedBehaves.Count())];
                return randombehave.Apply(playerId, board, coinSide);
            }

            return skipMoveBehaviour.Apply(playerId, board, coinSide);

        }
    }
}
