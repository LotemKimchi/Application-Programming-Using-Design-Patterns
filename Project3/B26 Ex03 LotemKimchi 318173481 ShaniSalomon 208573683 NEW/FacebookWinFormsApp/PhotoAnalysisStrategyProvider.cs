using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Strategy Pattern — Strategy Provider
    // Centralizes strategy creation outside FormMain.
    // Maps each analysis name to its concrete IFacebookFeature<Photo> strategy.
    public class PhotoAnalysisStrategyProvider
    {
        private readonly Dictionary<string, IFacebookFeature<Photo>> r_Strategies;

        public PhotoAnalysisStrategyProvider(IFacebookService i_Service)
        {
            r_Strategies = new Dictionary<string, IFacebookFeature<Photo>>
            {
                { "Most Liked Photo",     new MostLikedPhotoFeature(i_Service) },
                { "Most Commented Photo", new MostCommentedPhotoFeature(i_Service) },
                { "Oldest Photo",         new OldestPhotoFeature(i_Service) },
                { "Newest Photo",         new NewestPhotoFeature(i_Service) },
                { "Most Tagged Photo",    new MostTaggedPhotoFeature(i_Service) }
            };
        }

        public IFacebookFeature<Photo> GetStrategy(string i_StrategyName)
        {
            IFacebookFeature<Photo> strategy;
            r_Strategies.TryGetValue(i_StrategyName, out strategy);

            return strategy;
        }
    }
}
