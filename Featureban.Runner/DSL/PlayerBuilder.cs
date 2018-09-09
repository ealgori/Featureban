using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.PlayerBehave.Full;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Model;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Runner.DSL
{
    public class PlayerBuilder
    {
        public IEnumerable<PlayerBehaviourContainer> TailsBehaviours { get; }
        public IEnumerable<PlayerBehaviourContainer> EagleBehaviours { get; }
        private string _playerName = Guid.NewGuid().ToString();
       
        public PlayerBuilder()
        {
             TailsBehaviours =  new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(1,new MoveOwnCardForwardBehaviour()),
                new PlayerBehaviourContainer(2,new GetNewCardBahaviour()),
                new PlayerBehaviourContainer(2,new UnblockOwnCardBahaviour()),
                new PlayerBehaviourContainer(3,new MoveAnotherPlayerCardForwardBehaviour()),
                new PlayerBehaviourContainer(4,new UnblockAnotherPlayerCardBehaviour()),
            };

            EagleBehaviours = new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(1,new BlockOwnAndGetNewSticker())
            };
        }

        public PlayerBuilder WithName(string name)
        {
            _playerName = name;
            return this;
        }

        public Player Build()
        {
            var playerBehaviour = new PlayerBehaviour(TailsBehaviours, EagleBehaviours);
            return new Player(_playerName, new PlayerBehaviour(TailsBehaviours,EagleBehaviours));
        }
    }
}
