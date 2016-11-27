using UnityEngine;
using System.Collections;

public class SeedCollide : MonoBehaviour {
    public GameObject plant_prefab;
    //public GameObject particleSystemGO;  //GO stands for gameobject.
   
    public GameObject particle_gameobject;
    ShowParticles particle_shower;
    private IEnumerator coroutine;
    GameObject plant;
    Renderer rend;

    void Start()
    {
        //particle_system = particleSystemGO.GetComponent<ParticleSystem>();
        
    }
 void OnCollisionEnter(Collision collision)
    {
        
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag("Terrain"))
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
            GameObject ps = (GameObject)Instantiate(particle_gameobject, pos, particle_gameobject.transform.rotation);
            particle_shower = ps.GetComponent<ShowParticles>();
            particle_shower.play_particles(pos);

            
            StartCoroutine(wait(0.5f, pos, ps));
            


        }
    }

    void OnCollisionExit(Collision collision)
    {
        /*
        if (particle_system.isPlaying)
        {
            psemit.enabled = false;
            particle_system.Stop();
        }
        */
    }

    private IEnumerator wait(float waitTime, Vector3 pos, GameObject ps)
    {


        yield return new WaitForSeconds(waitTime);
        plant = (GameObject)Instantiate(plant_prefab, pos, plant_prefab.transform.rotation);
        StartCoroutine(destroyps(ps));
       

    }
    private IEnumerator destroyps(GameObject ps)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(ps);
        StartCoroutine(destroygb());
    }

    private IEnumerator destroygb()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        
    }


}
