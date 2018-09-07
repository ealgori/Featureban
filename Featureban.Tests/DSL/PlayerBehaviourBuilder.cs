using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.PlayerBehave.Full;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Tests.DSL
{
    public class PlayerBehaviourBuilder
    {
        private IEnumerable<IPlayerBehaviour> _tailsBehaviours= new List<IPlayerBehaviour>();
        private IEnumerable<IPlayerBehaviour> _eagleBehaviours = new List<IPlayerBehaviour>();

        public PlayerBehaviourBuilder WithTailsBehaviours(IEnumerable<IPlayerBehaviour> tailsBehaviours)
        {
            this._tailsBehaviours = tailsBehaviours;
            return this;
        }

        public PlayerBehaviourBuilder WithEagleBehaviours(IEnumerable<IPlayerBehaviour> eagleBehaviours)
        {
            this._eagleBehaviours = eagleBehaviours;
            return this;
        }

        public PlayerBehaviour Build()
        {
            return new PlayerBehaviour(_tailsBehaviours, _eagleBehaviours);
        }

    }
}
