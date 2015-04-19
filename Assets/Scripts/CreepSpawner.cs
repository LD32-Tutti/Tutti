using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour {

    private GameObject target;
    static private Object creepRes;
	void Awake () {
        target = GameObject.Find("boss");
        if(creepRes == null) creepRes = Resources.Load("creep");
	}

	// Use this for initialization
	void Start () {
        InvokeRepeating("TrySpawn", 0.0f, 1.0f);
	}

    private void TrySpawn() {
        if (Random.Range(0, 4) == 0){
            SpawnOnce();
        }
    }

    public void SpawnOnce () {
        GameObject creep = (GameObject)Instantiate(creepRes, transform.position, transform.rotation);
        creep.GetComponentInChildren<Creep>().target = target;
    }
}
