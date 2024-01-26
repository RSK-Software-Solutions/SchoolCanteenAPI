using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.RecipeDTOs;
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

        /********************************************** RecipeDetails  **/
        CreateMap<RecipeDetail, SimpleRecipeDetailsDto>();

        CreateMap<CreateRecipeDetailsDto, RecipeDetail>();

        /********************************************** Recipe  **/
        CreateMap<Recipe, SimpleRecipeDto>();

        CreateMap<CreateRecipeDto, Recipe>();
        CreateMap<EditRecipeDto, Recipe>();

        /********************************************** Product  **/
        CreateMap<Product, ProductForListDto>();
        CreateMap<Product, SimpleProductDto>();

        CreateMap<SimpleProductDto, Product>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<EditProductDto, Product>();

        /********************************************** Unit  **/
        CreateMap<SimpleUnitDto, Unit>();

        /********************************************** ApplicationUser  **/
        CreateMap<CreateUserDTO, ApplicationUser>();

        CreateMap<ApplicationUser, EditUserDTO>();
        CreateMap<EditUserDTO, ApplicationUser>();

        CreateMap<ApplicationUser, SimpleUserDTO>();
        CreateMap<SimpleUserDTO, ApplicationUser>();

        CreateMap<IdentityRole, SimpleRoleDTO>();
        CreateMap<SimpleRoleDTO, IdentityRole>();
    }
}
