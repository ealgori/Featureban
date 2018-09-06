using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class Game
    {
        public List<Player> Players { get; }
        public Board Board { get; private set; }
        public ICoin Coin { get; }
        public Guid Id { get;}
        public Game(Guid id, List<Player> players, ICoin coin, Board board)
        {
            if(players==null || !players.Any())
                throw new ArgumentException("No one player in game");

            Players = players;
            Board = board ?? throw new ArgumentException("No board in game");
            Coin = coin ?? throw new ArgumentException("No coin in game");

            if(!Board.Cards.Any())
                foreach (var player in players)
                {
                    AssignCardToPlayer(player);
                }
            Id = Guid.NewGuid();
            
            
        }
        public Game(List<Player> players, ICoin coin, Board board) : this(Guid.NewGuid(), players, coin, board)
        {
        }

        public void AssignCardToPlayer(Player player)
        {
            var cardsList = new List<Card>(this.Board.Cards)
            {
                new Card(player, CardState.InProgress)
            };
            this.Board = new Board(cardsList);
        }




    }
}
