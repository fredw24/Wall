using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wall.Models
{
    public class Users
    {

        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int userId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        // The MySQL DATETIME type can be represented by a DateTime
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        public string Confirm {get; set;}

        public List<Messages> Messages {get; set;}
        public List<Comments> Comments {get; set;}
       
    }

    public class LoginUsers
    {

        public string LogEmail { get; set; }
        public string LogPassword { get; set; }
    }
}