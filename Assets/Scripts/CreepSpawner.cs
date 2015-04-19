using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour {

    private GameObject target;
    static private Object creepRes;
	void Awake () {
        target = GameObject.Find("boss");
        if(creepRes == null) creepRes = Resources.Load("creep");
	}

    private void TrySpawn() {
        if (Random.Range(0, 4) == 0){
            SpawnOnce(10f, 30);
        }
    }

    public void SpawnOnce (float hp, int attack) {
        GameObject creep = (GameObject)Instantiate(creepRes, transform.position, transform.rotation);
        var c = creep.GetComponentInChildren<Creep>();
        c.target = target;
        c.attack = attack;
        var h = creep.GetComponentInChildren<HitPoint>();
        h.hp = hp;
        h.maxhp = hp;

    }
}
