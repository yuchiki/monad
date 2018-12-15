using System;

namespace Monad {
    using static Maybe;

    public static class Maybe {
        public static Maybe<T> Nothing<T>() => Maybe<T>.Nothing;
        public static Maybe<T> Return<T>(T value) => new Just<T>(value);
        public static Maybe<T> Bind<S, T>(Maybe<S> x, Func<S, Maybe<T>> f) {
            switch (x) {
                case Nothing<S> _:
                    return Nothing<T>();
                case Just<S> just:
                    return f(just.Value);
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class Maybe<T> {
        public static implicit operator Maybe<T>(T value) => Return(value);
        public static readonly Maybe<T> Nothing = Nothing<T>.Instance;
        public Maybe<T2> Select<T2>(Func<T, T2> f) {
            Func<T, Maybe<T2>> g = x => Maybe.Return(f(x));
            return Maybe.Bind(this, g);
        }
        public Maybe<T3> SelectMany<T2, T3>(Func<T, Maybe<T2>> f, Func<T, T2, T3> g) =>
        Maybe.Bind(
            this,
            y => Maybe.Bind(
                f(y),
                z => Maybe.Return(g(y, z))));
    }

    class Nothing<T> : Maybe<T> {
        public static readonly Nothing<T> Instance = new Nothing<T>();
        private Nothing() {}

        public override string ToString() => "Nothing";
    }

    class Just<T> : Maybe<T> {
        public readonly T Value;
        public Just(T value) => Value = value;

        public override string ToString() => $"Just {Value}";
    }
}
