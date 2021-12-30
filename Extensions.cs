using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec21Console
{
    public static class Extensions
    {

        /// <summary>
        /// Break a list of items into chunks of a specific size
        /// </summary>
        public static  IEnumerable<IEnumerable<T>>    Chunk1<T>   ( this IEnumerable<T> source, int chunksize )
        {
            while ( source.Any () )
            {
                //  Take a bite; remove a bite.
                yield return source.Take ( chunksize );
                source = source.Skip ( chunksize );
            }
        }

        #region  Short Answer
        //  Short Answer
        public static IEnumerable<IEnumerable<T>> ChunkTrivialBetter<T> ( this IEnumerable<T> source, int chunksize )
            {
                var pos = 0;
                while ( source.Skip ( pos ).Any () )
                {
                    yield return source.Skip ( pos ).Take ( chunksize );
                    pos += chunksize;
                }

            foreach ( var item in Enumerable.Range ( 1, int.MaxValue ).Chunk ( 8 ).Skip ( 100000 ).First () )
            {

            }

        }
        //  End of short answer
        #endregion


        #region  Long Answer
        //  Long Answer
        class ChunkedEnumerable<T> : IEnumerable<T>
        {
            class ChildEnumerator : IEnumerator<T>
            {
                ChunkedEnumerable<T> parent;
                int position;
                bool isDone = false;
                T currentTypeT;


                public ChildEnumerator ( ChunkedEnumerable<T> parent )
                {
                    this.parent = parent;
                    position = -1;
                    parent.wrapper.AddRef ();
                }

                public T Current
                {
                    get
                    {
                        if ( position == -1 || isDone )
                        {
                            throw new InvalidOperationException ();
                        }
                        return currentTypeT;

                    }
                }

                public void Dispose ()
                {
                    if ( !isDone )
                    {
                        isDone = true;
                        parent.wrapper.RemoveRef ();
                    }
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return Current; }
                }

                public bool MoveNext ()
                {
                    position++;

                    if ( position + 1 > parent.chunkSize )
                    {
                        isDone = true;
                    }

                    if ( !isDone )
                    {
                        isDone = !parent.wrapper.Get ( position + parent.start, out currentTypeT );
                    }

                    return !isDone;

                }

                public void Reset ()
                {
                    // per http://msdn.microsoft.com/en-us/library/system.collections.ienumerator.reset.aspx
                    throw new NotSupportedException ();
                }
            }

            EnumeratorWrapper<T> wrapper;
            int chunkSize;
            int start;

            public ChunkedEnumerable ( EnumeratorWrapper<T> wrapper, int chunkSize, int start )
            {
                this.wrapper = wrapper;
                this.chunkSize = chunkSize;
                this.start = start;
            }

            public IEnumerator<T> GetEnumerator ()
            {
                return new ChildEnumerator ( this );
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
            {
                return GetEnumerator ();
            }

        }

        class EnumeratorWrapper<T>
        {
            public EnumeratorWrapper ( IEnumerable<T> source )
            {
                SourceEumerable = source;
            }
            IEnumerable<T> SourceEumerable { get; set; }

            Enumeration currentEnumeration;

            class Enumeration
            {
                public IEnumerator<T> Source { get; set; }
                public int Position { get; set; }
                public bool AtEnd { get; set; }
            }

            public bool Get ( int pos, out T item )
            {

                if ( currentEnumeration != null && currentEnumeration.Position > pos )
                {
                    currentEnumeration.Source.Dispose ();
                    currentEnumeration = null;
                }

                if ( currentEnumeration == null )
                {
                    currentEnumeration = new Enumeration { Position = -1, Source = SourceEumerable.GetEnumerator (), AtEnd = false };
                }

                item = default ( T );
                if ( currentEnumeration.AtEnd )
                {
                    return false;
                }

                while ( currentEnumeration.Position < pos )
                {
                    currentEnumeration.AtEnd = !currentEnumeration.Source.MoveNext ();
                    currentEnumeration.Position++;

                    if ( currentEnumeration.AtEnd )
                    {
                        return false;
                    }

                }

                item = currentEnumeration.Source.Current;

                return true;
            }

            int refs = 0;

            // needed for dispose semantics 
            public void AddRef ()
            {
                refs++;
            }

            public void RemoveRef ()
            {
                refs--;
                if ( refs == 0 && currentEnumeration != null )
                {
                    var copy = currentEnumeration;
                    currentEnumeration = null;
                    copy.Source.Dispose ();
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T> ( this IEnumerable<T> source, int chunksize )
        {
            if ( chunksize < 1 ) throw new InvalidOperationException ();

            var wrapper = new EnumeratorWrapper<T> ( source );

            int currentPos = 0;
            T ignore;
            try
            {
                wrapper.AddRef ();
                while ( wrapper.Get ( currentPos, out ignore ) )
                {
                    yield return new ChunkedEnumerable<T> ( wrapper, chunksize, currentPos );
                    currentPos += chunksize;
                }
            }
            finally
            {
                wrapper.RemoveRef ();
            }
        }
    }

    #endregion



    class Program1
    {
        static void Main1 ( string [] args )
        {
            int i = 10;


            foreach ( var group in Enumerable.Range ( 1, int.MaxValue ).Skip ( 10000000 ).Chunk ( 3 ) )
            {
                foreach ( var n in group )
                {
                    Console.Write ( n );
                    Console.Write ( " " );
                }
                Console.WriteLine ();
                if ( i-- == 0 ) break;
            }


            var stuffs = Enumerable.Range ( 1, 10 ).Chunk ( 2 ).ToArray ();

            foreach ( var idx in new [] { 3, 2, 1 } )
            {
                Console.Write ( "idx " + idx + " " );
                foreach ( var n in stuffs [idx] )
                {
                    Console.Write ( n );
                    Console.Write ( " " );
                }
                Console.WriteLine ();
            }

            /*

10000001 10000002 10000003
10000004 10000005 10000006
10000007 10000008 10000009
10000010 10000011 10000012
10000013 10000014 10000015
10000016 10000017 10000018
10000019 10000020 10000021
10000022 10000023 10000024
10000025 10000026 10000027
10000028 10000029 10000030
10000031 10000032 10000033
idx 3 7 8
idx 2 5 6
idx 1 3 4
             */

            Console.ReadKey ();


        }


    }




}
