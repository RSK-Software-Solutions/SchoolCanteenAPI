﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.DTOs.UnitDTOs;
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

        CreateMap<FinishedProduct, SimpleFinishedProductDto>();
        CreateMap<SimpleFinishedProductDto, FinishedProduct>();

        CreateMap<FinishedProduct, SimpleFinishedProductDto>();
        CreateMap<SimpleFinishedProductDto, FinishedProduct>();

        CreateMap<SimpleProductFinishedProductDto, ProductFinishedProduct>();
        CreateMap<ProductFinishedProduct, SimpleProductFinishedProductDto>();

        CreateMap<Product, ProductForListDto>();
        CreateMap<ProductForListDto, Product>();

        CreateMap<Product, SimpleProductDto>();
        CreateMap<SimpleProductDto, Product>();

        CreateMap<CreateProductDto, Product>();
        CreateMap<EditProductDto, Product>();

        CreateMap<Unit, SimpleUnitDto>();
        CreateMap<SimpleUnitDto, Unit>();

        CreateMap<ApplicationUser, CreateUserDTO>();
        CreateMap<CreateUserDTO, ApplicationUser>();

        CreateMap<ApplicationUser, EditUserDTO>();
        CreateMap<EditUserDTO, ApplicationUser>();

        CreateMap<ApplicationUser, SimpleUserDTO>();
        CreateMap<SimpleUserDTO, ApplicationUser>();

        CreateMap<IdentityRole, SimpleRoleDTO>();
        CreateMap<SimpleRoleDTO, IdentityRole>();
    }
}
