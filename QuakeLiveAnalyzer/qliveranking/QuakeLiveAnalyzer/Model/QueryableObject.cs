using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace QuakeLiveAnalyzer.Model
{
	public abstract class QueryableObject : INotifyPropertyChanged
	{
		private State _state;
		private string _url;

		[XmlIgnore]
		public State State
		{
			get { return _state; }
			set
			{ 
				_state = value;
				OnPropertyChanged("State");
			}
		}

		[XmlAttribute("Id")]
		public string Id { get; set; }

		[XmlIgnore]
		public string Url
		{
			get { return _url ?? (_url = GetUrlFromId(Id)); }
			set
			{
				_url = value;
			}
		}

		public abstract string GetUrlFromId(string id);

		public abstract void MergeData(QueryableObject obj);

		public abstract void UpdateState();

		public QueryableObject()
		{
			State = Model.State.Waiting;
		}

		public QueryableObject(string id) : this()
		{
			Id = id;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion
	}
}
