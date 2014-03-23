using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class GetBest
    {
        public GetBest()
        {

        }
        /*
                public Move_Key get_best()
                {
                    return this.iterative_deep();
                }

                public Move_Key iterative_deep()
                {
                    DateTime end = DateTime.Now.AddMilliseconds(300);
            
                    int depth = 0;
                    Move_Key best;
                    do
                    {
                        Move_Key newBest = this.search(depth, -10000, 10000, 0, 0);
                        best = newBest;
                    } while (DateTime.Now < end);
                    return best;
                }

                /*
                AI.prototype.search = function(depth, alpha, beta, positions, cutoffs) {
                public Move_Key search()
                {
                    int bestScore;
                    Move_Key bestMove;
                    int result;
                    if ()

                }

          // the maxing player
          if (this.grid.playerTurn) {
            bestScore = alpha;
            for (var direction in [0, 1, 2, 3]) {
              var newGrid = this.grid.clone();
              if (newGrid.move(direction).moved) {
                positions++;
                if (newGrid.isWin()) {
                  return { move: direction, score: 10000, positions: positions, cutoffs: cutoffs };
                }
                var newAI = new AI(newGrid);

                if (depth == 0) {
                  result = { move: direction, score: newAI.eval() };
                } else {
                  result = newAI.search(depth-1, bestScore, beta, positions, cutoffs);
                  if (result.score > 9900) { // win
                    result.score--; // to slightly penalize higher depth from win
                  }
                  positions = result.positions;
                  cutoffs = result.cutoffs;
                }

                if (result.score > bestScore) {
                  bestScore = result.score;
                  bestMove = direction;
                }
                if (bestScore > beta) {
                  cutoffs++
                  return { move: bestMove, score: beta, positions: positions, cutoffs: cutoffs };
                }
              }
            }
          }

          else { // computer's turn, we'll do heavy pruning to keep the branching factor low
            bestScore = beta;

            // try a 2 and 4 in each cell and measure how annoying it is
            // with metrics from eval
            var candidates = [];
            var cells = this.grid.availableCells();
            var scores = { 2: [], 4: [] };
            for (var value in scores) {
              for (var i in cells) {
                scores[value].push(null);
                var cell = cells[i];
                var tile = new Tile(cell, parseInt(value, 10));
                this.grid.insertTile(tile);
                scores[value][i] = -this.grid.smoothness() + this.grid.islands();
                this.grid.removeTile(cell);
              }
            }

            // now just pick out the most annoying moves
            var maxScore = Math.max(Math.max.apply(null, scores[2]), Math.max.apply(null, scores[4]));
            for (var value in scores) { // 2 and 4
              for (var i=0; i<scores[value].length; i++) {
                if (scores[value][i] == maxScore) {
                  candidates.push( { position: cells[i], value: parseInt(value, 10) } );
                }
              }
            }

            // search on each candidate
            for (var i=0; i<candidates.length; i++) {
              var position = candidates[i].position;
              var value = candidates[i].value;
              var newGrid = this.grid.clone();
              var tile = new Tile(position, value);
              newGrid.insertTile(tile);
              newGrid.playerTurn = true;
              positions++;
              newAI = new AI(newGrid);
              result = newAI.search(depth, alpha, bestScore, positions, cutoffs);
              positions = result.positions;
              cutoffs = result.cutoffs;

              if (result.score < bestScore) {
                bestScore = result.score;
              }
              if (bestScore < alpha) {
                cutoffs++;
                return { move: null, score: alpha, positions: positions, cutoffs: cutoffs };
              }
            }

          }

          return { move: bestMove, score: bestScore, positions: positions, cutoffs: cutoffs };
        }

            }
        }
                */
    }
}