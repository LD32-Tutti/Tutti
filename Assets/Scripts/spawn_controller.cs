using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spawn_controller: MonoBehaviour {

    private Button mButton;

    void Awake()
    {
        mButton = GetComponent<Button>();
    }

    void Start()
    {
        mButton.onClick.AddListener(()=>{
            GameObject wall = Instantiate(Resources.Load("Wall")) as GameObject;
			wall.transform.position = new Vector3( 0, 1, -10 );
        });
    }


}
