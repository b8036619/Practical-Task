using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private float time = 0;

    //private GameObject[,] mapOfObjects;

    private List<GameObject> gameObjects;

    private char keyPressed;

    // Start is called before the first frame update
    void Start()
    {
        //mapOfObjects = new GameObject[15,15];

        for (int i = 0; i < 1; i++) { spawnObject(sphere, "Sphere"); spawnObject(capsule, "Capsule"); }
        spawnObject(cube, "Cube");

        keyPressed = 'f';
    }

    // Update is called once per frame
    void Update()
    {
        updateMapOfObjects();
        
        /*
        time = time + Time.deltaTime;
        if (time > 1)
        {
            spawnObject(cube, "Cube");
            time = 0;
        }
        */
        
    }

    private void spawnObject(GameObject obj, string name)
    {
        while (true)
        {
            int tempX = Random.Range(-7, 8);
            int tempZ = Random.Range(-7, 8);

            GameObject tempObj = (GameObject)Instantiate(obj, new Vector3(tempX, 0.5f, tempZ), Quaternion.identity);
            tempObj.name = name;
            gameObjects.Add(tempObj);
        }
    }

    private void updateMapOfObjects()
    {
        bool updateMap = false;

        if (Input.GetKeyDown("w")) { updateMap = true; keyPressed = 'w'; }

        if (Input.GetKeyDown("a")) { updateMap = true; keyPressed = 'a'; }

        if (Input.GetKeyDown("s")) { updateMap = true; keyPressed = 's'; }

        if (Input.GetKeyDown("d")) { updateMap = true; keyPressed = 'd'; }

        //Debug.Log("Key Pressed = " + keyPressed);
        
        if (updateMap == true) {

            for (int i = 0; i < gameObjects.Count; i++)
            {
                switch (keyPressed)
                {
                    case 'w':

                        if (gameObjects[i].name == "Cube") 
                        {
                            for (int k = 0; k < gameObjects.Count; k++)
                            {
                                if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && gameObjects[k].transform.position.z == gameObjects[i].transform.position.z + 1)
                                {
                                    gameObjects[k].transform.position += new Vector3(0, 0, 1);
                                }
                            }
                        }
                        

                        break;
                    case 'a':
                        break;
                    case 's':
                        break;
                    case 'd':
                        break;
                }
            }













            //GameObject[,] mapOfObjectsTemp = new GameObject[15, 15];

            /*
            for (int i = 0; i < mapOfObjects.GetLength(0); i++)
            {
                for (int k = 0; k < mapOfObjects.GetLength(1); k++)
                {
                    if (mapOfObjects[i, k] != null)
                    {
                        
                        switch (keyPressed)
                        {

                            case 'w':

                                
                                



                                

                                break;
                            case 'a':

                                


                                break;
                            case 's':

                                if (((k - 1) > -1) && mapOfObjects[i, k].name == "Cube")
                                {
                                    int pushCount = 1;
                                    while (true)
                                    {
                                        if ((k - pushCount) > -1)
                                        {
                                            if (mapOfObjects[i, (k - pushCount)] != null)
                                            {
                                                if (mapOfObjects[i , (k - pushCount)].name == "Sphere" || mapOfObjects[i, (k - pushCount)].name == "Capsule")
                                                {
                                                    pushCount++;
                                                }
                                                else { break; }
                                            }
                                            else { break; }
                                        }
                                        else { break; }
                                    }
                                    pushCount--;

                                    int pushCountNum = pushCount;
                                    for (int l = 0; l < pushCountNum; l++)
                                    {
                                        if ((k - pushCount - 1) < 0) { Destroy(mapOfObjects[i, (k - pushCount)]); }
                                        else
                                        {
                                            mapOfObjects[i, (k - pushCount)].transform.position += new Vector3(0, 0, -1);
                                            mapOfObjects[i, (k - pushCount - 1)] = mapOfObjects[i, (k - pushCount)];
                                        }
                                        pushCount--;
                                    }


                                    Debug.Log("S");
                                }
                                else if (mapOfObjects[i, k].name == "Cube") { Destroy(mapOfObjects[i, k]); }


                                break;
                            case 'd':

                                if ((i + 1) < 15 && mapOfObjects[i, k].name == "Cube")
                                {
                                    int pushCount = 1;
                                    while (true)
                                    {
                                        if ((i + pushCount) < 15)
                                        {
                                            if (mapOfObjects[i + pushCount, k] != null)
                                            {
                                                if (mapOfObjects[i + pushCount, k].name == "Sphere" || mapOfObjects[i + pushCount, k].name == "Capsule")
                                                {
                                                    pushCount++;
                                                }
                                                else { break; }
                                            }
                                            else { break; }
                                        }
                                        else { break; }
                                    }
                                    pushCount--;

                                    int pushCountNum = pushCount;
                                    for (int l = 0; l < pushCountNum; l++)
                                    {
                                        if ((i + pushCount + 1) > 14) { Destroy(mapOfObjects[i + pushCount, k]); }
                                        else
                                        {
                                            mapOfObjects[i + pushCount, k].transform.position += new Vector3(1, 0, 0);
                                            mapOfObjects[(i + pushCount + 1), k] = mapOfObjects[i + pushCount, k];
                                        }
                                        pushCount--;
                                    }


                                    Debug.Log("D");
                                }
                                else if (mapOfObjects[i, k].name == "Cube") { Destroy(mapOfObjects[i, k]); }


                                break;
                            default:
                                //Debug.LogError("Error!");
                                break;
                        }


                    }
                }
            }
            */
        }
    }

   private bool NearCheck(int i, int k, char direction)
    {
        /*
        Debug.Log("NearCheck");
        if (mapOfObjects[i,k] != null) 
        {
            Debug.Log("TEST");
            if (mapOfObjects[i, k].name == "Cube")
            {
                Debug.Log("Return True!!!!!!!!!!!!!!!!!");
                return true;
            }
            else
            {
                switch (direction)
                {
                    case 'w':
                        NearCheck(i, k - 1, 'w');
                        break;
                    case 'a':
                        NearCheck(i + 1, k, 'a');
                        break;
                    case 's':
                        NearCheck(i, k + 1, 's');
                        break;
                    case 'd':
                        NearCheck(i - 1, k, 'd');
                        break;
                }
            }
        }
        
        return false;
        ----------------------------------------------------
        if (mapOfObjects[i, k] != null)
        {
            Debug.Log("Object");
            if (mapOfObjects[i, k].name == "Cube")
            {
                Debug.Log("Return True!!!!!!!!!!!!!!!!!");
                return true;
            }
            else { return false; }
        }
        else { return false; }
        */
        return false;
    }

}
