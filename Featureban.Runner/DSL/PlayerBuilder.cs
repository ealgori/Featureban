using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.PlayerBehave.Full;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Multi;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Runner.DSL
{
    public class PlayerBuilder
    {
        private readonly IPlayerBehaviour _playerBehaviour;
       
        public PlayerBuilder()
        {
            var tailsBehaviours1Priority = new List<IPlayerBehaviour>
            {
                new GetNewCardBahaviour(),
                new MoveOwnCardForwardBehaviour(),
                new UnblockOwnCardBahaviour()

            };

            var tailsBehaviours2Priority = new List<IPlayerBehaviour>
            {
                new MoveAnotherPlayerCardForwardBehaviour(),
                new UnblockAnotherPlayerCardBehaviour()

            };

            var eagleBehaviours = new List<IPlayerBehaviour>
            {
                new BlockOwnAndGetNewSticker()

            };

            _playerBehaviour = new PlayerBehaviour(tailsBehaviours1Priority, tailsBehaviours2Priority, eagleBehaviours);

        }

        public Player Build()
        {
            return new Player(_playerBehaviour);
        }
    }
}
