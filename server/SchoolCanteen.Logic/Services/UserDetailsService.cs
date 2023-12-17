
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Repositories;
using SchoolCanteen.Logic.Services.Repositories.Interfaces;

namespace SchoolCanteen.Logic.Services;

public class UserDetailsService : IUserDetailsService
{
    private readonly IUserDetailsRepository _userDetailsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger logger;
    public UserDetailsService(DatabaseApiContext databaseApiContext, IMapper mapper, ILogger<UserDetailsService> logger)
    {
        _mapper = mapper;
        this.logger = logger;
        _userDetailsRepository = new UserDetailsRepository(databaseApiContext, logger);
    }
    public async Task<UserDetails> CreateAsync(UserDetails userDetails)
    {
        await _userDetailsRepository.AddAsync(userDetails);
        return userDetails;
    }

    public Task<bool> RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(UserDetails userDetails)
    {
        throw new NotImplementedException();
    }
}
