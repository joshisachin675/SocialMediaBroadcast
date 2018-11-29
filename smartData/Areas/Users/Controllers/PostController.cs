using Core.Domain;
using CoreEntities.CustomModels;
using Facebook;
using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using smartData.Areas.Users.ApiControllers;
using smartData.Areas.Users.Models.User;
using smartData.Common;
using smartData.Filter;
using smartData.Models;
using smartData.Models.User;
using Sparkle.LinkedInNET;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;
using TweetSharp;

namespace smartData.Areas.Users.Controllers
{
    //[Authorize]
    public class PostController : Controller
    {
        #region Global Variables
        ServiceLayer.Interfaces.IPostService _postService;
        ServiceLayer.Interfaces.IScheduleService _scheduleService;
        IPostAPIController _postAPIController;
        ServiceLayer.Interfaces.IManageContentService _manageContentService;
        ServiceLayer.Interfaces.IHomeService _homeService;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        IUserService _userService;
        #endregion


        #region constructor
        public PostController(IUserService userService, PostService postService, IPostAPIController postAPIController, IScheduleService scheduleService, ServiceLayer.Interfaces.IHomeService homeService, ServiceLayer.Interfaces.IManageContentService manageContentService)
        {
            _postService = postService;
            _postAPIController = postAPIController;
            _scheduleService = scheduleService;
            _homeService = homeService;
            _manageContentService = manageContentService;
            _userService = userService;
        }
        #endregion

        private class GoogleShortenedURLResponse
        {
            public string id { get; set; }
            public string kind { get; set; }
            public string longUrl { get; set; }
        }

        private class GoogleShortenedURLRequest
        {
            public string longUrl { get; set; }
        }


        //
        // GET: /Users/Post/
        [AuditLog]
        public ActionResult Index()
        {

            return View();
        }

        #region upload Image
        [AuditLog(Event = "Upload Image", Message = "Image uploaded for posting")]
        public ActionResult SaveUploadedFile()
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = SessionManager.LoggedInUser.UserID;
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = string.Empty;


            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        // var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));
                        var originalDirectory = Server.MapPath("~/Images/WallImages");

