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
        }
    }
}
