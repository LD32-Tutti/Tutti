using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Boss : MonoBehaviour {

    public float attackRadius;
    public int attackDamage;
    private Rigidbody rigidBody;
    private HitPoint hitPoints;
    private GameObject uiGameOver;

    Vector3 scaleBase;

	// Use this for initialization
	void Awake () {
	    rigidBody = GetComponent<Rigidbody>();
        hitPoints = gameObject.GetComponent<HitPoint>();
        scaleBase = transform.localScale;
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
        if (Input.GetKey("z") || Input.GetKey("w")) {
            transform.position += new Vector3(0.0f, 0.0f, speed*Time.deltaTime);
            Quaternion newAngles = Quaternion.Euler(
                    gameObject.transform.rotation.eulerAngles.x,
                    0,
                    gameObject.transform.rotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, newAngles, Time.deltaTime * speed);
        }
        if(Input.GetKey("s")) {
            transform.position += new Vector3(0.0f, 0.0f, -speed*Time.deltaTime);
            Quaternion newAngles = Quaternion.Euler(
                    gameObject.transform.rotation.eulerAngles.x,
                    180,
                    gameObject.transform.rotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, newAngles, Time.deltaTime * speed);
        }
        if(Input.GetKey("d")) {
            transform.position += new Vector3(speed*Time.deltaTime, 0.0f, 0.0f);
            Quaternion newAngles = Quaternion.Euler(
                    gameObject.transform.rotation.eulerAngles.x,
                    90,
                    gameObject.transform.rotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, newAngles, Time.deltaTime * speed);
        }
        if(Input.GetKey("q") || Input.GetKey("a")) {
            transform.position += new Vector3(-speed*Time.deltaTime, 0.0f, 0.0f);
            Quaternion newAngles = Quaternion.Euler(
                    gameObject.transform.rotation.eulerAngles.x,
                    270,
                    gameObject.transform.rotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, newAngles, Time.deltaTime * speed);
        }

        if (Input.GetKeyDown("space")) {
            if (Math.Abs(transform.position.y - 2.5f) < 0.2) {//origin at +2.0 from floor
                rigidBody.AddForce(new Vector3(0.0f, 200.0f, 0.0f), ForceMode.Impulse);
                StartCoroutine(Fall());
            }
        }

        transform.localScale = (float)(1+0.1*(transform.position.y-2.5))*scaleBase;
	
	}

    void DamageAround(Vector3 center, float radius, int damage) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        
        while (i < hitColliders.Length) {
            var distance = Math.Sqrt((hitColliders[i].transform.position - transform.position).sqrMagnitude);
            StartCoroutine(DamageAroundSignalEvent((float)(distance*0.03), hitColliders[i], damage));
            i++;
        }
    }
    IEnumerator DamageAroundSignalEvent(float delay, Collider source, int damage) {
        yield return new WaitForSeconds (delay);
        source.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator Fall() {
        yield return new WaitForSeconds (0.4f);
        rigidBody.AddForce(new Vector3(0.0f, -700.0f, 0.0f), ForceMode.Impulse);

        //Visuals
        yield return new WaitForSeconds (0.1f);
        var vfx = Instantiate(
            Resources.Load("vfx_stomp"), 
            new Vector3(transform.position.x, 0f, transform.position.z), 
            Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f))
        );

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
