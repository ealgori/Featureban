using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Interfaces;

namespace Featureban.Tests.DSL
{
    public class PlayerBuilder
    {
        public Player Build()
        {
            return new Player();
        }
    }
}
