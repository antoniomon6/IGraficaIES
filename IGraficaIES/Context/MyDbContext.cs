using _2HerenciaSimpleIES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGraficaIES.Context
{
    class MyDbContext : DbContext
    {
        // Crear Base de Datos, Copiar cadena de conexion, add-migration <nombre>, update-database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IGraficaIES;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public DbSet<ProfesorFuncionario> ProfesoresFuncionarios { get; set; }
        public DbSet<ProfesorExtendido> ProfesoresExtendidos { get; set; }
    }

}
