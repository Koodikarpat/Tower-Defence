using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager : MonoBehaviour
{
    private GameObject turretToBuild;

    public static BuildManager instance;
    

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one buildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    public GameObject thirdTurretPrefab;

    public bool CanBuild { get { return turretToBuild != null; } }

    private Nodes selectedNode;
    public NodeUI nodeUI;


    public GameObject getTurretToBuild()
    {
        return turretToBuild;
    }

    public void setTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }
    
    

    public void selectNode(Nodes node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
