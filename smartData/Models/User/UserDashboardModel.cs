using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;

namespace smartData.Models.User
{
    public class UserDashboardModel
    {
        public List<smPost> socialMediaPost { get; set; }
        public List<smPreference> socialPreference { get; set; }
        public List<smSocialMediaProfile> socialMediaProfile { get; set; }
        public List<smPost> scheduledPosts { get; set; }
        // public List<smPost> TotalPosts { get; set; }
        public PostCountModel postCount { get; set; }
        public PostCountModelByWeek postCountweek { get; set; }
        public PostCountModelByMonth postCountmonth { get; set; }
        public List<FacebookProfileAccount> FacebookProfile { get; set; }
        public List<TwitterProfileAccount> TwitterProfile { get; set; }
        public List<LinkedInProfileAccount> LinkedInProfile { get; set; }
        public List<GooglePlusProfileAccount> GooglePlusProfile { get; set; }
        public int timeZoneOffset { get; set; }
        public List<smPost> FacebookPosts { get; set; }
        public List<smPost> TwitterPosts { get; set; }
        public List<smPost> LinkedInPosts { get; set; }
    }

    public class PostCountModel
    {
        public int FacebookPosts { get; set; }
        public int TwitterPosts { get; set; }
        public int LinkedInPosts { get; set; }
        public int GooglePlusPosts { get; set; }
    }

    public class PostCountModelByWeek
    {
        public int FacebookPosts { get; set; }
        public int TwitterPosts { get; set; }
        public int LinkedInPosts { get; set; }
        public int GooglePlusPosts { get; set; }
    }

    public class PostCountModelByMonth
    {
        public int FacebookPosts { get; set; }
        public int TwitterPosts { get; set; }
        public int LinkedInPosts { get; set; }
        public int GooglePlusPosts { get; set; }
    }

    public class FacebookProfileAccount
    {
        public string SocialMedia { get; set; }
        public bool IsAccountActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class LinkedInProfileAccount
    {
        public string SocialMedia { get; set; }
        public bool IsAccountActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TwitterProfileAccount
    {
        public string SocialMedia { get; set; }
        public bool IsAccountActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GooglePlusProfileAccount
    {
        public string SocialMedia { get; set; }
        public bool IsAccountActive { get; set; }
        public bool IsDeleted { get; set; }
    } 
}