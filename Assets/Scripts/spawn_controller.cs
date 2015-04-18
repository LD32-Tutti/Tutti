using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spawn_controller: MonoBehaviour {

    private Button mButton;
    private bool isBuildMode;
    private GameObject mTmpWall;
    private Vector3 outOfScreenPosition = new Vector3(-999, -999, -999);

    public string planeTag = "Floor";
    public float maxDistanceHit = 250.0f;
    public float buildOffsetY = 1.0f;

    void Awake()
    {
        mButton = GetComponent<Button>();
        mTmpWall = (GameObject) Instantiate(Resources.Load("TmpWall"));
        mTmpWall.layer = 2; //Ignore Raycast
        mTmpWall.transform.position = outOfScreenPosition;
    }

    void Start()
    {
        isBuildMode = false;
        mButton.onClick.AddListener(()=>{

            isBuildMode = !isBuildMode;

            if (isBuildMode)
            {
                //Switch to build mode
                mButton.transform.FindChild("Text").GetComponent<Text>().text = "Quit build mode";
            }
            else
            {
                //Exit build mode
                mButton.transform.FindChild("Text").GetComponent<Text>().text = "Build mode";
            }

           
        });
    }

    void Update()
    {
        if (isBuildMode)
        {
            Vector3 cursorPosition = GetCursorPosition();

            cursorPosition.y = cursorPosition.y + buildOffsetY;
            mTmpWall.transform.position = cursorPosition;

            if ( !cursorPosition.Equals(outOfScreenPosition) )
            {
                if(Input.GetMouseButtonDown(0)){
                    BuildWall(cursorPosition);
                }
            }     
            
        }
    }

    void BuildWall(Vector3 position)
    {
        GameObject wall = (GameObject) Instantiate(Resources.Load("Wall"));
		wall.transform.position = position;
    }

    Vector3 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Vector3 cursorPosition = new Vector3(outOfScreenPosition.x, outOfScreenPosition.y, outOfScreenPosition.z);

        if(Physics.Raycast(ray, out hit, maxDistanceHit)){
            if(hit.transform.CompareTag(planeTag)){
               cursorPosition.Set(hit.point.x, hit.point.y, hit.point.z);
            }
        }

        return cursorPosition;
    }


}
