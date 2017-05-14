using System;
using System.Threading;

namespace RPSGames
{
    public class Pair
    {
        Game game;
        int pairId;

        Player redPlayer;
        Player bluePlayer;
        
        public Pair(Game g, int pId)
        {
            game = g;
            pairId = pId;

            redPlayer = new Player();
            bluePlayer = new Player();
        }

        /// <summary>
        /// Emulate one competition in loop.
        /// </summary>
        public void Run()
        {
            while (game.IsRunning)
            {
                //wait another thread
                Thread.Sleep(new Random().Next(1000, 1500));

                Team turnWinner = Team.None;
               
                // get player's gestures
                int redGesture = redPlayer.GetNextGesture();
                int blueGesture = bluePlayer.GetNextGesture();

                // check competition winner
                if (redGesture == blueGesture)
                    turnWinner = Team.None;
                else if (redGesture > blueGesture && 
                    !(redGesture == (int)Gesture.Scissors && blueGesture == (int)Gesture.Rock))
                    turnWinner = Team.Red;
                else
                    turnWinner = Team.Blue;

                // lock score variable
                lock (game.ScoreLock)
                {
                    // check if game over but thread start new competition
                    if (!game.IsRunning)
                        continue;

                    // increase winner score
                    if (turnWinner != Team.None)
                    {
                        game.AddScore(turnWinner);
                    }

                    // output competition result
                    Tuple<int, int> scores = game.GetScore();
                    int redScore = scores.Item1;
                    int blueScore = scores.Item2;

                    Console.WriteLine("{0, 9}:{1, 10}{2, 10}{3, 5} - {4}. {5} wins.", 
                        pairId, (Gesture)redGesture, (Gesture)blueGesture, redScore, blueScore, turnWinner);

                    if (!game.IsRunning)
                        Console.WriteLine("Game over. Team {0} won", redScore == game.ScoreLimit ? "Red" : "Blue");
                }
            }
        }
    }
}
