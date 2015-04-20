using UnityEngine;
using System.Collections;

public class Creep : MonoBehaviour {

    public GameObject target;
    public float attackRange;
    public int attack;
    private NavMeshAgent navAgent;
    private ui_controller ui;

	// Use this for initialization
	protected void Awake () {
	    navAgent = GetComponent<NavMeshAgent>();
        ui = GameObject.Find("Canvas").GetComponent<ui_controller>();
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
            Instantiate(Resources.Load("vfx_creep_death"), transform.position, Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f)));
            Destroy(gameObject);
            ui.addGold(1);
        }
	}

    void AddDamage(int damage)
    {
        gameObject.GetComponent<HitPoint>().hp -= damage;
    }

    void OnCollisionEnter (Collision col){
        if(navAgent.pathStatus == NavMeshPathStatus.PathPartial){

            if(col.gameObject.name == "Wall(Clone)" && navAgent.remainingDistance<0.5f){
                DestroyObject(col.gameObject);
                navAgent.Resume();
            }
        }
        
    }
}