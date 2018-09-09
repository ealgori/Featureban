using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Model;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Domain.PlayerBehave.Full
{
    public class PlayerBehaviour:IPlayerBehaviour
    {
        private IEnumerable<PlayerBehaviourContainer> tailsBehaviours;
        private IEnumerable<PlayerBehaviourContainer> eagleBehaviours;
        private readonly IPlayerBehaviour skipMoveBehaviour = new SkipMoveBehave();

        public PlayerBehaviour(IEnumerable<PlayerBehaviourContainer> tailsBehaviours, IEnumerable<PlayerBehaviourContainer> eagleBehaviours)
        {
            this.tailsBehaviours = tailsBehaviours;
            this.eagleBehaviours = eagleBehaviours;
        }

        public bool CanApply(string playerName, Board board, CoinSide coinSide) => true;
        

        public Board Apply(string playerName, Board board, CoinSide coinSide)
        {
            if (coinSide == CoinSide.Tails)
            {
                var satisfidedBehaves = tailsBehaviours.Where(b => b.Behaviour.CanApply(playerName, board, coinSide));
                if (!satisfidedBehaves.Any())
                    return skipMoveBehaviour.Apply(playerName, board, coinSide);
                var orderedByProirity = satisfidedBehaves
                    .OrderBy(b=>b.Priority)
                    .ThenBy(_=> Guid.NewGuid());
                return orderedByProirity.First().Behaviour.Apply(playerName, board, coinSide);
            }

            if (coinSide == CoinSide.Eagle)
            {
                var satisfidedBehaves =eagleBehaviours.Where(b => b.Behaviour.CanApply(playerName, board, coinSide)).ToList();
                if (!satisfidedBehaves.Any())
                    return skipMoveBehaviour.Apply(playerName, board, coinSide);
                var orderedByPriority = satisfidedBehaves
                    .OrderBy(b => b.Priority)
                    .ThenBy(_ => Guid.NewGuid());
                return orderedByPriority.First().Behaviour.Apply(playerName, board, coinSide);
            }

            return skipMoveBehaviour.Apply(playerName, board, coinSide);

        }
    }
}
