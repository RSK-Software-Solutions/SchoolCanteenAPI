
using Microsoft.Extensions.Configuration;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.Company;

namespace SchoolCanteen.Logic.Services.Repositories;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAll();
    Company GetByName(string companyName);
    Company GetById(Guid id);
    Task<bool> Add(Company company);
    Task<bool> Delete(Company company);
    Task<bool> Update(Company company);
}
