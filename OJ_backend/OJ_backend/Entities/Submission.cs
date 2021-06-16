using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OJ_backend.Entities
{
    public class Submission
    {
        [Key]
        public int submissionId{get;set;}

        public int problemId { get; set; }

        [MaxLength(60)]
        public string username { get; set; }

        public int languageId { get; set; }

        public DateTime submissionSubmitTime { get; set; }

        public int submissionUsedTime { get; set; }

        public int submissionUsedMemory { get; set; }

        [MaxLength(60)]
        public string submissionJudgeResult { get; set; }
        
        public string submissionCode { get; set; }
    }
}
