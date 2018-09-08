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
        private readonly IPlayerBehaviour _playerBehaviour;
       
        public PlayerBuilder()
        {
            var tailsBehaviour = new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(1,new MoveOwnCardForwardBehaviour()),
                new PlayerBehaviourContainer(2,new GetNewCardBahaviour()),
                new PlayerBehaviourContainer(2,new UnblockOwnCardBahaviour()),
                new PlayerBehaviourContainer(3,new MoveAnotherPlayerCardForwardBehaviour()),
                new PlayerBehaviourContainer(4,new UnblockAnotherPlayerCardBehaviour()),
            };

       


            var eagleBehaviours = new List<PlayerBehaviourContainer>
            {
                new PlayerBehaviourContainer(1,new BlockOwnAndGetNewSticker())

            };

            _playerBehaviour = new PlayerBehaviour(tailsBehaviour, eagleBehaviours);

        }

        public Player Build()
        {
            return new Player(_playerBehaviour);
        }
    }
}
