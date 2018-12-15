using System;

namespace Monad {
    public static class Writer {
        public static Writer<T> Return<T>(T value) => new Writer<T>(value);
        public static Writer<T2> Bind<T1, T2>(Writer<T1> x, Func<T1, Writer<T2>> f) {
            var (value, log) = f(x.Value);
            return new Writer<T2>(value, x.Log + log);

        }
    }
    public class Writer<T> {
        public T Value { get; private set; }
        public string Log { get; private set; }
        public Writer(T value, string log = "") => (Value, Log) = (value, log);

        public void Deconstruct(out T value, out string log) => (value, log) = (Value, Log);

        public Writer<T2> Select<T2>(Func<T, T2> f) {
            Func<T, Writer<T2>> g = x => Writer.Return(f(x));
            return Writer.Bind(this, g);
        }
        public Writer<T3> SelectMany<T2, T3>(Func<T, Writer<T2>> f, Func<T, T2, T3> g) =>
        Writer.Bind(
            this,
            y => Writer.Bind(
                f(y),
                z => Writer.Return(g(y, z))));

        public static implicit operator Writer<T>((T value, string log) pair) =>
        new Writer<T>(pair.value, pair.log);
    }
}
