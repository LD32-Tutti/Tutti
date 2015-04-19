using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Boss : MonoBehaviour {

    public float attackRadius;
    public int attackDamage;
    private NavMeshAgent navAgent;
    private Rigidbody rigidBody;
    private HitPoint hitPoints;
    private GameObject uiGameOver;

    Vector3 scaleBase, scaleTarget;

	// Use this for initialization
	void Awake () {
        navAgent = GetComponent<NavMeshAgent>();
	    rigidBody = GetComponent<Rigidbody>();
        hitPoints = gameObject.GetComponent<HitPoint>();
        scaleBase = transform.localScale;
        scaleTarget = scaleBase;
        uiGameOver = GameObject.Find("GameOverCanvas");
	}

    void Start()
    {
        uiGameOver.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
        
        // Death
        if (hitPoints.hp <= 0)
        {
            Destroy(gameObject);
            uiGameOver.SetActive(true);
        }

        //Boss control
        const float speed = 10.0f;
        if (Input.GetKey("z")) {
            transform.position += new Vector3(0.0f, 0.0f, speed*Time.deltaTime);
        }
        if(Input.GetKey("s")) {
            transform.position += new Vector3(0.0f, 0.0f, -speed*Time.deltaTime);
        }
        if(Input.GetKey("d")) {
            transform.position += new Vector3(speed*Time.deltaTime, 0.0f, 0.0f);
        }
        if(Input.GetKey("q")) {
            transform.position += new Vector3(-speed*Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown("space")) {
            if (Math.Abs(transform.position.y - 2f) < 0.2) {//origin at +2.0 from floor
                rigidBody.AddForce(new Vector3(0.0f, 200.0f, 0.0f), ForceMode.Impulse);
                scaleTarget = scaleBase*1.3f;
                StartCoroutine(Fall());
            }
        }

        transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, 10f*Time.deltaTime);
	
	}

    void DamageAround(Vector3 center, float radius, int damage) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            hitColliders[i].SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            i++;
        }
    }

    IEnumerator Fall() {
        yield return new WaitForSeconds (0.4f);
        rigidBody.AddForce(new Vector3(0.0f, -700.0f, 0.0f), ForceMode.Impulse);

        //Visuals
        yield return new WaitForSeconds (0.1f);
        var vfx = Instantiate(
            Resources.Load("vfx_stomp"), 
            new Vector3(transform.position.x, 0f, transform.position.z), 
            Quaternion.LookRotation(new Vector3(0f, 1f, 0f))
        );
        scaleTarget = scaleBase;

        //Damage
        Vector3 center = new Vector3(
               transform.position.x,
               0,
               transform.position.z
            );
        DamageAround(center, attackRadius, attackDamage);

        //Cleanup
        yield return new WaitForSeconds (1.5f);
        DestroyObject(vfx);
    }

}
