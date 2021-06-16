using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OJ_backend.Entities
{
    public class Problem
    {
        [Key]
        public int problemId { get; set; }

        [MaxLength(128)]
        public string problemName { get; set; }

        public int problemTimeLimit { get; set; }

        public int problemMemoryLimit { get; set; }

        public string problemDescription { get; set; }

        public string problemInputFormat { get; set; }

        public string problemOutputFormat { get; set; }

        public string problemSampleInput { get; set; }

        public string problemSampleOutput { get; set; }

        public string problemHint { get; set; }

        public int problemCheckPointCnt { get; set; }

        public int problemSubmitCnt { get; set; }

        public int problemAcceptCnt { get; set; }
    }
}
