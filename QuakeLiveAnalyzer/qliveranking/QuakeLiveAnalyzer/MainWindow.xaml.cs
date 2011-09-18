using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuakeLiveAnalyzer.Model;
using QuakeLiveAnalyzer.Ranking;
using QuakeLiveAnalyzer.QuakeLiveJSon;
using System.Globalization;

namespace QuakeLiveAnalyzer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public RequestViewModel Model { get; set; }

		public MainWindow()
		{
			DataContext = Model = new RequestViewModel();

			InitializeComponent();

			Model.Load();
		}

		protected override void OnClosed(EventArgs e)
		{
			if (Model != null)
			{
				Model.Save();

				Model.Dispose();

				Model = null;
			}

			base.OnClosed(e);
		}

		private void ShowDetails(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button != null)
			{
				Match request = button.DataContext as Match;
				if (request != null)
				{
					Model.ShowDetails(request);
				}
			}
		}

		private void QueryGames(object sender, RoutedEventArgs e)
		{
			Model.StartQueries();
		}

		private void Stop(object sender, RoutedEventArgs e)
		{
			Model.StopQueries();
		}

		private void TextBoxKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.Return)
			{
				Model.AddPlayer(Model.PlayerField);
			}
		}

		private void AddPlayerButtonClick(object sender, RoutedEventArgs e)
		{
			Model.AddPlayer(Model.PlayerField);
		}

        private void ComputePlayersRanking(object sender, RoutedEventArgs e)
        {
            SortedList<DateTime, Match> orderedMatchs = new SortedList<DateTime, Match>();
            
            foreach (Match m in Model.Matchs)
            {
                if (m.LastQueryTimestamp > 0)
                {
                    DateTime matchDate = DateTime.Parse(m.JSonObject.GAMETIMESTAMP, new CultureInfo("en-US", false));
                    matchDate = ShiftDateIfSameDate(orderedMatchs, matchDate);
                    orderedMatchs.Add(matchDate, m);
                }
            }

            Dictionary<string, PlayerRank> pr = new Dictionary<string,PlayerRank>();

            foreach(var match in orderedMatchs.Values) 
            {
                foreach (var pBlueResult in match.JSonObject.BLUESCOREBOARD)
                {
                    PlayerRank pBlue;
                    if (!pr.TryGetValue(pBlueResult.PLAYERNICK, out pBlue))
                    {
                        pBlue = new PlayerRank(pBlueResult.PLAYERNICK);
                        pr.Add(pBlueResult.PLAYERNICK, pBlue);
                    } 
                    
                    foreach (var pRedResult in match.JSonObject.REDSCOREBOARD) 
                    {                       
                        PlayerRank pRed;
                        if (!pr.TryGetValue(pRedResult.PLAYERNICK, out pRed)) 
                        {
                            pRed = new PlayerRank(pRedResult.PLAYERNICK);
                            pr.Add(pRedResult.PLAYERNICK, pRed);
                        } 

                        Fight(pBlue, pBlueResult, pRed, pRedResult);
                    } 
                }
            }

            var ordered = pr.OrderBy(kv => -kv.Value.Elo.Rating);

        }

        private DateTime ShiftDateIfSameDate(SortedList<DateTime, Match> orderedMatchs, DateTime matchDate)
        {
            if (orderedMatchs.ContainsKey(matchDate)) 
            {                
                return ShiftDateIfSameDate(orderedMatchs, matchDate.AddSeconds(1));
            }
            return matchDate;
        }

        private void Fight(PlayerRank p1, Scoreboard player1Result, PlayerRank p2, Scoreboard player2Result)
        {
            var time1 = Convert.ToDouble(player1Result.PLAYTIME);
            var time2 = Convert.ToDouble(player2Result.PLAYTIME);

            if (time1 < 300 || time2 < 300)
                return;

            double p1Dmg = Convert.ToDouble(player1Result.DAMAGEDEALT) / time1 * Convert.ToDouble(player1Result.ACCURACY);
            double p2Dmg = Convert.ToDouble(player2Result.DAMAGEDEALT) / time2 * Convert.ToDouble(player2Result.ACCURACY);
            if (p1Dmg > p2Dmg)
                p1.Elo.Victory(p2.Elo);
            else if (p1Dmg == p2Dmg)
                p1.Elo.Null(p2.Elo);
            else if (p1Dmg < p2Dmg)
                p1.Elo.Defeat(p2.Elo);
        }

        private class PlayerRank
        {
            public Elo Elo { get; private set; }
            public PlayerRank(string name)
            {
                Elo = new Elo();
            }
        }
	}
}
