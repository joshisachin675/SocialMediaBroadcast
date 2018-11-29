using Core.Domain;
using mshtml;
using Newtonsoft.Json;
using ServiceLayer.Services;
using smartData.Common;
using smartData.Filter;
using smartData.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace smartData.Areas.Admin.Controllers
{

    [XmlRoot(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
    public class Title
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
    public class Author
    {
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
    }

    [XmlRoot(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
    public class Content
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class Entry
    {
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public Title Title { get; set; }
        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public Link Link { get; set; }
        [XmlElement(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
        public Content Content { get; set; }
    }

    [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public Title Title { get; set; }
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }
        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }
        [XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
        public Author Author { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Link { get; set; }
        [XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Entry> Entry { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    class RssClass
    {
        public int ContentId { get; set; }
        public string TextMessage { get; set; }
        public string TextDescription { get; set; }
        public List<string> ImageArray { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubIndustryName { get; set; }
        public string SocialMedia { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Heading { get; set; }
        public string OriginalTitle { get; set; }
        public int feedID { get; set; } 
        //public string SubIndustryName { get; set; }

        //  public string CategoryName { get; set; }

    }

    public class ManageRssFeedsController : Controller
    {
        public enum FeedType : int
        {
            None = 0,
            RSS = 1,
            Atom = 2
        }

        #region Global Variables

        ServiceLayer.Interfaces.IManageRssFeedService _manageRssFeedService;
        IUsersAPIController _usersAPIController;
        ServiceLayer.Interfaces.IManageContentService _manageContentService;
        IManageRssFeedAPIController _manageRssFeedAPIController;
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        #endregion

        #region constructor

        public ManageRssFeedsController(ManageRssFeedService manageFeedService, IManageRssFeedAPIController manageFeedAPIController, ManageContentService manageContentService, IUsersAPIController usersAPIController)
        {
            _manageContentService = manageContentService;
            _manageRssFeedService = manageFeedService;
            _manageRssFeedAPIController = manageFeedAPIController;
            _usersAPIController = usersAPIController;
        }

        #endregion



        //
        // GET: /Admin/ManageRssFeeds/
        public ActionResult Index()
        {
            ViewBag.UserId = SessionManager.LoggedInUser.UserID;
            return View();
        }


        [HttpPost]
        [AuditLog(Event = "Check rss feed", Message = "Check rss feed for existence")]
        public ActionResult CheckRssFeedUrl(string FeedUrl)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                var xmlData = client.DownloadString(FeedUrl);
                return Json(new { Status = true, Message = "Valid URL" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false, Message = "Invalid URL!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public string GetRssFeedUrl(string FeedUrl)
        {
            var feed = _manageRssFeedService.GetFeedByUrl(FeedUrl);
            if (feed != null)
            {
                return "exists";
            }
            else
            {
                return "notexists";
            }
        }

        [HttpGet]
        public string GetRssFeedName(string FeedName)
        {
            var feed = _manageRssFeedService.GetFeedByName(FeedName);
            if (feed != null)
            {
                return "exists";
            }
            else
            {
                return "notexists";
            }
        }

        [AuditLog(Event = "Rss Read", Message = "Read rss articles")]
        public ActionResult RssRead(string FeedUrl)
        {
            if (SessionManager.LoggedInUser.UserType == 3)
            {
                List<smIndustry> newlist = _manageContentService.GetCategories();
                newlist.Select(x => x.IndustryId == smartData.Common.SessionManager.LoggedInUser.IndustryId);
                ViewBag.Industry = new SelectList(newlist, "IndustryId", "IndustryName");
            }
            else
            {
                smIndustry newlist = _manageContentService.GetIndustryById(smartData.Common.SessionManager.LoggedInUser.IndustryId);
                ViewBag.IndustryId = newlist.IndustryId.ToString();
                ViewBag.IndustryName = newlist.IndustryName.ToString();
            }



            int userId = SessionManager.LoggedInUser.UserID;
            int id = Convert.ToInt32(FeedUrl);
            smRssFeeds feeds = _manageRssFeedService.GetFeedById(id);
            FeedModel rss = new FeedModel();
            try
            {


                string url = feeds.FeedUrl; ;
                XmlReader r = XmlReader.Create(url);
                SyndicationFeed albums = SyndicationFeed.Load(r);
                r.Close();

                rss.Item = new List<FeedModelItem>();
                rss.Title = albums.Title.Text;
                rss.Link = albums.Links[0].Uri.ToString();

                //// new  albums.Description.Text
                if (albums.Description == null)
                {
                    rss.Description = albums.Title.Text;
                    var response = new WebClient().DownloadString(url);
                    XmlSerializer serializer = new XmlSerializer(typeof(Feed));
                    using (TextReader reader = new StringReader(response))
                    {
                        Feed RSSFeedDatas = (Feed)serializer.Deserialize(reader);
                        var FinalData = RSSFeedDatas.Entry;
                        var fullList = (from d in FinalData.Select(x => x)
                                        select new FeedModelItem
                                      {
                                          Title = d.Title.Text,
                                          Link = d.Link.Href,
                                          Description = d.Content.Text,
                                          PubDate = d.Updated,
                                          Image = Regex.Match(d.Content.Text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value
                                      }).ToList();
                        List<smRssArtical> IgnoredList = _manageRssFeedService.getIgnoreList(userId);

                        List<smContentLibrary> addesList = _manageRssFeedService.getAddedData(userId);

                        var rejectedDescription = IgnoredList.Select(ig => ig.Description);

                        var rejectList = fullList.Where(i => rejectedDescription.Contains(i.Title));

                        var addedDesList = addesList.Select(x => x.OriginalTitle);
                        // var addeddList = rejectList.Where(i => addedDesList.Contains(i.Title.Text));
                        Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
                        rss.Item = (from fl in fullList.Except(rejectList)
                                    select new FeedModelItem
                                    {
                                        Title = fl.Title,
                                        Link = fl.Link.ToString(),
                                        PubDate = fl.PubDate.ToString(),
                                        //Description = fl.Summary.Text,
                                        Description = regex.Replace(fl.Description, " ").Trim(),
                                        //  isAdded = addedDesList.Any(s => fl.Title.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0),
                                        isAdded = addedDesList.Contains(fl.Title),
                                        Image = Regex.Match(fl.Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value
                                    }).ToList();

                        rss.Item = rss.Item.Where(x => x.isAdded == false).ToList();     //// Remove Added Articals


                    }





                }
                else
                {
                    rss.Description = albums.Description.Text;
                    List<smRssArtical> IgnoredList = _manageRssFeedService.getIgnoreList(userId);

                    List<smContentLibrary> addesList = _manageRssFeedService.getAddedData(userId);

                    var rejectedDescription = IgnoredList.Select(ig => ig.Description);
                    var fullList = albums.Items.ToList();

                    var rejectList = fullList.Where(i => rejectedDescription.Contains(i.Title.Text));

                    var addedDesList = addesList.Select(x => x.OriginalTitle);
                    // var addeddList = rejectList.Where(i => addedDesList.Contains(i.Title.Text));
                    Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
                    //Regex rg = new Regex(@"<img.*?src=""(.*?)""", RegexOptions.IgnoreCase);
                    //string matchString = Regex.Match(original_text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    rss.Item = (from fl in fullList.Except(rejectList)
                                select new FeedModelItem
                                {
                                    Title = fl.Title.Text,
                                    Link = fl.Links.Count == 0 ? "" : fl.Links.Select(p => p.Uri).FirstOrDefault().ToString(),
                                    PubDate = String.IsNullOrEmpty(fl.PublishDate.ToString()) ? "" : fl.PublishDate.ToString(),
                                    ////Description = fl.Summary.Text,
                                    Description = fl.Summary == null ? "" : fl.Summary.Text.Contains("<p>")? Regex.Match(fl.Summary.Text, @"<p>\s*(.+?)\s*</p>", RegexOptions.IgnoreCase).Groups[1].Value: regex.Replace(fl.Summary.Text, " ").Trim(),
                                    isAdded = addedDesList.Contains(fl.Title.Text),
                                    Image =fl.Summary == null ? "" : Regex.Match(fl.Summary.Text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value
                                }).ToList();
                    rss.Item = rss.Item.Where(x => x.isAdded == false).ToList();     //// Remove Added Articals

                }
            }
            //// new  albums.Description.Text  SRohit 

            // rss.PubDate = albums.ImageUrl.ToString();

            catch (Exception ex)
            {


            }
            return View(rss);

        }

        public string ParseRssFile()
        {

            string html;
            WebClient webClient = new WebClient();
            using (Stream stream = webClient.OpenRead(new Uri("https://news.google.ca/news/section?cf=all&pz=1&q=GTARealEstate")))
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            IHTMLDocument2 doc = (IHTMLDocument2)new HTMLDocument();
            doc.write(html);
            foreach (IHTMLElement el in doc.all)
                Console.WriteLine(el.tagName);
            return "";
        }

        [HttpPost]
        [AuditLog(Event = "Add rss feed", Message = "Add rss feed url")]
        public ActionResult AddRssFeed(string FeedName, string FeedUrl, int UserId)
        {
            Core.Domain.Users user = _usersAPIController.GetUserByID(UserId);
            bool status = _manageRssFeedAPIController.AddRssFeed(FeedName, FeedUrl, UserId, user);
            return Json(new { status = status });
        }

        [HttpPost]
        [AuditLog(Event = "Delete rss feed", Message = "Delete rss feed url")]
        public ActionResult DeleteRssFeed(int FeedId)
        {
            bool status = _manageRssFeedAPIController.DeleteRssFeed(FeedId);
            return Json(new { status = status });
        }

        [AuditLog(Event = "Approve", Message = "Approve rss feed")]
        public JsonResult ApproveRssFeed(int FeedId)
        {
            int userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageRssFeedAPIController.ApproveRssFeed(FeedId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Add rss data", Message = "Add rss content to content library")]
        public ActionResult AddRssData(string PostInformation)
        {
            RssClass postedContent = new RssClass();
            postedContent = JsonConvert.DeserializeObject<RssClass>(PostInformation);
            int status = 0;
            string path = string.Empty;
            string industryName = postedContent.CategoryName;
            if (smartData.Common.SessionManager.LoggedInUser.UserType != 3)
            {
                int IndustryId = smartData.Common.SessionManager.LoggedInUser.IndustryId;
                var industry = _manageContentService.GetIndustryById(IndustryId);
                industryName = industry.IndustryName;
            }

            try
            {
                int userId = SessionManager.LoggedInUser.UserID;
                //if (postedContent.ImageArray.Count != 0 && postedContent.ImageArray[0]!="")
                //{
                //    if (postedContent.ImageArray[0].Contains("http"))
                //    {
                //      path =  DownloadImage(postedContent.ImageArray[0], userId);
                //    }
                //    else
                //    {
                //       var newImg = "http:"+postedContent.ImageArray[0];
                //      path =  DownloadImage(newImg, userId);
                //    }                  
                //}             
                //status = _manageRssFeedAPIController.AddRssData(postedContent.TextMessage, postedContent.ImageArray, postedContent.Url, userId, postedContent.CategoryId, postedContent.SocialMedia, postedContent.CategoryName, postedContent.CreatedDate, postedContent.TextDescription,postedContent.SubIndustryName);
                status = _manageRssFeedAPIController.AddRssData(postedContent.TextMessage, postedContent.ImageArray[0], postedContent.Url, userId, postedContent.CategoryId, postedContent.SocialMedia, industryName, postedContent.CreatedDate, postedContent.TextDescription, postedContent.SubIndustryName, postedContent.Heading, postedContent.OriginalTitle , postedContent.feedID);
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Ignore rss content", Message = "Ignore rss articles from content list")]
        public ActionResult IgnoreRssData(string PostInformation)
        {
            RssClass IgnoredRss = new RssClass();
            IgnoredRss = JsonConvert.DeserializeObject<RssClass>(PostInformation);
            int status = 0;
            try
            {
                int userId = SessionManager.LoggedInUser.UserID;
                status = _manageRssFeedAPIController.IgnoreRssData(IgnoredRss.TextMessage, IgnoredRss.Url, userId, IgnoredRss.CreatedDate);
            }
            catch (Exception ex)
            {
                status = 0;

            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }


        public string DownloadImage(string url, int userId)
        {
            string path = string.Empty;
            string name = string.Empty;
            Guid guid = Guid.NewGuid();
            name = guid.ToString();
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);
                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        var originalDirectory = Server.MapPath("~/Images/WallImages/ContentImages/");
                        string pathString = originalDirectory;
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        name = name + ".jpg";
                        path = string.Format("{0}\\{1}", pathString, name);
                        yourImage.Save(path);
                    }
                }
            }
            return name;
        }

    }
}