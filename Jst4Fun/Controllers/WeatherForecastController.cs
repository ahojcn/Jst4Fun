using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Dapper;

using Jst4Fun.Utils;

namespace Jst4Fun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //string connStr = "Server=127.0.0.1;database=bjpowernode;uid=root;password=200212;";
            //MySqlConnection conn = new MySqlConnection(connStr);
            //conn.Execute("insert into t_user(username) values('conn')");
            //var list = conn.Query("select * from t_user");
            //conn.CloseAsync();

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.id} -> {item.username}");
            //}

            //Fun f = new Fun();
            //var conn = f.GetSqlConn();
            //var list = conn.Query("select * from t_user");
            //conn.CloseAsync();
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.id} -> {item.username}");
            //}

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
