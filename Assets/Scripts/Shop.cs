using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Use this for initialization
    public void PurchaseStandardTurret ()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.setTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseAnotherTurret()
    {
        Debug.Log("Another Turret Selected");
        buildManager.setTurretToBuild(buildManager.anotherTurretPrefab);
    }
}
