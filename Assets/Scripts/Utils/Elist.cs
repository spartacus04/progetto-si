using System;
using System.Linq;
using System.Collections.Generic;

class Elist<T> : List<T> {
    // Impossibile vivere senza .forEach
    // - uno sviluppatore TS
    public void forEach(Action<T> act) {
        foreach(T e in this) {
            act(e);
        }
    }
}