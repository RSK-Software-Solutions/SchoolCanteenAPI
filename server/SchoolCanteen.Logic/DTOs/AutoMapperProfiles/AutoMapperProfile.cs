using AutoMapper;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.DTOs.UserDTOs;

namespace SchoolCanteen.Logic.DTOs.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Company, SimpleCompanyDTO>();
        CreateMap<SimpleCompanyDTO, Company>();

        CreateMap<Company, EditCompanyDTO>();
        CreateMap<EditCompanyDTO, Company>();

        CreateMap<Company, CreateCompanyDTO>();
        CreateMap<CreateCompanyDTO, Company>();

        CreateMap<User, CreateUserDTO>();
        CreateMap<CreateUserDTO, User>();

        CreateMap<User,SimpleUserDTO>();
        CreateMap<SimpleUserDTO, User>();

        CreateMap<Role, SimpleRoleDTO>();
        CreateMap<SimpleRoleDTO, Role>();
    }
}
