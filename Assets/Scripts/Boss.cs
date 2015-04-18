using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public float attackRadius;
    public int attackDamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Attack
        Vector3 center = new Vector3(
               transform.position.x,
               0,
               transform.position.z
            );
        DamageAround(center, attackRadius, attackDamage);

        // Death
        if (gameObject.GetComponent<HitPoint>().hp <= 0)
        {
            Destroy(gameObject);
            //TODO : End of Game
        }
	
	}

    void DamageAround(Vector3 center, float radius, int damage) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            hitColliders[i].SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            i++;
        }
    }

}
