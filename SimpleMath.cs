using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console
{
    class SimpleMath
    {
        //  Lambda Expressions with Multiple or Zero Parameters.

        //  Create MathMessage type.
        public delegate void MathMessage ( string message, int result );
        //  Create instance of MathMessage.
        MathMessage mathMessageDelagate;
        //  Add targets for the MathMessage instance.
        public void SetMathHandler ( MathMessage target )
        { mathMessageDelagate = target; }

        public void methodWithMathMessage (MathMessage target )
        {  }

        public void Add ( int x, int y )
        { mathMessageDelagate?.Invoke ( $"Added:  {x} & {y}", x + y ); }

        public int Subtract ( int x, int y ) => x - y;

        public void PrintSum ( int x, int y ) => Console.WriteLine ( x + y );

    }
}
