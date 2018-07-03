using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonExpand : MonoBehaviour {

    public float speed = 0f;
    public float sizeScaler = 0.3f;
    public bool isExpandOn = false;
    private float finalSizer;
    private float tickSpeed = 0.5f;
    private float tickTime = 0;
    public float realTimetimer = 0;
    public AnimationCurve smooth;

    public Vector3 scaleOne;
    public Vector3 scaleTwo;

    public float time = 1.5f;

    // Use this for initialization
    void Start() {
        scaleOne = new Vector3(0.1f, 0.1f, 0.1f);
        scaleTwo = new Vector3(3f, 3f, 3f);

    }

    // Update is called once per frame
    void Update() {
        tickTime += Time.deltaTime;

        while (realTimetimer <= time)
        {
            transform.localScale = Vector3.Lerp(scaleOne, scaleTwo, smooth.Evaluate(realTimetimer / time));
            realTimetimer += Time.deltaTime;
        }
        if (tickTime >= tickSpeed)
        {
            tickTime = 0;
            tickDebuff();
        }

    }

    void tickDebuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.localPosition, transform.localScale.x / 2);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().takeDamage(10);
            }
        }
    }
}
