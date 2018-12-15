using System;

using static Monad.Maybe;
using static System.Console;

namespace Monad {
    static class Program {
        static void Main() {
            MaybeTest();
            WriterTest();
        }

        static void MaybeTest() {
            var N = Maybe.Nothing<int>();
            WriteLine(f(N, 2, 3));
            WriteLine(f(1, N, N));
            WriteLine(f(1, 2, 3));
            WriteLine();
        }

        static void WriterTest() {
            var n =
                from a in (Writer<int>) (3, "a is 3, ")
            from b in (Writer<int>) (a + 4, "b is a + 4,")
            from c in (Writer<int>) (a * b, "c is a * b,")
            select c;
            WriteLine($"n.Value = {n.Value}, n.Log = {n.Log}");
            WriteLine();
        }

        static Maybe<int> f(Maybe<int> ma, Maybe<int> mb, Maybe<int> mc) =>
            from a in ma
        from b in mb
        from c in mc
        select a * 100 + b * 10 + c;


    }
}
