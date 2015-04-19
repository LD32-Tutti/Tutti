using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spawn_controller: MonoBehaviour {

    enum BuildMode
    {
        WALLS,
        TRAPS,
        NOTHING
    }

    private Button buttonWalls, buttonTraps, buttonCancel;
    private BuildMode buildMode;
    private GameObject tmpWall, tmpTrap;
    private Vector3 outOfScreenPosition = new Vector3(-999, -999, -999);

    public string planeTag = "Floor";
    public float maxDistanceHit = 250.0f;

    void Awake()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach(Button button in buttons){
            switch (button.name)
            {
                case "ButtonWalls":
                    buttonWalls = button;
                    break;
                case "ButtonTraps":
                    buttonTraps = button;
                    break;
                case "ButtonCancel":
                    buttonCancel = button;
                    break;
                default:
                    Debug.LogError("Found unkonwn UI button: " + button.name);
                    break;
            }
        }

        tmpWall = (GameObject) Instantiate(Resources.Load("TmpWall"));
        tmpWall.layer = 2; //Ignore Raycast
        tmpWall.transform.position = outOfScreenPosition;

        tmpTrap = (GameObject) Instantiate(Resources.Load("TmpTrap"));
        tmpTrap.layer = 2; //Ignore Raycast
        tmpTrap.transform.position = outOfScreenPosition;
    }

    void Start()
    {
        buildMode = BuildMode.NOTHING;
        buttonWalls.onClick.AddListener(()=>{
            setModeBuildWalls();           
        });
        buttonTraps.onClick.AddListener(()=>{
            setModeBuildTraps();           
        });
        buttonCancel.onClick.AddListener(()=>{
            setModeNoBuild();           
        });
    }

    void Update()
    {

        // Exit Build mode with mouse's right button
        if(Input.GetMouseButtonDown(1)){
            setModeNoBuild();
        }

        // Build walls
        if (buildMode == BuildMode.WALLS)
        {
            Vector3 cursorPosition = GetCursorPosition();

            tmpWall.transform.position = new Vector3 (
                cursorPosition.x,
                1.0f,
                cursorPosition.z
            );


            if ( !cursorPosition.Equals(outOfScreenPosition) )
            {
                if(Input.GetMouseButtonDown(0)){
                    BuildWall(cursorPosition);
                }
            } 
            
        }
        // Build traps
        else if (buildMode == BuildMode.TRAPS)
        {
            Vector3 cursorPosition = GetCursorPosition();

            tmpTrap.transform.position = new Vector3 (
                cursorPosition.x,
                1.0f,
                cursorPosition.z
            );

            if ( !cursorPosition.Equals(outOfScreenPosition) )
            {
                if(Input.GetMouseButtonDown(0)){
                    BuildTrap(cursorPosition);
                }
            }
        }
    }

    void BuildWall(Vector3 position)
    {
        GameObject wall = (GameObject) Instantiate(Resources.Load("Wall"));
        wall.transform.position = new Vector3 (
            position.x,
            0.09999914f,
            position.z
        );
    }

    void BuildTrap(Vector3 position)
    {
        GameObject trap = (GameObject) Instantiate(Resources.Load("trap"));
		trap.transform.position = new Vector3 (
            position.x,
            0.09999914f,
            position.z
        );
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

    void setModeBuildWalls()
    {
        tmpTrap.transform.position = outOfScreenPosition;
        buildMode = BuildMode.WALLS;
    }

    void setModeBuildTraps()
    {
        tmpWall.transform.position = outOfScreenPosition;
        buildMode = BuildMode.TRAPS;
    }

    void setModeNoBuild()
    {
        tmpWall.transform.position = outOfScreenPosition;
        tmpTrap.transform.position = outOfScreenPosition;
        buildMode = BuildMode.NOTHING;
    }


}
