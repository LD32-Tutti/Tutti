using UnityEngine;
using System.Collections;

public class HitPoint : MonoBehaviour {
    public float hp;
    public float maxhp;

    void Awake(){
        maxhp = hp;
    }
}
