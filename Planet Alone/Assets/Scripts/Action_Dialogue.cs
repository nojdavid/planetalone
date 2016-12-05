using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Action_Dialogue
{
    /* Number of sound clips for each Action:
     Instruction: 5
     Greeting: 10
     Grab: 3
     Hitting: 5
     Hostile_items: 2
     Revovery: 6
     Shake: 3
     Throw: 5
         */

    AudioClip[] sources;
    Dialogue data;
    const int friend = 0;
    const int foe = 1;
    float weight;
    AudioSource audiosource;
    Utility utility;
    Robot_State rs;
    /*
    public Dictionary<int, List<Tuple>> dialogues = new Dictionary<int, List<Tuple>>()
    {
        { friend, new List<Tuple>() },
        { foe, new List<Tuple>() }
    };
    */

    public float get_weight()
    {
        return weight;
    }

    public List<List<Tuple>> dialogue = new List<List<Tuple>>{new List<Tuple>(),new List<Tuple>()};

    public Action_Dialogue(string tag, float w)
    {
        weight = w;
        data = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Dialogue>();
        audiosource = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<AudioSource>();
        utility = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Utility>();
        rs = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Robot_State>();
        for (int i = 0; i < data.database.Count; ++i)
        {
            if (data.database[i].Action_tag == tag)
            {
                dialogue[data.database[i].FOF].Add(new Tuple(data.database[i].Sources, 0));
            }
        }
    }

    public void Talk(int fof, int index, ref float ls) // ls = last speak
    {
        audiosource.clip = dialogue[fof][index].A;
        if (audiosource.clip != null)
        {
            if (rs.idle)
            {
                //fadeOut(audiosource,10f);
                audiosource.Stop();
                rs.idle = false;
            }
            dialogue[fof][index].count += 1;
            audiosource.Play();
            ls = Time.time;
        }
    }

    public static IEnumerator fadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
