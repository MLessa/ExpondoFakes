using DFN.Models;
using DFN.Services;
using System;
using System.IO;
using System.Web.Mvc;
using Tweetinvi.Core.Extensions;

namespace DFN_api.Controllers
{
	public class DenounceController : Controller
	{
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateDenounce(Denounce denounce)
		{
			var memoryStream = new MemoryStream();
			denounce.LogoFile.InputStream.CopyTo(memoryStream);

			string tweetText;
			if (!denounce.FakeNewsURL.IsNullOrEmpty() &&
				Uri.IsWellFormedUriString(denounce.FakeNewsURL, UriKind.Absolute))
			{
				if (denounce.TweetText.IndexOf("@slpng_giants_pt") != -1)
					tweetText = denounce.TweetText.Replace("@slpng_giants_pt", "") + denounce.FakeNewsURL.Trim() + " @slpng_giants_pt";
				else
					tweetText = denounce.TweetText + denounce.FakeNewsURL.Trim();
			}
			else
				tweetText = denounce.TweetText ?? "";

			var success = TwitterIntegrationService.GetInstance().PostMediaTweet(memoryStream, tweetText);
			if (!success)
				success = TwitterIntegrationService.GetInstance().PostMediaTweet(memoryStream, denounce.TweetText);
			if (success)
				new DatabaseService().IncrementDenounce();
			TempData["posted"] = success ? "success" : "fail";
			return RedirectToAction("Index", "Home");
		}
	}
}