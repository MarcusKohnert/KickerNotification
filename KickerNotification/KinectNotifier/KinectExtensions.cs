namespace KinectNotifier
{
	using System;
	using System.Reactive;
	using System.Reactive.Linq;
	using Microsoft.Kinect;

	public static class KinectExtensions
	{
		public static IObservable<EventPattern<AllFramesReadyEventArgs>> GetAllFramesReadyObservable(this KinectSensor kinectSensor)
		{
			if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

			return Observable.FromEventPattern<AllFramesReadyEventArgs>(kinectSensor, "AllFramesReady");
		}

		public static IObservable<EventPattern<SkeletonFrameReadyEventArgs>> GetSkeletonFrameReadyObservable(this KinectSensor kinectSensor)
		{
			if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

			return Observable.FromEventPattern<SkeletonFrameReadyEventArgs>(kinectSensor, "SkeletonFrameReady");
		}
	}
}