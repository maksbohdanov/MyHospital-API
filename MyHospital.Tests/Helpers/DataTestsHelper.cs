using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyHospital.Tests.Helpers
{
    internal class DataTestsHelper
    {
        public static DbContextOptions<HospitalDbContext> GetHospitalDbOptions()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new HospitalDbContext(options))
            {
                SeedData(context);
            }
            return options;
        }


        public static void SeedData(HospitalDbContext context)
        {
            context.FavorNames.AddRange(FavorNames);
            context.FavorTypes.AddRange(FavorTypes);
            context.Specializations.AddRange(Specializations);
            context.Patients.AddRange(Patients);
            context.Doctors.AddRange(Doctors);
            context.Favors.AddRange(Favors);
            context.Appointments.AddRange(Appointments);

            context.SaveChanges();
        }
        #region Data
        public static List<FavorName> FavorNames = new()
        {
            new FavorName(){ Id = 1, Name = "Первинна консультація"},
            new FavorName(){ Id = 2, Name = "Повторна консультація"},
            new FavorName(){ Id = 3, Name = "Лабораторні дослідження"},
            new FavorName(){ Id = 4, Name = "Ультразвукова діагностика"},
            new FavorName(){ Id = 5, Name = "Рентгенографія"},
            new FavorName(){ Id = 6, Name = "Комп'ютерна томографія"}
        };

        public static List<FavorType> FavorTypes = new()
        {
            new FavorType(){ Id = 1, Type = "Консультація"},
            new FavorType(){ Id = 2, Type = "Діагностика"}
        };

        public static List<Specialization> Specializations = new()
        {
            new Specialization(){ Id = 1, Title = "Дослідження та діагностика"},
            new Specialization(){ Id = 2, Title = "Терапія"},
            new Specialization(){ Id = 3, Title = "Хірургія"},
            new Specialization(){ Id = 4, Title = "Педіатрія"},
        };

        public static List<Patient> Patients = new()
        {
            new Patient(){ Id = 1, FullName = "Петренко Петро", Phone = "380961234567"},
            new Patient(){ Id = 2, FullName = "Іваненко Іван", Phone = "380501234567"}
        };        

        public static List<Doctor> Doctors = new()
        {
            new Doctor(){ Id = 1, FirstName = "Анна", LastName = "Мельник", MiddleName = "Олексіївна", SpecializationId = 1, Experience = 3 },
            new Doctor(){ Id = 2, FirstName = "Юрій", LastName = "Дрогобич", MiddleName = "Михайлович", SpecializationId = 2, Experience = 10 },
            new Doctor(){ Id = 3, FirstName = "Микола", LastName = "Амосов", MiddleName = "Михайлович", SpecializationId = 3, Experience = 15 },
            new Doctor(){ Id = 4, FirstName = "Олександр", LastName = "Тур", MiddleName = "Федорович", SpecializationId = 4, Experience = 12 }
        };

        public static List<Favor> Favors = new()
        {
            new Favor(){ Id = 1, FavorTypeId = 2, FavorNameId = 4, SpecializationId = 1, Price = 500},
            new Favor(){ Id = 2, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 2, Price = 400},
            new Favor(){ Id = 3, FavorTypeId = 1, FavorNameId = 1, SpecializationId = 3, Price = 350},
            new Favor(){ Id = 4, FavorTypeId = 1, FavorNameId = 2, SpecializationId = 3, Price = 300},
        };

        public static List<Appointment> Appointments = new()
        {
            new Appointment(){ Id = 1, DoctorId = 1, FavorId= 1, PatientId= 1, Date = new DateTime(2022, 12, 12, 16, 0, 0) },
            new Appointment(){ Id = 2, DoctorId = 2, FavorId= 2, PatientId= 1, Date = new DateTime(2022, 12, 12, 16, 0, 0) },
            new Appointment(){ Id = 3, DoctorId = 3, FavorId= 3, PatientId= 2 , Date = new DateTime(2022, 12, 12, 16, 0, 0)},
        };
        #endregion
    }
}
