using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorial : MonoBehaviour {

    //TEXT NOT IMPLEMENTED -> COPY OBJECTIVE

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.E))
        {
            GameController.instance.targets = 0;
            GameController.instance.objectives = 0;
        }
    }
}
