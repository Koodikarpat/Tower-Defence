using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour {

    //private Color startColor;
    public Color hoverColor;
    public Color transparent;
    public Color startColor;

    public float targetTime = 2f;

    public GameObject text;

    public Vector3 positionOffset = new Vector3(0, 0, -1);

    public GameObject turret;

    private SpriteRenderer rend;

    BuildManager buildManager;
    public GameObject gameMaster;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        //rend.material.color = transparent;

        buildManager = BuildManager.instance;
        gameMaster = GameObject.Find("GameMaster");

        text = GameObject.Find("GameUICanvas").transform.Find("NoMoneyBG").gameObject;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject turretToBuild = buildManager.getTurretToBuild();
            if (turret != null)
            {
                buildManager.selectNode(this);
                return;
            }
            else if (turretToBuild == null)
            {
                buildManager.DeselectNode();
                return;
            }

            TurretBase turretScript = turretToBuild.GetComponent<TurretBase>();
            if (turretToBuild != null && turretScript != null && GameMaster.goldamount >= turretScript.cost)
            {

                if (buildManager.getTurretToBuild() == null)
                    return;

                if (!buildManager.CanBuild)
                    return;

                turret = (GameObject)Instantiate(turretToBuild, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                gameMaster.GetComponent<GameMaster>().goldupdate(-turretScript.cost);

            }
        }
        if (Input.GetMouseButton(1))
        {
             buildManager.setTurretToBuild(null);
             rend.material.color = startColor;
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        rend.material.color = hoverColor;
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void Update()
    {

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        text.SetActive(false);
        targetTime = 2f;
    }
}
