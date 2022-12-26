using AutoMapper;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using BLL.Validation;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<HospitalDbContext>(x => x.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
            builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
            builder.Services.AddScoped<IRepository<Favor>, FavorRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new AutomapperProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IFavorService, FavorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<NewAppointmentValidator>();

            builder.Services.AddControllers();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();            

            app.Run();

        }
    }
}