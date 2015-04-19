using UnityEngine;
using System.Collections;

public class CreepFriend : MonoBehaviour {

    private GameObject target;
    public float attackRange;
    public int attack;
    private NavMeshAgent navAgent;

	// Use this for initialization
	protected void Awake () {
	    navAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("AttackNearest", 0f, 1f);
	}
	
	// Update is called once per frame
	protected void Update () {

        // Target
        if (target != null) {

            float distance = Vector3.Distance(transform.position, target.transform.position);

            // Walk To
            if (distance > attackRange)
            {
                navAgent.Resume();
                navAgent.destination = target.transform.position;
            }
            // Attack
            else
            {
                navAgent.Stop();
                target.GetComponent<HitPoint>().hp -= attack*Time.deltaTime;
            }
            
        }

        // Death
        if (gameObject.GetComponent<HitPoint>().hp <= 0)
        {
            Destroy(gameObject);
        }
	}

    void AddDamage(int damage)
    {
        gameObject.GetComponent<HitPoint>().hp -= damage;
    }

        void AttackNearest(){
        target = (GameObject)FindClosestCreep().gameObject;
    }

    Creep FindClosestCreep() {
        Creep closest = null;
        float distClosest;

        var creeps = GameObject.FindObjectsOfType<Creep>();
        var dist = Mathf.Infinity;
        foreach (Creep creep in creeps) {
            Vector3 diff = creep.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < dist) {
                closest = creep;
                distClosest = curDistance;
            }
        }
        return closest;
    }
}