using AutoMapper;
using BLL.Mapping;

namespace MyHospital.Tests.Helpers
{
    public class MapperHelper
    {
        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}
