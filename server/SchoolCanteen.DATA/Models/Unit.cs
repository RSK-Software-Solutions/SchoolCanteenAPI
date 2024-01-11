
namespace SchoolCanteen.DATA.Models;

public class Unit
{
    public int UnitId { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public Product Product { get; set; }
}
