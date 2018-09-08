using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Runner.Models
{
    public class Stage
    {
        public int StagesLimit { get; set; }
        public int PlayersCount { get; set; }
        public int GamesCount { get; set; }
        public int WipLimit { get; set; }
    }
}
