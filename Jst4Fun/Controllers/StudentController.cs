using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Dapper;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

using Jst4Fun.Utils;
using Jst4Fun.Models;

namespace Jst4Fun.Controllers
{
    [EnableCors("any")]
    [Route("stu")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // 学生注册
        // POST stu/register
        [HttpPost("register")]
        public StuRegisterResp Register([FromBody] StuRegisterReq req)
        {
            StuRegisterResp resp = new StuRegisterResp();
            if (!req.Check())
            {
                resp.data = req;
                resp.status = -1;
                resp.msg = "参数错误";
                return resp;
            }

            try
            {
                if (!Fun.ExistStuID(req.stu_id))
                {
                    Fun.GetSqlConn().Execute($"insert into student (stu_id, name, nick_name, gender, email, phone, pwd) values ('{req.stu_id}','{req.name}','{req.nick_name}','{req.gender}','{req.email}', '{req.phone}', '{Fun.GetMD5String(req.pwd)}')");
                    resp.msg = "注册成功";
                    resp.status = 0;
                }
                else
                {
                    resp.msg = "用户已存在";
                    resp.status = -1;
                }
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }
            
            return resp;
        }

        // 学生登录
        // POST stu/login
        [HttpPost("login")]
        public StuLoginResp Login([FromBody] StuLoginReq req)
        {
            StuLoginResp resp = new StuLoginResp();
            try
            {
                if (!Fun.ExistStuID(req.stu_id))
                {
                    resp.msg = "账号密码错误";
                    resp.status = -1;
                }
                else
                {
                    var result = Fun.GetSqlConn().Query($"select * from student where stu_id = '{req.stu_id}'");
                    var stu = result.Single();
                    if (Fun.GetMD5String(req.pwd) == stu.pwd)
                    {
                        resp.msg = "登陆成功";
                        resp.status = 0;
                        StuProfile sp = new StuProfile(stu.id, stu.stu_id, stu.name, stu.nick_name, stu.gender, stu.email, stu.is_active);
                        resp.data = sp;
                    }
                    else
                    {
                        resp.msg = "账号密码错误";
                        resp.status = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 学生获取新闻
        // GET stu/news
        [HttpGet("news")]
        public StuNewsResp News(int page_index = 1, int page_size = 3)
        {
            StuNewsResp resp = new StuNewsResp();

            try
            {
                // var news = Fun.GetSqlConn().Query($"select * from news limit {(page_index - 1) * page_size}, {page_size}");
                var news = Fun.GetSqlConn().Query($"select * from news");
                resp.data = news;
                resp.status = 0;
                resp.msg = "ok";
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 查看竞赛
        // GET stu/match
        [HttpGet("match")]
        public StuMatchResp Match(int page_index = 1, int page_size = 3)
        {
            StuMatchResp resp = new StuMatchResp();

            try
            {
                // var matchs = Fun.GetSqlConn().Query($"select * from `match` where is_active = {true} limit {(page_index - 1) * page_size}, {page_size}");
                var matchs = Fun.GetSqlConn().Query($"select * from `match`");
                resp.data = matchs;
                resp.status = 0;
                resp.msg = "ok";
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 竞赛报名
        // POST stu/match
        [HttpPost("match")]
        public StuMatchResp Match([FromBody] StuMatchReq req)
        {
            StuMatchResp resp = new StuMatchResp();

            try
            {
                if (!Fun.ExistSID(req.sid) || !Fun.ExistMID(req.mid))
                {
                    resp.msg = "用户或比赛不存在";
                    resp.status = -1;
                }
                else
                {
                    if (req.CheckStuMatch())
                    {
                        resp.msg = "你已经报名这个比赛啦";
                        resp.status = -1;
                    }
                    else
                    {
                        //Console.WriteLine($"insert into `student_match` (sid, mid, create_time, is_upload, upload_file_path, upload_file_time) values ({req.sid}, {req.mid}, '{Fun.GetNowTimestampString()}', {0}, '', '')");
                        Fun.GetSqlConn().Execute($"insert into `student_match` (sid, mid, create_time, is_upload, upload_file_path, upload_file_time) values ({req.sid}, {req.mid}, '{Fun.GetNowTimestampString()}', '{0}', '', '')");
                        resp.msg = "报名成功";
                        resp.status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 我的竞赛
        // GET stu/mymatch
        [HttpGet("mymatch")]
        public StuMatchResp MyMatch(int sid)
        {
            StuMatchResp resp = new StuMatchResp();

            try
            {
                if (Fun.ExistSID(sid))
                {
                    var result = Fun.GetSqlConn().Query($"select * from `student_match` sm inner join `match` m on sm.sid = {sid} and sm.mid = m.id and is_active = true;");
                    resp.data = result;
                    resp.status = 0;
                    resp.msg = "ok";
                }
                else
                {
                    resp.msg = "参数错误";
                    resp.status = -1;
                }
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 作品提交
        // POST stu/upload
        [HttpPost("upload")]
        public StuUploadResp Upload([FromForm] IFormFile file, IFormCollection c)
        {
            StuUploadResp resp = new StuUploadResp();

            try
            {
                string smid = c["smid"];
                if (Fun.ExistSMID(Convert.ToInt32(smid)))
                {
                    string uploads_folder = Path.Combine("wwwroot/uploads");
                    string unique_file_name = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string file_path = Path.Combine(uploads_folder, unique_file_name);
                    file.CopyToAsync(new FileStream(file_path, FileMode.Create));
                    file_path = Path.Combine("uploads", unique_file_name);

                    Fun.GetSqlConn().Execute($"update `student_match` set is_upload={1}, upload_file_path='{file_path}', upload_file_time='{Fun.GetNowTimestampString()}' where id={smid};");
                    resp.data = Fun.GetSqlConn().Query($"select * from `student_match` where id={smid}");
                }
                else
                {
                    resp.status = -1;
                    resp.msg = "参数错误";
                }
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 测试
        [HttpPost("test")]
        public string Test([FromForm] IFormFile file, IFormCollection c)
        {
            Console.WriteLine(c["smid"]);
            //Console.WriteLine(c["file"]);
            string uploads_folder = Path.Combine("static");
            string unique_file_name = Guid.NewGuid().ToString() + "_" + file.FileName;
            string file_path = Path.Combine(uploads_folder, unique_file_name);

            file.CopyToAsync(new FileStream(file_path, FileMode.Create));

            return "ok stu/test";
        }
    }
}