                        //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
                        string pathString = originalDirectory + "/" + userId;

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = path });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        [HttpPost]
        public Image SaveByteArrayAsImage(string img, int userId, string imgName)
        {
            //string fullOutputPath =     
            Image image = null;
            if (img != null)
            {
                img = img.Split(',')[1];
                string base64String = img;
                byte[] bytes = Convert.FromBase64String(base64String);

                // Image image;
                string WorkingDirectory = Server.MapPath("~/Images/WallImages");
                //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
                string fullOutputPath = WorkingDirectory + "/" + userId;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save(fullOutputPath + "\\" + imgName);
                    }
                    image = Image.FromStream(ms);
                }
                //Image imgPhoto = Image.FromFile(fullOutputPath + "\\" + image);

                //image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);
                return image;
            }
            return image;


        }

        [AuditLog(Event = "Upload Image", Message = "Image uploaded for watermark")]
        public ActionResult SaveUploadedFileWatermark(string img, int userId)
        {
            bool isSavedSuccessfully = true;
            // string fName = "";
            string path = string.Empty;
            try
            {

                string WorkingDirectory = Server.MapPath("~/Images/WallImages");
                //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
                string pathString = WorkingDirectory + "/" + userId;
                path = WorkingDirectory + "/" + userId + "/" + img;
                Image imgPhoto = Image.FromFile(pathString + "\\" + img);

                //var fileName1 = Path.GetFileName(file.FileName);

                bool isExists = System.IO.Directory.Exists(path);

                //if (!isExists)
                //    System.IO.Directory.CreateDirectory(pathString);

                //path = string.Format("{0}\\{1}", pathString, file.FileName);
                imgPhoto.Save(pathString + "\\" + "wtr-" + img, ImageFormat.Jpeg);
                // imgPhoto.SaveAs(path);                  


            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = path });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        #endregion

        #region post content and image
        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Post Content", Message = "Post content to social media")]
        public ActionResult PostContent(string PostInformation)
        {
            //bool status = false;
            string Message = string.Empty;
            CustomErrorModel errorModel = new CustomErrorModel();
            // Create a string builder class for adding response from social media
            StringBuilder sb = new StringBuilder();
            try
            {
                MembershipUser mu = Membership.GetUser();
                int userId = 0;
                userId = SessionManager.LoggedInUser.UserID;
                int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
                ImageModel model = new ImageModel();
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                model = JsonConvert.DeserializeObject<ImageModel>(PostInformation);
                var media = "";
                List<int> Idsocial = new List<int>();
                if (model.PostType == 0)
                {
                    foreach (var id in model.Ids)
                    {
                        if (id == 0)
                        {
                            media = "Facebook";
                        }
                        else if (id == 1)
                        {
                            media = "Twitter";
                        }
                        else if (id == 2)
                        {
                            media = "LinkedIn";
                        }
                        var socila = _postAPIController.GetSocilaMediaAccountByName(media);
                        Idsocial.Add(socila.Fid);
                    }
                    model.Ids = Idsocial;
                    model.PostType = 1;
                }
                string ids = Convert.ToString(model.Ids);
                var socialMedia = _postAPIController.GetSocialMediaAccounts(userId, model.Ids);
                foreach (var item in socialMedia)
                {
                    if (item.SocialMedia.ToLower() == "facebook")   // Posts to facebook
                    {
                        errorModel = PostToFacebook(item, model, userId, ImpersonateUserId);
                        if (model.PostType == 1)
                        {
                            sb.AppendLine("<br />");
                            sb.Append(errorModel.Message);
                        }
                        else
                        {
                            errorModel.Message = "Schedule post on facebook has been completed";
                            sb.Append("<br />");
                            sb.Append(errorModel.Message);
                        }

                    }
                    else if (item.SocialMedia.ToLower() == "linkedin")  // Posts to linkedIn
                    {
                        errorModel = PostToLinkedInNetwork(item, model, userId, ImpersonateUserId);
                        if (model.PostType == 1)
                        {
                            sb.AppendLine("<br />");
                            sb.Append(errorModel.Message);
                        }
                        else
                        {
                            errorModel.Message = "Schedule post on linkedIn has been completed";
                            sb.Append("<br />");
                            sb.Append(errorModel.Message);
                        }
                    }
                    else if (item.SocialMedia.ToLower() == "twitter") // Posts to twitter
                    {
                        errorModel = PostToTwitter(item, model, userId, ImpersonateUserId);
                        if (model.PostType == 1)
                        {
                            sb.AppendLine("<br />");
                            sb.Append(errorModel.Message);
                        }
                        else
                        {
                            errorModel.Message = "Schedule post on twitter has been completed";
                            sb.Append("<br />");
                            sb.Append(errorModel.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //status = false;
                errorModel.Status = false;
                errorModel.Message = "Some error occured";
                sb.AppendLine("<br />");
                sb.Append(errorModel.Message);
            }

            return Json(new { ErrorModel = sb.ToString() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Facebook Posts
        [AuditLog(Event = "Facebook Post", Message = "Posting to facebook")]
        private CustomErrorModel PostToFacebook(smSocialMediaProfile data, ImageModel model, int userId, int ImpersonateUserId)
        {
            CustomErrorModel errorModel = new CustomErrorModel();
            List<smFacebookPageDetail> FullPageDetail = new List<smFacebookPageDetail>();
            bool status = false;
            string Message = string.Empty;
            string post = string.Empty;
            smPost smpost = new smPost();

            var PostDetail = _homeService.GetFacebookPereference(userId);
            dynamic parameters = new ExpandoObject();

            string accesstoken = string.Empty;
            accesstoken = data.AccessToken;
            var client = new FacebookClient(accesstoken);
            client.AppId = ConfigurationManager.AppSettings["FBClientID"];
            client.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
            var imagePath = ConfigurationManager.AppSettings["SiteUrl"];



            try
            {
                if (PostDetail.PageId == 0)
                {



                    if (model.ImageArray.Count > 0)
                    {
                        foreach (var images in model.ImageArray)
                        {
                            if (model.PostMethod == "Republish")
                            {
                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = ConfigurationManager.AppSettings["SiteUrl"] + images;
                                }
                               
                            }

                            else
                            {
                                // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
                                // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
                                // imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;

                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = Server.MapPath("~/Images/");
                                    imagePath = imagePath + "WallImages/" + userId + "/" + images;
                                }
                                
                            }
                        }
                        //parameters.source = new FacebookMediaObject
                        //{
                        //    ContentType = "image/jpeg",
                        //    FileName = Path.GetFileName(imagePath)
                        //}.SetValue(System.IO.File.ReadAllBytes(imagePath));
                    }


                    if (model.ImageArray.Count > 0)
                    {

                        // PhotoPostFBResponseModel postModel = new PhotoPostFBResponseModel();
                        //parameters.name = model.TextMessage;  
                        //parameters.name = model.Heading;      
                        //parameters.description = model.Heading; 
                        parameters.message = model.Heading;
                        if (model.PostType == 1)
                        {
                            //post = "Image";
                            post = "Status";
                            smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            //var link1 = string.Format("http://{0}.sm4y.cc/Post/GetResults?postId=", "Imran");
                            //var link1 = string.Format("http://{0}.sm4y.cc/Post/GetResults?p1={1}&p2={2}&p3={3}", "Imran");

                            //var link1 = string.Format("http://{0}.sm4y.cc/post/getresults?postid=", "imran");
                            //link1 = link1 + smpost.PostId;
                            var user = _userService.GetUserById(userId);
                            var link1 = "";
                            if (model.MultipostType==1)
                            {
                                 link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 2)
                            {
                                 link1 = string.Format("http://{0}.sm4y.cc/p/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 3)
                            {
                                 link1 = string.Format("http://{0}.sm4y.cc/d/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else
                            {
                                 link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            //link1 = link1 + smpost.PostId;

                            //var newlink = urlShort(link);
                            //dynamic result = client.Post("me/photos", parameters); //To post in facebook
                            parameters.link = link1;
                            dynamic result = client.Post("me/feed", parameters);
                            if (result != null)
                            {
                                // Save post data to database

                                // Save posts to database and get last inserted row
                                //smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                                // save facebook response to database
                                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                            }
                        }
                        else if (model.PostType == 2)
                        {
                            post = "Image";
                            DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                            SavePostedContent(data, model, userId, parameters, post, model.PostType, time, ImpersonateUserId);
                            //SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime);
                        }
                        else if (model.PostType == 3)
                        {
                            post = "Image";
                            string day = DateTime.Now.DayOfWeek.ToString();
                            int Day = GetDay(day);
                            int day2 = GetDay2(day);
                            List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                            //var publishingtime = timelist.Where(x => x.DayId == Day);
                            var isSaved = false;
                            if (timelist.Count > 0)
                            {
                                for (int Days = Day; Days <= day2; Days++)
                                {
                                    var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                                    foreach (var iten in publishingtimes)
                                    {
                                        int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                        int nedt = iten.DayId;
                                        int remainday = nedt - dt;
                                        if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                        {
                                            //var utcDate = DateTime.SpecifyKind(Convert.toda iten.TimeStampPosted, DateTimeKind.Utc);
                                            //var localTime = utcDate.ToLocalTime();
                                            if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                            //if (localTime.TimeOfDay > DateTime.Now.TimeOfDay)
                                            {
                                                if (isSaved != true)
                                                {
                                                    //DateTime d = DateTime.Today;
                                                    //// lastMonday is always the Monday before nextSunday.
                                                    //// When today is a Sunday, lastMonday will be tomorrow.     
                                                    //int offset = d.DayOfWeek - DayOfWeek.Monday;
                                                    //DateTime lastMonday = d.AddDays(-offset);
                                                    //remainday >= 0 ? remainday : -(remainday)
                                                    DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                    TimeSpan time = iten.TimeStampPosted;
                                                    DateTime result = date + time;
                                                    SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                    isSaved = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isSaved != true)
                                            {
                                                DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                TimeSpan time = iten.TimeStampPosted;
                                                DateTime result = date + time;
                                                SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                isSaved = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //parameters.name = model.Heading;     
                        parameters.message = model.Heading;
                        if (model.PostType == 1)
                        {
                            post = "Status";
                            smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            ////  var link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + smpost.PostId;

                            var user = _userService.GetUserById(userId);
                            var link1 = "";
                            if (model.MultipostType == 1)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 2)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/p/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 3)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/d/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            //  var newlink = urlShort(link);
                            ////parameters.link = newlink;
                            parameters.link = link1;
                            //parameters.link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + smpost.PostId;     
                            dynamic result = client.Post("me/feed", parameters);
                            if (result != null)
                            {
                                // Save posts to database and get last inserted row
                                // smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                                // save facebook response to database
                                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                            }
                        }
                        else if (model.PostType == 2)
                        {
                            post = "Status";
                            DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                            SavePostedContent(data, model, userId, parameters, post, model.PostType, time, ImpersonateUserId);
                            //SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime);
                        }

                        else if (model.PostType == 3)
                        {
                            post = "Status";
                            string day = DateTime.Now.DayOfWeek.ToString();
                            int Day = GetDay(day);
                            int day2 = GetDay2(day);
                            List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                            var isSaved = false;
                            if (timelist.Count > 0)
                            {
                                for (int Days = Day; Days <= day2; Days++)
                                {
                                    var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                                    foreach (var iten in publishingtimes)
                                    {
                                        int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                        int nedt = iten.DayId;
                                        int remainday = nedt - dt;
                                        if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                        {
                                            if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                            //if (iten.TimeStampPosted.TimeOfDay > DateTime.Now.TimeOfDay)
                                            {
                                                if (isSaved != true)
                                                {
                                                    //DateTime d = DateTime.Today;
                                                    //// lastMonday is always the Monday before nextSunday.
                                                    //// When today is a Sunday, lastMonday will be tomorrow.     
                                                    //int offset = d.DayOfWeek - DayOfWeek.Monday;
                                                    //DateTime lastMonday = d.AddDays(-offset);
                                                    //remainday >= 0 ? remainday : -(remainday)
                                                    DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                    TimeSpan time = iten.TimeStampPosted;
                                                    DateTime result = date + time;
                                                    SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                    isSaved = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isSaved != true)
                                            {
                                                DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                TimeSpan time = iten.TimeStampPosted;
                                                DateTime result = date + time;
                                                SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                isSaved = true;
                                                break;
                                            }
                                        }

                                    }
                                }
                                //SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime);
                            }
                        }
                    }

                    status = true;
                    Message = "Your post was added successfully to your Facebook account.";
                }
                else
                {


                    if (model.ImageArray.Count > 0)
                    {
                        foreach (var images in model.ImageArray)
                        {
                            if (model.PostMethod == "Republish")
                            {
                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = ConfigurationManager.AppSettings["SiteUrl"] + images;
                                }
                               
                            }

                            else
                            {
                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = Server.MapPath("~/Images/");
                                    imagePath = imagePath + "WallImages/" + userId + "/" + images;
                                }
                               
                            }
                        }

                    }


                    if (model.ImageArray.Count > 0)
                    {


                        parameters.message = model.Heading;
                        if (model.PostType == 1)
                        {
                            //post = "Image";
                            post = "Status";
                            smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            //var link1 = string.Format("http://{0}.sm4y.cc/Post/GetResults?postId=", "Imran");
                            //var link1 = string.Format("http://{0}.sm4y.cc/Post/GetResults?p1={1}&p2={2}&p3={3}", "Imran");

                            //var link1 = string.Format("http://{0}.sm4y.cc/post/getresults?postid=", "imran");
                            //link1 = link1 + smpost.PostId;
                            var user = _userService.GetUserById(userId);
                            var link1 = "";
                            if (model.MultipostType == 1)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 2)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/p/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 3)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/d/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            //link1 = link1 + smpost.PostId;

                            //var newlink = urlShort(link);
                            //dynamic result = client.Post("me/photos", parameters); //To post in facebook
                            parameters.link = link1;
                            dynamic result = "";


                            List<long> PageList = new List<long>();
                            PageList.Add(PostDetail.PageId);

                            List<List<long>> list = new List<List<long>>();
                            list.Add(PageList);



                            FullPageDetail = _postAPIController.getPageAccesstoken(list);

                            /// SRohit   post on page
                            foreach (var item in FullPageDetail)
                            {
                                var pageclient = new FacebookClient(item.PageAccessToken);
                                pageclient.AppId = ConfigurationManager.AppSettings["FBClientID"];
                                pageclient.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
                                result = pageclient.Post("/" + item.PageId + "/feed", parameters);
                            }


                            if (result != null)
                            {
                                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                            }
                        }
                        else if (model.PostType == 2)
                        {
                            post = "Image";
                            DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                            SavePostedContent(data, model, userId, parameters, post, model.PostType, time, ImpersonateUserId);
                            
                        }
                        else if (model.PostType == 3)
                        {
                            post = "Image";
                            string day = DateTime.Now.DayOfWeek.ToString();
                            int Day = GetDay(day);
                            int day2 = GetDay2(day);
                            List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                          
                            var isSaved = false;
                            if (timelist.Count > 0)
                            {
                                for (int Days = Day; Days <= day2; Days++)
                                {
                                    var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                                    foreach (var iten in publishingtimes)
                                    {
                                        int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                        int nedt = iten.DayId;
                                        int remainday = nedt - dt;
                                        if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                        {
                                            
                                            if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                         
                                            {
                                                if (isSaved != true)
                                                {
                                                   
                                                    DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                    TimeSpan time = iten.TimeStampPosted;
                                                    DateTime result = date + time;
                                                    SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                    isSaved = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isSaved != true)
                                            {
                                                DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                TimeSpan time = iten.TimeStampPosted;
                                                DateTime result = date + time;
                                                SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                isSaved = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //parameters.name = model.Heading;     
                        parameters.message = model.Heading;
                        if (model.PostType == 1)
                        {
                            post = "Status";
                            smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            ////  var link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + smpost.PostId;

                            var user = _userService.GetUserById(userId);
                            var link1 = "";
                            if (model.MultipostType == 1)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 2)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/p/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else if (model.MultipostType == 3)
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/d/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            else
                            {
                                link1 = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, smpost.UniquePostId);
                            }
                            //  var newlink = urlShort(link);
                            ////parameters.link = newlink;
                            parameters.link = link1;
                            //parameters.link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + smpost.PostId;     
                            dynamic result = "";
                            List<long> PageList = new List<long>();
                            PageList.Add(PostDetail.PageId);

                            List<List<long>> list = new List<List<long>>();
                            list.Add(PageList);



                            FullPageDetail = _postAPIController.getPageAccesstoken(list);
                           
                            /// SRohit   post on page
                            foreach (var item in FullPageDetail)
                            {
                                var pageclient = new FacebookClient(item.PageAccessToken);
                                pageclient.AppId = ConfigurationManager.AppSettings["FBClientID"];
                                pageclient.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
                                result = pageclient.Post("/" + item.PageId + "/feed", parameters);
                            }

                            if (result != null)
                            {
                           
                                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                            }
                        }
                        else if (model.PostType == 2)
                        {
                            post = "Status";
                            DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                            SavePostedContent(data, model, userId, parameters, post, model.PostType, time, ImpersonateUserId);
                          
                        }

                        else if (model.PostType == 3)
                        {
                            post = "Status";
                            string day = DateTime.Now.DayOfWeek.ToString();
                            int Day = GetDay(day);
                            int day2 = GetDay2(day);
                            List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                            var isSaved = false;
                            if (timelist.Count > 0)
                            {
                                for (int Days = Day; Days <= day2; Days++)
                                {
                                    var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                                    foreach (var iten in publishingtimes)
                                    {
                                        int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                        int nedt = iten.DayId;
                                        int remainday = nedt - dt;
                                        if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                        {
                                            if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                       
                                            {
                                                if (isSaved != true)
                                                {
                                                    DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                    TimeSpan time = iten.TimeStampPosted;
                                                    DateTime result = date + time;
                                                    SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                    isSaved = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isSaved != true)
                                            {
                                                DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                                TimeSpan time = iten.TimeStampPosted;
                                                DateTime result = date + time;
                                                SavePostedContent(data, model, userId, parameters, post, model.PostType, result, ImpersonateUserId);
                                                isSaved = true;
                                                break;
                                            }
                                        }

                                    }
                                }
                                //SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime);
                            }
                        }
                    }

                    status = true;
                    Message = "Your post was added successfully to your Facebook account.";

              


                    /// SRohit

                    //if (model.selectedPageList.Count != 0 && model.selectedPageList != null)
                    //{

                    //    FullPageDetail = _postAPIController.getPageAccesstoken(model.selectedPageList);
                    //    parameters.message = model.Heading;
                    //    /// SRohit   post on page
                    //    foreach (var item in FullPageDetail)
                    //    {
                    //        var pageclient = new FacebookClient(item.PageAccessToken);
                    //        pageclient.AppId = ConfigurationManager.AppSettings["FBClientID"];
                    //        pageclient.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
                    //        pageclient.Post("/" + item.PageId + "/feed", parameters);
                    //    }

                    //    /// SRohit
                    //}
                }
            }
            catch (Exception ex)
            {
                status = false;
                Message = "Error occured while posting to facebook";

            }
            errorModel.Status = status;
            errorModel.Message = Message;

            return errorModel;
        }


        public static DateTime FromUTCData(DateTime? dt, int timezoneOffset)
        {
            //  Convert a DateTime (which might be null) from UTC timezone
            //  into the user's timezone. 
            //if (dt == null)
            //    return null;

            DateTime newDate = dt.Value + new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
            return newDate;
        }

        private smPost SavePostedContent(smSocialMediaProfile data, ImageModel model, int userId, dynamic param, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId)
        {
            string image = string.Empty;

            if (model.ImageArray.Count > 0)
            {
                for (int i = 0; i < model.ImageArray.Count; i++)
                {
                    image = model.ImageArray[i];
                }
            }

            smPost status = _postAPIController.SavePostDetails(data, image, userId, model.TextMessage, post, postType, scheduleDate, ImpersonateUserId, model.Title, model.Heading, model.Link, model.PostMethod);
            return status;
        }

        private void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId)
        {
            _postAPIController.SaveFacebookPostDetails(postId, userId, type, fbId, fbPostId);
        }

        #endregion

        #region Linkedin Posts
        [AuditLog(Event = "LinkedIn Post", Message = "Posting to linkedin")]
        public CustomErrorModel PostToLinkedInNetwork(smSocialMediaProfile data, ImageModel model, int userId, int ImpersonateUserId)
        {
            CustomErrorModel errorModel = new CustomErrorModel();
            smPost smpost = new smPost();
            string Message = string.Empty;
            string post = string.Empty;
            bool status = false;
            string accessToken = data.AccessToken;
            string imagePath = string.Empty;
            string linkedinSharesEndPoint = "https://api.linkedin.com/v1/people/~/shares?oauth2_access_token={0}";

            if (model.ImageArray.Count > 0)
            {
                foreach (var images in model.ImageArray)
                {
                    if (model.PostMethod == "Republish")
                    {

                        if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                        {
                            imagePath = images;
                        }
                        else
                        {
                            imagePath = ConfigurationManager.AppSettings["SiteUrl"] + images;
                        }
                      
                    }

                    //if (images.Contains("*"))
                    //{
                    //    var img = images.Split('*')[1];
                    //    imagePath = Server.MapPath("~/Images/");
                    //    imagePath = ConfigurationManager.AppSettings["SiteUrl"] + "WallImages/ContentImages/" + img;
                    //}
                    else
                    {

                        if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                        {
                            imagePath = images;
                        }
                        else
                        {
                            imagePath = ConfigurationManager.AppSettings["SiteUrl"] + "/Images/WallImages/" + userId + "/" + images;
                        }
                        
                    }

                    // imagePath = "https://localhost:55038/Images/WallImages/" + userId + "/" + images;
                    // imagePath = "https://108.168.203.227/SocialMediaBroadcast8/Images/WallImages/" + userId + "/" + images;
                    // imagePath = Server.MapPath("~/Images/");
                    //  imagePath = imagePath + "WallImages/" + userId + "/" + images;
                    //imagePath = "http://108.168.203.227//socialmediabroadcast/Images/WallImages/3/christmas.jpg";

                    //imagePath = Server.MapPath("~/Images/");
                    //imagePath = imagePath + "WallImages/" + userId + "/" + images;

                }
                post = "Image";
            }
            else
            {
                post = "Status";
            }

            var requestUrl = String.Format(linkedinSharesEndPoint, accessToken);
            string title = model.Title;
            //if (model.Title.Length > 30)
            //{
            //    title = model.Title.Substring(0, 30);
            //}
            //else
            //{
            //    title = model.Title;
            //}       
            string url = model.Link;

            //string title = "This is the linledin post demo";
            //string url = "https://developer.linkedin.com";

            //var msg = model.TextMessage;
            var msg = model.TextMessage;

            //if (model.Heading.Length > 30)
            //{
            //    msg = model.Heading.Substring(0, 30);
            //}
            //else
            //{
            //    msg = model.Heading;
            //}
            var message = new
            {
                comment = msg,
                content = new Dictionary<string, string>
        { 
            { "title", title },
            { "description", msg },
            { "submitted-url", model.Link  },
            {"submitted-image-url" , imagePath}
            
        },

                //    string title = "This is the linledin post demo";
                //    string url = "https://developer.linkedin.com";
                //    var msg = model.TextMessage;
                //    if ( model.TextMessage.Length > 30)
                //    {
                //        msg = model.TextMessage.Substring(0, 30);
                //    }
                //    else
                //    {
                //        msg = model.TextMessage;
                //    }
                //    #region message content
                //    var message = new
                //    {
                //        comment = msg,
                //        content = new Dictionary<string, string>
                //{ 
                //    { "title", title },
                //    { "description", msg },
                //    { "submitted-url", "https://developer.linkedin.com"  },
                //    {"submitted-image-url" , imagePath}

                //},

                visibility = new
                {
                    code = "anyone"
                }
            };
        #endregion

            var requestJson = new JavaScriptSerializer().Serialize(message);

            var client = new WebClient();
            var requestHeaders = new NameValueCollection
       {
        { "Content-Type", "application/json" },
        { "x-li-format", "json" }
       };
            client.Headers.Add(requestHeaders);
            LinkedInPostResponseModel response = new LinkedInPostResponseModel();
            try
            {
                if (model.PostType == 1)
                {
                    // Get response from the api
                    var responseJson = client.UploadString(requestUrl, "POST", requestJson);
                    // response = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(responseJson);
                    response = JsonConvert.DeserializeObject<LinkedInPostResponseModel>(responseJson);
                    // Save posts to database and get last inserted row
                    smpost = SavePostedContentLinkedIn(data, model, userId, title, url, model.TextMessage, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                    // Save linkedIn response to database
                    SaveLinkedInPostDetails(smpost.PostId, userId, post, response.updateKey, response.updateUrl);
                    errorModel.Message = "Your post was added successfully to your LinkedIn account.";
                    errorModel.Status = true;
                }
                else if (model.PostType == 2)
                {
                    DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                    SavePostedContentLinkedIn(data, model, userId, title, url, model.TextMessage, post, model.PostType, time, ImpersonateUserId);
                    //SavePostedContentLinkedIn(data, model, userId, title, url, model.TextMessage, post, model.PostType, model.ScheduledTime);
                }
                else if (model.PostType == 3)
                {
                    string day = DateTime.Now.DayOfWeek.ToString();
                    int Day = GetDay(day);
                    int day2 = GetDay2(day);
                    List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                    var publishingtime = timelist.Where(x => x.DayId == Day);
                    var isSaved = false;
                    if (timelist.Count > 0)
                    {
                        for (int Days = Day; Days <= day2; Days++)
                        {
                            var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                            foreach (var iten in publishingtimes)
                            {
                                int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                int nedt = iten.DayId;
                                int remainday = nedt - dt;
                                if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                {
                                    if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                    //if (iten.TimeStampPosted.TimeOfDay > DateTime.Now.TimeOfDay)
                                    {
                                        if (isSaved != true)
                                        {
                                            //DateTime d = DateTime.Today;
                                            //// lastMonday is always the Monday before nextSunday.
                                            //// When today is a Sunday, lastMonday will be tomorrow.     
                                            //int offset = d.DayOfWeek - DayOfWeek.Monday;
                                            //DateTime lastMonday = d.AddDays(-offset);
                                            //remainday >= 0 ? remainday : -(remainday)
                                            DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                            TimeSpan time = iten.TimeStampPosted;
                                            DateTime result = date + time;
                                            SavePostedContentLinkedIn(data, model, userId, title, url, model.TextMessage, post, model.PostType, result, ImpersonateUserId);
                                            isSaved = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (isSaved != true)
                                    {
                                        DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                        TimeSpan time = iten.TimeStampPosted;
                                        DateTime result = date + time;
                                        SavePostedContentLinkedIn(data, model, userId, title, url, model.TextMessage, post, model.PostType, result, ImpersonateUserId);
                                        isSaved = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                //if (Message.Contains("400"))
                //{
                //    Message = "You cannot post the same content you posted earlier in linkedIn.";
                //}
                //else
                //{
                //    Message = "Error occured while posting to linkedIn.";
                //}
                status = false;
                errorModel.Message = "Error occured while posting to linkedIn.";
                errorModel.Status = status;

            }

            return errorModel;
        }

        //public void postToLinkedIn(smSocialMediaProfile data, ImageModel model, int userId)
        //{
        //    JToken accessCode = data.AccessToken;
        //    string requestUrl = "https://api.linkedin.com/v1/people/~/shares?oauth2_access_token=" + accessCode;
        //    WebRequest request = WebRequest.Create(requestUrl);
        //    request.Method = "POST";
        //    string imagePath = string.Empty;
        //    string post = string.Empty;
        //    request.ContentType = "application/json";
        //    if (model.ImageArray.Count > 0)
        //    {
        //        foreach (var images in model.ImageArray)
        //        {
        //            imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
        //        }
        //        post = "Image";
        //    }
        //    else
        //    {
        //        post = "Status";
        //    }
        //    using (var stream = new StreamWriter(request.GetRequestStream()))
        //    {
        //        var shareMsg =
        //            new
        //                {
        //                    comment =
        //                        model.TextMessage,
        //                    content =
        //                        new
        //                            {
        //                                title = model.TextMessage,
        //                                submitted_url = string.Empty,
        //                                submitted_image_url = imagePath,                                            
        //                                description = string.Empty
        //                            },
        //                    visibility = new { code = "anyone" }
        //                };

        //        string json = new JavaScriptSerializer().Serialize(shareMsg);
        //        stream.Write(json);
        //        stream.Flush();
        //        stream.Close();
        //    }

        //    WebResponse webResponse = request.GetResponse();
        //    Stream dataStream = webResponse.GetResponseStream();
        //    var reader = new StreamReader(dataStream);
        //    string response = reader.ReadToEnd();
        //    // return response;

        //}

        private smPost SavePostedContentLinkedIn(smSocialMediaProfile data, ImageModel model, int userId, string title, string url, string message, string post, int? postType, DateTime? scheduledTime, int ImpersonateUserId)
        {
            string image = string.Empty;

            if (model.ImageArray.Count > 0)
            {
                for (int i = 0; i < model.ImageArray.Count; i++)
                {
                    image = model.ImageArray[i];
                }
            }
            return _postAPIController.SaveLinkedInPost(data, image, userId, title, url, message, post, postType, scheduledTime, ImpersonateUserId, model.Heading, model.PostMethod);
        }

        private void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url)
        {
            _postAPIController.SaveLinkedInPostDetails(postId, userId, type, key, url);
        }


        #region Twitter Posts
        [AuditLog(Event = "Twitter Post", Message = "Posting to twitter")]
        public CustomErrorModel PostToTwitter(smSocialMediaProfile data, ImageModel model, int userId, int ImpersonateUserId)
        {
            smPost smpost = new smPost();
            CustomErrorModel errorModel = new CustomErrorModel();
            try
            {
                string post = string.Empty;
                string imagePath = string.Empty;
                var consumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
                var consumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
                var service = new TweetSharp.TwitterService(consumerKey, consumerSecret, data.AccessToken, data.TokenSecret);
                // send message
                string message = model.TextMessage;
                if (model.PostType == 1)
                {
                    var twitterStatus = (dynamic)null;
                    // Get Image
                    if (model.ImageArray.Count > 0)
                    {
                        foreach (var images in model.ImageArray)
                        {
                            if (model.PostMethod == "Republish")
                            {

                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = ConfigurationManager.AppSettings["SiteUrl"] + "/Images/WallImages/" + userId + "/" + images;
                                }
                            }

                            //if (images.Contains("*"))
                            //{
                            //    var img = images.Split('*')[1];
                            //    imagePath = Server.MapPath("~/Images/");
                            //    imagePath = imagePath + "WallImages/ContentImages/" + img;
                            //}

                            else
                            {


                                if (Uri.IsWellFormedUriString(images, UriKind.RelativeOrAbsolute))
                                {
                                    imagePath = images;
                                }
                                else
                                {
                                    imagePath = Server.MapPath("~/Images/");
                                    imagePath = imagePath + "WallImages/" + userId + "/" + images;
                                }
                               
                            }

                            // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
                            // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
                            //imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;

                        }

                        post = "Image";
                        // Read Image
                        var stream = new FileStream(imagePath, FileMode.Open);
                        twitterStatus = service.SendTweetWithMedia(new SendTweetWithMediaOptions()
                        {
                            Status = message,
                            Images = new Dictionary<string, Stream> { { "image", stream } }
                        });
                    }
                    else
                    {
                        post = "Status";
                        twitterStatus = service.SendTweet(new SendTweetOptions() { Status = message });
                    }

                    if (twitterStatus != null)
                    {
                        // Save posts to database and get last inserted row
                        smpost = SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                        // Save linkedIn response to database
                        SaveTwitterPostDetails(smpost.PostId, userId, post, twitterStatus.IdStr);
                        errorModel.Message = "Your post was added successfully to your Twitter account.";
                        errorModel.Status = true;
                    }
                    else
                    {
                        errorModel.Message = service.Response.Error == null ? "Error occured while posting to twitter" : service.Response.Error.Message;
                    }
                }
                else if (model.PostType == 2) // For scheduled post
                {
                    DateTime time = FromUTCData(model.ScheduledTime, model.timeoffset);
                    SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, time, ImpersonateUserId);
                    //SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, model.ScheduledTime);
                }

                else if (model.PostType == 3)
                {
                    string day = DateTime.Now.DayOfWeek.ToString();
                    int Day = GetDay(day);
                    int day2 = GetDay2(day);
                    List<smPublishingTime> timelist = _scheduleService.GetAllPublishingTime(userId);
                    var publishingtime = timelist.Where(x => x.DayId == Day);
                    var isSaved = false;
                    if (timelist.Count > 0)
                    {
                        for (int Days = Day; Days <= day2; Days++)
                        {
                            var publishingtimes = timelist.Where(x => x.DayId == Days).OrderBy(x => x.TimeStampPosted);
                            foreach (var iten in publishingtimes)
                            {
                                int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                                int nedt = iten.DayId;
                                int remainday = nedt - dt;
                                if (DateTime.UtcNow.Date == DateTime.UtcNow.AddDays(Math.Abs(remainday)).Date)
                                {
                                    //if (iten.TimeStampPosted.TimeOfDay > DateTime.Now.TimeOfDay)
                                    if (iten.TimeStampPosted > DateTime.UtcNow.TimeOfDay)
                                    {
                                        if (isSaved != true)
                                        {
                                            //DateTime d = DateTime.Today;
                                            //// lastMonday is always the Monday before nextSunday.
                                            //// When today is a Sunday, lastMonday will be tomorrow.     
                                            //int offset = d.DayOfWeek - DayOfWeek.Monday;
                                            //DateTime lastMonday = d.AddDays(-offset);
                                            //remainday >= 0 ? remainday : -(remainday)
                                            DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                            TimeSpan time = iten.TimeStampPosted;
                                            DateTime result = date + time;
                                            SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, result, ImpersonateUserId);
                                            isSaved = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (isSaved != true)
                                    {
                                        DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                                        TimeSpan time = iten.TimeStampPosted;
                                        DateTime result = date + time;
                                        SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, result, ImpersonateUserId);
                                        isSaved = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorModel.Message = "Error occured while posting to twitter";
                errorModel.Status = false;
            }
            return errorModel;
        }

        //private smPost SavePostedContentTwitter(smSocialMediaProfile data, ImageModel model, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId)
        //{
        //    string image = string.Empty;

        //    if (model.ImageArray.Count > 0)
        //    {
        //        for (int i = 0; i < model.ImageArray.Count; i++)
        //        {
        //            image = model.ImageArray[i];
        //        }
        //    }
        //    return _postAPIController.SaveTwitterPost(data, image, userId, message, post, postType, scheduleDate, ImpersonateUserId,model.PostMethod);
        //}


        private smPost SavePostedContentTwitter(smSocialMediaProfile data, ImageModel model, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId)
        {
            string image = string.Empty;

            if (model.ImageArray.Count > 0)
            {
                for (int i = 0; i < model.ImageArray.Count; i++)
                {
                    image = model.ImageArray[i];
                }
            }
            return _postAPIController.SaveTwitterPost(data, image, model.Link, model.Title, userId, message, post, postType, scheduleDate, ImpersonateUserId, model.PostMethod);
        }

        private void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId)
        {
            _postAPIController.SaveTwitterPostDetails(PostId, userId, post, twitterId);
        }
        #endregion

        [AllowAnonymous]
        public ActionResult GetContentFromSocialMedia()
        {
            ResponseModels responseModel = new ResponseModels();
            List<smPost> post = _scheduleService.GetAllPosts();
            foreach (var item in post)
            {
                if (item.SocialMedia.ToLower() == "facebook")
                {
                    responseModel = GetLikesandCommentsFromFacebook(item.PostId);
                }
                else if (item.SocialMedia.ToLower() == "twitter")
                {
                    responseModel = GetLikesFromTwitter(item.PostId);
                }
                else if (item.SocialMedia.ToLower() == "linkedin")
                {
                    responseModel = GetLikesandCommentsFromLinkedIn(item.PostId);
                }
            }

            return Json(new { response = responseModel }, JsonRequestBehavior.AllowGet);
        }

        [AuditLog(Event = "Get facebook reports", Message = "Get reports from facebook posts")]
        public ResponseModels GetLikesandCommentsFromFacebook(int PostId)
        {
            ResponseModels response = new ResponseModels();
            List<smFacebookPostDetails> list = _postAPIController.GetAllFacebookPostDetails().Where(x => x.PostId == PostId).ToList();
            var socialMedia = _postAPIController.GetSocilaMediaAccountByName("Facebook");
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            FacebookLikesModel likesmodel = new FacebookLikesModel();
            FaceBookComments commentsModel = new FaceBookComments();

            var client = new WebClient();
            foreach (var item in list)
            {
                try
                {
                    if (item.FBId != null)
                    {
                        var socialMediaaccount = _scheduleService.GetSocialMediaAccnt(item.UserId, "Facebook");
                        string likes = client.DownloadString(string.Format("https://graph.facebook.com/{0}/likes?access_token={1}", item.FBId, socialMediaaccount.AccessToken));
                        likesmodel = JsonConvert.DeserializeObject<FacebookLikesModel>(likes);
                        var name = likesmodel.data.Select(x => x.name);
                        string comments = client.DownloadString(string.Format("https://graph.facebook.com/{0}/comments?access_token={1}", item.FBId, socialMediaaccount.AccessToken));
                        commentsModel = JsonConvert.DeserializeObject<FaceBookComments>(comments);
                        var CommentName = commentsModel.data.Select(x => x.from.name);
                        var CommentMessage = commentsModel.data.Select(x => x.message);
                        response.LikesCount = name.Count();
                        response.Comments = CommentMessage.ToList();
                        response.CommentsName = CommentName.ToList();
                        response.LikesName = name.ToList();
                        response.fbcomment = commentsModel;

                    }
                    else
                    {
                        var socialMediaaccount = _scheduleService.GetSocialMediaAccnt(item.UserId, "Facebook");
                        string likes = client.DownloadString(string.Format("https://graph.facebook.com/{0}/likes?access_token={1}", item.FBId, socialMediaaccount.AccessToken));
                        likesmodel = JsonConvert.DeserializeObject<FacebookLikesModel>(likes);
                        var name = likesmodel.data.Select(x => x.name);
                        string comments = client.DownloadString(string.Format("https://graph.facebook.com/{0}/comments?access_token={1}", item.FBId, socialMediaaccount.AccessToken));
                        commentsModel = JsonConvert.DeserializeObject<FaceBookComments>(comments);
                        var CommentName = commentsModel.data.Select(x => x.from.name);
                        var CommentMessage = commentsModel.data.Select(x => x.message);
                        response.LikesCount = name.Count();
                        response.Comments = CommentMessage.ToList();
                        response.CommentsName = CommentName.ToList();
                        response.LikesName = name.ToList();
                        response.fbcomment = commentsModel;
                    }

                    smPost post = _scheduleService.GetPostById(PostId);
                    var myLikes = new List<string>();
                    var myComments = new List<string>();
                    if (response != null)
                    {
                        post.LikesCount = response.LikesCount;
                        if (response.LikesName != null)
                        {
                            foreach (var like in response.LikesName)
                            {
                                myLikes.Add(like);
                            }
                            post.LikesNames = string.Join(", ", myLikes);
                        }
                        post.CommentsCount = response.Comments.Count();
                        if (commentsModel != null)
                        {
                            foreach (var like in commentsModel.data)
                            {
                                myComments.Add(like.from.name + ": " + like.message);
                            }
                            post.CommentsText = string.Join(", ", myComments);
                        }
                    }
                    var status = _scheduleService.UpdatePostDetails(post);
                }
                catch
                {

                    continue;
                }
            }
            return response;
        }

        [AuditLog(Event = "Get twitter reports", Message = "Get reports from twitter posts")]
        public ResponseModels GetLikesFromTwitter(int PostId)
        {
            ResponseModels response = new ResponseModels();
            try
            {

                List<smTwitterPostDetails> list = _postAPIController.GetAllTwitterPostDetails().Where(x => x.PostId == PostId).ToList();
                var socialMedia = _postAPIController.GetSocilaMediaAccountByName("Twitter");

                var oAuthConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
                var oAuthConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
                TwitterDataModel twitterdata = new TwitterDataModel();
                var oAuthUrl = "https://api.twitter.com/oauth2/token";
                var tweetId = list.Select(x => x.TwitterId).FirstOrDefault();

                // Do the Authenticate
                var authHeaderFormat = "Basic {0}";

                var authHeader = string.Format(authHeaderFormat,
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
                    Uri.EscapeDataString((oAuthConsumerSecret)))
                ));

                var postBody = "grant_type=client_credentials";

                HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);
                authRequest.Headers.Add("Authorization", authHeader);
                authRequest.Method = "POST";
                authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (Stream stream = authRequest.GetRequestStream())
                {
                    byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                }

                authRequest.Headers.Add("Accept-Encoding", "gzip");

                WebResponse authResponse = authRequest.GetResponse();
                // deserialize into an object
                TwitAuthenticateResponse twitAuthResponse;
                using (authResponse)
                {
                    using (var reader = new StreamReader(authResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objectText = reader.ReadToEnd();
                        twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                    }
                }

                // Get the retweets by Id
                var retweetFormat = "https://api.twitter.com/1.1/statuses/show/{0}.json";
                var retweetsUrl = string.Format(retweetFormat, tweetId);
                HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(retweetsUrl);
                var retweetHeaderFormat = "{0} {1}";
                timeLineRequest.Headers.Add("Authorization", string.Format(retweetHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
                timeLineRequest.Method = "Get";
                WebResponse retweetResponse = timeLineRequest.GetResponse();
                var retweetJson = string.Empty;
                using (retweetResponse)
                {
                    using (var reader = new StreamReader(retweetResponse.GetResponseStream()))
                    {
                        retweetJson = reader.ReadToEnd();
                        twitterdata = JsonConvert.DeserializeObject<TwitterDataModel>(retweetJson);
                        response.LikesCount = twitterdata.favorite_count;

                    }
                }
                smPost post = _scheduleService.GetPostById(PostId);
                if (response != null && response.LikesCount != null)
                {
                    post.LikesCount = response.LikesCount;
                }
                var status = _scheduleService.UpdatePostDetails(post);
            }
            catch (Exception e)
            {

            }
            return response;
        }

        [AuditLog(Event = "Get linkedin reports", Message = "Get reports from linkedin posts")]
        public ResponseModels GetLikesandCommentsFromLinkedIn(int PostId)
        {
            List<smLinkedInPostDetails> list = _postAPIController.GetAllLinkedInPostDetails().Where(x => x.PostId == PostId).ToList();
            var socialMedia = _postAPIController.GetSocilaMediaAccountByName("LinkedIn");
            ResponseModels response = new ResponseModels();
            String consumerKey = ConfigurationManager.AppSettings["LinkedinClientID"]; // key obtained by linkedIn app
            String consumerSecret = ConfigurationManager.AppSettings["LinkedinClientSecret"];
            var client = new WebClient();
            LinkedInComments commentmodel = new LinkedInComments();
            LinkedInLikes likesmodel = new LinkedInLikes();

            string acesstoken = socialMedia.AccessToken;
            foreach (var item in list)
            {
                try
                {
                    string key = item.LIinkedInId;
                    string acestoken = socialMedia.AccessToken;
                    var comnet = "https://api.linkedin.com/v1/companies/2414183/updates/key={0}/update-comments?oauth2_access_token=AQWDs63lXlOv19oz6zaaBptKS8EQmrw6TkhDanJa7BIdaXUrg6_9s3rA0g5QosADev_8M2_ebhCkJLa8VAa4v7ijvjOS-3MjliEPaLU4kL_17ttss62V9YUdwf_cTSLhWKzlSzg9A2s4o_aKBtfPFjM7_YZkArbSXwKltxvtSJbkH3SErL4&format=json";
                    string comment = client.DownloadString(string.Format(comnet, key));
                    commentmodel = JsonConvert.DeserializeObject<LinkedInComments>(comment);
                    var like = "https://api.linkedin.com/v1/companies/2414183/updates/key={0}/likes?oauth2_access_token=AQWDs63lXlOv19oz6zaaBptKS8EQmrw6TkhDanJa7BIdaXUrg6_9s3rA0g5QosADev_8M2_ebhCkJLa8VAa4v7ijvjOS-3MjliEPaLU4kL_17ttss62V9YUdwf_cTSLhWKzlSzg9A2s4o_aKBtfPFjM7_YZkArbSXwKltxvtSJbkH3SErL4&format=json";
                    string likes = client.DownloadString(string.Format(like, key));
                    likesmodel = JsonConvert.DeserializeObject<LinkedInLikes>(likes);
                    if (commentmodel != null && commentmodel.values != null)
                    {
                        response.Comments = commentmodel.values.Select(x => x.comment).ToList();
                        response.linkedincomment = commentmodel;
                    }
                    else
                    {
                        response.Comments = new List<string>();
                    }
                    if (likesmodel != null)
                    {
                        //response.LikesCount = likesmodel._total;
                        response.linkedInlikes = likesmodel;
                    }
                    else
                    {
                        response.LikesCount = 0;
                    }
                }
                catch
                {

                    continue;
                }
            }


            smPost post = _scheduleService.GetPostById(PostId);
            try
            {
                var myLikes = new List<string>();
                var myComments = new List<string>();
                if (response != null)
                {
                    if (likesmodel != null)
                    {
                        post.LikesCount = likesmodel._total;
                        foreach (var like in response.linkedInlikes.values)
                        {
                            myLikes.Add(like.person.firstName);
                        }
                        post.LikesNames = string.Join(", ", myLikes);
                    }

                    if (commentmodel != null && commentmodel.values != null)
                    {
                        post.CommentsCount = response.Comments.Count();
                        foreach (var like in commentmodel.values)
                        {
                            myComments.Add(like.person.firstName + ": " + like.comment);
                        }
                        post.CommentsText = string.Join(", ", myComments);
                    }
                }
            }
            catch { }
            var status = _scheduleService.UpdatePostDetails(post);
            return response;
        }

        //var client = new WebClient();
        //string likes = client.DownloadString(string.Format("https://api.linkedin.com/v1/companies/1337/updates/key=UPDATE-279806079-6217349860492963840/likes?format=json"));

        [AuditLog]
        public string GetLikesFromLinkedIn()
        {
            string accessToken = "AQXWLoYiyzpiaCG54Xv7unDLuU93IesQsyhcNKiEa_Jnc4NcsB2sHj-Wu0-3SLHJmJzui6P4lYEauwOs4MryftMLTP06lrgK0rcvirFkvE6GaNCEyEC0vaIppIzg2Nh8TPAc9zQkA4kkBcnYbxums8Ymjn_ZTLEpFENZsRbKEU5ma-qSsh8";
            string updateKey = "824222064303308800";
            string linkedinSharesEndPoint = "https://api.linkedin.com/v1/companies/1337/updates/key={0}/likes?oauth2_access_token={1}?format=json";
            var requestUrl = String.Format(linkedinSharesEndPoint, updateKey, accessToken);
            var client = new WebClient();
            var requestHeaders = new NameValueCollection
       {
        { "Content-Type", "application/json" },
        { "x-li-format", "json" }
       };
            client.Headers.Add(requestHeaders);
            string responseJson = client.DownloadString(requestUrl);

            return "";
        }

        [AuditLog]
        public void getFileList()
        {
            List<string> files = new List<string>();

            try
            {
                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create("ftp://ftp.gnu.org/") as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential();
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                //Read the server's response         
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    files.Add(reader.ReadLine());
                }

                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                response.Close();
            }
            catch (Exception)
            {
            }

            if (files.Count != 0)
            {
            }
        }


        public int GetDay(string day)
        {
            int Day = 0;
            int Day2 = 0;
            if (day == "Monday")
            {
                Day = 1;
                Day2 = 7;
            }
            if (day == "Tuesday")
            {
                Day = 2;
                Day2 = 8;
            }
            if (day == "Wednesday")
            {
                Day = 3;
                Day2 = 9;
            }
            if (day == "Thursday")
            {
                Day = 4;
                Day2 = 10;
            }
            if (day == "Friday")
            {
                Day = 5;
                Day2 = 11;
            }
            if (day == "Saturday")
            {
                Day = 6;
                Day2 = 12;
            }
            if (day == "Sunday")
            {
                Day = 7;
                Day2 = 13;
            }
            return Day;
        }

        public int GetDay2(string day)
        {
            int Day = 0;
            int Day2 = 0;
            if (day == "Monday")
            {
                Day = 1;
                Day2 = 7;
            }
            if (day == "Tuesday")
            {
                Day = 2;
                Day2 = 8;
            }
            if (day == "Wednesday")
            {
                Day = 3;
                Day2 = 9;
            }
            if (day == "Thursday")
            {
                Day = 4;
                Day2 = 10;
            }
            if (day == "Friday")
            {
                Day = 5;
                Day2 = 11;
            }
            if (day == "Saturday")
            {
                Day = 6;
                Day2 = 12;
            }
            if (day == "Sunday")
            {
                Day = 7;
                Day2 = 13;
            }
            return Day2;
        }

        public int GetDiffrenceDays(string old, string newday)
        {
            int Day = 0;
            int oldday = GetDay(old);
            int newDay = GetDay(newday);
            int remainingDay = newDay - oldday;
            return remainingDay;
        }


        [AllowAnonymous]
        public ActionResult GetResults(string postId)
        {
            //smPost post = _scheduleService.GetPostById(postId);
            smPost post = _scheduleService.GetPostByUniqueId(postId);
            ViewBag.ImgUrl = ConfigurationManager.AppSettings["SiteUrl"] + post.ImageUrl;
            /// post.Url = ConfigurationManager.AppSettings["SiteUrl"];

            return View(post);
        }

        [AllowAnonymous]
        public ActionResult GetResultsForStatus(int postId)
        {
            smPost post = _scheduleService.GetPostById(postId);
            return View(post);
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetResultsForAspPAges(int postId)
        {
            smPost post = _scheduleService.GetPostById(postId);

            return Json(post, JsonRequestBehavior.AllowGet);


        }

        [AllowAnonymous]
        public ActionResult Service(string p1, string p2, string p3)
        {
            return View("Service");
        }


        public string urlShort(string urlToShorten)
        {
            string statusCode = string.Empty; // The variable which we will be storing the status code of the server response
            string statusText = string.Empty;                       // The variable which we will be storing the status text of the server response
            string shortUrl = string.Empty;                         // The variable which we will be storing the shortened url
            string longUrl = string.Empty;                         // The variable which we will be storing the long url
            //string  = "http://108.168.203.227/SocialMediaBroadcast/Post/GetResults?postId=95";      // The url we want to shorten
            XmlDocument xmlDoc = new XmlDocument();                 // The xml document which we will use to parse the response from the server
            WebRequest request = WebRequest.Create("http://api.bitly.com/v3/shorten");
            byte[] data = Encoding.UTF8.GetBytes(string.Format("login={0}&apiKey={1}&longUrl={2}&format={3}",
             "o_703tethqf5",                             // Your username
             "R_4176db7a17fe468f932c99c222656dcb",                              // Your API key
             HttpUtility.UrlEncode(urlToShorten),         // Encode the url we want to shorten
             "xml"));                                     // The format of the response we want the server to reply with
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (Stream ds = request.GetRequestStream())
            {
                ds.Write(data, 0, data.Length);
            }
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    xmlDoc.LoadXml(sr.ReadToEnd());
                }
            }
            statusCode = xmlDoc.GetElementsByTagName("status_code")[0].InnerText;
            statusText = xmlDoc.GetElementsByTagName("status_txt")[0].InnerText;
            shortUrl = xmlDoc.GetElementsByTagName("url")[0].InnerText;
            longUrl = xmlDoc.GetElementsByTagName("long_url")[0].InnerText;
            return shortUrl;
        }


        [AllowAnonymous]
        public ActionResult GetResultsTwitter()
        {
            //smPost post = _scheduleService.GetPostById(postId);
            //ViewBag.ImgUrl = ConfigurationManager.AppSettings["SiteUrl"] + post.ImageUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetResultsTwitterForStatus(int postId)
        {
            smPost post = _scheduleService.GetPostById(postId);
            return View(post);
        }

        public string TinyUrl()
        {
            Uri uri = new Uri("http://tinyit.cc/api.php?url=108.168.203.227/SocialMediaBroadcast/Post/GetResults?PostId=162&user=Sdd&api=3c352b19-7989-41eb-b5ed-27be8ec8a232"); //The API access URL containing the long URL
            string data = "field-keywords=ASP.NET 2.0";
            string tinyccurl = "";
            if (uri.Scheme == Uri.UriSchemeHttp)
            {
                HttpWebRequest request = (HttpWebRequest)
            HttpWebRequest.Create(uri); //New web request object for our long URL
                request.Method = WebRequestMethods.Http.Post;
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(data);
                writer.Close();
                HttpWebResponse response = (HttpWebResponse)
               request.GetResponse(); //Web response object to get the results
                StreamReader reader = new StreamReader(response.GetResponseStream());
                tinyccurl = reader.ReadToEnd(); //Our tiny.cc shortlink
                response.Close();
                // Response.Write(tinyccurl);
            }
            return tinyccurl;
        }

        //public string PostNowSchedular(string EventId)
        //{
        //    int Id = Convert.ToInt32(EventId);
        //    var eventd = _homeService.GetEventById(Id);
        //    var content = _manageContentService.GetContentById(Id);       
        //    return "";
        //}

        #region SyncFacebookPage
        [AllowAnonymous]
        public dynamic SyncFacebookPage(int userId)
        {
            var AccessTocken = string.Empty;
            smFacebookPageDetail pageDetail = new smFacebookPageDetail();
            AccessTocken = _postAPIController.SyncFacebookPage(userId);

            if (!String.IsNullOrEmpty(AccessTocken))
            {
                var fbClient = new FacebookClient();
                fbClient.AccessToken = AccessTocken;

                string me = Convert.ToString(fbClient.Get("me?fields=link,first_name,last_name,email,gender,picture,age_range,accounts"));
                FacebookProfileModel fbDetail = new FacebookProfileModel();
                JavaScriptSerializer js = new JavaScriptSerializer();
                fbDetail = js.Deserialize<FacebookProfileModel>(me);
                return Json(SaveSyncFacebookPage(fbDetail, userId), JsonRequestBehavior.AllowGet);

            }
            return pageDetail;


        }

        public dynamic SaveSyncFacebookPage(FacebookProfileModel fb, int userID)
        {
            List<smFacebookPageDetail> fbpage = new List<smFacebookPageDetail>();

            try
            {



                List<smFacebookPageDetail> _pageDetail = new List<smFacebookPageDetail>();

                _pageDetail = (from c in fb.accounts.data
                               select new smFacebookPageDetail
                               {
                                   PageAccessToken = c.access_token,
                                   PageName = c.name,
                                   PageId = c.id,
                                   IsActive = true,
                                   IsDeleted = false,
                                   CreatedDate = DateTime.UtcNow,
                                   category = c.category,
                                   UserId = SessionManager.LoggedInUser.UserID
                               }).ToList();


                fbpage = _postAPIController.SaveSyncFacebookPage(_pageDetail, userID);
                //foreach (var item in fb.accounts.data)
                //{
                //    _pageDetail.PageAccessToken = item.access_token;
                //    _pageDetail.PageName = item.name;
                //    _pageDetail.PageId = item.id;
                //    _pageDetail.PageName = item.name;
                //    _pageDetail.CreatedDate = DateTime.UtcNow;
                //    _pageDetail.IsActive = true;
                //    _pageDetail.IsDeleted = false;
                //    _pageDetail.UserFaceBookID = fb.Id;
                //    _pageDetail.UserId = SessionManager.LoggedInUser.UserID;
                //    response = _postAPIController.SaveSyncFacebookPage(_pageDetail , userID);

                //}






            }
            catch (Exception ex)
            {
                throw;
            }
            return fbpage;

        }
        #endregion

    }
}