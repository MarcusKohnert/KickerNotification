namespace KinectNotifierClient
{
	using System;
	using System.Net;
	using System.Reactive.Disposables;
	using System.Reactive.Linq;
	using System.Threading;
	using KinectNotifier;
	using SignalR.Client;
	using SignalR.Client.Hubs;

	class Program
	{
		private static PersonNotification personNotification;
		private static IHubProxy hub;

		static void Main(string[] args)
		{
			personNotification = new PersonNotification();

			var connection = new HubConnection("http://localhost:???/");

			hub = connection.CreateProxy("KickerNotifyHub");
			IDisposable setPlayerCountSubscription = Disposable.Empty;
			IDisposable setStatusSubscription = Disposable.Empty;

			connection.Start().ContinueWith(task =>
			{
				if (task.IsFaulted)
				{
					Console.WriteLine("Failed to start: {0}", task.Exception.GetBaseException());
				}
				else
				{
					Console.WriteLine("Success! Connected with client connection id {0}", connection.ConnectionId);

					hub.Invoke("UpdateStatus", "connected...");

					setPlayerCountSubscription = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(l => hub.Invoke("SetPlayerCount", personNotification.PersonCount));

					setStatusSubscription = Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(l => hub.Invoke("UpdateStatus", l.ToString()));
				}
			});			

			Console.ReadLine();
			Console.WriteLine("disconnecting...");

			setPlayerCountSubscription.Dispose();
			setStatusSubscription.Dispose();

			hub.Invoke("UpdateStatus", "disconnected").Wait();
		}
	}
}