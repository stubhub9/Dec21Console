using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console
{
    public class Pet
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class Home
    {


        string [] goodMovies = { "Elmer Fudd", "Darn Rabbit 2", "Beep 3", "Duck" };

        public class Person
        {
            public string LastName { get; set; }
            public Pet [] Pets { get; set; }
        }



        public static List<Person> People { get; set; }

        public Home ()
        {
            People =
            //List<Person> people = 
            new List<Person>
        { new Person { LastName = "Haas",
                       Pets = new Pet[] { new Pet { Name="Barley", Age=10 },
                                          new Pet { Name="Boots", Age=14 },
                                          new Pet { Name="Whiskers", Age=6 }}},
          new Person { LastName = "Fakhouri",
                       Pets = new Pet[] { new Pet { Name = "Snowball", Age = 1}}},
          new Person { LastName = "Antebi",
                       Pets = new Pet[] { new Pet { Name = "Belle", Age = 8} }},
          new Person { LastName = "Philips",
                       Pets = new Pet[] { new Pet { Name = "Sweetie", Age = 2},
                                          new Pet { Name = "Rover", Age = 13}} }
        };

        }

        public static void AllEx2 ()
        {
            // Determine which people have pets that are all older than 5.
            IEnumerable<string> names = from person in People
                                        where person.Pets.All ( pet => pet.Age > 5 )
                                        select person.LastName;
            /* Haas
            * Antebi
            */
        }

        bool AnyGoodMovies ()
        {
            return ( goodMovies.Any () );
        }

        void AnyPet ()
        {
            // Determine which people have a non-empty Pet array.
            IEnumerable<string> names = from person in People
                                        where person.Pets.Any ()
                                        select person.LastName;


            // Determine whether any pets over age 1 are also unvaccinated.
            bool morePoop =
            //People.Any ( p => p.Pets.Length > 1 );
            People.Any ( p => p.Pets.Any ( pet => pet.Age > 3 ) );


        }

        void AppendMovie ()
        {// Creating a list of numbers
            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers = (List<int>)numbers.Append ( 5 );

        }

        #region Clump class with Where override
        // Custom class.
        // AsEnumerable method can be used to hide the custom methods
        // and instead make the standard query operators available.
        class Clump<T> : List<T>
        {
            // Custom implementation of Where().
            public IEnumerable<T> Where ( Func<T, bool> predicate )
            {
                Console.WriteLine ( "In Clump's implementation of Where()." );
                return Enumerable.Where ( this, predicate );
            }
        }

        static void AsEnumerableEx1 ()
        {
            // Create a new Clump<T> object.
            Clump<string> fruitClump =
                new Clump<string> { "apple", "passionfruit", "banana",
            "mango", "orange", "blueberry", "grape", "strawberry" };

            // First call to Where():
            // Call Clump's Where() method with a predicate.
            IEnumerable<string> query1 =
                fruitClump.Where ( fruit => fruit.Contains ( "o" ) );

            Console.WriteLine ( "query1 has been created.\n" );

            // Second call to Where():
            // First call AsEnumerable() to hide Clump's Where() method and thereby
            // force System.Linq.Enumerable's Where() method to be called.
            IEnumerable<string> query2 =
                fruitClump.AsEnumerable ().Where ( fruit => fruit.Contains ( "o" ) );

            // Display the output.
            Console.WriteLine ( "query2 has been created." );


        }
        // In Clump's implementation of Where().
        // query1 has been created.
        //
        // query2 has been created.
        #endregion


        void CastMethod ()
        {
            System.Collections.ArrayList fruits = new System.Collections.ArrayList ();
            fruits.Add ( "mango" );
            fruits.Add ( "apple" );
            fruits.Add ( "lemon" );

            //  Need to cast objects to string.
            IEnumerable<string> query =
                fruits.Cast<string> ().OrderBy ( fruit => fruit ).Select ( fruit => fruit );

            // The following code, without the cast, doesn't compile.
            //IEnumerable<string> query1 =
            //    fruits.OrderBy(fruit => fruit).Select(fruit => fruit);
        }

        //public static IList<IList<T>> Split<T> ( IList<T> source )
        //{
        //    return
        //        source
        //        .Cast<IList<IList<T>> ()
        //        .Select ( ( x, i ) => new { Index = i, Value = x } )
        //        .GroupBy ( x => x.Index / 3 )
        //        .Select ( x => x.Select ( v => v.Value ).ToList () )
        //        .ToList ();


        //    source.AsEnumerable ()
        //       .Cast < IList<IList<T>> ()
        //    var _ = new List<List<T>> ();
        //    return _;
        //}







    }

}
