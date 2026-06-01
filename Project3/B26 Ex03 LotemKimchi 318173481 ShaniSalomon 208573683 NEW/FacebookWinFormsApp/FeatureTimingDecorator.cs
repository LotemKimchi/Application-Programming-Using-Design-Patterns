using FacebookWrapper.ObjectModel;
using System;
using System.Diagnostics;

namespace BasicFacebookFeatures
{
    // Decorator Pattern — Decorator
    // Wraps any IFacebookFeature<T> and measures how long Execute() takes,
    // without modifying the wrapped feature at all.
    public class FeatureTimingDecorator<T> : IFacebookFeature<T>
    {
        private readonly IFacebookFeature<T> r_WrappedFeature;

        public TimeSpan LastElapsed { get; private set; }

        public FeatureTimingDecorator(IFacebookFeature<T> i_Feature)
        {
            r_WrappedFeature = i_Feature;
        }

        public T Execute(User i_User)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            T result = r_WrappedFeature.Execute(i_User);
            stopwatch.Stop();
            LastElapsed = stopwatch.Elapsed;
            return result;
        }
    }
}
