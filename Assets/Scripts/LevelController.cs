using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public int level = 0;
    public bool inLevel = false;
    public Text levelText;

    private CreepSpawner[] creepSpawners;

    void Awake() {
        levelText.text = "Level "+level.ToString();
        creepSpawners = GameObject.FindObjectsOfType<CreepSpawner>();
    }

    // Use this for initialization
    void Start () {
	    
    }


    IEnumerator Spawn(int qty, float hp, int attack) {
        for (int i = 0; i < qty; i++) {
            creepSpawners[Random.Range(0, 3)].SpawnOnce(hp, attack);
            yield return new WaitForSeconds (0.2f);
        }
    }

    public IEnumerator SpawnLevel() {
        levelText.text = "Level "+level.ToString();
        inLevel = true;

        for (int i = 0; i < level + 2; i++) {
            Debug.Log("Spawn: "+((int)((level*2+4)*i/4f)).ToString());
            StartCoroutine(Spawn(
                (int)((level*2+4)*(1+i/4f)),//Number
                (float)(level+2)*(level+2)*0.5f,//Hit points
                (int)(Mathf.Sqrt(level*10+90)*2)//Attack
            ));
            yield return new WaitForSeconds (8f);
        }

        inLevel = false;
    }
}
