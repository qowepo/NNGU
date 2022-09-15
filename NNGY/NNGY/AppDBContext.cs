using Microsoft.EntityFrameworkCore;
using NNGY.Models;

namespace NNGY
{
    public class AppDBContext : DbContext
    {
        public DbSet<student> student { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=NNGY;Password=sa;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"workstation id=NNGYNNGY.mssql.somee.com;packet size=4096;user id=qowepo_SQLLogin_1;pwd=anx2z8g8q8;data source=NNGYNNGY.mssql.somee.com;persist security info=False;initial catalog=NNGYNNGY");
        }
    }
}

