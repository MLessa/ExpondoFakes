using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace DFN.Services
{
	public class TwitterIntegrationService
	{
		private static TwitterIntegrationService _TweeterIntegrationService = null;
		private TwitterIntegrationService()
		{
			Authenticate();
		}

		private void Authenticate()
		{
			if (Auth.Credentials == null || Tweetinvi.User.GetAuthenticatedUser() == null)
			{
				Auth.SetUserCredentials(ConfigurationManager.AppSettings["TwitterAPIKey"],
				ConfigurationManager.AppSettings["TwitterAPIKeySecret"],
				ConfigurationManager.AppSettings["TwitterAccesToken"],
				ConfigurationManager.AppSettings["TwitterAccesTokenSecret"]);
			}
		}

		public static TwitterIntegrationService GetInstance()
		{
			if (_TweeterIntegrationService == null)
			{
				_TweeterIntegrationService = new TwitterIntegrationService();
			}

			return _TweeterIntegrationService;
		}

		public bool PostMediaTweet(MemoryStream mediaStream, string tweetBody)
		{
			Authenticate();
			var media = Upload.UploadBinary(mediaStream.ToArray());

		   var tweet = Tweet.PublishTweet(tweetBody, new PublishTweetOptionalParameters
			{
				Medias = new List<IMedia> { media }
			});
			return tweet != null;
		}

	}
}