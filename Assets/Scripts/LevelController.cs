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


    IEnumerator Spawn(int qty) {
        for (int i = 0; i < qty; i++) {
            creepSpawners[Random.Range(0, 3)].SpawnOnce();
            yield return new WaitForSeconds (0.2f);
        }
    }

    public IEnumerator SpawnLevel() {
        levelText.text = "Level "+level.ToString();
        inLevel = true;
        switch (level) {
            case 1:
                StartCoroutine(Spawn(6));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(10));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(20));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(10));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(30));
                break;
            case 2:
                StartCoroutine(Spawn(50));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(100));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(200));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(200));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(500));
                break;
            default:
                StartCoroutine(Spawn(500));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(600));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(1000));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(500));
                yield return new WaitForSeconds (10f);
                StartCoroutine(Spawn(5000));
                break;
        }
        inLevel = false;
    }
}
