using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitPointBar : MonoBehaviour {
    public HitPoint target;
    public bool floating = true;
    private Slider lifeSlider;
    
	// Use this for initialization
	void Start () {
	    lifeSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        if(floating)
            transform.rotation = Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f));

	    lifeSlider.value = (float)target.hp / (float)target.maxhp;
	}
}
