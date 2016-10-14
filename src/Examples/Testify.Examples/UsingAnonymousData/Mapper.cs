using System;
using System.Collections.Generic;

namespace Examples.UsingAnonymousData
{
    public class Mapper
    {
        private Dictionary<Tuple<Type, Type>, Func<object, object>> mappers = new Dictionary<Tuple<Type, Type>, Func<object, object>>();

        public Mapper()
        {
            this.mappers.Add(
                Tuple.Create(typeof(EmployeeDto), typeof(Employee)),
                e =>
                {
                    var emp = (EmployeeDto)e;
                    return new Employee(emp.FirstName, emp.LastName);
                });
        }

        public TDestination Map<TDestination>(object source)
        {
            var key = Tuple.Create(source.GetType(), typeof(TDestination));
            Func<object, object> mapper;
            if (this.mappers.TryGetValue(key, out mapper))
            {
                return (TDestination)mapper.Invoke(source);
            }

            return default(TDestination);
        }
    }
}