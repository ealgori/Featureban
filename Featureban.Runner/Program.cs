using System;
using Featureban.Runner.DSL;

namespace Featureban.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Create.Game.WithMovesLimit(15).WithPlayersCount(10).WithWipLimit(5).Build();
            game.Play();

        }
    }
}
