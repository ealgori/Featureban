using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Game
    {
        public List<Player> Players { get; }
        public Board Board { get; }
        public ICoin Coin { get; }
        public Guid Id { get;}
        public Game(Guid id, List<Player> players, ICoin coin)
        {
            if(players==null || !players.Any())
                throw new ArgumentException("No one player in game");

            if(coin == null)
                throw  new ArgumentException("No coin in game");

            Players = players;
            Id = Guid.NewGuid();
            Coin = coin;
        }
        public Game(List<Player> players, ICoin coin) : this(Guid.NewGuid(), players, coin)
        {
        }
    }
}
