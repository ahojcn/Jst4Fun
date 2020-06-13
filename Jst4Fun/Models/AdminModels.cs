using System;
namespace Jst4Fun.Models
{
    public class AdminLoginReq
    {
        public string name { get; set; }
        public string pwd { get; set; }
    }

    public class AdminLoginResp : CommResponce
    { }

    public class MatchProfile
    {
        public object match { get; set; }
        public object stus { get; set; }
    }

    public class AdminMatchResp : CommResponce
    { }

    public class AdminMatchReq
    {
        public int aid { get; set; }
        public string title { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public bool need_upload { get; set; }
    }

    public class AdminCMatchReq
    {
        public int mid { get; set; }
        public string title { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public bool need_upload { get; set; }
    }

    public class AdminAwardsResp : CommResponce
    { }

    public class AdminAwardsReq
    {
        public int smid { get; set; }
        public string awards { get; set; }
    }

    public class AdminNewsResp : CommResponce
    { }

    public class AdminNewsReq
    {
        public string title { get; set; }
        public string detail { get; set; }
    }

    public class AdminCNewsReq : AdminNewsReq
    {
        public int nid { get; set; }
    }

    public class AdminAnalysisProfile
    {
        public int m_cnt { get; set; }
        public int m_is_active_cnt { get; set; }
        public int awards_cnt { get; set; }
        public int awards_man { get; set; }
        public int stu_cnt { get; set; }
        public int stu_match_cnt { get; set; }
    }

    public class AdminAnalysisResp : CommResponce
    { }

    public class AdminFindPwdResp : CommResponce
    { }

    public class AdminFindPwdReq
    {
        public bool is_stu { set; get; }
        public string email { get; set; }
    }

    public class AdminTokenResp : CommResponce
    { }

    public class AdminTokenReq
    {
        public string token { get; set; }
        public string email { get; set; }
    }
}
