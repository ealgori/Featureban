using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;
using Xunit;

namespace Featureban.Tests
{
    public class CardShould
    {
        [Fact]
        public void ThrowOnBlock_IfBlocked()
        {
            var card = new Card(isBlocked:true);

            Assert.Throws<NotSupportedException>(()=>card.Block());
        }

        [Fact]
        public void ThrowOnUnBlock_IfNotBlocked()
        {
            var card = new Card(isBlocked: false);

            Assert.Throws<NotSupportedException>(() => card.Unblock());
        }

        [Fact]
        public void ChangeState_OnChangeState()
        {
            var card = new Card();

            card.ChangeStatus(CardState.InProgress);

            Assert.Equal(CardState.InProgress, card.State);
        }

    }
}
