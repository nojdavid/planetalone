using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using LitJson;

public class Dialogue : MonoBehaviour {

    /*
    //CATEGORIES OF DIOLOGUE
    List<Tuple> frustration; // 0
    List<Tuple> comfort;     // 1
    List<Tuple> instructions; // FOF == 3
    */
    public List<Item> database = new List<Item>();
    private JsonData itemData;

    // Use this for initialization
    void Awake () {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/Sounds/sounds.json"));
        ConstructItemDatabse();
	}

    void ConstructItemDatabse()
    {
        //Debug.Log(" itemData.Count " + itemData.Count);
        for (int i = 0; i < itemData.Count; ++i)
        {
            database.Add(new Item((string)itemData[i]["title"], (int)itemData[i]["fof"], (int)itemData[i]["emotion"], (string)itemData[i]["action_tag"]));
        }
    }
	
	public Item ItemFetchItemByID(int id)
    {
        return id < itemData.Count ? database[id] : null;
    }
}

public class Item
{
    public string Title { get; set; }
    public AudioClip Sources { get; set; }
    public int FOF { get; set; }
    public int Emotion { get; set; }
    public string Action_tag { get; set; }

    public Item(string title, int fof, int emotion, string action_tag)
    {
        this.Title = title;
       // Debug.Log("Sounds/" + title);
        this.Sources = Resources.Load<AudioClip>("Sounds/" + title);
        //Debug.Log(this.Sources);
        this.FOF = fof;
        this.Emotion = emotion;
        this.Action_tag = action_tag;
    }
}
