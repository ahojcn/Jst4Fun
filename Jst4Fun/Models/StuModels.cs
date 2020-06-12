using System;
namespace Jst4Fun.Models
{
    public class StudentModels
    {}

    public class StuRegisterReq
    {
        public string stu_id { get; set; }
        public string name { get; set; }
        public string nick_name { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string pwd { get; set; }
        public string cpwd { get; set; }
    }
}
