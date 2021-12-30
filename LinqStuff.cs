using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console

{
    public class LinqStuff
    {


        string [] goodMovies = { "Elmer Fudd", "Darn Rabbit 2", "Beep 3", "Duck" };


        public void QueryOverStrings ()
        {

            IEnumerable<string> subset =
                goodMovies.Where ( a => a.Contains ( " " ) ).OrderBy ( g => g ).Select ( g => g );

            //  Possible that subset is a different type here; than what
            //  "var" would have chosen. 
            subset = from film in goodMovies
                     where film.Contains ( " " )
                     orderby film
                     select film;

            IEnumerable<string> query =
                goodMovies.TakeWhile ( ( stringSelect, sourceIndex ) => stringSelect.Length >= sourceIndex );
            //  TSource          Func  ( <TSource> , int )     bool

        }

        void AggregateAccumulator ()
        {
            string [] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };

            // Determine whether any string in the array is longer than "banana".
            string longestName = fruits.Aggregate ( "banana", ( longest, next ) =>
                                 next.Length > longest.Length ? next : longest,
                                    fruit => fruit.ToUpper () );

            Console.WriteLine ( $"The fruit with the longest name is {longestName}." );
            // The fruit with the longest name is PASSIONFRUIT.

        }


        void AggregateAccumulator1 ()
        {
            int [] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };

            // Count the even numbers in the array, using a seed value of 0.
            int numEven = ints.Aggregate ( 0, ( total, next ) =>
                                                  next % 2 == 0 ? total + 1 : total );

            Console.WriteLine ( "The number of even integers is: {0}", numEven );
            // The number of even integers is: 6
        }




        void AggregateAccumulator2 ()
        {
            string sentence = "the quick brown fox jumps over the lazy dog";

            // Split the string into individual words.
            string [] words = sentence.Split ( ' ' );

            // Prepend each word to the beginning of the new sentence to reverse the word order.
            string reversed = words.Aggregate ( ( workingSentence, next ) =>
                                                    next + " " + workingSentence );

            Console.WriteLine ( reversed );
            // dog lazy the over jumps fox brown quick the


        }



        public void AllEx ()
        {
            bool allStartWithB =  goodMovies.All ( item =>
            item.StartsWith ( "B" ) );

            Console.WriteLine (
                $"{0} pet names start with 'B'.",
                allStartWithB ? "All" : "Not all" ); 
            //Console.WriteLine (
            //    $"{allStartWithB ? "All" : "Not all"} pet names start with 'B'."    );
        }
        //  Not all pet names start with 'B'.







    }
}
