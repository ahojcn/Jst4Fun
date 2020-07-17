using System;
using MySql.Data.MySqlClient;
using Dapper;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Jst4Fun.Utils
{
    public class Fun
    {
        // 获取数据库连接
        public static MySqlConnection GetSqlConn()
        {
            //string connStr = "Server=127.0.0.1;database=bjpowernode;uid=root;password=200212;";
            string connStr = "Server=127.0.0.1;database=jst4fun;uid=root;password=200212;charset=utf8;";
            //conn.Execute("insert into t_user(username) values('conn')");
            //var list = conn.Query("select * from t_user");
            //conn.CloseAsync();
            return new MySqlConnection(connStr);
        }

        // md5 加密字符串
        public static string GetMD5String(string s)
        {
            var md5 = MD5.Create();
            var res = md5.ComputeHash(Encoding.ASCII.GetBytes(s));
            var pwd_md5 = BitConverter.ToString(res);
            
            return pwd_md5.Replace("-", "");
        }

        // 获取当前时间戳字符串 毫秒
        public static string GetNowTimestampString()
        {
            return ((DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Ticks) / 10000).ToString();
        }

        // 校验邮箱
        public static bool CheckEmail(string e)
        {
            Regex regex_email = new Regex(@"^[\w-]+@[\w-]+\.(com|net|org|edu|mil|tv|biz|info)$");
            Match m = regex_email.Match(e);
            
            return m.Success;
        }

        // 校验手机号
        public static bool CheckPhone(string p)
        {
            Regex regex_email = new Regex(@"^0?[1][3-8]\d{9}$");
            Match m = regex_email.Match(p);

            return m.Success;
        }

        // 校验性别
        public static bool CheckGender(int g)
        {
            if (g < 0 || g > 2)
            {
                return false;
            }
            return true;
        }

        // 校验 stu_id 学生stu_id 是否存在
        public static bool ExistStuID(string stu_id)
        {
            bool ret = true;
            try
            {
                var result = GetSqlConn().Query($"select id from student where stu_id = '{stu_id}'");
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

        // 校验 sid 学生id 是否存在
        // 存在返回 true，否则返回 false
        public static bool ExistSID(int sid)
        {
            bool ret = true;
            try
            {
                var result = GetSqlConn().Query($"select id from student where id = {sid}");
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

        // 校验 mid 竞赛id 是否存在
        // 存在返回true，否则返回false
        public static bool ExistMID(int mid)
        {
            bool ret = true;
            try
            {
                var result = GetSqlConn().Query($"select id from `match` where id = {mid}");
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

        // 校验 smid 学生-竞赛id 是否存在
        // 存在返回 true，否则返回 false
        public static bool ExistSMID(int smid)
        {
            bool ret = true;
            try
            {
                var result = GetSqlConn().Query($"select id from `student_match` where id = {smid}");
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

        // 校验 aid 管理员id 是否存在
        // 存在返回 true，否则返回 false
        public static bool ExistAID(int aid)
        {
            bool ret = true;
            try
            {
                var result = GetSqlConn().Query($"select id from `admin` where id = {aid}");
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

        // 发送邮件
        public static void SendEMail(string email, string text)
        {
            MailMessage mail_msg = new MailMessage();
            mail_msg.From = new MailAddress("ahojcn@qq.com", "【Jst4Fun】super admin");
            mail_msg.To.Add(new MailAddress(email));
            mail_msg.Subject = "修改密码";
            mail_msg.Body = $"验证码：{text}，有效时间 2min，开始时间：{DateTime.Now}";
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.qq.com";
            client.Port = 587;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("ahojcn@qq.com", "vdergmndgppzdjhj");
            try
            {
                client.SendAsync(mail_msg, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("send");
        }
    }
}
