using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Featureban.Domain.Enums;
using Featureban.Domain.Interfaces;

[assembly: InternalsVisibleTo("Featureban.Tests")]
namespace Featureban.Domain
{
    
    public class Game
    {
        private readonly int _moveLimit;
        public List<Player> Players { get; }
        public Board Board { get; private set; }
        public ICoin Coin { get; }
        public Guid Id { get;}
        public int MovesDone { get; private set; } = 0;

        public Game(Guid id, List<Player> players, ICoin coin, Board board, int moveLimit)
        {
            if(players==null || !players.Any())
                throw new ArgumentException("No one player in game");
            if(moveLimit==0)
                throw new ArgumentException("Game without move limits");
            _moveLimit = moveLimit;

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
        public Game(List<Player> players, ICoin coin, Board board, int moveLimit) : this(Guid.NewGuid(), players, coin, board, moveLimit)
        {
        }

        internal void AssignCardToPlayer(Player player)
        {

            if (Board.HasSlotsFor(CardState.InProgress))
            {
                var cardsList = new List<Card>(this.Board.Cards)
                {
                    new Card(player.Id, CardState.InProgress)
                };
                UpdateBoard(new Board(cardsList, Board.WipLimit));
            }
        }

        internal void PlayerIterate(Player player)
        {
            var coinSide = player.DropCoin(Coin);
            var newBoard = player.Play(coinSide, Board);
            UpdateBoard(newBoard);
            MovesDone++;
        }


        public void Play()
        {
            while (MovesDone < _moveLimit)
            {
                foreach (var player in Players)
                {
                    PlayerIterate(player);
                    
                    if (MovesDone >= _moveLimit)
                        break;
                }
            }
        }

        private void UpdateBoard(Board newBoard)
        {
            Board = newBoard;
        }

    }
}
