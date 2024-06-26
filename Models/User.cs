﻿using System.ComponentModel.DataAnnotations;

namespace CrimeAdminAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; }
        public string UserEmailConfirmed { get; set; }
        public string UserPhone { get; set; }
        public string UserPhoneConfirmed { get; set; }
        public string UserPassword { get; set; }
        public string UserPasswordConfirmed { get; set;}
        
        public string UserImage { get;set; }

    }
    
}
