using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

    public int damages = 1;

	private Color clrBase;
    private bool active = true;

	void Awake () {
	    clrBase = GetComponent<Renderer>().material.color;
        objectsInside = new List<GameObject>();
	}


	void OnMouseDown() {
        if (active) {
            Instantiate(
                Resources.Load("vfx_trap"),
                new Vector3(transform.position.x, 3f, transform.position.z), 
                Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f))
            );
            active = false;
            GetComponent<Renderer>().material.color = new Color(0.0F, 0.0F, 0.0F);
            Invoke("Rearm", 5f);

            foreach (GameObject o in objectsInside) {
                if (o != null)
                {
                    HitPoint hp = o.GetComponent<HitPoint>();
                    if (hp != null)
                    {
                        hp.hp -= damages;
                    }
                }
            }
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
        if(active)
            GetComponent<Renderer>().material.color = new Color(1.0F, 0.87F, 0.75F);
        else
            GetComponent<Renderer>().material.color = new Color(0.1F, 0.1F, 0.1F);
	}

	void OnMouseExit() {
        if(active)
            GetComponent<Renderer>().material.color = clrBase;
        else
            GetComponent<Renderer>().material.color = new Color(0.0F, 0.0F, 0.0F);
	}

    void Rearm() {
        active = true;
        GetComponent<Renderer>().material.color = clrBase;
    }
}
