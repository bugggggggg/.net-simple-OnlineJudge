using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OJ_backend.Entities
{
    public class Context:DbContext
    {
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<Problem> problem { get; set; }//变量名要与数据库表名一致

        public virtual DbSet<Submission> submission { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(@"Server=106.14.67.53;port=3306;
                                                Database=donetDb;
                                                User ID=root;password=123456;SslMode=None");
                
            }
        }
    }
}
