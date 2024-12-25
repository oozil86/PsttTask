using AutoMapper;
using PsttTask.Domain.Entities;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetName()));

            CreateMap<SaveCompanyModel, Company>();

        }
    }
}
