using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Featureban.Domain.PlayerBehave.Full;
using Featureban.Domain.PlayerBehave.Interface;

namespace Featureban.Tests.DSL
{
    public class PlayerBehaviourBuilder
    {
        private IEnumerable<IPlayerBehaviour> _tailsBehaviours1Priority= new List<IPlayerBehaviour>();
        private IEnumerable<IPlayerBehaviour> _tailsBehaviours2Priority = new List<IPlayerBehaviour>();
        private IEnumerable<IPlayerBehaviour> _eagleBehaviours = new List<IPlayerBehaviour>();

        public PlayerBehaviourBuilder WithTailsBehaviours1Priority(IEnumerable<IPlayerBehaviour> tailsBehaviours)
        {
            this._tailsBehaviours1Priority = tailsBehaviours;
            return this;
        }

        public PlayerBehaviourBuilder WithTailsBehaviours1Priority(params IPlayerBehaviour[] tailsBehaviours)
        {
            return WithTailsBehaviours1Priority(tailsBehaviours.AsEnumerable());
        }
        public PlayerBehaviourBuilder WithTailsBehaviours2Priority(IEnumerable<IPlayerBehaviour> tailsBehaviours)
        {
            this._tailsBehaviours2Priority = tailsBehaviours;
            return this;
        }

        public PlayerBehaviourBuilder WithTailsBehaviours2Priority(params IPlayerBehaviour[] tailsBehaviours)
        {
            return WithTailsBehaviours2Priority(tailsBehaviours.AsEnumerable());
        }

        public PlayerBehaviourBuilder WithEagleBehaviours(IEnumerable<IPlayerBehaviour> eagleBehaviours)
        {
            this._eagleBehaviours = eagleBehaviours;
            return this;
        }

        public PlayerBehaviour Build()
        {
            return new PlayerBehaviour(_tailsBehaviours1Priority, _tailsBehaviours2Priority, _eagleBehaviours);
        }

    }
}
