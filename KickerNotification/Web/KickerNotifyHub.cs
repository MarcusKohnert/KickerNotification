namespace Web
{
	using System;
	using SignalR.Hubs;

	public class KickerNotifyHub : Hub
	{
		private Int32 playerCount = -1;

		public void SetPlayerCount(Int32 count)
		{
			if (this.playerCount != count)
			{
				this.playerCount = count;
				Clients.setCurrentPlayerCount(this.playerCount);
			}
		}

		public void UpdateStatus(String status)
		{
			Clients.updateStatus(status);
		}
	}
}