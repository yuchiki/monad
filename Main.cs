using System;

using static Monad.Maybe;
using static System.Console;

namespace Monad {
    class Program {
        static void Main() {
            var N = Nothing<int>();
            WriteLine(f(1, 2, 3));
            WriteLine(f(N, 2, 3));
            WriteLine(f(1, N, 3));
            WriteLine(f(1, 2, N));
            WriteLine(f(N, N, 3));
        }

        static Maybe<int> f(Maybe<int> ma, Maybe<int> mb, Maybe<int> mc) =>
            from a in ma
        from b in mb
        from c in mc
        select a * 100 + b * 10 + c;

    }
}
