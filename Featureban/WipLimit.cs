using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;

namespace Featureban.Domain
{
    public class WipLimit
    {
        private readonly Dictionary<CardState,int> _wipLimits= new Dictionary<CardState, int>();
        public IReadOnlyDictionary<CardState, int> Limits => _wipLimits;
        public WipLimit(params KeyValuePair<CardState, int>[] wipLimit)
        {
            if (wipLimit.Any())
            {
                foreach (var limit in wipLimit)
                {
                    _wipLimits.Add(limit.Key, limit.Value);
                }
            }
        }
    }
}
