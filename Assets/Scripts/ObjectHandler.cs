using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHandler : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private float time = 0;

    private List<GameObject> gameObjects;

    private char keyPressed;

    private bool start;

    private float score;
    public TextMeshProUGUI scoreText;

    private int level;

    // Start is called before the first frame update
    void Start()
    {
        start = false;

        gameObjects = new List<GameObject>();

        for (int i = 0; i < 3; i++) { SpawnObject(sphere, "Sphere"); SpawnObject(capsule, "Capsule"); }

        keyPressed = 'f';

        level = 1;


    }

    // Update is called once per frame
    void Update()
    {
        if (start) 
        {
            UpdateMapOfObjects();

            scoreText.text = "Score: " + score;

            time = time + Time.deltaTime;
            if (time > 1.5f)
            {
                SpawnObject(cube, "Cube");
                time = 0;
            }

        }

    }

    public void StartGame() { start = true; }

    private void SpawnObject(GameObject obj, string name)
    {
        int attempts = 0;
        while (true)
        {
            int tempX = Random.Range(-7, 8);
            int tempZ = Random.Range(-7, 8);

            if ((tempX == 0 && tempZ == 0) == false) {

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

            }

            attempts++;
            if (attempts == 300) { break; }
        }

    }

    private void UpdateMapOfObjects()
    {
        bool updateMap = false;

        if (Input.GetKeyDown("w")) { updateMap = true; keyPressed = 'w'; }

        else if (Input.GetKeyDown("a")) { updateMap = true; keyPressed = 'a'; }

        else if (Input.GetKeyDown("s")) { updateMap = true; keyPressed = 's'; }

        else if (Input.GetKeyDown("d")) { updateMap = true; keyPressed = 'd'; }


        if (updateMap == true)
        {
            int testX = 0;
            int testZ = 0;
            bool possible = true;
            switch (keyPressed)
            {
                case 'w':
                    testZ = -1;
                    break;
                case 'a':
                    testX = 1;
                    break;
                case 's':
                    testZ = 1;
                    break;
                case 'd':
                    testX = -1;
                    break;
            }
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if ((gameObjects[i].name == "Cube") && (gameObjects[i].transform.position.x == testX && gameObjects[i].transform.position.z == testZ)) { possible = false; }
            }

            if (possible)
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
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].transform.position.x == 0 && gameObjects[i].transform.position.z == 0)
                {
                    AddPoints(gameObjects[i].name);
                    GameObject tempObj = gameObjects[i];
                    gameObjects.RemoveAt(i);
                    Destroy(tempObj);
                }
            }
            
        }
    }

    private void AddPoints(string objName)
    {
        int increaseAmount = 0;
        switch (objName)
        {
            case "Sphere":

                switch (level)
                {
                    case 1:
                        increaseAmount = 1;
                        break;
                    case 2:
                        increaseAmount = 10;
                        break;
                    case 3:
                        increaseAmount = 20;
                        break;
                }

                break;
            case "Capsule":

                switch (level)
                {
                    case 1:
                        increaseAmount = 2;
                        break;
                    case 2:
                        increaseAmount = 12;
                        break;
                    case 3:
                        increaseAmount = 22;
                        break;
                }

                break;
            default:

                break;
        }

        score = score + increaseAmount;

    }
}
