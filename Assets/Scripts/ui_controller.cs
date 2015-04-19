using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_controller: MonoBehaviour {

    enum BuildMode
    {
        WALLS,
        TRAPS,
        NOTHING
    }

    private Button buttonWalls, buttonTraps, buttonCancel;
    private Text goldPointsText;
    private BuildMode buildMode;
    private GameObject tmpWall, tmpTrap;
    private float yRotation = 0.0f;
    private Vector3 outOfScreenPosition = new Vector3(-999, -999, -999);
    private int goldPoints = 0;

    public string planeTag = "Floor";
    public float maxDistanceHit = 250.0f;
    public float deltaRotation = 45.0f;
    public int costWall = 5;
    public int costTrap = 2;

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

        goldPointsText = FindObjectOfType<Text>();

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
        enableButtons();

        // Exit Build mode with mouse's right button
        if(Input.GetMouseButtonDown(1)){
            setModeNoBuild();
        }

        float deltaScroll = Input.GetAxis("Mouse ScrollWheel");
        if(deltaScroll > 0.0f){
            yRotation += deltaRotation;
        }
        else if(deltaScroll < 0.0f) {
            yRotation -= deltaRotation;
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
            tmpWall.transform.rotation = Quaternion.Euler(
                tmpWall.transform.rotation.eulerAngles.x,
                yRotation,
                tmpWall.transform.rotation.eulerAngles.z
            );


            if ( !cursorPosition.Equals(outOfScreenPosition) )
            {
                if(Input.GetMouseButtonDown(0)){
                    BuildWall(cursorPosition, yRotation);
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

            tmpTrap.transform.rotation = Quaternion.Euler(
                tmpTrap.transform.rotation.eulerAngles.x,
                yRotation,
                tmpTrap.transform.rotation.eulerAngles.z
            );

            if ( !cursorPosition.Equals(outOfScreenPosition) )
            {
                if(Input.GetMouseButtonDown(0)){
                    BuildTrap(cursorPosition, yRotation);
                }
            }
        }
    }

    void BuildWall(Vector3 position, float rotation)
    {
        if (goldPoints >= costWall)
        {
            GameObject wall = (GameObject) Instantiate(Resources.Load("Wall"));
            wall.transform.position = new Vector3 (
                position.x,
                0.09999914f,
                position.z
            );
            wall.transform.rotation = Quaternion.Euler(
                    wall.transform.rotation.eulerAngles.x,
                    rotation,
                    wall.transform.rotation.eulerAngles.z
            );
            addGold(-costWall);
        }
        
    }

    void BuildTrap(Vector3 position, float rotation)
    {
        if (goldPoints >= costTrap)
        {
            GameObject trap = (GameObject) Instantiate(Resources.Load("trap"));
		    trap.transform.position = new Vector3 (
                position.x,
                0.09999914f,
                position.z
            );
            trap.transform.rotation = Quaternion.Euler(
                    trap.transform.rotation.eulerAngles.x,
                    rotation,
                    trap.transform.rotation.eulerAngles.z
            );
            addGold(-costTrap);
        }
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

    public void addGold(int amount)
    {
        goldPoints += amount;
        goldPointsText.text = goldPoints + " " + (goldPoints <= 1 ?  "Gold" : "Golds");
    }

    void enableButtons()
    {
        // Do you have enough gold to set traps ?
        if (goldPoints >= costTrap)
        {
            buttonTraps.enabled = true;
        }
        else 
        {
            buttonTraps.enabled = false;
        }

        // Do you have enough gold to set walls ?
        if (goldPoints >= costWall)
        {
            buttonWalls.enabled = true;
        }
        else 
        {
            buttonWalls.enabled = false;
        }

        if (buildMode != BuildMode.NOTHING)
        {
            buttonWalls.enabled = true;
        }
        else 
        {
            buttonWalls.enabled = false;
        }
    }


}
