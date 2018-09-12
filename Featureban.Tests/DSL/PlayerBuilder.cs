using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Interfaces;
using Featureban.Domain.PlayerBehave.Interface;
using Featureban.Domain.PlayerBehave.Single;

namespace Featureban.Tests.DSL
{
    public class PlayerBuilder
    {

        IPlayerBehaviour _playerBehave = new SkipMoveBehave();

        public PlayerBuilder WithGetNewCardBehaviour()
        {
            _playerBehave = new GetNewCardBahaviour();
            return this;
        }
        public PlayerBuilder WithMoveOwnCardForwardBehaviour()
        {
            _playerBehave = new MoveOwnCardForwardBehaviour();
            return this;
        }

        public PlayerBuilder WithMoveAnotherPlayerCardForwardBehaviour()
        {
            _playerBehave = new MoveAnotherPlayerCardForwardBehaviour();
            return this;
        }

        public PlayerBuilder WithBehaviour(IPlayerBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException("Player behavior is null");
            _playerBehave = behaviour;
            return this;
        }

        public Player Build()
        {
            return new Player(_playerBehave);
        }
    }
}
