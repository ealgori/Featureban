using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.PlayerBehave.Single;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests.PlayerBehave
{
    public class PlayerBehaviourShould
    {
        [Fact]
        public void SkipMove_IfNoBehavioursAcceptable()
        {
            var playerBehaviour = Create.PlayerBehavoiur
                .WithTailsBehaviours1Priority(new UnblockOwnCardBahaviour())
                .WithTailsBehaviours2Priority(new UnblockAnotherPlayerCardBehaviour()).Build();
            var playerId = Guid.NewGuid();
            var card1 = Create.Card.OwnedTo(playerId).Build();
            var card2 = Create.Card.Build();
            var board = Create.Board.WithCards(card1, card2).Build();

            var newBoard = playerBehaviour.Apply(playerId, board, CoinSide.Tails);


            Assert.Equal(board,newBoard);
        }


        [Fact]
        public void ApplyBehavioursFrom2TailPrioriry_OnlyIfNoSatisfiedBehaveIn1TailPriority()
        {
            var playerBehaviour = Create.PlayerBehavoiur
                .WithTailsBehaviours1Priority(new UnblockOwnCardBahaviour())
                .WithTailsBehaviours2Priority(new UnblockAnotherPlayerCardBehaviour()).Build();
            var playerId = Guid.NewGuid();
            var card1 = Create.Card.OwnedTo(playerId).Build();
            var card2 = Create.Card.WhichBlocked().Build();
            var board = Create.Board.WithCards(card1, card2).Build();

            var newBoard = playerBehaviour.Apply(playerId, board, CoinSide.Tails);

            Assert.Single(newBoard.Cards, c=> !c.IsBlocked && c.PlayerId!=playerId);
        }
    }
}
