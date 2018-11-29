﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    [Table("smHomeValue")]
    public class smHomeValue
    {
            [Key]
            public int HomeValueId { get; set; }
            public string Address { get; set; }
            public string StreetAddress { get; set; }
            public string Unit { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PostalCode { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }          
            public int TimePeriodId { get; set; }
            public bool IsCompleted { get; set; }
            public int PostId { get; set; }
            public bool Notify { get; set; }
            public Nullable<DateTime> DateSubmit { get; set; }
            public int? userID { get; set; }
            public string IPAddress { get; set; }
                 
      
                 
    }
}
