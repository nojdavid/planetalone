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
    void Start () {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/Sounds/sounds.json"));
        ConstructItemDatabse();
	}

    void ConstructItemDatabse()
    {
        for (int i = 0; i < itemData.Count; ++i)
        {
            database.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["title"], (int)itemData[i]["fof"], (int)itemData[i]["emotion"]));
        }
    }
	
	public Item ItemFetchItemByID(int id)
    {
        return id < itemData.Count ? database[id] : null;
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public AudioClip Sources { get; set; }
    public int FOF { get; set; }
    public int Emotion { get; set; }

    public Item(int id, string title, int fof, int emotion)
    {
        this.ID = id;
        this.Title = title;
        Debug.Log("Sounds/" + title);
        this.Sources = Resources.Load<AudioClip>("Sounds/" + title);
        Debug.Log(this.Sources);
        this.FOF = fof;
        this.Emotion = emotion;
    }
}
