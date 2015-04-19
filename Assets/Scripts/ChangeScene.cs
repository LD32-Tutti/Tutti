using UnityEngine;
using System.Collections;
using System;

public class ChangeScene : MonoBehaviour {

	public string nextScene;
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown || Input.GetMouseButton(0))
        {
            Application.LoadLevel(nextScene);
        }

	}

}
