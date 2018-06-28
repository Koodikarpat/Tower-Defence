using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    private Nodes target;

    public int upgradeCost = 75;

    public Text upgradeCostText;

    public Button upgradeButton;

    public Text sellAmountText;

    private bool isClicked=false;
    


    public void SetTarget (Nodes _target)
    {
        if (target == _target)
        {
            Hide();
            return;
        }

        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.GetComponent<Nodes>().turret.GetComponent<TurretBase>().isUpgraded)
        {
            upgradeCostText.text = "$" + upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCostText.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmountText.text = "$" + target.GetComponent<Nodes>().turret.GetComponent<TurretBase>().GetSellAmount();

        
        ui.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("hei");
        if (EventSystem.current.IsPointerOverGameObject()&&!isClicked)
        {
            return;
        }
        isClicked = false;
        ui.SetActive(false);
        target = null;
    }

    public void Upgrade()
    {
        
        target.GetComponent<Nodes>().turret.GetComponent<TurretBase>().UpgradeTurret();
        target.GetComponent<Nodes>().gameMaster.GetComponent<GameMaster>().goldupdate(-upgradeCost);
        isClicked = true;
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.GetComponent<Nodes>().turret.GetComponent<TurretBase>().SellTurret();
        isClicked = true;
        BuildManager.instance.DeselectNode();
    }

}
