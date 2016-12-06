using UnityEngine;
using System.Collections;
/// <summary>
/// Audio, count
/// </summary>
public class Tuple{
    public int count;
    public AudioClip A;


    public Tuple(AudioClip B, int c) { 
        this.A = B;
        this.count = c;
    }

    public void Add(int c)
    {
        this.count += c;
    }
}
