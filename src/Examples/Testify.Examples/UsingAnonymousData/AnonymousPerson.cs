using System;
using System.ComponentModel;
using Testify;

namespace Examples.UsingAnonymousData
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousPerson
    {
        public static Person AnyFemalePerson(this AnonymousData anon)
        {
            if (anon == null)
            {
                throw new ArgumentNullException(nameof(anon));
            }

            return new Person(anon.AnyFirstName(false), anon.AnySurname());
        }

        public static Person AnyMalePerson(this AnonymousData anon)
        {
            if (anon == null)
            {
                throw new ArgumentNullException(nameof(anon));
            }

            return new Person(anon.AnyFirstName(true), anon.AnySurname());
        }

        public static Person AnyPerson(this AnonymousData anon)
        {
            if (anon == null)
            {
                throw new ArgumentNullException(nameof(anon));
            }

            return new Person(anon.AnyFirstName(), anon.AnySurname());
        }
    }
}