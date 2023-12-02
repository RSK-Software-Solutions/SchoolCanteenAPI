﻿namespace SchoolCanteen.Logic.DTOs.Company;

public class EditCompanyDTO
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public int Nip { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

