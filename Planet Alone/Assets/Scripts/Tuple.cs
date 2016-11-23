using UnityEngine;
using System.Collections;

public struct Tuple{
    public int count;
    public AudioClip A;

    public Tuple(AudioClip B, int c) {
        this.A = B;
        this.count = c;
    }
}
