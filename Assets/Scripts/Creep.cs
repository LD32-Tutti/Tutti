using UnityEngine;
using System.Collections;

public class Creep : MonoBehaviour {

    public GameObject target;
    public float attackRange;
    public int attack;
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Awake () {
	    navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        // Target
        if (target != null) {

            float distance = Vector3.Distance(transform.position, target.transform.position);

            // Walk To
            if (distance > attackRange)
            {
                navAgent.destination = target.transform.position;
            }
            // Attack
            else
            {
                navAgent.Stop();
                target.GetComponent<HitPoint>().hp -= attack;
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
}