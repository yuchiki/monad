using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Monad {
    public abstract class MyList<T> : IEnumerable<T> {
        public IEnumerator<T> GetEnumerator() {
            switch (this) {
                case Nil<T> _:
                    yield break;
                case Cons<T> c:
                    {
                        yield return c.Car;
                        foreach (var e in c.Cdr) yield return e;
                        yield break;
                    }
                default:
                    throw new InvalidOperationException();
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Nil<T> : MyList<T> {
        public override string ToString() => "[]";
    }

    public class Cons<T> : MyList<T> {
        public readonly T Car;
        public readonly MyList<T> Cdr;

        public Cons(T car, MyList<T> cdr) => (Car, Cdr) = (car, cdr);

        public void Deconstruct(out T car, out MyList<T> cdr) => (car, cdr) = (Car, Cdr);

        public override string ToString() => $"{Car} : {Cdr}";
    }
}
