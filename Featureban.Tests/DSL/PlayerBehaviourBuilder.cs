using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Featureban.Domain.PlayerBehave.Full;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Model;

namespace Featureban.Tests.DSL
{
    public class PlayerBehaviourBuilder
    {
        private IEnumerable<PlayerBehaviourContainer> _tailsBehaviours= new List<PlayerBehaviourContainer>();
        private IEnumerable<PlayerBehaviourContainer> _eagleBehaviours = new List<PlayerBehaviourContainer>();


        public PlayerBehaviourBuilder WithTailBehaviours(IPlayerBehaviour tailBehaviour)
        {
            this._tailsBehaviours = new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(0,tailBehaviour)
            };
            return this;
        }
        public PlayerBehaviourBuilder WithTailsBehaviours(IEnumerable<PlayerBehaviourContainer> tailsBehaviours)
        {
            this._tailsBehaviours = tailsBehaviours;
            return this;
        }

        public PlayerBehaviourBuilder WithTailsBehaviours(params PlayerBehaviourContainer[] tailsBehaviours)
        {
            return WithTailsBehaviours(tailsBehaviours.AsEnumerable());
        }
      

      
        public PlayerBehaviourBuilder WithEagleBehaviours(IEnumerable<PlayerBehaviourContainer> eagleBehaviours)
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
