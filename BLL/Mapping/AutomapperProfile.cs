using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Doctor, DoctorModel>()
                .ForMember(dm => dm.Specialization, d => d.MapFrom(x => x.Specialization.Title))
                .ForMember(dm => dm.Appointments, d => d.MapFrom(x => x.Appointments.Select(a => a.Date)));

            CreateMap<Favor, FavorModel>()
                .ForMember(fm => fm.Specialization, f => f.MapFrom(x => x.Specialization.Title))
                .ForMember(fm => fm.FavorName, f => f.MapFrom(x => x.FavorName.Name))
                .ForMember(fm => fm.FavorType, f => f.MapFrom(x => x.FavorType.Type));                
        }
    }
}
