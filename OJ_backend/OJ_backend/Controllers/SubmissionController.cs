using Judge;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OJ_backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OJ_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController:ControllerBase
    {
        [HttpPost("submit")]
        public List<Object> Submit([FromBody]JObject jObject)
        {
            var jsonStr = JsonConvert.SerializeObject(jObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            string Language = jsonParams.language;
            int LanguageId = jsonParams.languageId;
            int ProblemId = jsonParams.problem_id;
            string Username = jsonParams.uid;
            string SubmissionCode = jsonParams.submissionCode;

            List<Object> response = new List<object>();

            Context context = new Context();

            FileOperation.File.Write(Judger.WorkSpace + @"\1.cpp", SubmissionCode);            

            Problem problem = context.problem.Find(ProblemId);
            JudgeResult result = Judger.Excute(1, ProblemId,
                Judger.Language.CPLUSPLUS, problem.problemCheckPointCnt, problem.problemTimeLimit, problem.problemMemoryLimit);

            List<Submission> submissions = context.Set<Submission>().ToList();
            int id = 0;
            foreach(Submission sub in submissions)
            {
                id = Math.Max(id, sub.submissionId);
            }
            ++id;
            Submission submission = new Submission();
            submission.submissionId = id;
            submission.problemId = ProblemId;
            submission.submissionCode = SubmissionCode;
            submission.username = Username;
            submission.submissionJudgeResult = result.Msg;
            submission.submissionUsedMemory = result.UsedMemory;
            submission.submissionUsedTime = result.UsedTime;
            submission.languageId = LanguageId;
            submission.submissionSubmitTime = DateTime.Now;


            context.submission.Add(submission);
            context.SaveChanges();

            response.Add(new
            {
                submissionJudgeResult = result.Msg
            });

            return response;
        }

        [HttpGet("submissions")]
        public List<Object> Submissions([FromQuery]int pagenum, [FromQuery]int pagesize)
        {

            List<Object> response = new List<object>();

            Context context = new Context();

            List<Submission> submissions = context.Set<Submission>().ToList();
           
            int total = submissions.Count;

            List<Submission> retList = new List<Submission>();
            for (int i = Math.Min(total - 1, (pagenum - 1) * pagesize); i < Math.Min(total, pagenum * pagesize); i++)
            {
                if (i < 0) break;
                retList.Add(submissions[i]);
            }

            response.Add(new
            {
                total = total,
                statusList = retList
            });
            return response;
        }
    }
}
