using System;

namespace Monad {
    public static class State {
        public static State<TState, TResult> Return<TState, TResult>(TResult value) =>
            new State<TState, TResult>(state => (value, state));

        public static State<TState, TResult2> Bind<TState, TResult1, TResult2>(State<TState, TResult1> x, Func<TResult1, State<TState, TResult2>> f) =>
            new State<TState, TResult2>(y => {
                var (z, newState) = x.Function(y);
                return f(z).Function(newState);
            });
    }

    public class State<TState, T> {
        public readonly Func < TState,
        (T, TState) > Function;
        public State(Func < TState, (T, TState) > function) => Function = function;


        public State<TState, T2> Select<T2>(Func<T, T2> f) {
            Func<T, State<TState, T2>> g = x => State.Return<TState, T2>(f(x));
            return State.Bind(this, g);
        }
        public State<TState, T3> SelectMany<T2, T3>(Func<T, State<TState, T2>> f, Func<T, T2, T3> g) =>
        State.Bind<TState, T, T3>(
            this,
            y => State.Bind<TState, T2, T3>(
                f(y),
                z => State.Return<TState, T3>(g(y, z))));
    }
}
