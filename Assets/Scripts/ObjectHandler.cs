using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private float time = 0;

    private List<GameObject> gameObjects;

    private char keyPressed;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();

        for (int i = 0; i < 3; i++) { spawnObject(sphere, "Sphere"); spawnObject(capsule, "Capsule"); }

        keyPressed = 'f';


    }

    // Update is called once per frame
    void Update()
    {
        updateMapOfObjects();

        
        time = time + Time.deltaTime;
        if (time > 1.5f)
        {
            spawnObject(cube, "Cube");
            time = 0;
        }
        

    }

    private void spawnObject(GameObject obj, string name)
    {
        int attempts = 0;
        while (true)
        {
            int tempX = Random.Range(-7, 8);
            int tempZ = Random.Range(-7, 8);

            bool empty = true;

            for (int i = 0; i < gameObjects.Count; i++) 
            {
                if (((int)gameObjects[i].transform.position.x) == tempX && ((int)gameObjects[i].transform.position.z) == tempZ)
                {
                    empty = false;
                }
            }
            
            if (empty) 
            {
                GameObject newObj = Instantiate(obj, new Vector3(tempX, 0.5f, tempZ), Quaternion.identity);
                gameObjects.Add(newObj.gameObject);
                gameObjects[(gameObjects.Count - 1)].name = name;

                break;
            }

            attempts++;

            if (attempts == 300) { break; }
        }

    }

    private void updateMapOfObjects()
    {
        bool updateMap = false;

        if (Input.GetKeyDown("w")) { updateMap = true; keyPressed = 'w'; }

        else if (Input.GetKeyDown("a")) { updateMap = true; keyPressed = 'a'; }

        else if (Input.GetKeyDown("s")) { updateMap = true; keyPressed = 's'; }

        else if (Input.GetKeyDown("d")) { updateMap = true; keyPressed = 'd'; }

        //Debug.Log("Key Pressed = " + keyPressed);

        if (updateMap == true)
        {

            for (int i = 0; i < gameObjects.Count; i++)
            {
                switch (keyPressed)
                {
                    case 'w':

                        if (gameObjects[i].name == "Cube")
                        {
                            int l = i;
                            bool found = false;

                            int checkVal = 1;
                            
                            while (true)
                            {
                                for (int k = 0; k < gameObjects.Count; k++)
                                {
                                    if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && (gameObjects[k].transform.position.z == gameObjects[l].transform.position.z + checkVal) 
                                        && (gameObjects[k].transform.position.x == gameObjects[l].transform.position.x) && (k != l))
                                    {
                                        
                                        gameObjects[k].transform.position += new Vector3(0, 0, 1);
                                        l = k;
                                        found = true;
                                        checkVal = 0;
                                        
                                    }
                                }

                                if (found == false) { break; }
                                else { found = false; }
                            }

                            gameObjects[i].transform.position += new Vector3(0, 0, 1);
                        }


                        break;
                    case 'a':

                        if (gameObjects[i].name == "Cube")
                        {
                            int l = i;
                            bool found = false;

                            int checkVal = 1;

                            while (true)
                            {
                                for (int k = 0; k < gameObjects.Count; k++)
                                {
                                    if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && (gameObjects[k].transform.position.x == gameObjects[l].transform.position.x - checkVal)
                                        && (gameObjects[k].transform.position.z == gameObjects[l].transform.position.z) && (k != l))
                                    {

                                        gameObjects[k].transform.position += new Vector3(-1, 0, 0);
                                        l = k;
                                        found = true;
                                        checkVal = 0;

                                    }
                                }

                                if (found == false) { break; }
                                else { found = false; }
                            }

                            gameObjects[i].transform.position += new Vector3(-1, 0, 0);
                        }

                        break;
                    case 's':

                        if (gameObjects[i].name == "Cube")
                        {
                            int l = i;
                            bool found = false;

                            int checkVal = 1;

                            while (true)
                            {
                                for (int k = 0; k < gameObjects.Count; k++)
                                {
                                    if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && (gameObjects[k].transform.position.z == gameObjects[l].transform.position.z - checkVal)
                                        && (gameObjects[k].transform.position.x == gameObjects[l].transform.position.x) && (k != l))
                                    {

                                        gameObjects[k].transform.position += new Vector3(0, 0, -1);
                                        l = k;
                                        found = true;
                                        checkVal = 0;

                                    }
                                }

                                if (found == false) { break; }
                                else { found = false; }
                            }

                            gameObjects[i].transform.position += new Vector3(0, 0, -1);
                        }

                        break;
                    case 'd':

                        if (gameObjects[i].name == "Cube")
                        {
                            int l = i;
                            bool found = false;

                            int checkVal = 1;

                            while (true)
                            {
                                for (int k = 0; k < gameObjects.Count; k++)
                                {
                                    if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && (gameObjects[k].transform.position.x == gameObjects[l].transform.position.x + checkVal)
                                        && (gameObjects[k].transform.position.z == gameObjects[l].transform.position.z) && (k != l))
                                    {

                                        gameObjects[k].transform.position += new Vector3(1, 0, 0);
                                        l = k;
                                        found = true;
                                        checkVal = 0;

                                    }
                                }

                                if (found == false) { break; }
                                else { found = false; }
                            }

                            gameObjects[i].transform.position += new Vector3(1, 0, 0);
                        }

                        break;
                }
            }

        }
    }
}
