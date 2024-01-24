﻿using Microsoft.AspNetCore.Identity;
using SchoolCanteen.Logic.DTOs.UserDTOs;

namespace SchoolCanteen.Logic.Services.User;

public interface IUserService
{
    Task<IdentityResult> CreateAsync(CreateUserDTO userDto);
    Task<IdentityResult> UpdateAsync(EditUserDTO userDto);
    Task<IdentityResult> DeleteAsync(Guid Id);
    Task<SimpleUserDTO> GetByNameAsync(string userLogin);
    Task<IEnumerable<SimpleUserDTO>> GetAllAsync();
}