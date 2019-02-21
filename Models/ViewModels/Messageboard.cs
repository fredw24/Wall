using System.Collections.Generic;

namespace Wall.Models
{
  public class Messageboard
  {
    public List<Messages> Messages { get; set; }
    public Users User { get; set; }

    public Messages Message {get; set;}

    public Comments Comment {get; set;}
  }
}