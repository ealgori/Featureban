using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Domain.PlayerBehave.Model
{
    public class PlayerBehaviourContainer
    {
        public int Priority { get; }
        public IPlayerBehaviour Behaviour { get; }

        public PlayerBehaviourContainer(int priority, IPlayerBehaviour behaviour)
        {
            Priority = priority;
            Behaviour = behaviour;
        }
    }
}
