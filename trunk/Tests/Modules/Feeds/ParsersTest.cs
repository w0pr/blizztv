using BlizzTV.Feeds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests.Modules.Feeds
{       
    [TestClass()]
    public class ParsersTest
    {
        /// <summary>
        ///Feed parsing tests
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            bool result = false;
            List<FeedItem> items = null;

            result = FeedParser.Instance.Parse("http://www.blizztv.com/rss/ccs/1-blizztvcom/", ref items); /* RSS 2.0 */
            Assert.IsTrue(result && items.Count > 0, "Failed parsing a RSS 2.0 feed");

            result = FeedParser.Instance.Parse("http://feeds.feedburner.com/blizztv?format=xml", ref items); /* feedburner.com */
            Assert.IsTrue(result && items.Count > 0, "Failed parsing a feedburner.com feed");

            result = FeedParser.Instance.Parse("http://blogsearch.google.com/blogsearch_feeds?hl=en&q=blizztv&ie=utf-8&num=10&output=atom", ref items); /* atom */
            Assert.IsTrue(result && items.Count > 0, "Failed parsing an atom feed");

            result = FeedParser.Instance.Parse("http://us.battle.net/wow/en/feed/news", ref items); /* atom */
            Assert.IsTrue(result && items.Count > 0, "Failed parsing worldofwarcraft.com atom feed");

            result = FeedParser.Instance.Parse("http://us.battle.net/sc2/en/feed/news", ref items); /* atom */
            Assert.IsTrue(result && items.Count > 0, "Failed parsing starcraft2.com atom feed");
        }
    }
}
