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
	}
}
