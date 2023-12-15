
using AutoMapper;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Repositories;

namespace SchoolCanteen.Logic.Services;

public class UserDetailsService : IUserDetailsService
{
    private readonly IUserDetailsRepository _userDetailsRepository;
    private readonly IMapper _mapper;
    public UserDetailsService(DatabaseApiContext databaseApiContext, IMapper mapper)
    {
        _userDetailsRepository = new UserDetailsRepository(databaseApiContext);
        _mapper = mapper;
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
