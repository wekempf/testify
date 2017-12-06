using System;
using System.Collections.Generic;

namespace Examples.UsingAnonymousData
{
    public class Mapper
    {
        private Dictionary<(Type, Type), Func<object, object>> mappers = new Dictionary<(Type, Type), Func<object, object>>();

        public Mapper() =>
            mappers.Add(
                (typeof(EmployeeDto), typeof(Employee)),
                e =>
                {
                    var emp = (EmployeeDto)e;
                    return new Employee(emp.FirstName, emp.LastName, emp.IsManager);
                });

        public TDestination Map<TDestination>(object source)
        {
            if (mappers.TryGetValue((source.GetType(), typeof(TDestination)), out Func<object, object> mapper))
            {
                return (TDestination)mapper.Invoke(source);
            }

            return default(TDestination);
        }
    }
}