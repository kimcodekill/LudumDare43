using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public ParticleSystem particles;
    public Material particleMaterial;
	// Use this for initialization
	void Start ()
    {
        particleMaterial.SetTexture(Shader.PropertyToID("Particles/Additive"), GetComponent<SpriteRenderer>().sprite.texture);
        particles.GetComponent<ParticleSystemRenderer>().material = particleMaterial;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        Instantiate(particles);
    }
}
