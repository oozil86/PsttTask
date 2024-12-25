using AutoMapper;
using PsttTask.Domain.Entities;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Mapping
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<Branch, BranchModel>();

        }
    }
}
