using System;
using System.Collections.Generic;

using static Monad.Maybe;
using static System.Console;

namespace Monad {
    using IntStack = MyList<int>;
    using StackUpdater = Func < MyList<int>, (int, MyList<int>) >;

    static class Program {
        static void Main() {
            MaybeTest();
            WriterTest();
            StateTest();
        }

        static void MaybeTest() {
            WriteLine("Maybe Monad");

            var N = Maybe.Nothing<int>();
            WriteLine(f(N, 2, 3));
            WriteLine(f(1, N, N));
            WriteLine(f(1, 2, 3));
            WriteLine();
        }

        static void WriterTest() {
            WriteLine("Writer Monad");

            var n =
                from a in (Writer<int>) (3, "a is 3, ")
            from b in (Writer<int>) (a + 4, "b is a + 4,")
            from c in (Writer<int>) (a * b, "c is a * b,")
            select c;
            WriteLine($"n.Value = {n.Value}, n.Log = {n.Log}");
            WriteLine();
        }

        static void StateTest() {
            WriteLine("State Monad");

            var query =
                from _1 in new State<IntStack, int>(push(1))
            from _2 in new State<IntStack, int>(push(2))
            from _3 in new State<IntStack, int>(push(3))
            from _4 in new State<IntStack, int>(push(4))
            from _5 in new State<IntStack, int>(push(5))
            from _6 in new State<IntStack, int>(push(6))
            from a in new State<IntStack, int>(pop)
            from b in new State<IntStack, int>(pop)
            from c in new State<IntStack, int>(pop)
            from _ in new State<IntStack, int>(push(a + b))
            select c;
            WriteLine(query.Function(new Nil<int>()));
            WriteLine();
        }

        static Maybe<int> f(Maybe<int> ma, Maybe<int> mb, Maybe<int> mc) =>
            from a in ma
        from b in mb
        from c in mc
        select a * 100 + b * 10 + c;

        static IntStack newStack() => new Nil<int>();
        static StackUpdater pop = s => {
            var (v, newState) = ((Cons<int>) s);
            return (v, newState);
        };

        static StackUpdater push(int n) => s =>
            (0, new Cons<int>(n, s));
    }
}
