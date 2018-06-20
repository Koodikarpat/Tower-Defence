using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    private Nodes target;

    public int upgradeCost = 50;

    public Text upgradeCostText;
    




    public void SetTarget (Nodes _target)
    {
        if (target == _target)
        {
            Hide();
            return;
        }

        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.GetComponent<Nodes>().turret.GetComponent<turret>().isUpgraded)
        {
            upgradeCostText.text = "$" + upgradeCost;
        }
        else
        {
            upgradeCostText.text = "DONE";
        }
        

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.GetComponent<Nodes>().turret.GetComponent<turret>().UpgradeTurret();
        target.GetComponent<Nodes>().gameMaster.GetComponent<GameMaster>().goldupdate(-upgradeCost);
        BuildManager.instance.DeselectNode();
    }

}
