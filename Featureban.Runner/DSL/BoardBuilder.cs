using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain;

namespace Featureban.Runner.DSL
{
    public class BoardBuilder
    {
        private int _wipLimit=0;

      

        public BoardBuilder WithWipLimit(int wipLimit)
        {
            this._wipLimit = wipLimit;
            return this;
        }


        public Board Build()
        {
            return new Board(new List<Card>(), new WipLimitBuilder().WithLimit(_wipLimit).Build());
        }
    }
}
