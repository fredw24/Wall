using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wall.Models
{
    public class Comments
    {

        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int commentId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required]
        public string comment {get; set;}

        // The MySQL DATETIME type can be represented by a DateTime
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public int userId {get; set;}
        public Users User {get; set;}
        public int messageId {get; set;}
        public Messages Message {get; set;}

       
        

    }
}