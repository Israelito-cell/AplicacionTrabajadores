using Aplicacion_Web_Para_Trabajadores.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion_Web_Para_Trabajadores.Data;

    public class AppDbContext:DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }



    // DbSet representa tu tabla en la base de datos
    public DbSet<Trabajador> Trabajadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //  aquí fuerzas el nombre de la tabla real en tu BD
        modelBuilder.Entity<Trabajador>().ToTable("Trabajador");
    }


}

