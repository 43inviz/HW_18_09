using Microsoft.EntityFrameworkCore;

namespace HW_18_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db =new ApplicationContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //1
                //db.Database.ExecuteSqlRaw("INSERT INTO Stations (Name) VALUES ('Station1')");
                //db.Database.ExecuteSqlRaw("INSERT INTO Trains (Number,Model,TravelTime,ManufacturingDate,StationId)" + "VALUES ('1234','Model1','04:04:04','2022-01-01',1)");


                //2

                //var trains = db.Trains.FromSqlRaw("SELECT * FROM Trains WHERE TravelTime > '04:00:00'").ToList();


                //3

                //var fullInfo = db.Stations.FromSqlRaw("SELECT * FROM Stations").Include(s => s.Trains).ToList();

                //4

                //var stations = db.Stations.FromSqlRaw("SELECT * FROM Stations").Include(e => e.Trains).ToList();

                //5
                //var trains = db.Trains.FromSqlRaw("SELECT * FROM Trains WHERE Model Like 'Pell%'").ToList();

                //6

                //var trains = db.Trains.FromSqlRaw($"SELECT * FROM Trains WHERE ManufacturingDate <= DATEADD(year,-15,{DateTime.Now.Date})").ToList();


                //7
                //var station = db.Stations.FromSqlRaw($"SELECT DISTINCT s.* FROM Stations s INNER JOIN Trains t " + "ON s.Id = t.StationId WHERE t.StationId IS NULL").ToList();


                //8

                //var stations = db.Stations.FromSqlRaw("SELECT s.Id,s.Name FROM Stations s LEFT JOIN Trains t "+ "ON s.Id = t.StationId WHERE t.StationId IS NULL").ToList();
            }
        }
    }

    public class Station
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Train> Trains { get; set; } = new();
    }


    public class Train
    {
        public int Id { get; set; }

        public string Number { get; set; }
        public string Model { get; set; }

        public TimeSpan TravelTime { get; set; }

        public DateOnly ManufacturingDate { get; set; }

        public int StationId { get; set; }

        public Station Station { get; set; }

    }


    public class ApplicationContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }

        public DbSet<Train> Trains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-R3LQDV9;Database = TestDb1;Trusted_Connection =True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().HasOne(t=>t.Station).WithMany(s=>s.Trains).HasForeignKey(t=>t.StationId);
        }

    }
}
