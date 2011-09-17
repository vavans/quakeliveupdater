#region

using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;

#endregion

namespace QuakeLiveAnalyzer.Model
{
	public abstract class ASyncRequester<T> : SyncRequester, INotifyPropertyChanged where T : QueryableObject
	{
		/// <summary>
		/// Method to update the container after retrieving the result
		/// </summary>
		/// <param name="queriedObject">The object which served the query</param>
		/// <param name="response">stream response</param>
		/// <returns></returns>
		protected abstract void UpdateContainerAfterQuery(T queriedObject, string response);

		public EventHandler OnComplete;
		public EventHandler<QueryEventArgs<T>> OnRequestProcessed;
		public event PropertyChangedEventHandler PropertyChanged;

		protected IPopulable<T> PopulableContainer;

		private BackgroundWorker _mainLoopWorker;

		private int _delayBetweenRequests;
		private int _maxSimultaneous;
		private State _state;

		public ASyncRequester(IPopulable<T> populableContainer, int delayBetweenRequests, int maxSimultaneous)
		{
			State = Model.State.Waiting;

			PopulableContainer = populableContainer;

			_delayBetweenRequests = delayBetweenRequests;
			_maxSimultaneous = maxSimultaneous;
		}

		public void RunAsync()
		{
			if (State == Model.State.Processing)
			{
				return;
			}

			State = Model.State.Processing;

			_mainLoopWorker = new BackgroundWorker();
			_mainLoopWorker.WorkerSupportsCancellation = true;
			_mainLoopWorker.DoWork += MainLoop;
			_mainLoopWorker.RunWorkerCompleted += MainLoopDone;
			_mainLoopWorker.RunWorkerAsync();
		}

		internal void CancelAsync()
		{
			if (_mainLoopWorker != null && !_mainLoopWorker.CancellationPending)
			{
				_mainLoopWorker.CancelAsync();
			}
		}

		private void MainLoop(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;

			while (PopulableContainer.GetObjects().Any(r => r.State == State.Waiting) && !worker.CancellationPending)
			{
				if (PopulableContainer.GetObjects().Count(r => r.State == State.Processing) < _maxSimultaneous)
				{
					T player = PopulableContainer.GetObjects().FirstOrDefault(r => r.State == State.Waiting);

					if (player != null)
					{
						ProcessRequest(player);
					}
				}

				Thread.Sleep(Math.Max(_delayBetweenRequests, 10));
			}
		}

		private void MainLoopDone(object sender, RunWorkerCompletedEventArgs e)
		{
			_mainLoopWorker = null;

			State = Model.State.Done;

			if (OnComplete != null)
			{
				OnComplete(this, EventArgs.Empty);
			}
		}

		private void ProcessRequest(T queryObject)
		{
			queryObject.State = State.Processing;

			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += SendRequest;
			worker.RunWorkerCompleted += RequestProcessed;
			worker.RunWorkerAsync(queryObject);
		}

		private void SendRequest(object sender, DoWorkEventArgs e)
		{
			T request = (T)e.Argument;

			string queryResult = Query(request.Url);

			e.Result = new[] { request.Id, queryResult };
		}

		private void RequestProcessed(object sender, RunWorkerCompletedEventArgs e)
		{
			string[] array = (string[])e.Result;

			Application.Current.Dispatcher.BeginInvoke((Action)(() => { RequestProcessedForeground(array[0], array[1]); }));
		}

		private void RequestProcessedForeground(string id, string response)
		{
			T obj = PopulableContainer.GetObjects().FirstOrDefault(o => o.Id == id);

			UpdateContainerAfterQuery(obj, response);

			obj.State = Model.State.Done;

			if (OnRequestProcessed != null)
			{
				OnRequestProcessed(this, new QueryEventArgs<T>(obj));
			}
		}

		#region INotifyPropertyChanged Members

		public State State
		{
			get { return _state; }
			set
			{
				_state = value;
				OnPropertyChanged("State");
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

	}
}
