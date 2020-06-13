using System;
namespace Jst4Fun.Models
{
    public class CommResponce
    {
        public int status { get; set; }
        public string msg { get; set; }
        public virtual object data { get; set; }
    }
}
