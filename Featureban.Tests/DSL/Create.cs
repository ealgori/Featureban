using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Interfaces;

namespace Featureban.Tests.DSL
{
    public static class Create
    {
        public static CoinBuilder Coin => new CoinBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
        public static GameBuilder Game => new GameBuilder();
        public static CardBuilder Card => new CardBuilder();
        public static BoardBuilder Board => new BoardBuilder();
        public static PlayerBehaviourBuilder PlayerBehavoiur => new PlayerBehaviourBuilder();
        public static WipLimitBuilder WipLimit => new WipLimitBuilder();
    }
}
