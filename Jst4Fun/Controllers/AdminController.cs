using System;
using System.IO;
using System.Linq;
using NPOI.XWPF.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using Jst4Fun.Models;
using Jst4Fun.Utils;

namespace Jst4Fun.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // 找回密码, 学生/admin通用
        // POST admin/findpwd
        [HttpPost("findpwd")]
        public AdminFindPwdResp FindPwd([FromBody] AdminFindPwdReq req)
        {
            AdminFindPwdResp resp = new AdminFindPwdResp();

            try
            {
                // 212q3123123123123123123sfasdfasdfwerqwe
                var token = Guid.NewGuid().ToString();

                //HttpContext.Session.Timeout
                HttpContext.Session.SetString(req.email, token);

                Fun.SendEMail(req.email, token);
                resp.msg = "已发送";
                resp.status = 0;
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 找回密码, 学生/admin通用
        // POST admin/findpwd
        [HttpPost("token")]
        public AdminTokenResp Token([FromBody] AdminTokenReq req)
        {
            AdminTokenResp resp = new AdminTokenResp();

            try
            {
                //var token = Guid.NewGuid().ToString();
                //HttpContext.Session.SetString(token, req.email);
                //Fun.SendEMail(req.email, token);
                if (HttpContext.Session.GetString(req.email) == req.token)
                {
                    resp.status = 0;
                    resp.msg = "验证通过";
                }
                else
                {
                    resp.status = -1;
                    resp.msg = "验证失败";
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


        // 管理员登录
        // POST admin/login
        [HttpPost("login")]
        public AdminLoginResp Login([FromBody] AdminLoginReq req)
        {
            AdminLoginResp resp = new AdminLoginResp();

            try
            {
                var result = Fun.GetSqlConn().Query($"select * from admin where name = '{req.name}'");
                if (result.Count() == 0)
                {
                    resp.status = -1;
                    resp.msg = "账号密码错误";
                }
                else
                {
                    var admin = result.Single();
                    Console.WriteLine(Fun.GetMD5String(req.pwd));
                    if (Fun.GetMD5String(req.pwd) == admin.pwd)
                    {
                        resp.msg = "登陆成功";
                        resp.status = 0;
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

        // 查看竞赛
        // GET admin/match
        [HttpGet("match")]
        public AdminMatchResp Match(int page_index = 1, int page_size = 3)
        {
            AdminMatchResp resp = new AdminMatchResp();

            try
            {
                // var results = Fun.GetSqlConn().Query($"select * from `match` limit {(page_index - 1) * page_size}, {page_size}");
                var results = Fun.GetSqlConn().Query($"select * from `match`");
                MatchProfile[] mp_arr = new MatchProfile[results.Count()];
                int i = 0;
                foreach (var match in results)
                {
                    MatchProfile mp = new MatchProfile();
                    // Console.WriteLine($"select s.id, s.name, s.nick_name, s.gender, s.email, sm.is_upload, sm.create_time, sm.upload_file_time, sm.upload_file_path, sm.is_awards, sm.awards from student_match sm inner join `student` s on sm.id = {match.id} and sm.sid = s.id");
                    var stus = Fun.GetSqlConn().Query($"select s.id, s.name, s.nick_name, s.gender, s.email, sm.is_upload, sm.create_time, sm.upload_file_time, sm.upload_file_path, sm.is_awards, sm.awards from student_match sm inner join `student` s on sm.mid = {match.id} and sm.sid = s.id");
                    mp.match = match;
                    mp.stus = stus;
                    mp_arr[i++] = mp;
                }
                resp.data = mp_arr;
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

        // 竞赛发布
        // POST admin/match
        [HttpPost("match")]
        public AdminMatchResp Match([FromBody] AdminMatchReq req)
        {
            AdminMatchResp resp = new AdminMatchResp();

            try
            {
                if (Fun.ExistAID(req.aid))
                {
                    Fun.GetSqlConn().Execute($"insert into `match` (title, start_time, end_time, description, detail, create_time, need_upload, is_active) values ('{req.title}', '{req.start_time}', '{req.end_time}', '{req.description}', '{req.detail}', '{Fun.GetNowTimestampString()}', {req.need_upload}, '{1}')");
                    resp.msg = "ok";
                    resp.status = 0;
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

        // 竞赛删除
        // DELETE admin/match
        [HttpDelete("match")]
        public AdminMatchResp Match(int mid)
        {
            AdminMatchResp resp = new AdminMatchResp();

            try
            {
                if (Fun.ExistMID(mid))
                {
                    Fun.GetSqlConn().Execute($"update `match` set is_active = false where id={mid}");
                    resp.msg = "ok";
                    resp.status = 0;
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

        // 竞赛修改
        // POST admin/match
        [HttpPost("cmatch")]
        public AdminMatchResp CMatch([FromBody] AdminCMatchReq req)
        {
            AdminMatchResp resp = new AdminMatchResp();

            try
            {
                if (Fun.ExistMID(req.mid))
                {
                    Fun.GetSqlConn().Execute($"update `match` set title = '{req.title}', start_time = '{req.start_time}', end_time = '{req.end_time}', description = '{req.description}', detail = '{req.detail}', need_upload = {req.need_upload} where id = {req.mid}");
                    resp.msg = "ok";
                    resp.status = 0;
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

        // 竞赛奖项
        // POST admin/awards
        [HttpPost("awards")]
        public AdminAwardsResp Awards([FromBody] AdminAwardsReq req)
        {
            AdminAwardsResp resp = new AdminAwardsResp();

            try
            {
                if (Fun.ExistSMID(req.smid))
                {
                    Fun.GetSqlConn().Execute($"update student_match set is_awards = true, awards = '{req.awards}' where id={req.smid}");
                    resp.status = 0;
                    resp.msg = "ok";
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


        // 生成证书
        [HttpGet("awards")]
        public AdminAwardsResp Awards(int smid)
        {
            AdminAwardsResp resp = new AdminAwardsResp();

            try
            {
                var result = Fun.GetSqlConn().Query($"select sid, mid, awards from student_match where  id = {smid}");
                var sm_obj = result.Single();
                var m_result = Fun.GetSqlConn().Query($"select title from `match` where id = {sm_obj.mid}");
                var m_obj = m_result.Single();
                var stu_result = Fun.GetSqlConn().Query($"select name from student where id = {sm_obj.sid}");
                var stu_obj = stu_result.Single();

                // 生成证书
                string unique_file_name = Guid.NewGuid().ToString() + ".docx";
                string file_path = Path.Combine("wwwroot", "awards", unique_file_name);
                XWPFDocument doc = new XWPFDocument();
                // 添加段落
                XWPFParagraph gp = doc.CreateParagraph();
                gp.Alignment = ParagraphAlignment.CENTER;//水平居中
                XWPFRun gr = gp.CreateRun();
                gr.GetCTR().AddNewRPr().AddNewRFonts().ascii = "黑体";
                gr.GetCTR().AddNewRPr().AddNewRFonts().eastAsia = "黑体";
                //gr.GetCTR().AddNewRPr().AddNewRFonts().hint = ST_Hint.eastAsia;
                gr.GetCTR().AddNewRPr().AddNewSz().val = (ulong)44;//2号字体
                gr.GetCTR().AddNewRPr().AddNewSzCs().val = (ulong)44;
                gr.GetCTR().AddNewRPr().AddNewB().val = true; //加粗
                gr.GetCTR().AddNewRPr().AddNewColor().val = "red";//字体颜色
                gr.SetText("荣誉证书");
                gr.SetText("——————");
                gr.SetText($"恭喜{stu_obj.name}同学在{m_obj.title}比赛中获得{sm_obj.awards}，特发此证，予以鼓励！");
                FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
                doc.Write(fs);
                doc.Close();

                resp.status = 0;
                resp.msg = "ok";
                resp.data = Path.Combine("awards", unique_file_name);
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }


        // 新闻发布
        // POST admin/news
        [HttpPost("news")]
        public AdminNewsResp News([FromBody] AdminNewsReq req)
        {
            AdminNewsResp resp = new AdminNewsResp();

            try
            {
                Fun.GetSqlConn().Execute($"insert into `news` (title, detail, create_time, is_active) values ('{req.title}', '{req.detail}', '{Fun.GetNowTimestampString()}', {1})");
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

        // 新闻修改
        // POST admin/cnews
        [HttpPost("cnews")]
        public AdminNewsResp CNews([FromBody] AdminCNewsReq req)
        {
            AdminNewsResp resp = new AdminNewsResp();

            try
            {
                Fun.GetSqlConn().Execute($"update news set title = '{req.title}', detail = '{req.detail}' where id = {req.nid}");
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }

        // 新闻删除
        // DELETE admin/news
        [HttpDelete("news")]
        public AdminNewsResp News(int nid)
        {
            AdminNewsResp resp = new AdminNewsResp();

            try
            {
                Fun.GetSqlConn().Execute($"update news set is_active = false where id={nid}");
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

        // 统计分析
        // GET admin/analysis
        [HttpGet("analysis")]
        public AdminAnalysisResp Analysis()
        {
            AdminAnalysisResp resp = new AdminAnalysisResp();
            try
            {
                //Fun.SendEMail("ahojcn@gmail.com", Guid.NewGuid().ToString());

                AdminAnalysisProfile aap = new AdminAnalysisProfile();
                var result_1 = Fun.GetSqlConn().Query("select id, is_active from `match`");
                int m_cnt = result_1.Count();  // 总共比赛场次
                int m_is_active_cnt = 0;   // 正在进行的比赛场次
                foreach (var item in result_1)
                {
                    if (item.is_active == true)
                    {
                        m_is_active_cnt+=1;
                    }
                }
                aap.m_cnt = m_cnt;
                aap.m_is_active_cnt = m_is_active_cnt;

                var result_2 = Fun.GetSqlConn().Query("select sid from student_match where is_awards = true");
                int awards_cnt = result_2.Count();   // 总获奖人数/次
                aap.awards_cnt = awards_cnt;
                int awards_man = 0;
                foreach (var item in result_2)
                {
                    var result_3 = Fun.GetSqlConn().Query($"select id, gender from student where id = {item.sid}");
                    foreach (var stu in result_3)
                    {
                        if (stu.gender == 1) awards_man+=1;
                    }
                }
                aap.awards_man = awards_man;  // 获奖学生中男生人数

                var result_4 = Fun.GetSqlConn().Query("select id from student");
                int stu_cnt = result_4.Count();
                aap.stu_cnt = stu_cnt;  // 学生总人数

                var result_5 = Fun.GetSqlConn().Query("select distinct sid from student_match");
                int stu_match_cnt = result_5.Count();  // 参加学生人数
                aap.stu_match_cnt = stu_match_cnt;

                resp.data = aap;
                resp.msg = "ok";
                resp.status = 0;
            }
            catch (Exception ex)
            {
                resp.msg = "未知错误" + ex.ToString();
                resp.status = -2;
                Console.WriteLine(resp.msg);
            }

            return resp;
        }


        // 测试 POST admin/test
        [HttpPost("test")]
        public string Test()
        {
            string unique_file_name = Guid.NewGuid().ToString() + ".docx";
            string file_path = Path.Combine("wwwroot", "awards", unique_file_name);
            XWPFDocument doc = new XWPFDocument();
            // 添加段落
            XWPFParagraph gp = doc.CreateParagraph();
            gp.Alignment = ParagraphAlignment.CENTER;//水平居中
            XWPFRun gr = gp.CreateRun();
            gr.GetCTR().AddNewRPr().AddNewRFonts().ascii = "黑体";
            gr.GetCTR().AddNewRPr().AddNewRFonts().eastAsia = "黑体";
            //gr.GetCTR().AddNewRPr().AddNewRFonts().hint = ST_Hint.eastAsia;
            gr.GetCTR().AddNewRPr().AddNewSz().val = (ulong)44;//2号字体
            gr.GetCTR().AddNewRPr().AddNewSzCs().val = (ulong)44;
            gr.GetCTR().AddNewRPr().AddNewB().val = true; //加粗
            gr.GetCTR().AddNewRPr().AddNewColor().val = "red";//字体颜色
            gr.SetText("NPOI创建Word2007Docx");

            FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
            doc.Write(fs);
            Console.WriteLine("生成word成功");

            return "ok admin/test";
        }
    }
}
