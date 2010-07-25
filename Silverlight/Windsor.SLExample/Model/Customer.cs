namespace Windsor.SLExample.Model
{
    using System;

    public class Customer
    {
        public Customer(DateTime joinedAt)
        {
            JoinedAt = joinedAt;
        }

        public DateTime JoinedAt { get; private set; }

        public virtual string Firstname
        {
            get; set;
        }

        public virtual string Lastname
        {
            get; set;
        }
    }
}