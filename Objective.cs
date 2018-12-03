using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public AudioClip activeClip;
    public AudioClip doneClip;

    private AudioSource source;
    private Slider progress;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        progress = GetComponentInChildren<Slider>();
        progress.value = 0;
        progress.gameObject.SetActive(false);
	}
	
	void Update ()
    {
        if (GameController.instance.CurrentState == GameController.GameState.Playing)
        {
            if (progress.IsActive())
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (!source.isPlaying)
                    {
                        source.PlayOneShot(activeClip);
                    }

                    progress.value += Time.deltaTime / 2;

                    if (progress.value > .99f)
                    {
                        GameController.instance.objectives--;
                        progress.gameObject.SetActive(false);
                        GetComponent<Collider2D>().enabled = false;
                        source.Stop();
                        source.PlayOneShot(doneClip);
                    }
                }
                else
                {
                    progress.value -= Time.deltaTime / 2;
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            progress.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            progress.gameObject.SetActive(false);
            progress.value = 0;
        }
    }
}
