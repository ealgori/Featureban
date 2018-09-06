using System;
using System.Collections.Generic;
using System.Text;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Game
    {
        public List<Player> Players { get; }
        public Board Board { get; }
        public Guid Id { get;}
        public Game(Guid id, List<Player> players)
        {
            Players = players;
            Id = Guid.NewGuid();
        }
        public Game(List<Player> players): this(Guid.NewGuid(), players)
        {
        }
    }
}
