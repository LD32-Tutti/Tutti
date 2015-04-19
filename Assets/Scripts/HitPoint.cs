using UnityEngine;
using System.Collections;

public class HitPoint : MonoBehaviour {
    public int hp;
    public int maxhp;

    void Awake(){
        maxhp = hp;
    }
}
