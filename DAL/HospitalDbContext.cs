using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<FavorName> FavorNames { get; set; }
        public DbSet<FavorType> FavorTypes { get; set; }
        public DbSet<Favor> Favors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId);

            modelBuilder.Entity<Favor>()
                .HasOne(f => f.Specialization)
                .WithMany(s => s.Favors)
                .HasForeignKey(f => f.SpecializationId);
            modelBuilder.Entity<Favor>()
                .HasOne(f => f.FavorName)
                .WithMany(fn => fn.Favors)
                .HasForeignKey(f => f.FavorNameId);
            modelBuilder.Entity<Favor>()
                .HasOne(f => f.FavorType)
                .WithMany(ft => ft.Favors)
                .HasForeignKey(f => f.FavorTypeId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Favor)
                .WithMany(f => f.Appointments)
                .HasForeignKey(a => a.FavorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Phone)
                .IsUnique(true);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavorName>().HasData(
                new FavorName() { Id = 1, Name = "Первинна консультація" },
                new FavorName() { Id = 2, Name = "Повторна консультація" },
                new FavorName() { Id = 3, Name = "Лабораторні дослідження" },
                new FavorName() { Id = 4, Name = "Ультразвукова діагностика" },
                new FavorName() { Id = 5, Name = "Рентгенографія" },
                new FavorName() { Id = 6, Name = "Комп'ютерна томографія" });

            modelBuilder.Entity<FavorType>().HasData(
                new FavorType() { Id = 1, Type = "Консультація" },
                new FavorType() { Id = 2, Type = "Діагностика" });

            modelBuilder.Entity<Specialization>().HasData(
                new Specialization() { Id = 1, Title = "Дослідження та діагностика" },
                new Specialization() { Id = 2, Title = "Терапія" },
                new Specialization() { Id = 3, Title = "Хірургія" },
                new Specialization() { Id = 4, Title = "Педіатрія" },
                new Specialization() { Id = 5, Title = "Дерматологія" },
                new Specialization() { Id = 6, Title = "Психотерапія" });

            modelBuilder.Entity<Patient>().HasData(
                new Patient() { Id = 1, FullName = "Петренко Петро", Phone = "380961234567" },
                new Patient() { Id = 2, FullName = "Іваненко Іван", Phone = "380501234567" },
                new Patient() { Id = 3, FullName = "Семененко Семен", Phone = "380661234567" },
                new Patient() { Id = 4, FullName = "Ольченко Ольга", Phone = "380931234567" });

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { Id = 1, FirstName = "Анна", LastName = "Мельник", MiddleName = "Олексіївна", SpecializationId = 1, Experience = 3 },
                new Doctor() { Id = 2, FirstName = "Юрій", LastName = "Дрогобич", MiddleName = "Михайлович", SpecializationId = 2, Experience = 10 },
                new Doctor() { Id = 3, FirstName = "Микола", LastName = "Амосов", MiddleName = "Михайлович", SpecializationId = 3, Experience = 15 },
                new Doctor() { Id = 4, FirstName = "Олександр", LastName = "Тур", MiddleName = "Федорович", SpecializationId = 4, Experience = 12 },
                new Doctor() { Id = 5, FirstName = "Сергій", LastName = "Шевченко", MiddleName = "Олександрович", SpecializationId = 5, Experience = 7 },
                new Doctor() { Id = 6, FirstName = "Ольга", LastName = "Кравчук", MiddleName = "Ігорівна", SpecializationId = 6, Experience = 9 });

            modelBuilder.Entity<Favor>().HasData(
                new Favor() { Id = 1, FavorTypeId = 2, FavorNameId = 3, SpecializationId = 1, Price = 200 },
                new Favor() { Id = 2, FavorTypeId = 2, FavorNameId = 4, SpecializationId = 1, Price = 500 },
                new Favor() { Id = 3, FavorTypeId = 2, FavorNameId = 5, SpecializationId = 1, Price = 400 },
                new Favor() { Id = 4, FavorTypeId = 2, FavorNameId = 6, SpecializationId = 1, Price = 700 },

                new Favor() { Id = 5, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 2, Price = 350 },
                new Favor() { Id = 6, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 2, Price = 300 },

                new Favor() { Id = 7, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 3, Price = 350 },
                new Favor() { Id = 8, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 3, Price = 300 },

                new Favor() { Id = 9, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 4, Price = 300 },
                new Favor() { Id = 10, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 4, Price = 250 },

                new Favor() { Id = 11, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 5, Price = 350 },
                new Favor() { Id = 12, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 5, Price = 300 },

                new Favor() { Id = 13, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 6, Price = 400 },
                new Favor() { Id = 14, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 6, Price = 350 });

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment() { Id = 1, DoctorId = 1, FavorId = 1, PatientId = 1, Date = new DateTime(2022, 12, 12, 15, 0, 0) },
                new Appointment() { Id = 2, DoctorId = 1, FavorId = 2, PatientId = 1, Date = new DateTime(2022, 12, 12, 16, 0, 0) },
                new Appointment() { Id = 3, DoctorId = 1, FavorId = 3, PatientId = 2, Date = new DateTime(2022, 12, 12, 17, 0, 0) },
                new Appointment() { Id = 4, DoctorId = 2, FavorId = 5, PatientId = 1, Date = new DateTime(2022, 12, 12, 17, 0, 0) },
                new Appointment() { Id = 5, DoctorId = 3, FavorId = 7, PatientId = 2, Date = new DateTime(2022, 12, 12, 17, 0, 0) },
                new Appointment() { Id = 6, DoctorId = 4, FavorId = 9, PatientId = 3, Date = new DateTime(2022, 12, 12, 17, 0, 0) },
                new Appointment() { Id = 7, DoctorId = 5, FavorId = 11, PatientId = 4, Date = new DateTime(2022, 12, 12, 17, 0, 0) });
        }
    }   
}
