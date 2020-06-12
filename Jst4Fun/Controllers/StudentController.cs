using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Dapper;
using System.Text.RegularExpressions;

using Jst4Fun.Utils;
using Jst4Fun.Models;

namespace Jst4Fun.Controllers
{
    // [EnableCors("any")]
    [Route("stu")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // stu/register
        [HttpPost("register")]
        public StuRegisterReq Post([FromBody] StuRegisterReq req)
        {
            Fun f = new Fun();
            if (f.CheckEmail(req.email))
            {

            }

            //new Fun().GetSqlConn();
            //return "ok, POST /stu!";
            return req;
        }

        [HttpGet]
        public string Get()
        {
            return "ok, GET /stu";
        }
    }
}
