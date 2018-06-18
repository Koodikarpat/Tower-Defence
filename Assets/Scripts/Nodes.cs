using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour {

    private Color startColor;
    public Color hoverColor;

    public Vector3 positionOffset = new Vector3(0,0, -1);

    public GameObject turret;

    private SpriteRenderer rend;

    BuildManager buildManager;


	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
	}

    void OnMouseDown()
    {
        if (GameMaster.instance.goldamount >= 15)
        {
            if (buildManager.getTurretToBuild() == null)
            return;

            if (turret != null)
            {
                Debug.Log("Can't build there! - TODO: Display on screen.");
                return;
            }

            GameObject turretToBuild = buildManager.getTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
            GameMaster.instance.goldupdate(-15);
        }

    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
