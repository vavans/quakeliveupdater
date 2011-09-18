using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QuakeLiveAnalyzer.Ranking;

namespace QuakeLiveAnalyzer.Model
{
    [XmlRoot("Player")]
    [XmlInclude(typeof(QueryableObject))]
    public class Player : QueryableObject
    {
        [XmlAttribute("LastQueryTimestamp")]
        public long LastQueryTimestamp { get; set; }

        [XmlArray("Matchs")]
        [XmlArrayItem("Match", typeof(MatchID))]
        public ObservableCollection<MatchID> MatchsIds { get; set; }

        internal Elo Elo { get; set; }

        public Player()
        {
            Elo = new Elo();
        }

        public Player(string name) : base(name)
        {
            MatchsIds = new ObservableCollection<MatchID>();
            Elo = new Elo();
        }

        public override string GetUrlFromId(string id)
        {
            string dateString = DateTime.UtcNow.ToString("yyyy-MM-dd");

            return string.Format(@"http://www.quakelive.com/profile/matches_by_week/{0}/{1}", id, dateString);
        }

        internal void AddGame(string id)
        {
            if (id.StartsWith("ca_", StringComparison.InvariantCultureIgnoreCase) && !MatchsIds.Any(m => m.Id == id))
            {
                MatchsIds.Add(new MatchID(id));
            }
        }

        public override void MergeData(QueryableObject obj)
        {
            Player player = obj as Player;
            if (player == null)
            {
                return;
            }

            LastQueryTimestamp = Math.Max(player.LastQueryTimestamp, LastQueryTimestamp);

            foreach (MatchID id in player.MatchsIds)
            {
                MatchID firstId = MatchsIds.FirstOrDefault(m => m.Id == id.Id);
                if (firstId == null)
                {
                    MatchsIds.Add(id);
                }
                else
                {
                    firstId.MergeData(id);
                }
            }
        }

        public override void UpdateState()
        {
            if (LastQueryTimestamp == 0)
            {
                State = Model.State.Waiting;
                return;
            }

            TimeSpan timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - LastQueryTimestamp);

            // On refresh la liste des matchs d'un joueur tous les 3 jours

            if (timeSpan.Hours >= 72)
            {
                State = Model.State.Waiting;
            }
            else
            {
                State = Model.State.Done;
            }
        }
    }
}
