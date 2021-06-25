using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Judge;
using Microsoft.AspNetCore.Mvc;

using OJ_backend.Entities;
using OJ_backend.Tools;


namespace OJ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //GET api/values
        //[HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        [HttpGet]
        public bool Get()
        {
            Context context = new Context();

            User user = new User("qq","1");
            //context.Add<User>(user);
            //context.SaveChanges();//增
            //context.Remove<User>(user);
            //context.SaveChanges();//删

            //context.Set<User>().ToList();//所有user


            //User user = context.user.Find(1);//查找
            //user.Username = "www";
            //context.user.Update(user);
            //context.SaveChanges();//修改
            //int a = 1, b = 2;
            //JudgeResult result = Judger.Excute(1, 1000, Judger.Language.CPLUSPLUS, 1, 1000, 65535);

            return JudgeStringLegal.Class1.Judge("ffuck");
            //return (new Mycom.Class1()).MD5("qwr");
            //return 1;
            //return result.Msg;
            //Class1 p=new Class1();

            //return Judge.Judger.Add(1, 2);
            //return Comparator.TestInt(a);
            //return Comparator.TestString("test");
            //return Comparator.Compare("../Judgefile/a.txt", "../Judgefile/b.txt");

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
