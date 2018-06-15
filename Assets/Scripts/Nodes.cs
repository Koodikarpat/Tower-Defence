using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour {

    private Color startColor;
    public Color hoverColor;

    public Vector3 positionOffset;

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
        if (buildManager.getTurretToBuild() == null)
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }

        GameObject turretToBuild = buildManager.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
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
