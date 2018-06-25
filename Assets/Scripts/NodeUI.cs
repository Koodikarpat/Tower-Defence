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

       ToggleRange();

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

        target.GetComponent<Nodes>().turret.GetComponent<turret>().UpdateRange();
        ToggleRange();
        ui.SetActive(true);
    }
    void ToggleRange()
    {
       if(target != null) target.GetComponent<Nodes>().turret.GetComponent<turret>().ToggleRange();
    }
    public void Hide()
    {
        ToggleRange();
        target = null;
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.GetComponent<Nodes>().turret.GetComponent<turret>().UpgradeTurret();
        target.GetComponent<Nodes>().gameMaster.GetComponent<GameMaster>().goldupdate(-upgradeCost);
        BuildManager.instance.DeselectNode();
    }

}
