using Microsoft.EntityFrameworkCore;

namespace TestApi.Models {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) {

        }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Upazilla> Upazillas { get; set; }
    }
}