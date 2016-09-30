using System;
using System.Text;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="string"/> values.
    /// </summary>
    // Temorary (hopefull) workaround for DocFX
    // [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousString
    {
        private static readonly string[] FemaleFirstNames = new[]
        {
            "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Maria", "Susan", "Margaret",
            "Dorothy", "Lisa", "Nancy", "Karen", "Betty", "Helen", "Sandra", "Donna", "Carol", "Ruth",
            "Sharon", "Michelle", "Laura", "Sarah", "Kimberly", "Deborah", "Jessica", "Shirley", "Cynthia",
            "Angela", "Melissa", "Brenda", "Amy", "Anna", "Rebbecca", "Virginia", "Kathleen", "Pamela",
            "Martha", "Debra", "Amanda", "Stephanie", "Carolyn", "Christine", "Marie", "Janet", "Catherine",
            "Frances", "Ann", "Joyce", "Diane", "Alice", "Julie", "Heather", "Teresa", "Doris", "Gloria",
            "Evelyn", "Jean", "Cheryl", "Mildred", "Katherine", "Joan", "Ashley", "Judith", "Rose", "Janice",
            "Kelly", "Nicole", "Judy", "Christina", "Kathy", "Theresa", "Beverly", "Denise", "Tammy", "Irene",
            "Jane", "Lori", "Rachel", "Marilyn", "Andrea", "Kathryn", "Louise", "Sara", "Anne", "Jacqueline",
            "Wanda", "Bonnie", "Julia", "Ruby", "Lois", "Tina", "Phyllis", "Norma", "Paula", "Diana", "Annie",
            "Lillian", "Emily", "Robin", "Peggy", "Crystal", "Gladys", "Rita", "Dawn", "Connie", "Florence",
            "Tracy", "Edna", "Tiffany", "Carmen", "Rosa", "Cindy", "Grace", "Wendy", "Victoria", "Edith",
            "Kim", "Sherry", "Sylvia", "Josephine", "Thelma", "Shannon", "Sheila", "Ethel", "Ellen",
            "Elaine", "Marjorie", "Carrie", "Charlotte", "Monica", "Esther", "Pauline", "Emma", "Juanita",
            "Anita", "Rhonda", "Hazel", "Amber", "Eva", "Debbie", "April", "Leslie", "Clara", "Lucille",
            "Jamie", "Joanne", "Eleanor", "Valerie", "Danielle", "Megan", "Alicia", "Suzanne", "Michele",
            "Gail", "Bertha", "Darlene", "Veronica", "Jill", "Erin", "Geraldine", "Lauren", "Cathy", "Joann",
            "Lorraine", "Lynn", "Sally", "Regina", "Erica", "Beatrice", "Dolores", "Bernice", "Audrey",
            "Yvonne", "Annette", "June", "Samantha", "Marion", "Dana", "Stacy", "Ana", "Renee", "Ida",
            "Vivian", "Roberta", "Holly", "Brittany", "Melanie", "Loretta", "Yolanda", "Jeanette",
            "Laurie", "Katie", "Kristen", "Vanessa", "Alma", "Sue", "Elsie", "Beth", "Jeanne", "Vicki",
            "Carla", "Tara", "Rosemary", "Eileen", "Terri", "Gertrude", "Lucy", "Tonya", "Ella", "Stacey",
            "Wilma", "Gina", "Kristin", "Jessie", "Natalie", "Agnes", "Vera", "Willie", "Charlene", "Bessie",
            "Delores", "Melinda", "Pearl", "Arlene", "Maureen", "Colleen", "Allison", "Tamara", "Joy", "Georgia",
            "Constance", "Lillie", "Claudia", "Jackie", "Marcia", "Tanya", "Nellie", "Minnie", "Marlene", "Heidi",
            "Glenda", "Lydia", "Viola", "Courtney", "Marian", "Stella", "Caroline", "Dora", "Jo", "Vickie",
            "Mattie", "Terry", "Maxine", "Irma", "Mabel", "Marsha", "Myrtle", "Lena", "Christy"
        };

        private static readonly string[] FemalPrefixes = new[]
                {
            "Mrs.", "Ms.", "Miss", "Dr."
        };

        private static readonly string[] GenerationalSuffixes = new[]
        {
            "Jr.", "Sr.", "I", "II", "III"
        };

        private static readonly string[] MaleFirstNames = new[]
                        {
            "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas",
            "Christopher", "Daniel", "Paul", "Mark", "Donald", "George", "Kenneth", "Steven", "Edward", "Brian",
            "Ronald", "Anthony", "Kevin", "Jason", "Mathew", "Gary", "Timothy", "Jose", "Larry", "Jeffrey",
            "Frank", "Scott", "Eric", "Stephen", "Andrew", "Raymond", "Gregory", "Joshua", "Jerry", "Dennis",
            "Walter", "Patrick", "Peter", "Harold", "Douglas", "Henry", "Carl", "Arthur", "Ryan", "Roger",
            "Joe", "Juan", "Jack", "Albert", "Jonathan", "Justin", "Terry", "Gerald", "Keith", "Samuel", "Willie",
            "Ralph", "Lawrence", "Nicholas", "Roy", "Benjamin", "Bruce", "Brandon", "Adam", "Harry", "Fred",
            "Wayne", "Billy", "Steve", "Louis", "Jeremy", "Aaron", "Randy", "Howard", "Eugene", "Carlos",
            "Russel", "Bobby", "Victor", "Martin", "Ernest", "Phillip", "Todd", "Jesse", "Craig", "Alan",
            "Shawn", "Clarence", "Sean", "Philip", "Chris", "Johnny", "Earl", "Jimmy", "Antonio", "Danny",
            "Bryan", "Tony", "Luis", "Mike", "Stanley", "Leonard", "Nathan", "Dale", "Manuel", "Rodney",
            "Curtis", "Norman", "Allen", "Marvin", "Vincent", "Glenn", "Jeffery", "Travis", "Jeff", "Chad",
            "Jacob", "Lee", "Melvin", "Alfred", "Kyle", "Francis", "Bradley", "Jesus", "Herbert", "Frederick",
            "Ray", "Joel", "Edwin", "Don", "Eddie", "Ricky", "Troy", "Randall", "Barry", "Alexander", "Bernard",
            "Mario", "Leroy", "Francisco", "Marcus", "Michael", "Theodore", "Clifford", "Miguel", "Oscar",
            "Jay", "Jim", "Tom", "Calvin", "Alex", "Jon", "Ronnie", "Bill", "Lloyd", "Tommy", "Leon",
            "Derek", "Warren", "Darrell", "Jerome", "Floyd", "Leo", "Alvin", "Tim", "Wesley", "Gordon",
            "Dean", "Greg", "Jorge", "Dustin", "Pedro", "Derrick", "Dan", "Lewis", "Zachary", "Corey", "Herman",
            "Maurice", "Vernon", "Roberto", "Clyde", "Glen", "Hector", "Shane", "Ricardo", "Sam", "Rick",
            "Lester", "Brent", "Ramon", "Charlie", "Tyler", "Gilbert", "Gene", "Marc", "Reginald",
            "Ruben", "Brett", "Angel", "Nathaniel", "Rafael", "Leslie", "Edgar", "Milton", "Raul", "Ben",
            "Chester", "Cecil", "Duane", "Franklin", "Andre", "Elmer", "Brad", "Gabriel", "Ron", "Mitchell",
            "Roland", "Arnold", "Harvey", "Jared", "Adrian", "Karl", "Cory", "Claude", "Erik", "Darryl",
            "Jamie", "Neil", "Jessie", "Christian", "Javier", "Fernando", "Clinton", "Ted", "Mathew",
            "Tyrone", "Darren", "Lonnie", "Lance", "Cody", "Julio", "Kelly", "Kurt", "Allan", "Nelson", "Guy",
            "Clayton", "Hugh", "Max", "Dwayne", "Dwight", "Armando", "Felix", "Jimmie", "Everett", "Jordan",
            "Ian", "Wallace", "Ken", "Bob", "Jaime", "Casey", "Alfredo", "Alberto", "Dave", "Ivan", "Johnnie",
            "Sidney", "Byron", "Julian", "Isaac", "Morris", "Clifton", "Willard", "Daryl", "Ross", "Virgil",
            "Andy", "Marshall", "Salvador", "Perry", "Kirk", "Sergio", "Marion", "Tracy", "Seth", "Kent",
            "Terrance", "Rene", "Eduardo", "Terrence", "Enrique", "Freddie", "Wade"
        };

        private static readonly string[] MalePrefixes = new[]
                        {
            "Mr.", "Dr."
        };

        private static readonly string[] Suffixes = new[]
        {
            "MD", "DDS", "PhD", "DVM"
        };

        private static readonly string[] Surnames = new[]
                {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson",
            "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson",
            "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez",
            "Hall", "Young", "Allen", "Sanchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson",
            "Hill", "Ramirez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner",
            "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy",
            "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez",
            "Kelly", "Howard", "Ward", "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett",
            "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross",
            "Morales", "Powell", "Sullivan", "Russel", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler",
            "Barnes", "Fisher", "Henderson", "Coleman", "Simmons", "Patterson", "Jordan", "Reynolds", "Hamilton",
            "Graham", "Kim", "Gonzales", "Alexander", "Ramos", "Wallace", "Griffin", "West", "Cole", "Hayes",
            "Chavez", "Gibson", "Bryant", "Ellis", "Stevens", "Murray", "Ford", "Marshall", "Owens", "McDonald",
            "Harrison", "Ruiz", "Kennedy", "Wells", "Alvarez", "Woods", "Mendoza", "Catillo", "Olson", "Webb",
            "Washington", "Tucker", "Freeman", "Burns", "Henry", "Vasquez", "Snyder", "Simpson", "Crawford",
            "Jimenez", "Porter", "Mason", "Shaw", "Gordon", "Wagner", "Hunter", "Romero", "Hicks", "Dixon",
            "Hunt", "Palmer", "Robertson", "Black", "Holmes", "Stone", "Meyer", "Boyd", "Mills", "Warren",
            "Fox", "Rose", "Rice", "Moreno", "Schmidt", "Patel", "Ferguson", "Nichols", "Herrera", "Medina",
            "Ryan", "Fernandez", "Weaver", "Daniels", "Stephens", "Gardner", "Payne", "Kelley", "Dunn", "Pierce",
            "Arnold", "Tran", "Spencer", "Peters", "Hawkins", "Grant", "Hansen", "Castro", "Hoffman", "Hart",
            "Elliott", "Cunningham", "Knight", "Bradley", "Carroll", "Hudson", "Duncan", "Armstrong", "Berry",
            "Andrews", "Johnston", "Ray", "Lane", "Riley", "Carpenter", "Perkins", "Aguilar", "Silva",
            "Richards", "Willis", "Mathews", "Chapman", "Lawrence", "Garza", "Vargas", "Watkins", "Wheeler",
            "Larson", "Carlson", "Harper", "George", "Greene", "Burke", "Guzman", "Morrison", "Munoz", "Jacobs",
            "Obrien", "Lawson", "Franklin", "Lynch", "Bishop", "Carr", "Salazar", "Austin", "Mendez", "Gilbert",
            "Jensen", "Williamson", "Montgomery", "Harvey", "Oliver", "Howell", "Dean", "Hanson", "Weber",
            "Garrett", "Sims", "Burton", "Fuller", "Soto", "McCoy", "Welch", "Chen", "Schultz", "Walters",
            "Reid", "Fields", "Walsh", "Little", "Fowler", "Bowman", "Davidson", "May", "Day", "Schneider"
        };

        /// <summary>
        /// Creates a random <see langword="string"/> value representing a first name.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random first name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyFirstName(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyFirstName(anon.AnyBool());
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value representing a first name.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="male">If set to <c>true</c> a male first name is created; otherwise, a female
        /// first name is created.</param>
        /// <returns>A random first name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyFirstName(this IAnonymousData anon, bool male)
        {
            Argument.NotNull(anon, nameof(anon));

            var names = male ? MaleFirstNames : FemaleFirstNames;
            return anon.AnyItem(names);
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value representing a full name.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random full name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyFullName(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyFullName(anon.AnyBool());
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value representing a full name.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="male">If set to <c>true</c> a male full name is created; otherwise, a female
        /// full name is created.</param>
        /// <returns>A random full name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyFullName(this IAnonymousData anon, bool male)
        {
            Argument.NotNull(anon, nameof(anon));

            var prefixes = male ? MalePrefixes : FemalPrefixes;
            switch (anon.AnyInt32(0, 10))
            {
                case 0:
                    return $"{anon.AnyItem(prefixes)} {anon.AnySimpleFullName(male)}";

                case 1:
                    return $"{anon.AnySimpleFullName(male)} {anon.AnyItem(Suffixes)}";

                case 2:
                    return $"{anon.AnySimpleFullName(male)} {anon.AnyItem(GenerationalSuffixes)}";

                default:
                    return anon.AnySimpleFullName(male);
            }
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value with a length of 0 to 20 characters using
        /// a uniform distribution algorithm for generating alpha characters.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="string"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyString(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyString(0, 20, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value with a length within the specified range using
        /// a uniform distribution algorithm for generating alpha characters.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <returns>A random <see langword="string"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumLength"/> is less than <paramref name="minimumLength"/>.</exception>
        public static string AnyString(this IAnonymousData anon, int minimumLength, int maximumLength)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximumLength, minimumLength, int.MaxValue, nameof(maximumLength), "The maximum length must be greater than the minimum length.");

            return anon.AnyString(minimumLength, maximumLength, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value with a length of 0 to 20 characters using
        /// the specified distribution algorithm for generating alpha characters.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="string"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnyString(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyString(0, 20, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value with a length within the specified range using
        /// the specified distribution algorithm for generating alpha characters.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="string"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumLength"/> is less than <paramref name="minimumLength"/>.</exception>
        public static string AnyString(this IAnonymousData anon, int minimumLength, int maximumLength, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximumLength, minimumLength, int.MaxValue, nameof(maximumLength), "The maximum length must be greater than the minimum length.");

            int length = anon.AnyInt32(minimumLength, maximumLength);
            StringBuilder builder = new StringBuilder(length);
            while (builder.Length < length)
            {
                builder.Append(anon.AnyAlphaChar(distribution));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a random <see langword="string"/> value representing a surname.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random first name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static string AnySurname(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyItem(Surnames);
        }

        private static string AnySimpleFullName(this IAnonymousData anon, bool male)
        {
            switch (anon.AnyInt32(0, 5))
            {
                case 0:
                    return $"{anon.AnyFirstName(male)} {anon.AnyFirstName(male).Substring(0, 1)}. {anon.AnySurname()}";

                default:
                    return $"{anon.AnyFirstName(male)} {anon.AnySurname()}";
            }
        }
    }
}