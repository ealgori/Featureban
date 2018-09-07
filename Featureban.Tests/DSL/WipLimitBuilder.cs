using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain;
using Featureban.Domain.Enums;

namespace Featureban.Tests.DSL
{
    public class WipLimitBuilder
    {
        private int inProgerssLimit = 0;
        private int inTestingLimit = 0;

        public WipLimitBuilder WithInProgressLimit(int count)
        {
            inProgerssLimit = count;
            return this;
        }

        public WipLimitBuilder WithInTestingLimit(int count)
        {
            inTestingLimit = count;
            return this;
        }

        public WipLimitBuilder WithLimit(int count)
        {
            inProgerssLimit = count;
            inTestingLimit = count;
            return this;
        }

        public WipLimit Build()
        {
            return  new WipLimit(
                new KeyValuePair<CardState, int>(CardState.InProgress, inProgerssLimit),
                new KeyValuePair<CardState, int>(CardState.InTesting, inTestingLimit)
                );
        }

    }
}
