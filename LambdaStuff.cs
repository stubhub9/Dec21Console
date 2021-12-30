using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console
{
    class LambdaStuff
    {

        #region  Methods
        static void LambdaWriteLines ()
        {
            List<int> list = new List<int> ();
            list.AddRange ( new int [] { 20, 1, 4, 8, 9, 44 } );

            //  Anonymous method.
            List<int> evenNumbers = list.FindAll ( delegate ( int i )
            { return ( i % 2 ) == 0; } );

            List<int> oddNumbers = list.FindAll ( i => ( i % 2 ) != 0 );

            Console.WriteLine ( "Even numbers:" );
            foreach ( var evenNumber in evenNumbers )
            {
                Console.Write ( $"{evenNumber}\t" );
            }


            evenNumbers = list.FindAll ( ( i ) =>
            {
                Console.WriteLine ( $"i = {i}" );
                bool isEven = ( ( i % 2 ) == 0 );
                return isEven;
            } );






            //  End of Lambda writes.
        }
        #endregion


        #region  LambdaPalooza
        static void LambdaPaloozaStuff ()
        {
            //var lambdaStuff = new LambdaPalooza ();
            //Console.WriteLine ( lambdaStuff.square ( 5 ) );
            //  Lambda  Expressions
            Func<int, int> square = x => x * x;
            Console.WriteLine ( square ( 5 ) );

            System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine ( e );
            // x => (x * x)
            // for Task.Run ( Action )

            //  C#  LINQ
            int [] numbers = { 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select ( x => x * x );
            Console.WriteLine ( string.Join ( " ", squaredNumbers ) );
            // Output:
            // 4 9 16 25

            /*   The body of an expression lambda can consist of a method call. 
             *    expression trees that are evaluated outside the context of the .NET Common Language Runtime (CLR), 
             *    such as in SQL Server, you shouldn't use method calls in lambda expressions. 
             *    The methods will have no meaning outside the context of the .NET Common Language Runtime (CLR).
             * */

            //  Statement lambdas use braces; with typically 1 - 3 statements.
            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}!";
                Console.WriteLine ( greeting );
            };
            greet ( "World" );

            //  Use () for zero params.
            Action line = () => Console.WriteLine ();

            //  Single param or (param)
            Func<double, double> cube = x => x * x * x;

            //  (,)
            Func<int, int, bool> testForEquality = ( x, y ) => x == y;


            //  define a tuple by enclosing a comma-delimited list of its components in parentheses.
            //  refer to tuple members as Item1, Item2, ...
            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            //Func<(int n1, int n2, int n3), (int, int, int)> doubleThem = ns => (2 * ns.n1, 2 * ns.n2, 2 * ns.n3);
            var moNumbers = (2, 3, 4);
            var doubledNumbers = doubleThem ( moNumbers );
            Console.WriteLine ( $"The set {moNumbers} doubled: {doubledNumbers}" );
            // Output:
            // The set (2, 3, 4) doubled: (4, 6, 8)

            //  Async lambdas   ******           ***************           ********************             Async lambdas

            /*public Form1()
            {InitializeComponent ();
                button1.Click += button1_Click;   }

            private async void button1_Click ( object sender, EventArgs e )
            {
                await ExampleMethodAsync ();
                textBox1.Text += "\r\nControl returned to Click event handler.\n";
            }

            private async Task ExampleMethodAsync ()
            {    await Task.Delay ( 1000 );  }
            //or async click!!!!!!!!!!!!!!           !!!!!!!!!!!!!!!!!                              !!!!!!!!!!!!!!!!!!!!
            public Form1()
            {
                InitializeComponent ();
                button1.Click += async ( sender, e ) =>
                {    await ExampleMethodAsync ();
                    textBox1.Text += "\r\nControl returned to Click event handler.\n";        };
            }

            private async Task ExampleMethodAsync ()
            {   await Task.Delay ( 1000 );    }*/


            //  LINQ to objects have an input parameter  of the Func<TResult> family
            Func<int, bool> equalsFive = x => x == 5;
            bool result = equalsFive ( 4 );
            Console.WriteLine ( result );   // False


            //  **********         System.Linq.Enumerable.Methods.  XXX  Aggregate thru Zip. 
            //  re:  TakeWhile
            int [] numberArray = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var firstNumbersLessThanSix = numberArray.TakeWhile ( n => n < 6 );
            Console.WriteLine ( string.Join ( " ", firstNumbersLessThanSix ) );
            // Output:
            // 5 4 1 3

            var firstSmallNumbers = numberArray.TakeWhile ( ( n, index ) => n >= index );
            Console.WriteLine ( string.Join ( " ", firstSmallNumbers ) );


            var numberSets = new List<int []>
{
    new[] { 1, 2, 3, 4, 5 },
    new[] { 0, 0, 0 },
    new[] { 9, 8 },
    new[] { 1, 0, 1, 0, 1, 0, 1, 0 }
};

            var setsWithManyPositives =
                from numberSet in numberSets
                where numberSet.Count ( n => n > 0 ) > 3
                select numberSet;

            foreach ( var numberSet in setsWithManyPositives )
            {
                Console.WriteLine ( string.Join ( " ", numberSet ) );
            }
            // Output:
            // 1 2 3 4 5
            // 1 0 1 0 1 0 1 0

            //customers.Where ( c => c.City == "London" );


            //  Anonymous type,   in an anonymous type.
            var purchaseItem = new
            {
                TimeBought = DateTime.Now,
                ItemBought = new
                {
                    Color = "Red",
                    Make = "Ford",
                    CurrentSpeed = 55
                },
                Price = 34.000
            };




            //  End of LambdaPaloozaStuff
        }


        public int Add ( int x, int y ) => x + y;


        #endregion

    }
}
