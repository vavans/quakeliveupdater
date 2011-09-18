using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuakeLiveAnalyzer.Ranking
{
    internal enum MatchResult {
        Victory,
        Defeat
    }

    internal class Elo
    {
        private int previousRating;
        internal int PreviousRating { get { return previousRating; } }

        private int rating;
        internal int Rating { get { return rating; } set { rating = value; } }

        public double K 
        { 
            get 
            {
                if (rating < 2000)
                    return 32;
                if (rating < 2400)
                    return 24;
                return 16;
            } 
        }


        internal Elo()
        {
            rating = 1500;
        }

        internal Elo(int rating)
        {
            this.rating = rating;
        }


        internal void Victory(Elo opponent)
        {
            int eloDiff = ComputeEloDiff(1, opponent);
            rating += eloDiff;
            opponent.Rating -= eloDiff;
        }

      

        internal void Defeat(Elo opponent)
        {
            int eloDiff = ComputeEloDiff(0, opponent);
            rating += eloDiff;
            opponent.Rating -= eloDiff;
        }

        internal void Null(Elo opponent)
        {
            int eloDiff = ComputeEloDiff(0.5, opponent);
            rating += eloDiff;
            opponent.Rating -= eloDiff;
        }

        private int ComputeEloDiff(double matchResult, Elo opponent)
        {
            return Convert.ToInt32(K * (matchResult - VictoryProbability(opponent)));
        }

        double VictoryProbability(Elo opponent)
        {
            return 1.0 / (1.0 + Math.Pow(10, (opponent.Rating - rating) / 400.0));
        }
    }
}
