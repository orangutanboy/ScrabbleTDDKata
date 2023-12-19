using System.Collections.Generic;
using System.Linq;

namespace Scrabble.Lib
{
    public class BoardScoreCalculator
    {
        public int ScoreWord(IEnumerable<(Square Square, Tile Tile)> laidTiles, IEnumerable<Square> boardSquares)
        {
            var laid = laidTiles.ToList();
            var board = boardSquares.ToList();

            var score = 0;
            var wordFactor = 1;

            foreach (var (square, tile) in laid)
            {
                score += tile.Value * square.Type.LetterFactor;
                wordFactor *= square.Type.WordFactor;
            }

            var alreadyPlacedSquare = laid.First().Square;
            alreadyPlacedSquare = alreadyPlacedSquare.Up(board);
            while (alreadyPlacedSquare?.State is Occupied occupied)
            {
                score += occupied.Tile.Value;
                alreadyPlacedSquare = alreadyPlacedSquare.Up(board);
            }

            return score * wordFactor;
        }
    }

    public static class SquareExtensions
    {
        public static Square Up(this Square square, IEnumerable<Square> board) =>
            board.FirstOrDefault(x => x.Point.X == square.Point.X && x.Point.Y == square.Point.Y - 1);
    }
}