using System.Collections.Generic;

namespace Scrabble.Lib
{
    public class BoardScoreCalculator
    {
        public int ScoreWord(IEnumerable<(Square Square, Tile Tile)> laidTiles, IEnumerable<Square> boardSquares)
        {
            var score = 0;
            var wordFactor = 1;

            foreach (var (square, tile) in laidTiles)
            {
                score += tile.Value * square.Type.LetterFactor;
                wordFactor *= square.Type.WordFactor;
            }

            return score * wordFactor;
        }
    }
}