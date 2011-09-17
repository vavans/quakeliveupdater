#region

using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.ComponentModel;
using System;

#endregion

namespace QuakeLiveAnalyzer.Model
{
	public class PlayerRequester : ASyncRequester<Player>
	{
		public PlayerRequester(IPopulable<Player> playersContainer, int delay, int maxThreads)
			: base(playersContainer, delay, maxThreads)
		{ }

		protected override void UpdateContainerAfterQuery(Player player, string response)
		{
			player.LastQueryTimestamp = DateTime.UtcNow.Ticks;

			foreach (string id in ParseResponse(response))
			{
				player.AddGame(id);
			}
		}

		private static List<string> ParseResponse(string xml)
		{
			HashSet<string> list = new HashSet<string>();

			Regex regEx = new Regex(@"<div class=\""areaMapC\"" id=\""([^\""]+)\"">");

			MatchCollection matchCollection = regEx.Matches(xml);

			foreach (System.Text.RegularExpressions.Match m in matchCollection)
			{
				list.Add(m.Groups[1].Value);
			}

			return list.ToList();
		}
	}
}
