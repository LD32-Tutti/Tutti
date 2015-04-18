using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public int healthPoints;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Death
         Debug.Log(gameObject.GetComponent<HitPoint>().hp);
        if (gameObject.GetComponent<HitPoint>().hp <= 0)
        {
            Destroy(gameObject);
            //TODO : End of Game
        }
	
	}
}
