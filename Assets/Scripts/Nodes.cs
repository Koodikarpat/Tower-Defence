using UnityEngine;

public class Nodes : MonoBehaviour {

    private Color startColor;
    public Color hoverColor;
    private SpriteRenderer rend;

	
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
	}

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
