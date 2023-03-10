using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private float time = 0;
    private float overallTime = 0;

    private int numPushedObj = 0;

    private List<GameObject> gameObjects;

    private char keyPressed;

    private bool play;

    private float points;
    public TextMeshProUGUI pointsText;
    private float score;
    public TextMeshProUGUI scoreText;
    private int level;
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI levelTitleText;

    private bool spawnCube;

    private List<string> previouslyRemovedObj;

    public Menu menu;


    void Start() // Variables set at start
    {
        play = false;

        gameObjects = new List<GameObject>();

        previouslyRemovedObj = new List<string>();

        keyPressed = 'f';

        level = 0;

        spawnCube = false;

        overallTime = 0;

        numPushedObj = 0;

        points = 0;

        score = 0;

    }

    void Update()
    {
        if (play) 
        {
            // Score, level and points are updated
            scoreText.text = "Score: " + (Mathf.Round(score * 1000.0f) * 0.001f);
            levelText.text = "Level: " + level;
            pointsText.text = "Points: " + points;

            UpdateMapOfObjects();

            time = time + Time.deltaTime; // Cubes are created every 0.5 seconds
            if (time > 0.5f && spawnCube)
            {
                SpawnObject(cube, "Cube");
                time = 0;
            }

            overallTime = overallTime + Time.deltaTime;

        }

    }

    private void FixedUpdate()
    {
        if (play)
        {
            LossCheck();
        }
    }

    public void StartGame() { play = true; } // Allows the game to run

    public void StartAgain() // Resets variables for the game to start from the begining
    {
        previouslyRemovedObj.Clear();
        keyPressed = 'f';
        level = 0;
        spawnCube = false;
        overallTime = 0;
        numPushedObj = 0;
        points = 0;
        score = 0;
        play = true;
    }

    public void NewLevel() // Sets up next level
    {
        level++;

        RemoveAllObjects();

        StartCoroutine(LevelTitleText());

        StartCoroutine(SpawnObjects());

    }

    IEnumerator SpawnObjects() // Creates new spheres and capsules after 1 second
    {
        spawnCube = false;
        yield return new WaitForSeconds(1f);
        spawnCube = true;
        for (int i = 0; i < 4; i++) { SpawnObject(sphere, "Sphere"); SpawnObject(capsule, "Capsule"); }
    }

    IEnumerator LevelTitleText() // Level number is displayed to the player for 2 seconds
    {
        levelTitleText.text = "Level " + level;
        yield return new WaitForSeconds(2f);
        levelTitleText.text = "";
    }

    private void LevelCheck() // Checks if the user has gained enough points to move to the next level or won
    {
        int pointCheck = 10000;
        switch (level)
        {
            case 1:
                pointCheck = 100;
                break;
            case 2:
                pointCheck = 200;
                break;
            case 3:
                pointCheck = 400;
                break;
            case 4:
                
                break;
        }

        if (points >= 400)
        {
            play = false;
            menu.EndScreen(overallTime, score, numPushedObj, true);
        }

        else if (points >= pointCheck) { NewLevel(); }
    }

    private void SpawnObject(GameObject obj, string name) // Creates a new sphere, capsule or cube and places it in a random location in the play area
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

    private void LossCheck() // Checks if the player has been surrounded by cubes and if so loses
    {
        int numSurroundingCubes = 0;

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].name == "Cube" && 
                ((gameObjects[i].transform.position.x == 1 && gameObjects[i].transform.position.z == 0) ||
                (gameObjects[i].transform.position.x == -1 && gameObjects[i].transform.position.z == 0) ||
                (gameObjects[i].transform.position.x == 0 && gameObjects[i].transform.position.z == 1) ||
                (gameObjects[i].transform.position.x == 0 && gameObjects[i].transform.position.z == -1))) 
            { numSurroundingCubes++; }
        }

        if (numSurroundingCubes == 4) 
        {
            play = false;
            menu.EndScreen(overallTime, score, numPushedObj, false);
        }
        
    }

    private void UpdateMapOfObjects() // Moves cubes based on player inputs and moves spheres and capsules if they are being pushed
    {
        bool updateMap = false;

        if (Input.GetKeyDown("w")) { updateMap = true; keyPressed = 'w'; }

        else if (Input.GetKeyDown("a")) { updateMap = true; keyPressed = 'a'; }

        else if (Input.GetKeyDown("s")) { updateMap = true; keyPressed = 's'; }

        else if (Input.GetKeyDown("d")) { updateMap = true; keyPressed = 'd'; }


        if (updateMap == true)
        {
            int testX = 0; // Check if the cube movement is possible
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
                                    for (int k = 0; k < gameObjects.Count; k++) // Loop to check if a row of objects is being pushed by cube
                                    {
                                        if ((gameObjects[k].name == "Sphere" || gameObjects[k].name == "Capsule") && (gameObjects[k].transform.position.z == gameObjects[l].transform.position.z + checkVal)
                                            && (gameObjects[k].transform.position.x == gameObjects[l].transform.position.x) && (k != l))
                                        {

                                            gameObjects[k].transform.position += new Vector3(0, 0, 1); // Moves spheres and capsules if being pushed
                                            l = k;
                                            found = true;
                                            checkVal = 0;
                                            numPushedObj++;

                                        }
                                    }

                                    if (found == false) { break; }
                                    else { found = false; }
                                }

                                gameObjects[i].transform.position += new Vector3(0, 0, 1); // Moves Cube
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
                                            numPushedObj++;

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
                                            numPushedObj++;

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
                                            numPushedObj++;

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
                if (gameObjects[i].transform.position.x == 0 && gameObjects[i].transform.position.z == 0) // Check if object is on player
                {
                    string name = gameObjects[i].name;
                    RemoveObject(i);
                    AddPoints(name);
                }

                else if (gameObjects[i].name == "Sphere" || gameObjects[i].name == "Capsule")
                {
                    if (gameObjects[i].transform.position.x > 8 || gameObjects[i].transform.position.x < -8 ||  // Check if object has left the play area
                        gameObjects[i].transform.position.z > 8 || gameObjects[i].transform.position.z < -8) { RemoveObject(i); }
                }

            }
            
        }
    }

    private void RemoveObject(int i) // Removes object from game and if it is sphere or capsule another is created
    {
        if (gameObjects[i].name == "Sphere") { SpawnObject(sphere, "Sphere"); }
        else if (gameObjects[i].name == "Capsule") { SpawnObject(capsule, "Capsule"); }

        GameObject tempObj = gameObjects[i];
        gameObjects.RemoveAt(i);
        Destroy(tempObj);
    }

    private void RemoveAllObjects() // Removes all objects from game
    {
        int loopCount = gameObjects.Count;
        for (int i = 0; i < loopCount; i++)
        {
            GameObject tempObj = gameObjects[0];
            gameObjects.RemoveAt(0);
            Destroy(tempObj);
        }
    }

    private void AddPoints(string objName) // Adds points depending on level and the previous objects the player has gained score from
    {
        points = points + 10;

        float increaseAmount = 0;
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

        if (previouslyRemovedObj.Count > 0)
        {
            int previousCount = 0;
            int pos = 1;
            while (previouslyRemovedObj.Count - pos > -1)
            {
                if (previouslyRemovedObj[previouslyRemovedObj.Count - pos] == objName) { previousCount++; pos++; }
                else { break; }
            }

            if (previousCount == 0) { previouslyRemovedObj.Clear(); }
            else 
            {
                for (int i = 0; i < previousCount; i++) { increaseAmount = increaseAmount / 2; }
            }

        }

        previouslyRemovedObj.Add(objName);

        score = score + increaseAmount;
        LevelCheck();
    }
}
