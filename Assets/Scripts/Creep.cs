using UnityEngine;
using System.Collections;

public class Creep : MonoBehaviour {

    public GameObject target;
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Awake () {
	    navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null) {
            navAgent.destination = target.transform.position;
        }
	}
}