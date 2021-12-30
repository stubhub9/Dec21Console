using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console
{
    class Program
    {
        /*  Although I dislike using  nonGUI programs; perhaps some thought towards a 
         *  SHOWPIECE console, shell, hybrid *limited* with blocky buttons on a tiny screen. 
         *  
         *  Good console programs:
         *          list what your options are; (y/n) or 
         *          Cut & Paste lines for inserting commands.
         */

        static void Main ( string [] args )
        {
            //var a = new LinqStuff ();
            //LambdaWriteLines ();
            //LambdaPaloozaStuff ();
            var simpleLinq = new LinqStuff ();
            ScratchPad ();



            Console.ReadLine ();
        }

        static void LinqMethod ()
        {

            var simpleMath = new SimpleMath ();
            //  Lambda Expressions with Multiple or Zero Parameters.
            simpleMath.SetMathHandler ( ( message, result ) =>
            { Console.WriteLine ( $"Message:  {message},  Result:  {result}" ); } );
            simpleMath.Add ( 10, 20 );
        }

        static void ScratchPad ()
        {
            IEnumerable<int> squares =
                Enumerable.Range ( 1, 10 ).Select ( x => x * x );

            foreach ( var item in squares )
            {
                Console.WriteLine ( item );

            }

            Console.ReadLine ();
        }


    }
}
