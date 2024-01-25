
namespace SchoolCanteen.DATA.Models;

public class AppMessage
{
    public bool result {  get; set; }
    public string message { get; set; }
    public AppMessage(bool result, string message)
    {
        this.result = result;
        this.message = message;
    }
}
