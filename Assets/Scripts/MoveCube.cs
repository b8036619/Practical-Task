using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public bool moveLeft;
    public bool moveRight;
    public bool moveUp;
    public bool moveDown;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        moveRight = true;
        moveUp = true;
        moveDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            this.gameObject.transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown("a"))
        {
            this.gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            this.gameObject.transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown("d"))
        {
            this.gameObject.transform.position += new Vector3(1, 0, 0);
        }
    }
}
