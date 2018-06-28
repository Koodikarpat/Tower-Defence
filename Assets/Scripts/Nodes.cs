using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour {

    //private Color startColor;
    public Color hoverColor;
    public Color transparent;

    public float targetTime = 2f;

    public GameObject noMoneyText;

    public Vector3 positionOffset = new Vector3(0, 0, -1);

    public GameObject turret;

    private SpriteRenderer rend;

    BuildManager buildManager;
    public GameObject gameMaster;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //startColor = rend.material.color;
        rend.material.color = transparent;

        buildManager = BuildManager.instance;
        gameMaster = GameObject.Find("GameMaster");

       // Debug.Log(GameObject.Find("Canvas").transform.childCount);
        noMoneyText = GameObject.Find("GameUICanvas").transform.Find("NoMoneyBG").gameObject;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
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

            if (turretToBuild != null && GameMaster.goldamount >= turretToBuild.GetComponent<turret>().cost)
            {
                if (buildManager.getTurretToBuild() == null)
                    return;

                if (!buildManager.CanBuild)
                    return;


                if (turret != null)
                {
                    Debug.Log("Can't build there! - TODO: Display on screen.");
                    return;
                }

                // GameObject turretToBuild = buildManager.getTurretToBuild();
                turret = (GameObject)Instantiate(turretToBuild, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                gameMaster.GetComponent<GameMaster>().goldupdate(-turret.GetComponent<turret>().cost);

                if (noMoneyText.activeSelf)
                {
                    noMoneyText.SetActive(false);

                }
            }
            else if (turretToBuild != null) noMoneyText.SetActive(true);
            //timerEnded();
        }
        if (Input.GetMouseButton(1))
        {
             buildManager.setTurretToBuild(null);
             rend.material.color = transparent;
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (buildManager.getTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
        
    }

    void OnMouseExit()
    {
        rend.material.color = transparent;
    }

    void Update()
    {

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        if (noMoneyText.activeSelf)
        {
            targetTime -= Time.deltaTime;
        }

    }

    void timerEnded()
    {
        noMoneyText.SetActive(false);
        targetTime = 2f;
    }
}
