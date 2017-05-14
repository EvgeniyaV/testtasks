using System;
using System.Threading;

namespace RPSGames
{
    class Player
    {
        /// <summary>
        /// Generate random integer number for player's gesture
        /// </summary>
        /// <returns>Pleayer gesture.</returns>
        public int GetNextGesture()
        {
            int a = StaticRandom.Instance.Next();
            int b = a % (int)(Gesture.Size);
            return b;//StaticRandom.Instance.Next() % (int)(Gesture.Size);
        }
    }

    /// <summary>
    /// Random helper for multithreading program
    /// </summary>
    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}
