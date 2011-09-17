using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace QuakeLiveAnalyzer.Model
{
	[XmlRoot("Match")]
	public class MatchID : INotifyPropertyChanged
	{
		private string _displayedId;

		[XmlAttribute("Id")]
		public string Id { get; set; }

		[XmlAttribute("AddedDate")]
		public long AddedDate { get; set; }

		[XmlIgnore]
		public string DisplayedId
		{
			get { return _displayedId ?? (_displayedId = GetShortId()); }
			set
			{
				_displayedId = value;
				OnPropertyChanged("DisplayedId");
			}
		}

		public MatchID() : this(null)
		{ }

		public MatchID(string id)
		{
			Id = id;

			AddedDate = DateTime.UtcNow.Ticks;

			DisplayedId = GetShortId();
		}

		private string GetShortId()
		{
			if (string.IsNullOrEmpty(Id))
			{
				return null;
			}

			string[] parts = Id.Split(new[] { '_' });

			return (parts.Length == 3 ? parts[1] : Id ) + " / " + new DateTime(AddedDate).ToString("yyyy-MM-dd / hh:mm:ss");
		}

		public void MergeData(MatchID matchId)
		{
			if (matchId.AddedDate > AddedDate)
			{
				AddedDate = matchId.AddedDate;
			}

			DisplayedId = GetShortId();
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion
	}
}
