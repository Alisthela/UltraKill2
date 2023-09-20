using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCards : MonoBehaviour
{
    public Vector3 screenPosition;
    public float mouseposY;
    public float mouseposX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        mouseposY = Input.mousePosition.y;
        mouseposX = Input.mousePosition.x;

        if (mouseposX <= transform.position.x && mouseposY <= transform.position.y)
        {
            Debug.Log("touching");
           // if (mouseposX >= transform.position.x && mouseposY >= transform.position.y)
           // {
            //    Debug.Log("touching");
                //transform.position = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
           // }
        }
    }
}
