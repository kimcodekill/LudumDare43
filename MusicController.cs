using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;

    private AudioSource source;
    
	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        if(intro != null)
        {
            source.loop = false;
            source.PlayOneShot(intro);
            StartCoroutine(Queue());
        }
        else
        {
            source.loop = true;
            source.clip = loop;
            source.Play();
        }
        		
	}

    IEnumerator Queue()
    {
        while (source.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        source.loop = true;
        source.clip = loop;
        source.Play();
    }
}
