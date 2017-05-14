using System;
using System.Collections.Generic;
using System.Threading;

namespace RPSGames
{
    public enum Gesture
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
        Size
    };

    public enum Team
    {
        Red,
        Blue,
        None
    };

    public class Game
    {
        private static int redScore;
        private static int blueScore;

        public readonly object ScoreLock = new object();
        public int ScoreLimit;
        public bool IsRunning;        

        public Game()
        {
            redScore = 0;
            blueScore = 0;
            IsRunning = true;
        }

        public Game(int pairCount, int scoreLimit)
            : this()
        {
            ScoreLimit = scoreLimit;

            List<Thread> threadList = new List<Thread>();

            // create determin thread count
            for (int i = 0; i < pairCount; i++)
            {
                Pair pair = new Pair(this, i + 1);
                Thread t = new Thread(pair.Run);
                t.Start();
                threadList.Add(t);
            }

            // join all thread to main thread
            foreach (var t in threadList)
            {
                t.Join();
            }
        }

        /// <summary>
        /// Calculate score for team.
        /// </summary>
        /// <param name="team">Competition winner.</param>
        public void AddScore(Team team)
        {
            if (team == Team.Red)
                redScore++;
            else
                blueScore++;

            // check game over
            if (redScore == ScoreLimit || blueScore == ScoreLimit)
            {
                IsRunning = false;
            }
        }

        /// <summary>
        /// Get current score.
        /// </summary>
        /// <returns>Red score, blue score.</returns>
        public Tuple<int, int> GetScore()
        {
            return Tuple.Create(redScore, blueScore);
        }
    }
}
