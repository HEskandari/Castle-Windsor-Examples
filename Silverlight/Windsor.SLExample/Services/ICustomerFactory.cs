using Windsor.SLExample.Model;

namespace Windsor.SLExample.Services
{
    public interface ICustomerFactory
    {
        Customer Create(string firstname, string lastname);
    }
}