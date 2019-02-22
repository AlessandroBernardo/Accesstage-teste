using Microsoft.EntityFrameworkCore;


namespace Teste.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {


        }

        public DbSet<Pais> Pais { get; set; }
      
    }
}
