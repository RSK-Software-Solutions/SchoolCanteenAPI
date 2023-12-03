using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.Logic.Factories.CompanyFactory;
using SchoolCanteen.Logic.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace SchoolCanteen.Logic.Services;

public class CompanyService : ICompanyService
{
    private List<Company> _companyList;
    private ICompanyDTOFactory<CreateCompanyDTO> _createCompany;
    private ICompanyDTOFactory<SimpleCompanyDTO> _simpleCompany;
    private ICompanyDTOFactory<EditCompanyDTO> _editCompany;

    public CompanyService()
    {
        _companyList = new List<Company>();
        _createCompany = new CreateCompanyDTOFactory();
        _simpleCompany = new SimpleCompanyDTOFactory();
        _editCompany = new EditCompanyDTOFactory();
    }

    public SimpleCompanyDTO CreateCompany(CreateCompanyDTO companyDTO)
    {
        var company = _createCompany.ConvertFromDTO(companyDTO);
        _companyList.Add(company);

        return _simpleCompany.ConvertFromModel(company);
    }
    
    public IEnumerable<SimpleCompanyDTO> GetAll()
    {
        var result = new List<SimpleCompanyDTO>();
        foreach (var company in _companyList)
        {
            result.Add(_simpleCompany.ConvertFromModel(company));
        }

        return result;
    }

    public SimpleCompanyDTO GetCompany(string companyName)
    {
        return _simpleCompany.ConvertFromModel(_companyList.FirstOrDefault(c => c.Name == companyName));
    }

    public bool UpdateCompany(EditCompanyDTO companyDTO)
    {
        var existingCompany = _companyList.Find(c => c.CompanyId == companyDTO.CompanyId);
        if (existingCompany == null) return false;

        UpdateProperties<Company>(_editCompany.ConvertFromDTO(companyDTO), existingCompany);

        //existingCompany.Name = companyDTO.Name;
        //existingCompany.Nip = companyDTO.Nip;
        //existingCompany.Street = companyDTO.Street;
        //existingCompany.Number = companyDTO.Number;
        //existingCompany.City = companyDTO.City;
        //existingCompany.PostalCode = companyDTO.PostalCode;
        //existingCompany.Phone = companyDTO.Phone;
        //existingCompany.Email = companyDTO.Email;

        return true;
    }

    public bool RemoveCompany(SimpleCompanyDTO companyDTO)
    {
        var company = _simpleCompany.ConvertFromDTO(companyDTO);
        if (company == null) return false;

        _companyList.Remove(company);
        return true;
    }

    private void UpdateProperties<T>(T source, T destination)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (property.CanWrite)
            {
                property.SetValue(destination, property.GetValue(source), null);
            }
        }
    }
}