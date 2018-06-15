using UnityEngine;

public class scriptcamera: MonoBehaviour
{
    public float speed = 30f;
    public float scrollspeed = 5f;
    public float minY = 10f;
    public float maxY = 60f;
    public Camera cam;


    void Update()
    {
        


        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.down * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoom = cam.orthographicSize;

        zoom -= scroll * 300 * scrollspeed * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, minY, maxY);

        cam.orthographicSize = zoom;
       


            
                   

    }

}




        
    

  











	

