using Core.Domain;
using Facebook;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using smartData.Areas.Users.ApiControllers;
using smartData.Areas.Users.Models.User;
using smartData.Common;
using smartData.Filter;
using smartData.Models;
using smartData.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using TweetSharp;

namespace smartData.Areas.Users.Controllers
{
    public class ScheduleController : Controller
    {

        #region Global Variables
        ServiceLayer.Interfaces.IHomeService _homeService;
        ServiceLayer.Interfaces.IScheduleService _scheduleService;
        ServiceLayer.Interfaces.IPostService _postService;
        IScheduleAPIController _scheduleAPIController;
        IPostAPIController _postAPIController;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        IUserService _userService;
        #endregion


        #region constructor
        public ScheduleController(IUserService userService, ScheduleService scheduleService, IScheduleAPIController scheduleAPIController, PostService postService, IPostAPIController postAPIController, ServiceLayer.Interfaces.IHomeService homeService)
        {
            _scheduleService = scheduleService;
            _scheduleAPIController = scheduleAPIController;
            _postAPIController = postAPIController;
            _postService = postService;
            _homeService = homeService;
            _userService = userService;
        }
        #endregion

        //
        // GET: /Users/Schedule/

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PostContentSchedule()
        {
            List<smPost> post = _scheduleAPIController.GetAllPostList();
            CustomErrorModel errorModel = new CustomErrorModel();
            ImageModel model = new ImageModel();

            if (post != null)
            {
                foreach (var item in post)
                {
                    //TimeZoneInfo tz = TimeZoneInfo.Local;
                    //var timeZoneId = tz.Id;
                    //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                    //DateTime cstDateTime = TimeZoneInfo.ConvertTimeFromUtc(item.PostDate, cstZone);

                    model.PostType = item.PostType;
                    model.ScheduledTime = item.PostDate;
                    model.TextMessage = item.Description;
                    model.Title = item.Name;
                    model.Heading = item.Caption;
                    model.Link = item.Url;
                    if (item.ImageUrl != null)
                    {
                        model.ImageArray = item.ImageUrl.Split(',').ToList();
                    }
                    //if (item.PostDate >= DateTime.UtcNow)
                    //{
                    if (item.PostDate < DateTime.UtcNow)
                    {
                        //var socialMedia = _scheduleAPIController.GetSocialMediaAccnt(item.UserId, item.SocialMediaProfileId);


                        var socialMedia = _scheduleAPIController.GetSocialMediaAccnt(item.UserId, item.SocialMedia);
                        if (socialMedia != null)
                        {
                            if (socialMedia.SocialMedia.ToLower() == "facebook")   // Posts to facebook
                            {
                                model.TextMessage = item.Caption;
                                errorModel = PostToFacebookSchedule(socialMedia, model, item.UserId, item.PostId, item.UniquePostId);
                            }
                            else if (socialMedia.SocialMedia.ToLower() == "linkedin")  // Posts to linkedIn
                            {
                                errorModel = PostToLinkedInNetworkSchedule(socialMedia, model, item.UserId, item.PostId);

                            }
                            else if (socialMedia.SocialMedia.ToLower() == "twitter") // Posts to twitter
                            {
                                errorModel = PostToTwitterSchedule(socialMedia, model, item.UserId, item.PostId);

                            }
                        }
                    }
                    //}                                    
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public ActionResult PostNow(int PostId, string socialarr)
        {
            smPost post = _scheduleAPIController.GetAllPostList().FirstOrDefault(x => x.PostId == PostId);
            CustomErrorModel errorModel = new CustomErrorModel();
            ImageModel model = new ImageModel();

            if (post != null)
            {

                //TimeZoneInfo tz = TimeZoneInfo.Local;
                //var timeZoneId = tz.Id;
                //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                //DateTime cstDateTime = TimeZoneInfo.ConvertTimeFromUtc(item.PostDate, cstZone);

                model.PostType = post.PostType;
                model.ScheduledTime = post.PostDate;
                model.TextMessage = post.Description;
                model.Title = post.Name;
                model.Heading = post.Caption;
                model.Link = post.Url;
                if (post.ImageUrl != null)
                {
                    model.ImageArray = post.ImageUrl.Split(',').ToList();
                }
                //if (item.PostDate >= DateTime.UtcNow)
                //{
                if (post.PostDate < DateTime.UtcNow)
                {
                    //var socialMedia = _scheduleAPIController.GetSocialMediaAccnt(item.UserId, item.SocialMediaProfileId);

                    foreach (string str2 in socialarr.Split(new char[] { ',' }))
                    {
                        var socialMedia = _scheduleAPIController.GetSocialMediaAccnt(post.UserId, str2);
                        switch (str2)
                        {
                            case "Facebook":

                                model.TextMessage = post.Caption;
                                if (socialMedia != null)
                                    errorModel = PostToFacebookSchedule(socialMedia, model, post.UserId, post.PostId, post.UniquePostId);

                                break;

                            case "LinkedIn":

                                model.TextMessage = post.Caption;
                                if (socialMedia != null)
                                    errorModel = PostToLinkedInNetworkSchedule(socialMedia, model, post.UserId, post.PostId);

                                break;

                            case "Twitter":

                                model.TextMessage = post.Caption;
                                if (socialMedia != null)
                                    errorModel = PostToTwitterSchedule(socialMedia, model, post.UserId, post.PostId);

                                break;
                        }
                    }


                    //if (socialMedia != null)
                    //{
                    //    if (socialMedia.SocialMedia.ToLower() == "facebook")   // Posts to facebook
                    //    {
                    //        model.TextMessage = post.Caption;
                    //        errorModel = PostToFacebookSchedule(socialMedia, model, post.UserId, post.PostId);
                    //    }
                    //    else if (socialMedia.SocialMedia.ToLower() == "linkedin")  // Posts to linkedIn
                    //    {
                    //        errorModel = PostToLinkedInNetworkSchedule(socialMedia, model, post.UserId, post.PostId);

                    //    }
                    //    else if (socialMedia.SocialMedia.ToLower() == "twitter") // Posts to twitter
                    //    {
                    //        errorModel = PostToTwitterSchedule(socialMedia, model, post.UserId, post.PostId);

                    //    }
                    //}
                }
                //}                                    

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// POst In Sechdule Old 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <param name="PostId"></param>
        /// <param name="UniquePostId"></param>
        /// <returns></returns>
        //private CustomErrorModel PostToFacebookSchedule(smSocialMediaProfile data, ImageModel model, int userId,int PostId, string UniquePostId)
        //{
        //    CustomErrorModel errorModel = new CustomErrorModel();
        //    bool status = false;
        //    string Message = string.Empty;
        //    string post = string.Empty;
        //    smPost smpost = new smPost();
        //    try
        //    {
        //        string accesstoken = string.Empty;
        //        accesstoken = data.AccessToken;
        //        var client = new FacebookClient(accesstoken);
        //        client.AppId = ConfigurationManager.AppSettings["FBClientID"];
        //        client.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
        //        dynamic parameters = new ExpandoObject();
        //        parameters.message = model.TextMessage;
        //        // parameters.link = "http://www.example.com/article.html";
        //        var imagePath = ConfigurationManager.AppSettings["SiteUrl"];

        //        if (model.ImageArray != null && model.ImageArray.Count > 0)
        //        {
        //            foreach (var images in model.ImageArray)
        //            {
        //                // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
        //                // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
        //                // imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;
        //                imagePath = Server.MapPath("~/Images/");
        //                var newimg = images.Substring(8);
        //                imagePath = imagePath + newimg;

        //            }
        //            //parameters.source = new FacebookMediaObject
        //            //{
        //            //    ContentType = "image/jpeg",
        //            //    FileName = Path.GetFileName(imagePath)
        //            //}.SetValue(System.IO.File.ReadAllBytes(imagePath));
        //        }


        //        //parameters.caption = "";

        //        if (model.ImageArray != null && model.ImageArray.Count > 0)
        //        {
        //            // PhotoPostFBResponseModel postModel = new PhotoPostFBResponseModel();
        //            parameters.message = model.Heading;              
        //                //post = "Image";
        //                post = "Status";
        //                //smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
        //                //var link = "http://sm4y.cc/Post/GetResults?postId=" + PostId;
        //                var user = _userService.GetUserById(userId);
        //                var link = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, UniquePostId);
        //               // var newlink = urlShort(link);
        //                parameters.link = link;                  
        //                //dynamic result = client.Post("me/photos", parameters); //To post in facebook
        //                dynamic result = client.Post("me/feed", parameters);
        //            //parameters.name = model.TextMessage;
        //            //dynamic result = client.Post("me/photos", parameters);
        //            if (result != null)
        //            {
        //                // Save post data to database
        //               // post = "Image";
        //                // Save posts to database and get last inserted row
        //                smpost = UpdatePostDetails(PostId);
        //                _homeService.UpdateEventById(smpost.EventId);

        //                // save facebook response to database
        //                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
        //            }
        //        }
        //        else
        //        {
        //            post = "Status";
        //            parameters.message = model.Heading;
        //            var link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + PostId;
        //            var newlink = urlShort(link);
        //            parameters.link = newlink;  
        //            dynamic result = client.Post("me/feed", parameters);
        //            if (result != null)
        //            {

        //                // Update posts to database and get last inserted row
        //                smpost = UpdatePostDetails(PostId);
        //                _homeService.UpdateEventById(smpost.EventId);
        //                // smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
        //                // save facebook response to database
        //                SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
        //            }


        //            //parameters.description = model.TextMessage;
        //            //dynamic result = client.Post("me/feed", parameters);
        //            //if (result != null)
        //            //{
        //            //    // Save posts to database and get last inserted row
        //            //    smpost = UpdatePostDetails(PostId);
        //            //    // save facebook response to database
        //            //    SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
        //            //}

        //            //else if (model.PostType == 2)
        //            //{
        //            //    post = "Status";
        //            //    UpdatePostDetails(PostId);
        //            //}
        //        }
        //        status = true;
        //        Message = "Content posted successfully on facebook.";
        //    }
        //    catch (Exception ex)
        //    {
        //        status = false;
        //        Message = "Error occured while posting to facebook";
        //    }
        //    errorModel.Status = status;
        //    errorModel.Message = Message;

        //    return errorModel;
        //}
        private CustomErrorModel PostToFacebookSchedule(smSocialMediaProfile data, ImageModel model, int userId, int PostId, string UniquePostId)
        {
            CustomErrorModel errorModel = new CustomErrorModel();
            bool status = false;
            string Message = string.Empty;
            string post = string.Empty;
            smPost smpost = new smPost();
            string accesstoken = string.Empty;
            accesstoken = data.AccessToken;
            var client = new FacebookClient(accesstoken);
            client.AppId = ConfigurationManager.AppSettings["FBClientID"];
            client.AppSecret = ConfigurationManager.AppSettings["FBClientSecret"];
            dynamic parameters = new ExpandoObject();
            var PostDetail = _homeService.GetFacebookPereference(userId);
            try
            {
                if (PostDetail.PageId == 0)
                {

                    parameters.message = model.TextMessage;
                    // parameters.link = "http://www.example.com/article.html";
                    var imagePath = ConfigurationManager.AppSettings["SiteUrl"];

                    if (model.ImageArray != null && model.ImageArray.Count > 0)
                    {
                        foreach (var images in model.ImageArray)
                        {
                            // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
                            // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
                            // imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;
                            imagePath = Server.MapPath("~/Images/");
                            var newimg = images.Substring(8);
                            imagePath = imagePath + newimg;

                        }
                        //parameters.source = new FacebookMediaObject
                        //{
                        //    ContentType = "image/jpeg",
                        //    FileName = Path.GetFileName(imagePath)
                        //}.SetValue(System.IO.File.ReadAllBytes(imagePath));
                    }


                    //parameters.caption = "";

                    if (model.ImageArray != null && model.ImageArray.Count > 0)
                    {
                        // PhotoPostFBResponseModel postModel = new PhotoPostFBResponseModel();
                        parameters.message = model.Heading;
                        //post = "Image";
                        post = "Status";
                        //smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                        //var link = "http://sm4y.cc/Post/GetResults?postId=" + PostId;
                        var user = _userService.GetUserById(userId);
                        var link = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, UniquePostId);
                        // var newlink = urlShort(link);
                        parameters.link = link;
                        //dynamic result = client.Post("me/photos", parameters); //To post in facebook
                        dynamic result = client.Post("me/feed", parameters);
                        //parameters.name = model.TextMessage;
                        //dynamic result = client.Post("me/photos", parameters);
                        if (result != null)
                        {
                            // Save post data to database
                            // post = "Image";
                            // Save posts to database and get last inserted row
                            smpost = UpdatePostDetails(PostId);
                            _homeService.UpdateEventById(smpost.EventId);

                            // save facebook response to database
                            SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        }
                    }
                    else
                    {
                        post = "Status";
                        parameters.message = model.Heading;
                        var link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + PostId;
                        var newlink = urlShort(link);
                        parameters.link = newlink;
                        dynamic result = client.Post("me/feed", parameters);
                        if (result != null)
                        {

                            // Update posts to database and get last inserted row
                            smpost = UpdatePostDetails(PostId);
                            _homeService.UpdateEventById(smpost.EventId);
                            // smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            // save facebook response to database
                            SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        }


                        //parameters.description = model.TextMessage;
                        //dynamic result = client.Post("me/feed", parameters);
                        //if (result != null)
                        //{
                        //    // Save posts to database and get last inserted row
                        //    smpost = UpdatePostDetails(PostId);
                        //    // save facebook response to database
                        //    SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        //}

                        //else if (model.PostType == 2)
                        //{
                        //    post = "Status";
                        //    UpdatePostDetails(PostId);
                        //}
                    }
                    status = true;
                    Message = "Content posted successfully on facebook.";
                }
                else
                {
                    parameters.message = model.TextMessage;
                    // parameters.link = "http://www.example.com/article.html";
                    var imagePath = ConfigurationManager.AppSettings["SiteUrl"];

                    if (model.ImageArray != null && model.ImageArray.Count > 0)
                    {
                        foreach (var images in model.ImageArray)
                        {
                            // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
                            // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
                            // imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;
                            imagePath = Server.MapPath("~/Images/");
                            var newimg = images.Substring(8);
                            imagePath = imagePath + newimg;

                        }
                        //parameters.source = new FacebookMediaObject
                        //{
                        //    ContentType = "image/jpeg",
                        //    FileName = Path.GetFileName(imagePath)
                        //}.SetValue(System.IO.File.ReadAllBytes(imagePath));
                    }


                    //parameters.caption = "";

                    if (model.ImageArray != null && model.ImageArray.Count > 0)
                    {

                        List<smFacebookPageDetail> FullPageDetail = new List<smFacebookPageDetail>();
                        // PhotoPostFBResponseModel postModel = new PhotoPostFBResponseModel();
                        parameters.message = model.Heading;
                        //post = "Image";
                        post = "Status";
                        //smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                        //var link = "http://sm4y.cc/Post/GetResults?postId=" + PostId;
                        var user = _userService.GetUserById(userId);
                        var link = string.Format("http://{0}.sm4y.cc/a/{1}/", user.Shortname, UniquePostId);
                        // var newlink = urlShort(link);
                        parameters.link = link;
                        //dynamic result = client.Post("me/photos", parameters); //To post in facebook
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
                        //parameters.name = model.TextMessage;
                        //dynamic result = client.Post("me/photos", parameters);
                        if (result != null)
                        {
                            // Save post data to database
                            // post = "Image";
                            // Save posts to database and get last inserted row
                            smpost = UpdatePostDetails(PostId);
                            _homeService.UpdateEventById(smpost.EventId);

                            // save facebook response to database
                            SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        }
                    }
                    else
                    {
                        List<smFacebookPageDetail> FullPageDetail = new List<smFacebookPageDetail>();
                        post = "Status";
                        parameters.message = model.Heading;
                        var link = "http://sm4y.cc/Post/GetResultsForStatus?postId=" + PostId;
                        var newlink = urlShort(link);
                        parameters.link = newlink;
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

                            // Update posts to database and get last inserted row
                            smpost = UpdatePostDetails(PostId);
                            _homeService.UpdateEventById(smpost.EventId);
                            // smpost = SavePostedContent(data, model, userId, parameters, post, model.PostType, model.ScheduledTime, ImpersonateUserId);
                            // save facebook response to database
                            SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        }


                        //parameters.description = model.TextMessage;
                        //dynamic result = client.Post("me/feed", parameters);
                        //if (result != null)
                        //{
                        //    // Save posts to database and get last inserted row
                        //    smpost = UpdatePostDetails(PostId);
                        //    // save facebook response to database
                        //    SaveFacebookPostDetails(smpost.PostId, userId, post, result.id, result.post_id);
                        //}

                        //else if (model.PostType == 2)
                        //{
                        //    post = "Status";
                        //    UpdatePostDetails(PostId);
                        //}
                    }
                    status = true;
                    Message = "Content posted successfully on facebook.";
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
        private smPost UpdatePostDetails(int PostId)
        {

            var post = _scheduleAPIController.GetPostById(PostId);
            //string image = string.Empty;

            //if (model.ImageArray.Count > 0)
            //{
            //    for (int i = 0; i < model.ImageArray.Count; i++)
            //    {
            //        image = model.ImageArray[i];
            //    }
            //}

            smPost status = _scheduleAPIController.UpdatePostDetails(post);
            return status;
        }

        private void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId)
        {
            _scheduleAPIController.SaveFacebookPostDetailsSchedule(postId, userId, type, fbId, fbPostId);
        }

        public CustomErrorModel PostToLinkedInNetworkSchedule(smSocialMediaProfile data, ImageModel model, int userId, int PostId)
        {
            CustomErrorModel errorModel = new CustomErrorModel();
            smPost smpost = new smPost();
            string Message = string.Empty;
            string post = string.Empty;
            bool status = false;
            string accessToken = data.AccessToken;
            string imagePath = string.Empty;
            string linkedinSharesEndPoint = "https://api.linkedin.com/v1/people/~/shares?oauth2_access_token={0}";

            if (model.ImageArray != null && model.ImageArray.Count > 0)
            {
                foreach (var images in model.ImageArray)
                {
                    // var newimg = images.Substring(8);

                    imagePath = ConfigurationManager.AppSettings["SiteUrl"] + images;


                    //imagePath = ConfigurationManager.AppSettings["SiteUrl"] + "/Images/WallImages/" + userId + "/" + newimg;
                    // imagePath = "https://localhost:55038/Images/WallImages/" + userId + "/" + images;
                    // imagePath = "https://108.168.203.227/SocialMediaBroadcast8/Images/WallImages/" + userId + "/" + images;
                    // imagePath = Server.MapPath("~/Images/");
                    //  imagePath = imagePath + "WallImages/" + userId + "/" + images;
                    //imagePath = "http://108.168.203.227//socialmediabroadcast/Images/WallImages/" + userId + "/" + images;

                    // imagePath = ConfigurationManager.AppSettings["SiteUrlForLinkedIn"] + "/Images/WallImages/" + userId + "/" + images;        

                }
                post = "Image";
            }
            else
            {
                post = "Status";
            }

            var requestUrl = String.Format(linkedinSharesEndPoint, accessToken);
            string title = "";

            if (!String.IsNullOrEmpty(model.Title))
            {
                if (model.Title.Length > 30)
                {
                    title = model.Title.Substring(0, 30);
                }
                else
                {
                    title = model.Title;
                }
            }

            string url = model.Link;

            //string title = "This is the linledin post demo";
            //string url = "https://developer.linkedin.com";

            //var msg = model.TextMessage;
            var msg = model.Heading;
            if (model.Heading == null)
            {
                model.Heading = "Rss";
            }
            if (model.Heading.Length > 30)
            {
                msg = model.Heading.Substring(0, 30);
            }
            else
            {
                msg = model.Heading;
            }
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

                visibility = new
                {
                    code = "anyone"
                }
            };

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
                //if (model.PostType == 1)
                //{
                // Get response from the api
                var responseJson = client.UploadString(requestUrl, "POST", requestJson);
                // response = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(responseJson);
                response = JsonConvert.DeserializeObject<LinkedInPostResponseModel>(responseJson);
                // Save posts to database and get last inserted row
                //smpost = UpdatePostedContentLinkedIn(PostId);
                smpost = UpdatePostDetails(PostId);
                _homeService.UpdateEventById(smpost.EventId);
                // Save linkedIn response to database
                SaveLinkedInPostDetails(smpost.PostId, userId, post, response.updateKey, response.updateUrl);
                errorModel.Message = "Content posted successfully on LinkedIn.";
                errorModel.Status = true;
                //}
                //else if (model.PostType == 2)
                //{
                //    UpdatePostedContentLinkedIn(PostId);
                //}
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

        private void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url)
        {
            _postAPIController.SaveLinkedInPostDetails(postId, userId, type, key, url);
        }

        public CustomErrorModel PostToTwitterSchedule(smSocialMediaProfile data, ImageModel model, int userId, int PostId)
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
                //if (model.PostType == 1)
                //{
                var twitterStatus = (dynamic)null;
                // Get Image

                if (model.ImageArray != null && model.ImageArray.Count > 0)
                {
                    foreach (var images in model.ImageArray)
                    {
                        // imagePath = ConfigurationManager.AppSettings["ImagePath2"] + userId + "/" + images;
                        // imagePath = "D:/Dayuser/Rohit Grover/Official/Project/Source Code/SocialMediaBroadcast/smartData/Images/WallImages/" + userId + "/" + images;
                        //imagePath = "http://108.168.203.227/SocialMediaBroadcast/Images/WallImages/" + userId + "/" + images;
                        //imagePath = Server.MapPath("~/Images/");
                        //imagePath = imagePath + "WallImages/" + userId + "/" + images;

                        imagePath = Server.MapPath("~/Images/");
                        var newimg = images.Substring(8);
                        imagePath = imagePath + newimg;
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
                    //smpost = SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, model.ScheduledTime);
                    smpost = UpdatePostDetails(PostId);
                    _homeService.UpdateEventById(smpost.EventId);
                    // Save linkedIn response to database
                    SaveTwitterPostDetails(smpost.PostId, userId, post, twitterStatus.IdStr);
                    errorModel.Message = "Content posted successfully on twitter";
                    errorModel.Status = true;
                }
                else
                {
                    errorModel.Message = "Error occured while posting to twitter";
                }
                //}
                //else if (model.PostType == 2) // For scheduled post
                //{
                //    SavePostedContentTwitter(data, model, userId, model.TextMessage, post, model.PostType, model.ScheduledTime);
                //}

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

        private void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId)
        {
            _postAPIController.SaveTwitterPostDetails(PostId, userId, post, twitterId);
        }

        [AuditLog(Event = "ManagePublishingTimes", Message = "Explore Publishing Time")]
        public ActionResult ManagePublishingTimes()
        {
            return View();
        }


        [AuditLog(Event = "AddPublishingTime", Message = "Add Publishing Time")]
        [HttpPost]
        public ActionResult AddPublishingTime(string Time, int Day, int offset)
        {
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            DateTime newTime = Convert.ToDateTime(Time);
            DateTime Postedtime = FromUTCData(newTime, offset);
            TimeSpan timesp = Postedtime.TimeOfDay;
            smPublishingTime status = _scheduleService.AddPublishingTime(timesp, Day, UserId, Time, ImpersonateUserId);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPublishingTimeByDay(int Day)
        {
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            List<smPublishingTime> status = _scheduleService.GetPublishingTimeByDay(Day, UserId);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public static DateTime FromUTCData(DateTime? dt, int timezoneOffset)
        {
            DateTime newDate = dt.Value + new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
            return newDate;
        }


        [AuditLog(Event = "DeletePublishingTime", Message = "Delete Publishing Time")]
        [HttpGet]
        public string DeletePublishingTime(string id)
        {
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            bool status = _scheduleService.DeletePublishingTime(id, UserId, ImpersonateUserId);
            if (status == true)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePostDataByPublishingTime(string PostInformation)
        {
            int userId = SessionManager.LoggedInUser.UserID;
            int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            smPost result = null;
            if (model.SocialMedia == "Rss")
            {
                var SocialMedia = _scheduleAPIController.GetAllSocialMediaAccountByUserId(userId);

                foreach (var items in SocialMedia)
                {
                    model.SocialMedia = items.SocialMedia;
                    var profile = _postAPIController.GetSocilaMediaAccountByName(model.SocialMedia);
                    foreach (var item in model.Time)
                    {
                        DateTime time = Convert.ToDateTime(item);
                        DateTime timenew = FromUTCData(time, model.timeoffset);
                        //int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                        //int nedt = iten.DayId;
                        //int remainday = nedt - dt;

                        //DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                        //TimeSpan time = iten.TimeStampPosted.TimeOfDay;
                        //DateTime result = date + time;

                        // lastMonday is always the Monday before nextSunday.
                        // When today is a Sunday, lastMonday will be tomorrow. 
                        DateTime d = DateTime.Today;
                        int offset = d.DayOfWeek - DayOfWeek.Monday;
                        DateTime lastMonday = d.AddDays(-offset);
                        DateTime nextMonday = lastMonday.AddDays(7);
                        int dayNew = GetDay(model.Day);
                        int dayToday = GetDay(DateTime.Now.DayOfWeek.ToString());
                        DateTime resTime = DateTime.Now;
                        if (dayToday == dayNew) // schedule on same day
                        {
                            if (time.TimeOfDay > DateTime.Now.TimeOfDay)
                            {
                                DateTime date = DateTime.UtcNow.Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;

                            }
                            else // schedule in next week
                            {
                                if (dayNew == 1)
                                {
                                    DateTime date = nextMonday.Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;
                                }
                                else if (dayNew == 2)
                                {
                                    DateTime date = nextMonday.AddDays(1).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;
                                }
                                else if (dayNew == 3)
                                {
                                    DateTime date = nextMonday.AddDays(2).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;
                                }
                                else if (dayNew == 4)
                                {
                                    DateTime date = nextMonday.AddDays(3).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;
                                }
                                else if (dayNew == 5)
                                {
                                    DateTime date = nextMonday.AddDays(4).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;

                                }
                                else if (dayNew == 6)
                                {
                                    DateTime date = nextMonday.AddDays(5).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;

                                }
                                else if (dayNew == 7)
                                {
                                    DateTime date = nextMonday.AddDays(6).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + timen;
                                }
                            }
                        }
                        else if (dayToday < dayNew) //schedule in that week 
                        {
                            int remain = Math.Abs(dayToday - dayNew);
                            DateTime nextDate = DateTime.Now.AddDays(remain).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = nextDate + timen;
                        }
                        else //schedule in next week
                        {
                            if (dayNew == 1)
                            {
                                DateTime date = nextMonday.Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 2)
                            {
                                DateTime date = nextMonday.AddDays(1).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 3)
                            {
                                DateTime date = nextMonday.AddDays(2).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 4)
                            {
                                DateTime date = nextMonday.AddDays(3).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 5)
                            {
                                DateTime date = nextMonday.AddDays(4).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;

                            }
                            else if (dayNew == 6)
                            {
                                DateTime date = nextMonday.AddDays(5).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;

                            }
                            else if (dayNew == 7)
                            {
                                DateTime date = nextMonday.AddDays(6).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                        }
                        DateTime localtime = resTime.Date + time.TimeOfDay;
                        result = _postService.SavePublishingPostDetails(model.SelectedPreferences, model.SocialMedia, model.GroupId, profile, model.ImageArray, model.TextMessage, resTime, userId, ImpersonateUserId, localtime);
                    }
                }


            }
            else
            {
                var profile = _postAPIController.GetSocilaMediaAccountByName(model.SocialMedia);
                foreach (var item in model.Time)
                {
                    DateTime time = Convert.ToDateTime(item);
                    DateTime timenew = FromUTCData(time, model.timeoffset);
                    //int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                    //int nedt = iten.DayId;
                    //int remainday = nedt - dt;

                    //DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                    //TimeSpan time = iten.TimeStampPosted.TimeOfDay;
                    //DateTime result = date + time;

                    // lastMonday is always the Monday before nextSunday.
                    // When today is a Sunday, lastMonday will be tomorrow. 
                    DateTime d = DateTime.Today;
                    int offset = d.DayOfWeek - DayOfWeek.Monday;
                    DateTime lastMonday = d.AddDays(-offset);
                    DateTime nextMonday = lastMonday.AddDays(7);
                    int dayNew = GetDay(model.Day);
                    int dayToday = GetDay(DateTime.Now.DayOfWeek.ToString());
                    DateTime resTime = DateTime.Now;
                    if (dayToday == dayNew) // schedule on same day
                    {
                        if (time.TimeOfDay > DateTime.Now.TimeOfDay)
                        {
                            DateTime date = DateTime.UtcNow.Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;

                        }
                        else // schedule in next week
                        {
                            if (dayNew == 1)
                            {
                                DateTime date = nextMonday.Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 2)
                            {
                                DateTime date = nextMonday.AddDays(1).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 3)
                            {
                                DateTime date = nextMonday.AddDays(2).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 4)
                            {
                                DateTime date = nextMonday.AddDays(3).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                            else if (dayNew == 5)
                            {
                                DateTime date = nextMonday.AddDays(4).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;

                            }
                            else if (dayNew == 6)
                            {
                                DateTime date = nextMonday.AddDays(5).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;

                            }
                            else if (dayNew == 7)
                            {
                                DateTime date = nextMonday.AddDays(6).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + timen;
                            }
                        }
                    }
                    else if (dayToday < dayNew) //schedule in that week 
                    {
                        int remain = Math.Abs(dayToday - dayNew);
                        DateTime nextDate = DateTime.Now.AddDays(remain).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = nextDate + timen;
                    }
                    else //schedule in next week
                    {
                        if (dayNew == 1)
                        {
                            DateTime date = nextMonday.Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;
                        }
                        else if (dayNew == 2)
                        {
                            DateTime date = nextMonday.AddDays(1).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;
                        }
                        else if (dayNew == 3)
                        {
                            DateTime date = nextMonday.AddDays(2).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;
                        }
                        else if (dayNew == 4)
                        {
                            DateTime date = nextMonday.AddDays(3).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;
                        }
                        else if (dayNew == 5)
                        {
                            DateTime date = nextMonday.AddDays(4).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;

                        }
                        else if (dayNew == 6)
                        {
                            DateTime date = nextMonday.AddDays(5).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;

                        }
                        else if (dayNew == 7)
                        {
                            DateTime date = nextMonday.AddDays(6).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = date + timen;
                        }
                    }

                    DateTime localtime = resTime.Date + time.TimeOfDay;
                    result = _postService.SavePublishingPostDetails(model.SelectedPreferences, model.SocialMedia, model.GroupId, profile, model.ImageArray, model.TextMessage, resTime, userId, ImpersonateUserId, localtime);
                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public int GetDay(string day)
        {
            int Day = 0;
            int Day2 = 0;
            if (day == "Mon" || day == "Monday")
            {
                Day = 1;
                Day2 = 7;
            }
            if (day == "Tue" || day == "Tuesday")
            {
                Day = 2;
                Day2 = 8;
            }
            if (day == "Wed" || day == "Wednesday")
            {
                Day = 3;
                Day2 = 9;
            }
            if (day == "Thr" || day == "Thursday")
            {
                Day = 4;
                Day2 = 10;
            }
            if (day == "Fri" || day == "Friday")
            {
                Day = 5;
                Day2 = 11;
            }
            if (day == "Sat" || day == "Saturday")
            {
                Day = 6;
                Day2 = 12;
            }
            if (day == "Sun" || day == "Sunday")
            {
                Day = 7;
                Day2 = 13;
            }
            return Day;
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


        [HttpGet]
        public dynamic GetLeadManage(string PostInformation)
        {
            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            Core.Domain.Users _User = _userService.GetLeadManager(UserId ,model.DayID);
            return PartialView("_LeadGeneration", _User);
        }
        [HttpGet]
        public dynamic GetContentPreference(string PostInformation)
        {

            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            smSubIndustry data = new smSubIndustry();
            data.dataList = _userService.GetContentPreference(UserId,model.DayID);
            return PartialView("_GetContentPreference", data);
        }
        [HttpGet]
        public dynamic SaveAutoPreferenceDetails(string PostInformation)
        {

            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = SessionManager.LoggedInUser.UserID;
            var dataList = _userService.SaveAutoPreferenceDetails(UserId, model.SelectedPreferences, model.SelectedPreferencesName, model.Status ,model.DayID);
            return true;
        }
        [HttpGet]
        public dynamic SaveAutoLandPageDetail(string PostInformation)
        {

            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = SessionManager.LoggedInUser.UserID;
            var dataList = _userService.SaveAutoLandPageDetail(UserId, model.PageID, model.Status , model.DayID);
            return true;
        }



        [AllowAnonymous]
        public async Task<ActionResult> SaveAutoPostDataByPublishingTime()
        {

            List<smPublishingTime> UserTimeDate = _postService.GetUserPublishingTime();
            int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            var DayGroup = UserTimeDate.GroupBy(x => x.UserId + " " + x.Day);
            PublishingModel model = new PublishingModel();
            //JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            smPost result = null;
            foreach (var items in DayGroup)
            {                 
                foreach (var itemss in items)
                {
                   

                    int userId = itemss.UserId;
                      List<smSubIndustry> selectedPreference = _userService.GetContentPreference(userId, itemss.DayId);
                      List<string> selectedPreferences = selectedPreference.Where(x=>x.Preference==true).Select(x => x.SubIndustryId.ToString()).ToList();
                      model.SelectedPreferences = selectedPreferences;
                    var profile = _postAPIController.GetSocilaMediaAccountByName(model.SocialMedia);
                    //foreach (var item in itemss.Time)
                    //{
                    DateTime time = Convert.ToDateTime(itemss.Time);
                        DateTime timenew = FromUTCData(time, model.timeoffset);
                        //int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
                        //int nedt = iten.DayId;
                        //int remainday = nedt - dt;

                        //DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
                        //TimeSpan time = iten.TimeStampPosted.TimeOfDay;
                        //DateTime result = date + time;

                        // lastMonday is always the Monday before nextSunday.
                        // When today is a Sunday, lastMonday will be tomorrow. 
                        DateTime d = DateTime.Today;
                        int offset = d.DayOfWeek - DayOfWeek.Monday;
                        DateTime lastMonday = d.AddDays(-offset);
                        DateTime nextMonday = lastMonday.AddDays(7);
                        int dayNew = GetDay(itemss.Day);
                        int dayToday = GetDay(DateTime.Now.DayOfWeek.ToString());
                        DateTime resTime = DateTime.Now;
                        if (dayToday == dayNew) // schedule on same day
                        {
                            if (time.TimeOfDay > DateTime.Now.TimeOfDay)
                            {
                                DateTime date = DateTime.UtcNow.Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;

                            }
                            else // schedule in next week
                            {
                                if (dayNew == 1)
                                {
                                    DateTime date = nextMonday.Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;
                                }
                                else if (dayNew == 2)
                                {
                                    DateTime date = nextMonday.AddDays(1).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;
                                }
                                else if (dayNew == 3)
                                {
                                    DateTime date = nextMonday.AddDays(2).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;
                                }
                                else if (dayNew == 4)
                                {
                                    DateTime date = nextMonday.AddDays(3).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;
                                }
                                else if (dayNew == 5)
                                {
                                    DateTime date = nextMonday.AddDays(4).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;

                                }
                                else if (dayNew == 6)
                                {
                                    DateTime date = nextMonday.AddDays(5).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;

                                }
                                else if (dayNew == 7)
                                {
                                    DateTime date = nextMonday.AddDays(6).Date;
                                    TimeSpan timen = timenew.TimeOfDay;
                                    resTime = date + itemss.TimeStampPosted;
                                }
                            }
                        }
                        else if (dayToday < dayNew) //schedule in that week 
                        {
                            int remain = Math.Abs(dayToday - dayNew);
                            DateTime nextDate = DateTime.Now.AddDays(remain).Date;
                            TimeSpan timen = timenew.TimeOfDay;
                            resTime = nextDate + itemss.TimeStampPosted;
                        }
                        else //schedule in next week
                        {
                            if (dayNew == 1)
                            {
                                DateTime date = nextMonday.Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;
                            }
                            else if (dayNew == 2)
                            {
                                DateTime date = nextMonday.AddDays(1).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;
                            }
                            else if (dayNew == 3)
                            {
                                DateTime date = nextMonday.AddDays(2).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;
                            }
                            else if (dayNew == 4)
                            {
                                DateTime date = nextMonday.AddDays(3).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;
                            }
                            else if (dayNew == 5)
                            {
                                DateTime date = nextMonday.AddDays(4).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;

                            }
                            else if (dayNew == 6)
                            {
                                DateTime date = nextMonday.AddDays(5).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;

                            }
                            else if (dayNew == 7)
                            {
                                DateTime date = nextMonday.AddDays(6).Date;
                                TimeSpan timen = timenew.TimeOfDay;
                                resTime = date + itemss.TimeStampPosted;
                            }
                        }
                        DateTime localtime = resTime.Date + time.TimeOfDay;
                        if (resTime.Date == DateTime.Now.Date)
                        {
                            result = _postService.AutoSavePublishingPostDetails(model.SelectedPreferences, model.SocialMedia, model.GroupId, profile, model.ImageArray, model.TextMessage, resTime, userId, ImpersonateUserId, localtime);   
                        }
                }
               
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public dynamic GetSavedLeadManage(string PostInformation)
        {
            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            Core.Domain.Users _User = _userService.GetLeadManager(UserId, model.DayID);
            return PartialView("_SavedLeadGeneration ", _User);
        }
        [HttpGet]
        public dynamic GetSavedContentPreference(string PostInformation)
        {
            PublishingModel model = new PublishingModel();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(PostInformation);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            smSubIndustry data = new smSubIndustry();
            data.dataList = _userService.GetContentPreference(UserId, model.DayID);
            return PartialView("_GetSavedContentPreference", data);
        }

        [HttpGet] 
        public dynamic UpdateAutoPreference(string Data)
        {
            PublishingModel model = new PublishingModel();
            //JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<PublishingModel>(Data);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;       
          var s= _userService.UpdateAutoPreference(UserId, model.DayID , model.SelectedPreferences, model.PageID,model.Time);         ;
         return s;

        }

        [HttpGet]
        public dynamic DeleteAutoPreference(string Data)
        {
            PublishingModel model = new PublishingModel();          
            model = JsonConvert.DeserializeObject<PublishingModel>(Data);
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            var s = _userService.DeleteAutoPreference(UserId, model.DayID, model.SelectedPreferences, model.PageID, model.Time);
            return s;

        } 

    }
}