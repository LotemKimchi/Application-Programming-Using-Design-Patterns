using System;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Decorator Pattern — ConcreteDecorator
    // Wraps any IFacebookFeature<T> and intercepts exceptions thrown by the
    // Facebook API, so the caller receives null instead of a crash.
    public class SafeFeatureDecorator<T> : IFacebookFeature<T>
    {
        private readonly IFacebookFeature<T> r_WrappedFeature;

        public bool HadError { get; private set; }
        public string LastError { get; private set; }

        public SafeFeatureDecorator(IFacebookFeature<T> i_Feature)
        {
            r_WrappedFeature = i_Feature;
        }

        public T Execute(User i_User)
        {
            T result = default(T);

            HadError = false;
            LastError = null;

            try
            {
                result = r_WrappedFeature.Execute(i_User);
            }
            catch (Exception ex)
            {
                HadError = true;
                LastError = ex.Message;
            }

            return result;
        }
    }
}
