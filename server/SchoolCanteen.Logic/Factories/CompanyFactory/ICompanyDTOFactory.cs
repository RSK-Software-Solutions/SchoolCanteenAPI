﻿using SchoolCanteen.Logic.Models;

namespace SchoolCanteen.Logic.Factories.CompanyFactory;

public interface ICompanyDTOFactory<T>
{
    T ConvertFromModel(Company company);
    Company ConvertFromDTO(T dto);
}