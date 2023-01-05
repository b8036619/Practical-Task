using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private float time = 0;

    private GameObject[,] mapOfObjects;

    // Start is called before the first frame update
    void Start()
    {
        mapOfObjects = new GameObject[15,15];

        for (int i = 0; i < 4; i++) { spawnObject(sphere); spawnObject(capsule); }
    }

    // Update is called once per frame
    void Update()
    {
        updateMapOfObjects();

        time = time + Time.deltaTime;
        if (time > 5)
        {
            spawnObject(cube);
            time = 0;
        }
    }

    private void spawnObject(GameObject obj)
    {
        while (true)
        {
            int tempX = Random.Range(-7, 8);
            int tempZ = Random.Range(-7, 8);

            if (mapOfObjects[tempX + 7, tempZ + 7] == null)
            {
                mapOfObjects[tempX + 7, tempZ + 7] = (GameObject)Instantiate(obj, new Vector3(tempX, 0.5f, tempZ), Quaternion.identity);
                break;
            }
        }
    }

    private void updateMapOfObjects()
    {
        bool updateMap = false;
        char keyPressed = 'f';
        if (Input.GetKeyDown("w")) { updateMap = true; keyPressed = 'w'; }

        if (Input.GetKeyDown("a")) { updateMap = true; keyPressed = 'a'; }

        if (Input.GetKeyDown("s")) { updateMap = true; keyPressed = 's'; }

        if (Input.GetKeyDown("d")) { updateMap = true; keyPressed = 'd'; }

        if (updateMap == true) {
            GameObject[,] mapOfObjectsTemp = new GameObject[15, 15];

            for (int i = 0; i < mapOfObjects.GetLength(0); i++)
            {
                for (int k = 0; k < mapOfObjects.GetLength(1); k++)
                {
                    if (mapOfObjects[i,k] == cube) {
                        switch (keyPressed)
                        {
                            case 'w':

                                //if (mapOfObjects[i,k] == sphere && mapOfObjects[i,k - 1] != null) { }

                                if ((k + 1) < 15) { mapOfObjectsTemp[i, k + 1] = mapOfObjects[i, k]; }
                                break;
                            case 'a':
                                if ((i - 1) > -1) { mapOfObjectsTemp[i - 1, k] = mapOfObjects[i, k]; }
                                break;
                            case 's':
                                if ((k - 1) > -1) { mapOfObjectsTemp[i, k - 1] = mapOfObjects[i, k]; }
                                break;
                            case 'd':
                                if ((i + 1) < 15) { mapOfObjectsTemp[i + 1, k] = mapOfObjects[i, k]; }
                                break;
                            default:
                                Debug.LogError("Error!");
                                break;
                        }
                    }
                }
            }

            mapOfObjects = mapOfObjectsTemp;

        }
    }

}
