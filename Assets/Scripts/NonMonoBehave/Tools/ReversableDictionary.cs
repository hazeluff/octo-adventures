using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversableDictionary<L, R> {

    private Dictionary<L, R> left = new Dictionary<L, R>();
    private Dictionary<R, L> right = new Dictionary<R, L>();

    public ReversableDictionary() {

    }

    public void Add(L l, R r) {
        left.Add(l, r);
        right.Add(r, l);
    }

    public void Remove(L l) {
        R r = left[l];
        left.Remove(l);
        right.Remove(r);
    }

    public void Remove(R r) {
        L l = right[r];
        right.Remove(r);
        left.Remove(l);
    }

    public R Get(L l) {
        return left[l];
    }

    public L Get(R r) {
        return right[r];
    }

    public bool ContainsKey(L l) {
        return left.ContainsKey(l);
    }

    public bool ContainsKey(R r) {
        return right.ContainsKey(r);
    }

    public Dictionary<L, R>.Enumerator GetEnumerator() {
        return left.GetEnumerator();
    }

    public Dictionary<R, L>.Enumerator GetReverseEnumerator() {
        return right.GetEnumerator();
    }
}
