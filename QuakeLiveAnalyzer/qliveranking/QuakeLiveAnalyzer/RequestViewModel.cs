#region

using QuakeLiveAnalyzer.Model;
using QuakeLiveAnalyzer.QuakeLiveJSon;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using QuakeLiveAnalyzer.Serialization;

#endregion

namespace QuakeLiveAnalyzer
{
	public class RequestViewModel : IPopulable<Player>, IPopulable<Match>, INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<Player> Players { get; set; }
         
		public ObservableCollection<Match> Matchs { get; set; }

		public PlayerRequester PlayerRequester { get; private set; }
		public MatchRequester MatchRequester { get; private set; }

		private string _playerField;
         
		public RequestViewModel()
		{
			Players = new ObservableCollection<Player>();
			Matchs = new ObservableCollection<Match>();

			PlayerRequester = new PlayerRequester(this,
				GlobalParameters.SleepTimeBetweenPlayerRequests,
				GlobalParameters.MaxSimultaneousPlayerRequests);

			MatchRequester = new MatchRequester(this,
				GlobalParameters.SleepTimeBetweenMatchRequests,
				GlobalParameters.MaxSimultaneousMatchRequests);

			PlayerRequester.OnRequestProcessed += PlayerProcessed;

			MatchRequester.OnRequestProcessed += MatchProcessed;
		}

		public void Dispose()
		{
			PlayerRequester.OnRequestProcessed -= PlayerProcessed;

			MatchRequester.OnRequestProcessed -= MatchProcessed;
		}

		internal void Load()
		{
			const string file1 = @"players.xml";
			new ModelLoader<IPopulable<Player>, Player>(this).Load(file1);

			const string file2 = @"matchs.xml";
			new ModelLoader<IPopulable<Match>, Match>(this).Load(file2);
		}

		internal void Save()
		{
			const string file1 = @"players.xml";
			new ModelLoader<IPopulable<Player>, Player>(this).Save(file1);

			const string file2 = @"matchs.xml";
			new ModelLoader<IPopulable<Match>, Match>(this).Save(file2);
		}

		internal void ShowDetails(Match request)
		{
			MessageBox.Show(request.Result, request.Id);
		}

		internal void StartQueries()
		{
			PlayerRequester.RunAsync();

			MatchRequester.RunAsync();
		}

		internal bool AddPlayer(string nick)
		{
			if (AddPlayer(nick, true))
			{
				PlayerField = string.Empty;

				return true;
			}

			return false;
		}

		private bool AddPlayer(string nick, bool startQuery)
		{
			lock (Players)
			{
				if (string.IsNullOrEmpty(nick) || Players.Any(p => p.Id.Equals(nick, StringComparison.InvariantCultureIgnoreCase)))
				{
					return false;
				}

				Players.Add(new Player(nick.ToLowerInvariant()));
			}

			if (startQuery)
			{
				PlayerRequester.RunAsync();
			}

			return true;
		}

		private void PlayerProcessed(object sender, QueryEventArgs<Player> query)
		{
			if (query.Object.MatchsIds.Count == 0)
			{
				RemovePlayer(query.Object);
			}
			else
			{
				AddMatchsIds(query.Object);
			}
		}

		private void RemovePlayer(Player player)
		{
			MessageBox.Show("Player " + player + "'s history is empty");

			lock (Players)
			{
				Players.Remove(player);
			}
		}

		private void AddMatchsIds(Player player)
		{
			lock (Matchs)
			{
				foreach (MatchID matchId in player.MatchsIds)
				{
					if (!Matchs.Any(r => r.Id == matchId.Id))
					{
						Matchs.Add(new Match(matchId.Id));
					}
				}
			}
		}

		private void MatchProcessed(object sender, QueryEventArgs<Match> query)
		{
			QuakeLiveJSon.MatchStatistics stats = query.Object.JSonObject;

			List<Scoreboard> scoreBoards = new List<Scoreboard>();
			scoreBoards.AddRange(stats.BLUESCOREBOARD);
			scoreBoards.AddRange(stats.REDSCOREBOARD);
			scoreBoards.AddRange(stats.BLUESCOREBOARDQUITTERS);
			scoreBoards.AddRange(stats.REDSCOREBOARDQUITTERS);

			foreach (Scoreboard scoreboard in scoreBoards)
			{
				string nick = scoreboard.PLAYERNICK;

				AddPlayer(nick, false);
			}
		}

		internal void StopQueries()
		{
			PlayerRequester.CancelAsync();

			MatchRequester.CancelAsync();
		}

		#region INotifyPropertyChanged Members

		public string PlayerField
		{
			get { return _playerField; }
			set
			{
				_playerField = value;
				OnPropertyChanged("PlayerField");
			}
		}

		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion

		#region IPopulable Members

		ObservableCollection<Player> IPopulable<Player>.GetObjects() { return Players; }

		ObservableCollection<Match> IPopulable<Match>.GetObjects() { return Matchs; }

		#endregion

		public void MergeObjects(IEnumerable<Player> loadedPlayers)
		{
			ObservableCollection<Player> collection = Players;

			foreach (Player loadedObj in loadedPlayers)
			{
				if (loadedObj == null || string.IsNullOrEmpty(loadedObj.Id))
				{
					continue;
				}

				Player first = collection.FirstOrDefault(p => p.Id.Equals(loadedObj.Id, StringComparison.InvariantCultureIgnoreCase));

				if (first != null)
				{
					first.MergeData(loadedObj);
					first.UpdateState();
				}
				else
				{
					collection.Add(loadedObj);
					loadedObj.UpdateState();
				}
			}
		}

		public void MergeObjects(IEnumerable<Match> loadedMatchs)
		{
			ObservableCollection<Match> collection = Matchs;

			foreach (Match loadedObj in loadedMatchs)
			{
				if (loadedObj == null || string.IsNullOrEmpty(loadedObj.Id))
				{
					continue;
				}

				Match first = collection.FirstOrDefault(p => p.Id.Equals(loadedObj.Id, StringComparison.InvariantCultureIgnoreCase));

				if (first != null)
				{
					first.MergeData(loadedObj);
					first.UpdateState();
				}
				else
				{
					collection.Add(loadedObj);
					loadedObj.UpdateState();
				}
			}
		}
	}
}
