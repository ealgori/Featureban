using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featureban.Runner.Models
{
    public class StageResult
    {
        private readonly Stage _stage;
        private readonly double _result;

        public StageResult(Stage stage, List<int> results)
        {
            _stage = stage;
            _result = results.Average();
        }

        public override string ToString()
        {
            return $"Players:{_stage.PlayersCount} Stage:{_stage.StagesLimit} Games:{_stage.GamesCount} Wip:{_stage.WipLimit} {_result}";
        }

        public string ToCsvRow()
        {
            return $"{_stage.PlayersCount}:{_stage.StagesLimit} {_stage.WipLimit} {_result}";
        }
    }
}
