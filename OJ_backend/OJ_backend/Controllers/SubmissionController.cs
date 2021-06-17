using Judge;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OJ_backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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


            //求提交编号
            List<Submission> submissions = context.Set<Submission>().ToList();
            int id = 0;
            foreach(Submission sub in submissions)
            {
                id = Math.Max(id, sub.submissionId);
            }
            ++id;

            //暂存提交到数据库
            Submission submission = new Submission();
            submission.submissionId = id;
            submission.problemId = ProblemId;
            submission.submissionCode = SubmissionCode;
            submission.username = Username;
            submission.submissionJudgeResult = "Pending";
            submission.submissionUsedMemory = 0;
            submission.submissionUsedTime = 0;
            submission.languageId = LanguageId;
            submission.submissionSubmitTime = DateTime.Now;

            context.submission.Add(submission);
            context.SaveChanges();



            Dictionary<string, Object> dict = new Dictionary<string, object>();
            dict["jObject"] = jObject;
            dict["submissionId"] = id.ToString();


            Thread thread = new Thread(new ParameterizedThreadStart(JudgeThread));
            thread.Start(dict);

            return response;
        }

        [HttpGet("submissions")]
        public List<Object> Submissions([FromQuery]int pagenum, [FromQuery]int pagesize)
        {

            List<Object> response = new List<object>();

            Context context = new Context();

            List<Submission> submissions = context.Set<Submission>().ToList();
            submissions.Reverse();
           
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


        [HttpGet("getSubmissionById")]
        public List<Object> GetSubmissionById([FromQuery]int submissionId)
        {

            List<Object> response = new List<object>();

            Context context = new Context();

            Submission submission = context.Set<Submission>().Find(submissionId);


            response.Add(new
            {
                submissionCode = submission.submissionCode
            });
            return response;
        }

        void JudgeThread(Object s)
        {
            Dictionary<string, Object> dict = s as Dictionary<string, Object>;
            JObject jObject = dict["jObject"] as JObject;
            int submissionId = int.Parse(dict["submissionId"] as string);

            var jsonStr = JsonConvert.SerializeObject(jObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            string Language = jsonParams.language;
            int LanguageId = jsonParams.languageId;
            int ProblemId = jsonParams.problem_id;
            string Username = jsonParams.uid;
            string SubmissionCode = jsonParams.submissionCode;

           
            Context context = new Context();


            

            Problem problem = context.problem.Find(ProblemId);

            int codeFileId = submissionId % 2;

            FileOperation.File.Write(Judger.WorkSpace + @"\"+codeFileId.ToString()+".cpp", SubmissionCode);//提交代码写入文件

            JudgeResult result = Judger.Excute(codeFileId, problem.problemId,
                Judger.Language.CPLUSPLUS, problem.problemCheckPointCnt, problem.problemTimeLimit, problem.problemMemoryLimit);
            
            Submission submission = context.submission.Find(submissionId);
            submission.submissionJudgeResult = result.Msg;
            submission.submissionUsedMemory = result.UsedMemory;
            submission.submissionUsedTime = result.UsedTime;
            context.Update(submission);
            context.SaveChanges();

        }

    }
}
