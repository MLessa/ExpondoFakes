using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFN.Models
{
	public class Denounce
	{
		public HttpPostedFileBase LogoFile { get; set; }
		public string TweetText { get; set; }
		public string FakeNewsURL { get; set; }
	}
}