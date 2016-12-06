using UnityEngine;
using System.Collections;

public class ShowParticles : MonoBehaviour {
    ParticleSystem hit_particles;
	// Use this for initialization

	
	// Update is called once per frame
	public void play_particles (Vector3 pos) {
       
        hit_particles = GetComponentInChildren<ParticleSystem>();
        if (hit_particles.isPlaying)
        {
            hit_particles.Stop();
        }
        if (!hit_particles.isPlaying)
        {
            // hit_particles.transform.position = pos;
            //Debug.Log(hit_particles.transform.position);
            hit_particles.Play();
        }

       
	}
}
