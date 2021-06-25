using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OJ_backend.Entities;

namespace OJ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        


        /// <summary>
        /// post:
        /// Userid,Password
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public List<Object> Login([FromBody]JObject jObject)
        {
            var jsonStr = JsonConvert.SerializeObject(jObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            string Username = jsonParams.Username;
            string Password = jsonParams.Password;

            List<Object> response = new List<object>();

            Context context = new Context();

            User user = context.user.Find(Username);
            if(user==null)
            {
                response.Add(new
                {
                    status=400,
                    msg = "用户不存在"
                });
            }
            else if(user.Password!=Password)
            {
                response.Add(new
                {
                    status = 400,
                    msg = "密码错误"
                });
            }
            else
            {
                response.Add(new
                {
                    status = 200,
                    msg = "登录成功"
                });
            }

            return response;
        }



        [HttpPost("register")]
        public List<Object> Register([FromBody]JObject jObject)
        {
            var jsonStr = JsonConvert.SerializeObject(jObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            string Username = jsonParams.Username;
            string Password = jsonParams.Password;

            List<Object> response = new List<object>();

            if(!JudgeStringLegal.Class1.Judge(Username))
            {
                response.Add(new
                {
                    status = 400,
                    msg = "用户名不合法"
                });
                return response;
            }

            Context context = new Context();

            User user = context.user.Find(Username);
            if (user == null)
            {
                user = new User(Username, Password);
                context.user.Add(user);
                context.SaveChanges();
                response.Add(new
                {
                    status = 200,
                    msg = "注册成功"
                });
            }
            else 
            {
                response.Add(new
                {
                    status = 400,
                    msg = "用户名已经存在"
                });
            }


            return response;
        }



    }
}
