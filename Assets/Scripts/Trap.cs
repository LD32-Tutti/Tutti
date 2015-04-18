using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

	private Color clrBase;
	void Awake () {
	    clrBase = GetComponent<Renderer>().material.color;
        objectsInside = new List<GameObject>();
	}


	void OnMouseDown() {
        foreach (GameObject o in objectsInside) {
            Destroy(o);
        }
	}
    
    private List<GameObject> objectsInside;
    void OnTriggerEnter(Collider other) {
        objectsInside.Add(other.gameObject);
    }
    void OnTriggerExit(Collider other) {
        objectsInside.Remove(other.gameObject);
    }

	void OnMouseEnter() {
        GetComponent<Renderer>().material.color = new Color(1.0F, 0.87F, 0.75F);
	}

	void OnMouseExit() {
        GetComponent<Renderer>().material.color = clrBase;
	}
}
