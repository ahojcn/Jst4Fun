using System;
using Dapper;
using Jst4Fun.Utils;

namespace Jst4Fun.Models
{
    public class StuProfile
    {
        public int id { get; set; }
        public string stu_id { get; set; }
        public string name { get; set; }
        public string nick_name { get; set; }
        public int gender { get; set; }
        public string gender_str
        {
            get
            {
                string s;
                switch (this.gender)
                {
                    case 0:
                        s = "未知";
                        break;
                    case 1:
                        s = "男生";
                        break;
                    case 2:
                        s = "女生";
                        break;
                    default:
                        s = "xxx";
                        break;
                }
                return s;
            }
        }
        public string email { get; set; }
        public bool is_active { get; set; }

        public StuProfile(int id, string stu_id, string name, string nick_name, int gender, string email, bool is_active)
        {
            this.id = id;
            this.stu_id = stu_id;
            this.name = name;
            this.nick_name = nick_name;
            this.gender = gender;
            this.email = email;
            this.is_active = is_active;
        }
    }

    public class StuNews
    {
        public int id { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string create_time { get; set; }

        public StuNews(int id, string title, string detail, string create_time)
        {
            this.id = id;
            this.title = title;
            this.detail = detail;
            this.create_time = create_time;
        }
    }

    public class StuMatch
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public string create_time { get; set; }
        public bool need_upload { get; set; }

        public StuMatch(int id, string title, string start_time, string end_time, string description, string detail, string create_time, bool need_upload)
        {
            this.id = id;
            this.title = title;
            this.start_time = start_time;
            this.end_time = end_time;
            this.description = description;
            this.detail = detail;
            this.create_time = create_time;
            this.need_upload = need_upload;
        }
    }

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

        public bool Check()
        {
            try
            {
                if (!Fun.CheckEmail(this.email) || !Fun.CheckPhone(this.phone)
                || !Fun.CheckGender(this.gender) || !(this.pwd == this.cpwd))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }

    public class StuRegisterResp : CommResponce
    { }

    public class StuLoginReq
    {
        public string stu_id { get; set; }
        public string pwd { get; set; }
    }

    public class StuLoginResp : CommResponce
    { }

    public class StuNewsReq
    { }

    public class StuNewsResp : CommResponce
    { }

    public class StuMatchReq
    {
        public int sid { get; set; }
        public int mid { get; set; }

        public bool CheckStuMatch()
        {
            bool ret = true;
            try
            {
                var result = Fun.GetSqlConn().Query($"select id from `student_match` where mid = {mid} and sid={sid} ");
                if (result.AsList().Count == 0)
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ret = false;
                Console.WriteLine(ex.ToString());
            }
            return ret;
        }
    }

    public class StuMatchResp : CommResponce
    { }

    public class StuUploadResp : CommResponce
    { }
}
