namespace Windsor.SLExample.Factories
{
    using Windsor.SLExample.Model;

    public interface ICustomerFactory
    {
        Customer Create(string firstname, string lastname);
    }
}