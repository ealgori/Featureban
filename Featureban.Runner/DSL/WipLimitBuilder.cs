using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;

namespace Featureban.Runner.DSL
{
    public class WipLimitBuilder
    {
        private int _wipLimit = 0;
        public WipLimitBuilder WithLimit(int wipLimit)
        {
            _wipLimit = wipLimit;
            return this;
        }

        public WipLimit Build()
        {
            return new WipLimit(
                new KeyValuePair<CardState, int>(CardState.InProgress, _wipLimit),
                new KeyValuePair<CardState, int>(CardState.InTesting, _wipLimit)
            );
        }
    }
}
