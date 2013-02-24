namespace KinectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reactive.Linq;
	using Microsoft.Kinect;

	public class PersonNotification : INotifyPropertyChanged, IDisposable
	{
		private readonly KinectSensor kinect;
		private readonly IDisposable newSkeletonDataEvent;

		public PersonNotification()
		{
			this.kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected);
			if (this.kinect == null) throw new InvalidOperationException("No Kinect connected.");

			this.newSkeletonDataEvent = this.kinect.GetSkeletonFrameReadyObservable()
												.Select(e => e.EventArgs)
												.Subscribe(NewSkeletonData);

			this.kinect.SkeletonStream.Enable();
			this.kinect.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);

			this.kinect.Start();
		}

		private void NewSkeletonData(SkeletonFrameReadyEventArgs skeletonDataFrame)
		{
			using (var frame = skeletonDataFrame.OpenSkeletonFrame())
			{
				if (frame == null) return;

				var skeletons = new Skeleton[frame.SkeletonArrayLength];
				frame.CopySkeletonDataTo(skeletons);

				var persons = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.PositionOnly || s.TrackingState == SkeletonTrackingState.Tracked);

				var personCount = persons.Count();
				if (this.PersonCount != personCount)
				{
					this.PersonCount = personCount;
					PropertyChanged(this, new PropertyChangedEventArgs("PersonNotification"));
				}
			}
		}

		public Int32 PersonCount
		{
			get;
			private set;
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void Dispose()
		{
			this.newSkeletonDataEvent.Dispose();
		}
	}
}