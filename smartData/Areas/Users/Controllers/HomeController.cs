using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using smartData.Areas.Users.Models.User;
using System.Web.Script.Serialization;
using Core.Domain;
using Microsoft.AspNet.Identity;
using ServiceLayer.Services;
using smartData.Models.User;
using Sparkle.LinkedInNET;
using Sparkle.LinkedInNET.OAuth2;
using System.Threading.Tasks;
using Sparkle.LinkedInNET.Profiles;
using TweetSharp;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using ASPSnippets.GoogleAPI;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using System.Threading;
using smartData.Common;
using Newtonsoft.Json.Linq;
using smartData.Filter;
using smartData.Messages;
using smartData.Areas.Admin.Controllers;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using ServiceLayer.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace smartData.Areas.Users.Controllers
{

    class PostClass
    {
        public int PostId { get; set; }
        public string Description { get; set; }
        // public List<string> Ids { get; set; }
        public string Name { get; set; }
        public List<string> ImageArray { get; set; }
        public string Url { get; set; }
        public int PreferenceId { get; set; }
        public string Preference { get; set; }
    }

    //[Authorize]
    // [AuthorizedRoles(Roles = "User")]
    [CheckSession]
    public class HomeController : Controller
    {
        #region Global Variables
        ServiceLayer.Interfaces.IHomeService _homeService;
        IHomeAPIController _homeAPIController;
        IUserService _userService;
        IPostService _postService;
        IManageAdminAPIController _manageAdminAPIController;
        ServiceLayer.Interfaces.IManageContentService _manageContentService;
        public List<ScreenPermissionList> obj;
        public List<ScreenPermissionList> objScreenPermissionList = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        #region constructor
        public HomeController(HomeService homeService, IHomeAPIController homeAPIController, IManageAdminAPIController manageAdminAPIController, IUserService userService, ServiceLayer.Interfaces.IManageContentService manageContentService, IPostService postService)
        {
            _homeService = homeService;
            _homeAPIController = homeAPIController;
            _manageAdminAPIController = manageAdminAPIController;
            _userService = userService;
            _manageContentService = manageContentService;
            _postService = postService;
        }
        #endregion

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        //
        // GET: /Users/Home/
        // [AuditLog]


        public ActionResult Index()
        {

            var tenantId = this.RouteData.Values["tenant"]; // Get some tenant specific data 

            MembershipUser mu = Membership.GetUser();
            int UserId = 0;
            if (mu == null)
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            }
            else
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            }

            // Authenticated social media
            // var socialMedia = _homeAPIController.GetSocialMedia(UserId);
            // Get Scheduled Posts
            var scheduledPosts = _homeAPIController.GetScheduledPosts(UserId);
            TempData["SiteUrl"] = ConfigurationManager.AppSettings["SiteUrl"];
            // Get Post counts
            UserDashboardModel userModel = new UserDashboardModel();
            var posts = _homeAPIController.GetTotalPosts(UserId);
            ViewBag.totallikesfacebook = posts.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.LikesCount);
            ViewBag.totalCommentsfacebook = posts.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.CommentsCount);
            ViewBag.totallikestwitter = posts.Where(x => x.SocialMedia.ToLower() == "twitter").Sum(x => x.LikesCount);
            ViewBag.totallikeslinkedin = posts.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.LikesCount);
            ViewBag.totalCommentslinkedin = posts.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.CommentsCount);

            int fbpost = 0, lkpost = 0, twpost = 0, gppost = 0;

            fbpost = posts.Where(x => x.SocialMedia.ToLower() == "facebook").Count();
            lkpost = posts.Where(x => x.SocialMedia.ToLower() == "twitter").Count();
            twpost = posts.Where(x => x.SocialMedia.ToLower() == "linkedin").Count();
            gppost = posts.Where(x => x.SocialMedia.ToLower() == "googleplus").Count();

            //foreach (var post in posts)
            //{
            //    if (post.SocialMedia.ToLower() == "facebook")
            //    {
            //        fbpost = fbpost + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "twitter")
            //    {
            //        twpost = twpost + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "linkedin")
            //    {
            //        lkpost = lkpost + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "googleplus")
            //    {
            //        gppost = gppost + 1;
            //    }
            //}

            userModel.FacebookPosts = new List<smPost>();
            userModel.TwitterPosts = new List<smPost>();
            userModel.LinkedInPosts = new List<smPost>();
            var socialPosts = _homeAPIController.GetTotalPostsByUser(UserId);           
            userModel.FacebookPosts.AddRange(socialPosts.Where(x => x.SocialMedia.ToLower() == "facebook").ToList());
            userModel.TwitterPosts.AddRange(socialPosts.Where(x => x.SocialMedia.ToLower() == "twitter").ToList());
            userModel.LinkedInPosts.AddRange(socialPosts.Where(x => x.SocialMedia.ToLower() == "linkedin").ToList());




            //foreach (var post in socialPosts)
            //{
            //    if (post.SocialMedia.ToLower() == "facebook")
            //    {
            //        userModel.FacebookPosts.Add(post);
            //    }
            //    else if (post.SocialMedia.ToLower() == "twitter")
            //    {
            //        userModel.TwitterPosts.Add(post);
            //    }
            //    else if (post.SocialMedia.ToLower() == "linkedin")
            //    {
            //        userModel.LinkedInPosts.Add(post);
            //    }
            //}

            // userModel.socialMediaProfile = socialMedia;
            userModel.scheduledPosts = scheduledPosts;
            #region postcountmodel
            PostCountModel postCountModel = new PostCountModel();
            postCountModel.FacebookPosts = fbpost;
            postCountModel.LinkedInPosts = lkpost;
            postCountModel.TwitterPosts = twpost;
            postCountModel.GooglePlusPosts = gppost;
            #endregion
            userModel.postCount = postCountModel;


            DateTime? StartDateWeek = DateTime.UtcNow.AddDays(-7);
            DateTime? EndDateWeek = DateTime.UtcNow;

            var postsByweek = _homeService.GetTotalPostsByDate(UserId, StartDateWeek, EndDateWeek);
            ViewBag.weeklikesfacebook = postsByweek.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.LikesCount);
            ViewBag.weekCommentsfacebook = postsByweek.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.CommentsCount);
            ViewBag.weeklikestwitter = postsByweek.Where(x => x.SocialMedia.ToLower() == "twitter").Sum(x => x.LikesCount);
            ViewBag.weeklikeslinkedin = postsByweek.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.LikesCount);
            ViewBag.weekCommentslinkedin = postsByweek.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.CommentsCount);


            int fbpostweek = 0, lkpostweek = 0, twpostweek = 0;
            fbpostweek = postsByweek.Where(x => x.SocialMedia.ToLower() == "facebook").Count();
            lkpostweek = postsByweek.Where(x => x.SocialMedia.ToLower() == "twitter").Count();
            twpostweek = postsByweek.Where(x => x.SocialMedia.ToLower() == "linkedin").Count();
 


            ////  start Get Total Clicks 

           List<smViewsModel> Totalclickdata  = _homeService.GetTotalClicks(UserId);     
     
            /// Total Count
           ViewBag.totalClicksFacebook = Totalclickdata.Where(x => x.SocialMedia.ToLower().Contains("facebook")).Count();
           ViewBag.totalClicksTwitter = Totalclickdata.Where(x => x.SocialMedia.ToLower().Contains("twitter")).Count();
           ViewBag.totalClicksLinkedInd = Totalclickdata.Where(x => x.SocialMedia.ToLower().Contains("linkedin")).Count();
            /// Total Count
            /// 
            /// Total Week Count
            ViewBag.weeklikestotalClicksFacebook = Totalclickdata.Where(x => x.DateEntered >= StartDateWeek && x.DateEntered <= EndDateWeek && x.SocialMedia.ToLower().Contains("facebook")).Count();
            ViewBag.weeklikestotalClicksTwitter = Totalclickdata.Where(x => x.DateEntered >= StartDateWeek && x.DateEntered <= EndDateWeek && x.SocialMedia.ToLower().Contains("twitter")).Count();
            ViewBag.weeklikestotalClickslinkedin = Totalclickdata.Where(x => x.DateEntered >= StartDateWeek && x.DateEntered <= EndDateWeek && x.SocialMedia.ToLower().Contains("linkedin")).Count();
            /// Total Week Count
            /// 

            /// Total Month Count
            DateTime? StartDateMonths = DateTime.UtcNow.AddMonths(-1);
            DateTime? EndDateMonths = DateTime.UtcNow;

            ViewBag.MonthtotalClicksFacebook = Totalclickdata.Where(x => x.DateEntered >= StartDateMonths && x.DateEntered <= EndDateMonths && x.SocialMedia.ToLower().Contains("facebook")).Count();
            ViewBag.MonthtotalClicksTwitter = Totalclickdata.Where(x => x.DateEntered >= StartDateMonths && x.DateEntered <= EndDateMonths && x.SocialMedia.ToLower().Contains("twitter")).Count();
            ViewBag.MonthtotalClickslinkedin = Totalclickdata.Where(x => x.DateEntered >= StartDateMonths && x.DateEntered <= EndDateMonths && x.SocialMedia.ToLower().Contains("linkedin")).Count();                
            /// Total Month Count
            /// 

            //// End Get Total Clicks 
            //foreach (var post in postsByweek)
            //{
            //    if (post.SocialMedia.ToLower() == "facebook")
            //    {
            //        fbpostweek = fbpostweek + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "twitter")
            //    {
            //        twpostweek = twpostweek + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "linkedin")
            //    {
            //        lkpostweek = lkpostweek + 1;
            //    }
            //}

            PostCountModelByWeek postCountModelweek = new PostCountModelByWeek();
            postCountModelweek.FacebookPosts = fbpostweek;
            postCountModelweek.LinkedInPosts = lkpostweek;
            postCountModelweek.TwitterPosts = twpostweek;
            userModel.postCountweek = postCountModelweek;


            DateTime? StartDateMonth = DateTime.UtcNow.AddMonths(-1);
            DateTime? EndDateMonth = DateTime.UtcNow;
            var postsByMonth = _homeService.GetTotalPostsByDate(UserId, StartDateMonth, EndDateMonth);
            ViewBag.monthlikesfacebook = postsByMonth.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.LikesCount);
            ViewBag.monthCommentsfacebook = postsByMonth.Where(x => x.SocialMedia.ToLower() == "facebook").Sum(x => x.CommentsCount);
            ViewBag.monthlikestwitter = postsByMonth.Where(x => x.SocialMedia.ToLower() == "twitter").Sum(x => x.LikesCount);
            ViewBag.monthlikeslinkedin = postsByMonth.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.LikesCount);
            ViewBag.monthCommentslinkedin = postsByMonth.Where(x => x.SocialMedia.ToLower() == "linkedin").Sum(x => x.CommentsCount);
            int fbpostmonth = 0, lkpostmonth = 0, twpostmonth = 0;


            fbpostmonth = postsByMonth.Where(x => x.SocialMedia.ToLower() == "facebook").Count();
            lkpostmonth = postsByMonth.Where(x => x.SocialMedia.ToLower() == "twitter").Count();
            twpostmonth = postsByMonth.Where(x => x.SocialMedia.ToLower() == "linkedin").Count();
            
            //foreach (var post in postsByMonth)
            //{
            //    if (post.SocialMedia.ToLower() == "facebook")
            //    {
            //        fbpostmonth = fbpostmonth + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "twitter")
            //    {
            //        twpostmonth = twpostmonth + 1;
            //    }
            //    else if (post.SocialMedia.ToLower() == "linkedin")
            //    {
            //        lkpostmonth = lkpostmonth + 1;
            //    }
            //}

            PostCountModelByMonth postCountModelmonth = new PostCountModelByMonth();
            postCountModelmonth.FacebookPosts = fbpostmonth;
            postCountModelmonth.LinkedInPosts = lkpostmonth;
            postCountModelmonth.TwitterPosts = twpostmonth;
            userModel.postCountmonth = postCountModelmonth;
            var socialMedia = _homeAPIController.GetSocialMedia(UserId);
            // Bind posts
            userModel.socialMediaProfile = socialMedia;

            return View(userModel);
        }


        public ActionResult GetPostsCountsByDate(string startDate, string endDate)
        {
            int UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (startDate != "Invalid date" && endDate != "Invalid date")
            {
                StartDate = Convert.ToDateTime(startDate);
                EndDate = Convert.ToDateTime(endDate);
            }
            var posts = _homeService.GetTotalPostsByDate(UserId, StartDate, EndDate);
            int fbpost = 0, lkpost = 0, twpost = 0;
            foreach (var post in posts)
            {
                if (post.SocialMedia.ToLower() == "facebook")
                {
                    fbpost = fbpost + 1;
                }
                else if (post.SocialMedia.ToLower() == "twitter")
                {
                    twpost = twpost + 1;
                }
                else if (post.SocialMedia.ToLower() == "linkedin")
                {
                    lkpost = lkpost + 1;
                }
            }
            UserDashboardModel userModel = new UserDashboardModel();
            PostCountModel postCountModel = new PostCountModel();
            postCountModel.FacebookPosts = fbpost;
            postCountModel.LinkedInPosts = lkpost;
            postCountModel.TwitterPosts = twpost;
            userModel.postCount = postCountModel;
            return Json(userModel, JsonRequestBehavior.AllowGet);
        }




        #region Facebook Authentication
        [AllowAnonymous]
        [AuditLog(Event = " Facebook authentication", Message = "Add facebook authentication")]
        public ActionResult AddFacebookAuth()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FBClientID"],
                client_secret = ConfigurationManager.AppSettings["FBClientSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                auth_type = "reauthenticate",
                response_type = "code",
                scope = "email,publish_stream,publish_actions,public_profile,pages_show_list,manage_pages,publish_pages"
                // scope = "pages_show_list,manage_pages,publish_pages"
                /// scope = "publish_stream, publish_actions,pages_show_list"
                //   scope = "email,publish_stream,publish_actions,manage_pages,publish_pages,public_profile"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        //[AuditLog(Event = " Facebook authentication", Message = "Add facebook authentication")]
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FBClientID"],
                client_secret = ConfigurationManager.AppSettings["FBClientSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            // Short Lived Token
            var accessToken = result.access_token;

            var permissions = "https://graph.facebook.com/me/permissions?access_token=" + accessToken;

            dynamic result2 = fb.Post("oauth/access_token", new
            {
                grant_type = "fb_exchange_token",
                client_id = ConfigurationManager.AppSettings["FBClientID"],
                client_secret = ConfigurationManager.AppSettings["FBClientSecret"],
                fb_exchange_token = accessToken
            });

            var longLivedToken = result2.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            //dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");


            //string me = Convert.ToString(fb.Get("me?fields=link,first_name,last_name,email,gender,picture,age_range"));
            string me = Convert.ToString(fb.Get("me?fields=link,first_name,last_name,email,gender,picture,age_range,accounts"));

            // Initialize Facebook profile model
            FacebookProfileModel facebookProfileModel = new FacebookProfileModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            facebookProfileModel = serializer.Deserialize<FacebookProfileModel>(me);
            // Call method for saving info to DB
            string response = SaveUserFBProfileInfo(facebookProfileModel, longLivedToken);


            if (response.ToLower() == "already added")
            {
                TempData["Message"] = "Account is already added with the email you are using for authentication with Facebook account.";
            }
            else
            {
                string responseFromPageSave = SaveUserFBPageDetail(facebookProfileModel);
                TempData["Message"] = response;
            }

            /// Set default preference to facebook wall
            SetFacebookDefaultPost();
            /// Set default preference to facebook wall
            // Set the auth cookie
            // FormsAuthentication.SetAuthCookie(facebookProfileModel.email, false);
            return RedirectToAction("ManageSocialMedia", "Home");
        }
        /// <summary>
        ///  Set default preference to facebook wall
        /// </summary>
        public void SetFacebookDefaultPost()
        {

            int UserID = SessionManager.LoggedInUser.UserID;
            smFacebookDefaultPreference fbDefault = new smFacebookDefaultPreference();
            fbDefault.CreatedDate = DateTime.UtcNow;
            fbDefault.PageId = 0;
            fbDefault.Type = 0;
            fbDefault.UpdatedDate = DateTime.UtcNow;
            fbDefault.userID = UserID;
            fbDefault.isActive = true;
            fbDefault.isDeleted = false;
            _homeAPIController.SetFacebookDefaultPost(fbDefault);

        }


        private string SaveUserFBProfileInfo(FacebookProfileModel model, string token)
        {
            int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            string response = string.Empty;
            try
            {
                smSocialMediaProfile _profile = new smSocialMediaProfile();
                _profile.Email = model.email;
                _profile.FirstName = model.first_name;
                _profile.Gender = model.gender;
                _profile.SocialMediaId = Convert.ToString(model.Id);
                _profile.LastName = model.last_name;
                _profile.Link = model.link;
                _profile.Photo = model.picture.data.url;
                _profile.IsActive = true;
                _profile.SocialMedia = "Facebook";
                _profile.AccessToken = token;
                _profile.IsAccountActive = true;
                MembershipUser mu = Membership.GetUser();
                int UserID = SessionManager.LoggedInUser.UserID;
                if (ImpersonateUserId != 0)
                {
                    _profile.CreatedBy = ImpersonateUserId;
                    _profile.AccountActiveBy = ImpersonateUserId;
                }
                else
                {
                    _profile.CreatedBy = UserID;
                    _profile.AccountActiveBy = UserID;
                }
                _profile.CreatedDate = DateTime.UtcNow;
                _profile.UserId = Convert.ToInt32(UserID);
                TempData["UserId"] = UserID;
                // Create User
                response = _homeAPIController.Create(_profile);
            }
            catch (Exception ex)
            {
                response = "Error occured while adding account";
            }
            return response;
        }
        private string SaveUserFBPageDetail(FacebookProfileModel model)
        {
            int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            string response = string.Empty;
            try
            {
                smFacebookPageDetail _pageDetail = new smFacebookPageDetail();
                foreach (var item in model.accounts.data)
                {
                    _pageDetail.PageAccessToken = item.access_token;
                    _pageDetail.PageName = item.name;
                    _pageDetail.PageId = item.id;
                    _pageDetail.PageName = item.name;
                    _pageDetail.CreatedDate = DateTime.UtcNow;
                    _pageDetail.IsActive = true;
                    _pageDetail.IsDeleted = false;
                    _pageDetail.UserFaceBookID = model.Id;
                    _pageDetail.UserId = SessionManager.LoggedInUser.UserID;
                    _pageDetail.category = item.category;
                    response = _homeAPIController.SaveUserFBPageDetail(_pageDetail);

                }






            }
            catch (Exception ex)
            {
                response = "Error occured while adding account";
            }
            return response;
        }
        #endregion

        #region Linkedin Authentication
        [AllowAnonymous]
        [AuditLog(Event = " LinkedIn authentication", Message = "Add linkedin authentication")]
        public void AddLinkedinAuth()
        {
            var client_id = ConfigurationManager.AppSettings["LinkedinClientID"];
            var client_secret = ConfigurationManager.AppSettings["LinkedinClientSecret"];
            var config = new LinkedInApiConfiguration(client_id, client_secret);
            var api = new LinkedInApi(config);

            var scope = AuthorizationScope.ReadBasicProfile | AuthorizationScope.ReadEmailAddress | AuthorizationScope.WriteShare;
            var state = Guid.NewGuid().ToString();
            var redirectUrl = ConfigurationManager.AppSettings["SiteUrl"] + "/Home/OAuth2";
            var url = api.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl);
            Response.Redirect(url.OriginalString);
        }

        [AuditLog]
        public async Task<ActionResult> OAuth2(string code, string state, string error, string error_description)
        {
            if (!string.IsNullOrEmpty(error) || !string.IsNullOrEmpty(error_description))
            {

            }
            else
            {
                var client_id = ConfigurationManager.AppSettings["LinkedinClientID"];
                var client_secret = ConfigurationManager.AppSettings["LinkedinClientSecret"];
                var config = new LinkedInApiConfiguration(client_id, client_secret);
                var api = new LinkedInApi(config);
                var redirectUrl = ConfigurationManager.AppSettings["SiteUrl"] + "/Home/OAuth2";
                dynamic userToken = await api.OAuth2.GetAccessTokenAsync(code, redirectUrl);
                // Fetch User Token
                string response = FetchUserProfile(userToken);
                if (response.ToLower() == "already added")
                {
                    TempData["Message"] = "Account is already added with the email you are using for authentication with LinkedIn account.";
                }
                else
                {
                    TempData["Message"] = response;
                }
            }

            return RedirectToAction("ManageSocialMedia", "Home");
        }

        private string FetchUserProfile(dynamic usertoken)
        {
            var client_id = ConfigurationManager.AppSettings["LinkedinClientID"];
            var client_secret = ConfigurationManager.AppSettings["LinkedinClientSecret"];
            var config = new LinkedInApiConfiguration(client_id, client_secret);
            var api = new LinkedInApi(config);
            var user = new UserAuthorization(usertoken.AccessToken);
            string culture = "en-US";
            var acceptLanguages = new string[] { culture ?? "en-US", "fr-FR", };
            var fields = FieldSelector.For<Sparkle.LinkedInNET.Profiles.Person>().WithId().WithFirstName().WithLastName().WithEmailAddress().WithPictureUrl();
            var profile = api.Profiles.GetMyProfile(user, acceptLanguages, fields);
            // Save to database table smSocialMediaProfile
            string response = SaveUserLinkedinProfileInfo(profile, usertoken.AccessToken);
            return response;
        }

        private string SaveUserLinkedinProfileInfo(dynamic profile, string token)
        {
            string response = string.Empty;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            try
            {

                smSocialMediaProfile _profile = new smSocialMediaProfile();
                _profile.Email = profile.EmailAddress;
                _profile.FirstName = profile.Firstname;
                _profile.Gender = string.Empty;
                _profile.SocialMediaId = profile.Id;
                _profile.LastName = profile.Lastname;
                _profile.Link = string.Empty;
                _profile.Photo = profile.PictureUrl;
                _profile.IsActive = true;
                _profile.IsAccountActive = true;
                _profile.SocialMedia = "LinkedIn";
                _profile.AccessToken = token;
                MembershipUser mu = Membership.GetUser();
                int UserID = SessionManager.LoggedInUser.UserID;
                _profile.UserId = Convert.ToInt32(UserID);
                TempData["UserId"] = UserID;
                if (ImpersonateUserId != 0)
                {
                    _profile.CreatedBy = ImpersonateUserId;
                    _profile.AccountActiveBy = ImpersonateUserId;
                }
                else
                {
                    _profile.CreatedBy = UserID;
                    _profile.AccountActiveBy = UserID;
                }
                _profile.CreatedDate = DateTime.UtcNow;
                // Create User
                response = _homeAPIController.Create(_profile);
            }
            catch (Exception ex)
            {
                response = "Error Occured while adding account";
            }
            return response;
        }
        #endregion

        #region Twitter Authentication
        [AuditLog(Event = " Twitter authentication", Message = "Add twitter authentication")]
        public ActionResult AddTwitterAuth()
        {
            var consumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
            // Step 1 - Retrieve an OAuth Request Token
            TwitterService service = new TwitterService(consumerKey, consumerSecret);
            // This is the registered callback URL
            // OAuthRequestToken requestToken = service.GetRequestToken("http://localhost:55038/Home/AuthorizeCallback");
            OAuthRequestToken requestToken = service.GetRequestToken("http://sm4y.cc/Home/AuthorizeCallback");
            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback at http://dev.twitter.com
        [AuditLog]
        public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            if (oauth_token != null && oauth_verifier != null)
            {
                // Consumer Key
                var consumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
                // Consumer Secret
                var consumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
                var requestToken = new OAuthRequestToken { Token = oauth_token };

                // Step 3 - Exchange the Request Token for an Access Token
                TwitterService service = new TwitterService(consumerKey, consumerSecret);
                OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

                // Step 4 - User authenticates using the Access Token
                service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
                TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions { IncludeEntities = true, SkipStatus = false });
                //ViewModel.Message = string.Format("Your username is {0}", user.ScreenName);
                string response = SaveTwitterProfileInfo(user, accessToken.Token, accessToken.TokenSecret);
                if (response.ToLower() == "already added")
                {
                    TempData["Message"] = "Account is already added with the email you are using for authentication with Twitter account.";
                }
                else
                {
                    TempData["Message"] = response;
                }
            }
            return RedirectToAction("ManageSocialMedia");
        }

        private string SaveTwitterProfileInfo(TwitterUser profile, string token, string tokenSecret)
        {
            string response = string.Empty;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            try
            {
                smSocialMediaProfile _profile = new smSocialMediaProfile();
                _profile.Email = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;
                string[] name = null;
                if (profile.Name.Contains(" "))
                {
                    name = profile.Name.Split(' ');
                    firstname = name[0];
                    lastname = name[1];
                }
                _profile.FirstName = firstname;
                _profile.Gender = string.Empty;
                _profile.SocialMediaId = Convert.ToString(profile.Id);
                _profile.LastName = lastname;
                _profile.Link = profile.Url;
                _profile.Photo = profile.ProfileImageUrl;
                _profile.IsActive = true;
                _profile.IsAccountActive = true;
                _profile.SocialMedia = "Twitter";
                _profile.AccessToken = token;
                _profile.TokenSecret = tokenSecret;
                MembershipUser mu = Membership.GetUser();
                int UserID = SessionManager.LoggedInUser.UserID;
                _profile.UserId = Convert.ToInt32(UserID);
                TempData["UserId"] = UserID;

                if (ImpersonateUserId != 0)
                {
                    _profile.CreatedBy = ImpersonateUserId;
                    _profile.AccountActiveBy = ImpersonateUserId;
                }
                else
                {
                    _profile.CreatedBy = UserID;
                    _profile.AccountActiveBy = UserID;
                }
                _profile.CreatedDate = DateTime.UtcNow;
                // Create User
                response = _homeAPIController.Create(_profile);
            }
            catch (Exception ex)
            {
                response = "Error Occured while adding account";
            }
            return response;
        }
        #endregion

        #region Google Plus Authentication
        [AuditLog(Event = "Google authentication", Message = "Add google authentication")]
        public void AddGooglePlusAuth()
        {
            var gc = new GoogleConnect();
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GooglePlusClientId"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GooglePlusClientSecret"];
            // GoogleConnect.RedirectUri = "http://localhost:55038/Home/GoogleCallBack";
            GoogleConnect.RedirectUri = "http://stagingwin.com/SocialMediaBroadcast/Home/GoogleCallBack";
            GoogleConnect.Authorize("https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/plus.me");
        }

        [AuditLog]
        public ActionResult GoogleCallBack()
        {
            string response = string.Empty;

            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GooglePlusClientId"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GooglePlusClientSecret"];
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GooglePlusResponseModel model = new GooglePlusResponseModel();
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                model = JsonConvert.DeserializeObject<GooglePlusResponseModel>(json);
                response = SaveUserGooglePlusInfo(model);
                if (response.ToLower() == "already added")
                {
                    TempData["Message"] = "Account is already added with the email you are using for authentication with Google Plus account.";
                }
                else
                {
                    TempData["Message"] = response;
                }
            }
            if (Request.QueryString["error"] == "access_denied")
            {
                TempData["Message"] = "Access Denied";
            }

            return RedirectToAction("ManageSocialMedia");
        }

        public string SaveUserGooglePlusInfo(GooglePlusResponseModel model)
        {
            string response = string.Empty;
            try
            {
                smSocialMediaProfile _profile = new smSocialMediaProfile();
                _profile.Email = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;
                string[] name = null;
                if (model.name.givenName.Contains(" "))
                {
                    name = model.name.givenName.Split(' ');
                    firstname = name[0];
                    lastname = name[1];
                }
                _profile.FirstName = firstname;
                _profile.Gender = string.Empty;
                _profile.SocialMediaId = Convert.ToString(model.id);
                _profile.LastName = lastname;
                _profile.Link = string.Empty;
                _profile.Photo = model.image.url;
                _profile.IsActive = true;
                _profile.IsAccountActive = true;
                _profile.SocialMedia = "GooglePlus";
                _profile.AccessToken = string.Empty;
                MembershipUser mu = Membership.GetUser();
                //string UserID = mu.ProviderUserKey.ToString();
                int UserID = SessionManager.LoggedInUser.UserID;
                _profile.UserId = Convert.ToInt32(UserID);
                TempData["UserId"] = UserID;
                // Create User
                response = _homeAPIController.Create(_profile);
            }
            catch (Exception ex)
            {
                response = "Error Occured while adding account";
            }
            return response;
        }
        #endregion

        #region Update Social Media Status
        [HttpPost]
        [AuditLog(Event = "Update status", Message = "Update social account status")]
        public JsonResult UpdateSocialMediaStatus(string AccountInformation)
        {
            try
            {
                MembershipUser mu = Membership.GetUser();
                //int UserID = Convert.ToInt32(mu.ProviderUserKey);
                int UserID = SessionManager.LoggedInUser.UserID;
                dynamic fields = JsonConvert.DeserializeObject<dynamic>(AccountInformation);
                var response = _homeAPIController.SocialMediaStatus(AccountInformation, UserID);
            }
            catch (Exception ex)
            {

            }
            return Json(true);
        }
        #endregion

        #region Manage Social Media Accounts
        public ActionResult ManageSocialMedia()
        {
            int userId = SessionManager.LoggedInUser.UserID;
            UserDashboardModel userDashboardModel = new UserDashboardModel();
            // Get social media profile
            var socialMedia = _homeAPIController.GetSocialMediaAccountsProfie(userId);
            userDashboardModel.FacebookProfile = new List<FacebookProfileAccount>();
            userDashboardModel.LinkedInProfile = new List<LinkedInProfileAccount>();
            userDashboardModel.TwitterProfile = new List<TwitterProfileAccount>();
            userDashboardModel.GooglePlusProfile = new List<GooglePlusProfileAccount>();
            foreach (var social in socialMedia)
            {
                if (social.SocialMedia.ToLower() == "facebook")
                {
                    FacebookProfileAccount fb = new FacebookProfileAccount();
                    fb.SocialMedia = social.SocialMedia;
                    fb.IsAccountActive = social.IsAccountActive;
                    fb.IsDeleted = social.IsDeleted;
                    userDashboardModel.FacebookProfile.Add(fb);
                }
                else if (social.SocialMedia.ToLower() == "linkedin")
                {
                    LinkedInProfileAccount li = new LinkedInProfileAccount();
                    li.SocialMedia = social.SocialMedia;
                    li.IsAccountActive = social.IsAccountActive;
                    li.IsDeleted = social.IsDeleted;
                    userDashboardModel.LinkedInProfile.Add(li);
                }
                else if (social.SocialMedia.ToLower() == "twitter")
                {
                    TwitterProfileAccount tw = new TwitterProfileAccount();
                    tw.SocialMedia = social.SocialMedia;
                    tw.IsAccountActive = social.IsAccountActive;
                    tw.IsDeleted = social.IsDeleted;
                    userDashboardModel.TwitterProfile.Add(tw);
                }
                else if (social.SocialMedia.ToLower() == "googleplus")
                {
                    GooglePlusProfileAccount gp = new GooglePlusProfileAccount();
                    gp.SocialMedia = social.SocialMedia;
                    gp.IsAccountActive = social.IsAccountActive;
                    gp.IsDeleted = social.IsDeleted;
                    userDashboardModel.GooglePlusProfile.Add(gp);
                }
            }
            userDashboardModel.socialMediaProfile = socialMedia;
            return View(userDashboardModel);

        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete social account", Message = "Delete social media account")]
        public JsonResult DeleteSocialAccount(int id)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = SessionManager.LoggedInUser.UserID;
            int ImpersonateUserID = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            bool status = _homeAPIController.DeleteSocialAccount(id, userId, ImpersonateUserID);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update social account status", Message = "Update social account status to active")]
        public JsonResult UpdateSocialStatusActive(int id)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = SessionManager.LoggedInUser.UserID;
            int ImpersonateUserID = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            bool status = _homeAPIController.UpdateSocialAccount(id, userId, stat, ImpersonateUserID);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update social account status", Message = "Update social account status to deactive")]
        public JsonResult UpdateSocialStatusDeactive(int id)
        {
            bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = SessionManager.LoggedInUser.UserID;
            int ImpersonateUserID = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            bool status = _homeAPIController.UpdateSocialAccount(id, userId, stat, ImpersonateUserID);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Manage Post

        public ActionResult Post()
        {
            return View();

        }

        [AuditLog(Event = "Manage Past Post", Message = "View past post section")]
        public ActionResult ManagePost()
        {
            int userId = SessionManager.LoggedInUser.UserID;
            UserDashboardModel SmPost = new UserDashboardModel();
            // Get social media profile
            var socialMedia = _homeAPIController.GetSocialMediaPost(userId);
            SmPost.socialMediaPost = socialMedia;
            return View(SmPost);
        }

        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete Post", Message = "Delete scheduled post")]
        public JsonResult DeleteSocialPost(int id)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            userId = SessionManager.LoggedInUser.UserID;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            bool status = _homeAPIController.DeleteSocialPost(id, userId, ImpersonateUserId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [AuditLog(Event = "Manage scheduled Post", Message = "Manage scheduled post")]
        public ActionResult ManageFuturePost()
        {
            int userId = SessionManager.LoggedInUser.UserID;
            UserDashboardModel SmPost = new UserDashboardModel();
            var socialMedia = _homeAPIController.GetFuturePost(userId);
            SmPost.socialMediaPost = socialMedia;
            return View();
        }

        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Edit scheduled Post", Message = "Manage scheduled post")]
        public JsonResult EditContent(int id)
        {
            smPost content = _homeAPIController.EditContent(id);
            return Json(new { result = content });
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Update scheduled Post", Message = "Update scheduled post")]
        public ActionResult UpdateContent(string PostInformation)
        {
            PostClass postedContent = new PostClass();
            postedContent = JsonConvert.DeserializeObject<PostClass>(PostInformation);
            bool status = false;
            try
            {
                int userId = SessionManager.LoggedInUser.UserID;
                int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
                status = _homeAPIController.UpdateContent(postedContent.PostId, postedContent.Description, postedContent.Name, postedContent.ImageArray, postedContent.Url, userId, ImpersonateUserId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        #region upload Image
        [AuditLog(Event = "Upload New Image", Message = "Upload new image in edit mode")]
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
        #endregion

        #endregion
        #region Get Scheduled Post
        // [AuditLog(Event = "Get scheuled post", Message = "Get scheduled post")] 
        public ActionResult GetScheduledPost()
        {
            var scheduledPosts = _homeAPIController.GetScheduledPosts(SessionManager.LoggedInUser.UserID);
            UserDashboardModel modal = new UserDashboardModel
            {
                scheduledPosts = scheduledPosts
            };
            return PartialView("_partialScheduledPost", modal);
        }

        public ActionResult ManageUserAccount()
        {
            try
            {
                int Id = SessionManager.LoggedInUser.UserID;
                Core.Domain.Users _User = _userService.GetUserById(Id);
                ViewBag.UserTitle = _User.Title;

                if (_User.countries != null)
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });
                    foreach (var item in _User.countries)
                    {

                        list.Add(new SelectListItem { Text = item.Name, Value = item.CountryId.ToString() });

                    }



                    ViewBag.country = list;
                }


                if (_User.Photo != null && _User.Photo != "")
                {
                    ViewBag.imgUrl = _User.Photo;
                }
                return View(_User);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserAccount(Core.Domain.Users user)
        {
            try
            {
                int UserId = SessionManager.LoggedInUser.UserID;
                Core.Domain.Users _User = _userService.GetUserById(user.UserId);
                //Core.Domain.Users newUser = new Core.Domain.Users();
                _User.FirstName = user.FirstName;
                _User.LastName = user.LastName;
                _User.Title = user.Title;
                _User.PhoneNumber = user.PhoneNumber;
                _User.DisplayName = user.DisplayName;
                if (!string.IsNullOrEmpty(user.Photo))
                {
                    if (!user.Photo.Contains("/Images"))
                    {
                        _User.Photo = "/Images/WallImages/" + user.UserId + "/" + user.Photo;
                    }
                    else
                    {
                        _User.Photo = user.Photo;
                    }

                }
                else
                {
                    _User.Photo = null;
                }

                _User.CompanyName = user.CompanyName;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    _User.Password = smartData.Common.CommonFunction.Encrypt(user.Password);
                    _User.ConfirmPassword = smartData.Common.CommonFunction.Encrypt(user.ConfirmPassword);
                }
                else
                {
                    _User.ConfirmPassword = _User.Password;
                }
                _User.UserId = _User.UserId;
                _User.Email = user.Email;
                _User.Shortname = user.Shortname;
                _User.ModifiedDate = DateTime.UtcNow;
                if (SessionManager.LoggedInUser.ImpersonateUserId != 0)
                {
                    _User.ModifiedBy = SessionManager.LoggedInUser.ImpersonateUserId;
                }
                else
                {
                    _User.ModifiedBy = SessionManager.LoggedInUser.UserID;
                }
                _User.LandingPageList = user.LandingPageList;
                _User.Country = user.Country;
                _User.ProvinceState = user.ProvinceState;
                _User.Status = user.Status;
                _User.FacebookProfile = user.FacebookProfile;
                _User.TwitterProfile = user.TwitterProfile;
                _User.LinkedInProfile = user.LinkedInProfile;
                _userService.UpdateUsers(_User);

                return Json(new { Response = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" });
            }
        }



        #region upload Image
        [AuditLog(Event = "Upload Profile Image", Message = "Upload profile image")]
        public ActionResult SaveUploadedFileforProfile()
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
                       // bool FileisExist = System.IO.Directory.Exists(pathString + "/"+userId + Path.GetExtension(file.FileName));
                        bool FileisExist = System.IO.File.Exists(pathString + "/" + userId + Path.GetExtension(file.FileName));
                        if (FileisExist)
                        {
                            System.IO.File.Delete(pathString + "/"+userId + Path.GetExtension(file.FileName));
                        }
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        string extension = Path.GetExtension(file.FileName);

                        path = string.Format("{0}\\{1}", pathString, userId + extension);
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
        #endregion

        public string DownloadImage()
        {
            string path = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                //var originalDirectory = Server.MapPath("~/Images/WallImages/ContentImages/");
                //string pathString = originalDirectory;
                //bool isExists = System.IO.Directory.Exists(pathString);
                //if (!isExists)
                //    System.IO.Directory.CreateDirectory(pathString);
                //path = string.Format("{0}\\{1}", pathString, "Test.jpg");

                // webClient.DownloadFile("http://t3.gstatic.com/images?q=tbn:ANd9GcRO5Q6PWTVwMPTLwudQkePTO_H0X1T3aTjyThbLTO0sTponwbkmgRiM2SyBW7xxF0EUjK1hK51q", path);
                byte[] data = webClient.DownloadData("http://t3.gstatic.com/images?q=tbn:ANd9GcRO5Q6PWTVwMPTLwudQkePTO_H0X1T3aTjyThbLTO0sTponwbkmgRiM2SyBW7xxF0EUjK1hK51q");

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        var originalDirectory = Server.MapPath("~/Images/WallImages/ContentImages/");
                        string pathString = originalDirectory;
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var name = yourImage.RawFormat.Guid + ".jpg";
                        path = string.Format("{0}\\{1}", pathString, name);
                        yourImage.Save(path);
                    }
                }
            }
            return "";
        }


        [HttpGet]
        public ActionResult gettimeZoneOffset(int offset)
        {
            MembershipUser mu = Membership.GetUser();
            int UserId = 0;
            if (mu == null)
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            }
            else
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
                // UserId = Convert.ToInt32(mu.ProviderUserKey);
            }

            // Authenticated social media
            var socialMedia = _homeAPIController.GetSocialMedia(UserId);
            // Get Scheduled Posts
            var scheduledPosts = _homeAPIController.GetScheduledPosts(UserId).OrderBy(x => x.PostDate).ToList();
            TempData["SiteUrl"] = ConfigurationManager.AppSettings["SiteUrl"];
            // Get Post counts
            var posts = _homeAPIController.GetTotalPosts(UserId);
            int fbpost = 0, lkpost = 0, twpost = 0, gppost = 0;
            foreach (var post in posts)
            {
                if (post.SocialMedia.ToLower() == "facebook")
                {
                    fbpost = fbpost + 1;
                }
                else if (post.SocialMedia.ToLower() == "twitter")
                {
                    twpost = twpost + 1;
                }
                else if (post.SocialMedia.ToLower() == "linkedin")
                {
                    lkpost = lkpost + 1;
                }
                else if (post.SocialMedia.ToLower() == "googleplus")
                {
                    gppost = gppost + 1;
                }
            }
            UserDashboardModel userModel = new UserDashboardModel();
            userModel.socialMediaProfile = socialMedia;
            userModel.scheduledPosts = scheduledPosts;
            #region postcountmodel
            PostCountModel postCountModel = new PostCountModel();
            postCountModel.FacebookPosts = fbpost;
            postCountModel.LinkedInPosts = lkpost;
            postCountModel.TwitterPosts = twpost;
            postCountModel.GooglePlusPosts = gppost;
            #endregion
            userModel.postCount = postCountModel;
            userModel.timeZoneOffset = offset;
            //return View(userModel);
            return PartialView("_partialScheduledPost", userModel);
        }


        #endregion

        #region Create Post
        public ActionResult CreatePost()
        {
            MembershipUser mu = Membership.GetUser();
            int UserId = 0;
            if (mu == null)
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            }
            else
            {
                UserId = smartData.Common.SessionManager.LoggedInUser.UserID;
            }

            // Authenticated social media
            var socialMedia = _homeAPIController.GetSocialMedia(UserId);
            UserDashboardModel model = new UserDashboardModel();
            model.socialMediaProfile = socialMedia;
            return View(model);
        }
        #endregion


        public ActionResult SchedularCalendar()
        {
            int industryId = smartData.Common.SessionManager.LoggedInUser.IndustryId;
            int userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            DateTime fromDate = DateTime.UtcNow.AddMonths(-1);
            DateTime toDate = DateTime.UtcNow;
            List<smContentLibrary> contentlist = new List<smContentLibrary>(0);
            var prefrencelist = _homeService.GetPreference(userId);
            var events = _homeService.GetScheduleEvents(userId, fromDate, toDate).Select(x => x.ContentId);
            contentlist = _homeService.GetCategoryListforcalender(industryId, prefrencelist, userId,"");
            ViewBag.List = contentlist.GroupBy(x => x.Title).ToList();
            return View(contentlist);
        }

        //[HttpGet]
        //public string SaveCalenderEvents(string id, string title, string date, int offset, string socialarr, string evntId)
        //{
        //    smScheduleEvents events = new smScheduleEvents();
        //    string output = Regex.Match(id, @"\d+").Value;
        //    int Id = Int32.Parse(output);           
        //    int UserId = SessionManager.LoggedInUser.UserID;
        //    int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
        //    DateTime datenew = Convert.ToDateTime(date);
        //    DateTime newTime = DateTime.Now;
        //    DateTime newLocalTime = DateTime.Now;
        //    var newevents = _homeService.GetEventsBydate(UserId, datenew);
        //    List<smScheduleEvents> list = new List<smScheduleEvents>();
        //    //foreach (var item in newevents)
        //    //{
        //    //    DateTime dates = item.LocalTime.Date;
        //    //    if (dates == datenew.Date)
        //    //    {
        //    //        list.Add(item);
        //    //    }
        //    //}
        //    //if (list.Count > 0)
        //    //{
        //    //    var nels = list.LastOrDefault();
        //    //    if (DateTime.Now.Date == nels.LocalTime.Date)
        //    //    {
        //    //        newTime = DateTime.Now.AddHours(1);
        //    //    }
        //    //    else
        //    //    {
        //    //        newTime = nels.LocalTime.AddHours(1);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (datenew < newTime)
        //    //    {                 
        //    //        newTime = DateTime.Now.AddHours(1);
        //    //    }
        //    //    else
        //    //    {
        //    //        newTime = datenew;
        //    //    }

        //    //}
        //    DateTime time = FromUTCData(datenew, offset);
        //    var fj = title.Split('/');

        //    if (fj[1].Length > 15)
        //    {
        //        events.Title = fj[1].Substring(0, 15);
        //    }
        //    else
        //    {
        //        events.Title = fj[1];
        //    }
        //    events.ContentId = Id;
        //    events.UserId = UserId;
        //    events.ScheduleTime = time;
        //    events.LocalTime = datenew;
        //    events.Evnt_Id = evntId;
        //    string[] sarr;
        //    sarr = socialarr.Split(',');
        //    foreach (var iten in sarr)
        //    {
        //        if (iten == "Facebook")
        //        {
        //            events.IsFacebook = true;
        //        }
        //        else if (iten == "LinkedIn")
        //        {
        //            events.IsLinkedIn = true;
        //        }
        //        else if (iten == "Twitter")
        //        {
        //            events.IsTwitter = true;
        //        }
        //    }
        // var savedEvents =  _homeService.AddScheduleEvents(events);
        //    smContentLibrary content = _manageContentService.GetContentById(Id);
        //    //_manageContentService.UpdateContentOnSchedular(Id, socialarr);
        //    var socialMediaProfile = _postService.GetSocilaMediaAccountByName(content.SocialMedia.ToLower());
        //    var post = _postService.SaveScheduledPosts(content, time, UserId, ImpersonateUserId, socialMediaProfile.Fid, socialarr, savedEvents.EventId);
        //    return "true";
        //}



        [HttpGet]
        public string SaveCalenderEvents(string id, string GroupId ,string title, string date, int offset, string socialarr, string evntId)
        {
            smScheduleEvents events = new smScheduleEvents();
            int num = int.Parse(Regex.Match(id, @"\d+").Value);
            int userID = SessionManager.LoggedInUser.UserID;
            int impersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            DateTime time = Convert.ToDateTime(date);
            DateTime now = DateTime.Now;
            DateTime time3 = DateTime.Now;
            this._homeService.GetEventsBydate(userID, time);
            new List<smScheduleEvents>();
            DateTime dt = FromUTCData(new DateTime?(time), offset);
            string[] strArray = title.Split(new char[] { '/' });
            if (strArray[1].Trim().Length > 15)
            {
                events.Title = strArray[2].Trim().Substring(0, 15);
            }
            else
            {
                events.Title = strArray[2].Trim();
            }
            events.ContentId = num;
            events.UserId = userID;
            events.ScheduleTime = dt;
            events.LocalTime = time;
            events.Evnt_Id = evntId;
            foreach (string str2 in socialarr.Split(new char[] { ',' }))
            {
                switch (str2)
                {
                    case "Facebook":
                        events.IsFacebook = true;
                        break;

                    case "LinkedIn":
                        events.IsLinkedIn = true;
                        break;

                    case "Twitter":
                        events.IsTwitter = true;
                        break;
                }
            }
            smScheduleEvents events2 = this._homeService.AddScheduleEvents(events);
            smContentLibrary contentById = this._manageContentService.GetContentById(num);
            contentById.GroupId = GroupId.Trim();
            
            smSocialMediaProfile socilaMediaAccountByName = this._postService.GetSocilaMediaAccountByName(contentById.SocialMedia.ToLower());
            this._postService.SaveScheduledPosts(contentById, dt, userID, impersonateUserId, 0, socialarr, events2.EventId);
            return "true";
        }

        [HttpGet]
        public ActionResult PostNowContent(string id, string title, string date, int offset, string socialarr, string evntId)
        {
            smScheduleEvents events = new smScheduleEvents();
            int num = int.Parse(Regex.Match(id, @"\d+").Value);
            int userID = SessionManager.LoggedInUser.UserID;
            int impersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            DateTime time = Convert.ToDateTime(date);
            DateTime now = DateTime.Now;
            DateTime time3 = DateTime.Now;
            this._homeService.GetEventsBydate(userID, time);
            new List<smScheduleEvents>();
            DateTime dt = FromUTCData(new DateTime?(time), offset);
            events.Title = title;            
            events.ContentId = num;
            events.UserId = userID;
            events.ScheduleTime = dt;
            events.LocalTime = time;
            events.Evnt_Id = evntId;

            foreach (string str2 in socialarr.Split(new char[] { ',' }))
            {
                switch (str2)
                {

                    case "Facebook":
                        events.IsFacebook = true;
                        break;

                    case "LinkedIn":
                        events.IsLinkedIn = true;
                        break;

                    case "Twitter":
                        events.IsTwitter = true;
                        break;

                }
            }

            smScheduleEvents events2 = this._homeService.AddScheduleEvents(events);
            smContentLibrary contentById = this._manageContentService.GetContentById(num);
            smSocialMediaProfile socilaMediaAccountByName = this._postService.GetSocilaMediaAccountByName(contentById.SocialMedia.ToLower());
            smPost SMPost = this._postService.SaveScheduledPosts(contentById, dt, userID, impersonateUserId, 0, socialarr, events2.EventId);
            return RedirectToAction("PostNow", "Schedule", new { PostId = SMPost.PostId, socialarr = socialarr });
        }




        //[HttpGet]
        //public string SaveCalenderEventsDrag(string id, string title, string date, int offset, string Ismove, string socialarr)
        //{
        //    smScheduleEvents events = new smScheduleEvents();
        //    string output = Regex.Match(id, @"\d+").Value;
        //    int EventId = Int32.Parse(output);
        //    int UserId = SessionManager.LoggedInUser.UserID;
        //    int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
        //    DateTime time = FromUTCData(Convert.ToDateTime(date), offset);
        //    smScheduleEvents contents = _manageContentService.GetContentId(EventId);
        //    events.ContentId = contents.ContentId;

        //    var eventsDeatails = _homeService.GetEventById(EventId);


        //    if (title.Length > 15)
        //    {
        //        events.Title = title.Substring(0, 15);
        //    }
        //    else
        //    {
        //        events.Title = title;
        //    }
        //    events.UserId = UserId;
        //    events.ScheduleTime = time;
        //    events.LocalTime = Convert.ToDateTime(date);
        //    events.IsFacebook = eventsDeatails.IsFacebook;
        //    events.IsLinkedIn = eventsDeatails.IsLinkedIn;
        //    events.IsTwitter = eventsDeatails.IsTwitter;
        //    smScheduleEvents eventId = new smScheduleEvents();
        //    if (Ismove == "true")
        //    {
        //        _postService.DeleteScheduledPosts(EventId, UserId, ImpersonateUserId);
        //        eventId = _homeService.AddScheduleEvents(events);
        //    }
        //    else
        //    {
        //        eventId = _homeService.AddScheduleEvents(events);
        //    }
        //    smContentLibrary content = _manageContentService.GetContentById(contents.ContentId);
        //    var socialMediaProfile = _postService.GetSocilaMediaAccountByName(content.SocialMedia.ToLower());

        //    string list = "";

        //    if (eventsDeatails.IsFacebook == true)
        //    {
        //        list = "Facebook";
        //    }
        //    if (eventsDeatails.IsLinkedIn == true)
        //    {
        //        list = list + "," + "LinkedIn";
        //    }
        //    if (eventsDeatails.IsTwitter == true)
        //    {
        //        list = list + "," + "Twitter ";
        //    }
        //    var posts = _postService.SaveScheduledPosts(content, time, UserId, ImpersonateUserId, socialMediaProfile.Fid, list, eventId.EventId);
        //    return "true";
        //}






        [HttpGet]
        public string SaveCalenderEventsDrag(string id, string title, string date, int offset, string Ismove, string socialarr)
        {
            smScheduleEvents events = new smScheduleEvents();
            int num = int.Parse(Regex.Match(id, @"\d+").Value);
            int userID = SessionManager.LoggedInUser.UserID;
            int impersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            DateTime time = FromUTCData(new DateTime?(Convert.ToDateTime(date)), offset);
            smScheduleEvents contentId = this._manageContentService.GetContentId(num);
            events.ContentId = contentId.ContentId;
            smScheduleEvents eventById = this._homeService.GetEventById(num);
            if (title.Length > 15)
            {
                events.Title = title.Substring(0, 15);
            }
            else
            {
                events.Title = title;
            }
            events.UserId = userID;
            events.ScheduleTime = time;
            events.LocalTime = Convert.ToDateTime(date);
            events.IsFacebook = eventById.IsFacebook;
            events.IsLinkedIn = eventById.IsLinkedIn;
            events.IsTwitter = eventById.IsTwitter;
            smContentLibrary contentById = this._manageContentService.GetContentById(contentId.ContentId);
            smSocialMediaProfile socilaMediaAccountByName = this._postService.GetSocilaMediaAccountByName(contentById.SocialMedia.ToLower());
            string socialarrd = "";
            if (eventById.IsFacebook)
            {
                socialarrd = "Facebook";
            }
            if (eventById.IsLinkedIn)
            {
                socialarrd = socialarrd + ",LinkedIn";
            }
            if (eventById.IsTwitter)
            {
                socialarrd = socialarrd + ",Twitter ";
            }
            smScheduleEvents events4 = new smScheduleEvents();
            if (Ismove == "true")
            {
                this._postService.DeleteScheduledPosts(num, userID, impersonateUserId);
                events4 = this._homeService.AddScheduleEvents(events);
                foreach (smPost post in this._postService.GetPostsBYEventId(num))
                {
                    this._postService.UpdateScheduledPosts(post.PostId, time, userID, impersonateUserId, events4.EventId);
                }
            }
            else
            {
             //   socilaMediaAccountByName.Fid = 2;
                events4 = this._homeService.AddScheduleEvents(events);
               // this._postService.SaveScheduledPosts(contentById, time, userID, impersonateUserId, socilaMediaAccountByName.Fid, socialarrd, events4.EventId);
                this._postService.SaveScheduledPosts(contentById, time, userID, impersonateUserId, 0, socialarrd, events4.EventId);
            }
            return "true";
        }

        [HttpGet]
        public dynamic GetContentListScheduler(string data)
        {
            int industryId = smartData.Common.SessionManager.LoggedInUser.IndustryId;
            int userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            DateTime fromDate = DateTime.UtcNow.AddMonths(-1);
            DateTime toDate = DateTime.UtcNow;
            List<smContentLibrary> contentlist = new List<smContentLibrary>(0);
            var prefrencelist = _homeService.GetPreference(userId);
            var events = _homeService.GetScheduleEvents(userId, fromDate, toDate).Select(x => x.ContentId);
            contentlist = _homeService.GetCategoryListforcalender(industryId, prefrencelist, userId,data);
            ViewBag.List = contentlist.GroupBy(x => x.Title).ToList();
            return PartialView("_SchedularNewContent", contentlist);
        }


        //[HttpGet]
        //public string UpdateScheduleEvents(string id, string title, string date, int offset)
        //{
        //    smScheduleEvents events = new smScheduleEvents();
        //    string output = Regex.Match(id, @"\d+").Value;
        //    int EventId = Int32.Parse(output);
        //    int UserId = SessionManager.LoggedInUser.UserID;
        //    int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
        //    DateTime time = FromUTCData(Convert.ToDateTime(date), offset);
        //    smScheduleEvents contents = _manageContentService.GetContentId(EventId);
        //    events.ContentId = contents.ContentId;
        //    var eventsDeatails = _homeService.GetEventById(EventId);
        //    if (title.Length > 15)
        //    {
        //        events.Title = title.Substring(0, 15);
        //    }
        //    else
        //    {
        //        events.Title = title;
        //    }
        //    events.UserId = UserId;
        //    events.ScheduleTime = time;
        //    events.LocalTime = Convert.ToDateTime(date);
        //    events.IsFacebook = eventsDeatails.IsFacebook;
        //    events.IsLinkedIn = eventsDeatails.IsLinkedIn;
        //    events.IsTwitter = eventsDeatails.IsTwitter;
        //    events.EventId = EventId;
        //    _homeService.UpdateScheduleEvents(events);           
        //    smContentLibrary content = _manageContentService.GetContentById(contents.ContentId);
        //    var socialMediaProfile = _postService.GetSocilaMediaAccountByName(content.SocialMedia.ToLower());
        //    string list = "";
        //    if (eventsDeatails.IsFacebook == true)
        //    {
        //        list = "Facebook";
        //    }
        //    if (eventsDeatails.IsLinkedIn == true)
        //    {
        //        list = list + "," + "LinkedIn";
        //    }
        //    if (eventsDeatails.IsTwitter == true)
        //    {
        //        list = list + "," + "Twitter ";
        //    }
        //    var postByEventId = _postService.GetPostsBYEventId(EventId);
        //    foreach (var item in postByEventId)
        //    {
        //        if (item.IsPosted == true)
        //        {
        //            var posts = _postService.SaveScheduledPosts(content, time, UserId, ImpersonateUserId, socialMediaProfile.Fid, list, EventId);
        //        }
        //        else
        //        {
        //            var posts = _postService.UpdateScheduledPosts(item.PostId, time, UserId, ImpersonateUserId);
        //        }           
        //    }           
        //    return "true";
        //}




        [HttpGet]
        public string UpdateScheduleEvents(string id, string title, string date, int offset)
        {
            smScheduleEvents events = new smScheduleEvents();
            int num = int.Parse(Regex.Match(id, @"\d+").Value);
            int userID = SessionManager.LoggedInUser.UserID;
            int impersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            DateTime dt = FromUTCData(new DateTime?(Convert.ToDateTime(date)), offset);
            smScheduleEvents contentId = this._manageContentService.GetContentId(num);
            events.ContentId = contentId.ContentId;
            smScheduleEvents eventById = this._homeService.GetEventById(num);
            if (title.Length > 15)
            {
                events.Title = title.Substring(0, 15);
            }
            else
            {
                events.Title = title;
            }
            events.UserId = userID;
            events.ScheduleTime = dt;
            events.LocalTime = Convert.ToDateTime(date);
            events.IsFacebook = eventById.IsFacebook;
            events.IsLinkedIn = eventById.IsLinkedIn;
            events.IsTwitter = eventById.IsTwitter;
            events.EventId = num;
            this._homeService.UpdateScheduleEvents(events);
            smContentLibrary contentById = this._manageContentService.GetContentById(contentId.ContentId);
            smSocialMediaProfile socilaMediaAccountByName = this._postService.GetSocilaMediaAccountByName(contentById.SocialMedia.ToLower());
            string socialarrd = "";
            if (eventById.IsFacebook)
            {
                socialarrd = "Facebook";
            }
            if (eventById.IsLinkedIn)
            {
                socialarrd = socialarrd + ",LinkedIn";
            }
            if (eventById.IsTwitter)
            {
                socialarrd = socialarrd + ",Twitter ";
            }
            foreach (smPost post in this._postService.GetPostsBYEventId(num))
            {
                if (post.IsPosted)
                {
                    this._postService.SaveScheduledPosts(contentById, dt, userID, impersonateUserId, socilaMediaAccountByName.Fid, socialarrd, num);
                }
                else
                {
                    this._postService.UpdateScheduledPosts(post.PostId, dt, userID, impersonateUserId, post.EventId);
                }
            }
            return "true";
        }


        public ActionResult GetEventsBydate(string date)
        {
            int UserId = SessionManager.LoggedInUser.UserID;
            DateTime datenew = Convert.ToDateTime(date);
            var events = _homeService.GetEventsBydate(UserId, datenew);
            List<smScheduleEvents> list = new List<smScheduleEvents>();

            foreach (var item in events)
            {
                DateTime dates = item.LocalTime.Date;
                if (dates == datenew.Date)
                {
                    list.Add(item);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public static DateTime FromUTCData(DateTime? dt, int timezoneOffset)
        {
            DateTime newDate = dt.Value + new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
            return newDate;
        }


        //public string DeleteCalenderEvents(string id)
        //{
        //    smScheduleEvents events = new smScheduleEvents();
        //    int Id = Convert.ToInt32(id);
        //    int UserId = SessionManager.LoggedInUser.UserID;
        //    int ImpersonateUserId = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
        //    var post = _postService.DeleteScheduledPosts(Id, UserId, ImpersonateUserId);
        //    return "true";

        //}


        public string DeleteCalenderEvents(string id)
        {
            new smScheduleEvents();
            int num = Convert.ToInt32(id);
            int userID = SessionManager.LoggedInUser.UserID;
            int impersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            this._postService.DeleteScheduledPosts(num, userID, impersonateUserId);
            foreach (smPost post in this._postService.GetPostsBYEventId(num))
            {
                if (!post.IsPosted)
                {
                    this._postService.DeleteScheduledEvents(post.PostId, userID, impersonateUserId);
                }
            }
            return "true";
        }



        [HttpGet]
        public JsonResult GetEvents()
        {
            int UserId = SessionManager.LoggedInUser.UserID;
            DateTime fromDate = DateTime.UtcNow.AddMonths(-1);
            DateTime toDate = DateTime.UtcNow;
            var events = _homeService.GetScheduleEvents(UserId, fromDate, toDate);
            var eventList = from e in events
                            select new
                            {
                                id = e.EventId,
                                title = e.Title,
                                start = e.LocalTime.ToString("s"),
                                allDay = false,
                                contentPageID = e.ContentCreatedId
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetContentById(string Id, string eventid)
        {
            int contentId = Convert.ToInt32(Id);
            int EventId = Convert.ToInt32(eventid);
            smContentLibrary content = _manageContentService.GetContentById(contentId);
            var events = _homeService.GetEventById(EventId);
            var conte = _manageContentService.GetContentByTitle(content.Title).Select(x => x.SocialMedia).Distinct();
            return Json(new { status = content, social = events }, JsonRequestBehavior.AllowGet);
            //return Json(content, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContentByNewId(string Id, string eventid)
        {
            int contentId = Convert.ToInt32(Id);
            smContentLibrary content = _manageContentService.GetContentById(contentId);
            var events = _homeService.GetEventByNewId(eventid);
            var conte = _manageContentService.GetContentByTitle(content.Title).Select(x => x.SocialMedia).Distinct();
            return Json(new { status = content, social = events }, JsonRequestBehavior.AllowGet);
            //return Json(content, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetContentByEventId(string eventid)
        {
            int EventId = Convert.ToInt32(eventid);
            var events = _homeService.GetEventById(EventId);
            smContentLibrary content = _manageContentService.GetContentById(events.ContentId);
            return Json(new { status = content, social = events }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetContentId(string contentId)
        {
            int Id = Convert.ToInt32(contentId);
            smScheduleEvents content = _manageContentService.GetContentId(Id);
            return Json(content, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public string UpdateContentStatusOnCalender(int id)
        {
            bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            int impersonateUserID = smartData.Common.SessionManager.LoggedInUser.ImpersonateUserId;
            int IndustryId = smartData.Common.SessionManager.LoggedInUser.IndustryId;
            smContentLibrary status = _manageContentService.UpdateContentOnSchedular(id, userId, impersonateUserID, IndustryId);
            return "true";
        }






        //public ActionResult Index(string linkid)
        //{
        //    // Uncomment to use javascript-based solution to use your own Google Analytics code
        //    //ViewData["url"] = string.Format("http://bit.ly/{0}", linkid);
        //    //return View();

        //    Response.StatusCode = 302;
        //    Response.RedirectLocation = (string.IsNullOrWhiteSpace(linkid)
        //                         ? "http://restazured.com"
        //                         : string.Format("http://bit.ly/{0}", linkid));
        //    return new ContentResult();
        //}
        public ActionResult SessionExpire()
        {
            ViewBag.Message = CustomMessages.YourSessionExpired;
            return RedirectToAction("Login", "Account");
            //return View();
        }





        #region Manage Leads


        public ActionResult ManageLeads()
        {
            int userId = SessionManager.LoggedInUser.UserID;
            UserDashboardModel userDashboardModel = new UserDashboardModel();
            // Get social media profile
            var socialMedia = _homeAPIController.GetSocialMediaAccountsProfie(userId);
            userDashboardModel.FacebookProfile = new List<FacebookProfileAccount>();
            userDashboardModel.LinkedInProfile = new List<LinkedInProfileAccount>();
            userDashboardModel.TwitterProfile = new List<TwitterProfileAccount>();
            userDashboardModel.GooglePlusProfile = new List<GooglePlusProfileAccount>();
            foreach (var social in socialMedia)
            {
                if (social.SocialMedia.ToLower() == "facebook")
                {
                    FacebookProfileAccount fb = new FacebookProfileAccount();
                    fb.SocialMedia = social.SocialMedia;
                    fb.IsAccountActive = social.IsAccountActive;
                    fb.IsDeleted = social.IsDeleted;
                    userDashboardModel.FacebookProfile.Add(fb);
                }
                else if (social.SocialMedia.ToLower() == "linkedin")
                {
                    LinkedInProfileAccount li = new LinkedInProfileAccount();
                    li.SocialMedia = social.SocialMedia;
                    li.IsAccountActive = social.IsAccountActive;
                    li.IsDeleted = social.IsDeleted;
                    userDashboardModel.LinkedInProfile.Add(li);
                }
                else if (social.SocialMedia.ToLower() == "twitter")
                {
                    TwitterProfileAccount tw = new TwitterProfileAccount();
                    tw.SocialMedia = social.SocialMedia;
                    tw.IsAccountActive = social.IsAccountActive;
                    tw.IsDeleted = social.IsDeleted;
                    userDashboardModel.TwitterProfile.Add(tw);
                }
                else if (social.SocialMedia.ToLower() == "googleplus")
                {
                    GooglePlusProfileAccount gp = new GooglePlusProfileAccount();
                    gp.SocialMedia = social.SocialMedia;
                    gp.IsAccountActive = social.IsAccountActive;
                    gp.IsDeleted = social.IsDeleted;
                    userDashboardModel.GooglePlusProfile.Add(gp);
                }
            }
            userDashboardModel.socialMediaProfile = socialMedia;
            //return View(userDashboardModel);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageLeads(Core.Domain.smHomeValue data)
        {
            try
            {
                int UserId = SessionManager.LoggedInUser.UserID;
                var dara = _homeService.EditLeads(data);
                return Json(new { Response = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" });
            }
        }

        #endregion




    }
}

