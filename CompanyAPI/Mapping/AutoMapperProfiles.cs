using AutoMapper;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;

namespace CompanyAPI.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Company,CompanyDTO>().ReverseMap();
        }
    }
}
