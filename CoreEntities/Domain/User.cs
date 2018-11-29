
namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public partial class Users : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression("^[a-zA-Z_ ' -/ ]*$", ErrorMessage = "First Name is invalid.")]
        [StringLength(50, ErrorMessage = "The First Name must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[a-zA-Z_ ' -/ ]*$", ErrorMessage = "Last Name is invalid.")]
        [StringLength(50, ErrorMessage = "The Last Name must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string LastName { get; set; }
     
        [StringLength(50, ErrorMessage = "Display must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string DisplayName { get; set; }      
       
        [StringLength(50, ErrorMessage = "The Company Name must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string CompanyName { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [RegularExpression("^[a-zA-Z0-9_-]{3,45}$", ErrorMessage = "Invalid Entry - min 3 characters")]
        public string Shortname { get; set; }        

        public Nullable<int> UserTypeId { get; set; }
        public string AuthFacebookId { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        // [Display(Name = "SuperAdmin")]
        public bool IsSuperAdmin { get; set; }
        //Modify Details
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Photo { get; set; }
        //End
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }

        public bool? AcceptTerms { get; set; }
        public byte[] AcceptTermsDate { get; set; }
        public string AcceptTermsIP { get; set; }
        public Nullable<System.DateTime> LastChangePasswordDate { get; set; }
        public string FacebookProfile { get; set; }
        public string TwitterProfile { get; set; }
        public string LinkedInProfile { get; set; }
        
        [NotMapped]
       // [Required]
        public string RoleIDs { get; set; }
        [NotMapped]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public  string ConfirmPassword { get; set; }
        //   [Required(ErrorMessage = "Country  is required.")]
        public string Country { get; set;}
           
        public string Status { get; set; }
    //    [Required(ErrorMessage = "Province is required.")]
        public string ProvinceState { get; set; }
        // Added By SRohit  new Changes
       


        // Added By SRohit

        

     
         [NotMapped]
        public List<Roles> RolesList { get; set; }
        // Added By SRohit  new Changes
        [NotMapped]
        public List<smLandingPagesCollections> LandingPageList { get; set; }
        [NotMapped]
        public List<smLandingPagesForUser> UserLandingPageLIst { get; set; }
        [NotMapped]
        public List<Countries> countries { get; set; }

        


        // Added By SRohit
     
        //[NotMapped]
        //public bool IsExsits { get; set; }

        [NotMapped]
        public string ActiveDisplay
        {
            get
            {
                // If the value given to the side is negative,
                // then set it to 0
                if (Active == false)
                    return "No";
                else
                    return "Yes";
            }
        }
    }
    //For Insert User
    [NotMapped]
    public partial class UserInsert : Users
    {
        [Required]
        public  string Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }
        [Required]
        public  string ConfirmPassword
        {
            get { return base.Password; }
            set { base.Password = value; }
        }
    }
    public class smLandingPagesForUser
    {
        public int id { get; set; }
        public int id_user { get; set; }
        public int id_landingpage { get; set; }
        public bool IsActive { get; set; }
    }
    public class  smLandingPagesCollections
    {
        public int id { get; set; }
        public int id_industry { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public int sortorder { get; set; }
        public bool active { get; set; }
    }
}
