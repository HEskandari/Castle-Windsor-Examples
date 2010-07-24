namespace Windsor.SLExample.Factories
{
    using Windsor.SLExample.Model;

    public delegate Customer CustomerFactory(string firstName, string lastName);
}