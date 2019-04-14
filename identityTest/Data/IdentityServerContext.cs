using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace QuickstartIdentityServer.Data
{
    public class IdentityServerContext: DbContext
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory(changedMaxLength: null);
        }
    }

    [Table("t_wxqr_longin")]
    public class User
    {
        [Key, Column("wuuid")]
        public string wuuid { get; set; }
        
        [Column("wechatid")]
        public string wechatid { get; set; }
        
        [Column("login")]
        public int? login { get; set; }
        
        [Column("nickname")]
        public string nickname { get; set; }
        
        [Column("password")]
        public string password { get; set; }
    }
}
