using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OJ_backend.Entities;

namespace OJ_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        [HttpGet("problems")]
        public List<Object> Problems([FromQuery]int pagenum,[FromQuery]int pagesize)
        {

            List<Object> response = new List<object>();

            Context context = new Context();

            List<Problem> problems = context.Set<Problem>().ToList();
            int total = problems.Count;

            List<Problem> retList = new List<Problem>();
            for (int i = Math.Min(total - 1, (pagenum - 1) * pagesize); i < Math.Min(total, pagenum * pagesize); i++)
            {
                if (i < 0) break;
                retList.Add(problems[i]);
            }

            response.Add(new
            {
                total = total,
                problemList = retList
            });
            return response;
        }

        [HttpGet("getProblemById")]
        public List<object> GetProblemById([FromQuery]int problemId)
        {
            List<Object> response = new List<object>();

            Context context = new Context();
            Problem problem = context.problem.Find(problemId);
            response.Add(new
            {
                
                problem = problem
            });
            return response;
        }
    }
}