using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteJsonFile : MonoBehaviour
{
    public TextAsset jsonText;

    [System.Serializable]
    public class Player
    {
        public float time;
        public float score;
        public int objPushed;
    }

    public class PlayerList
    {
        public List<Player> players = new List<Player>();
    }

    private PlayerList playerList = new PlayerList();

    public void writeJSON(float newTime, float newScore, int newObjPushed) // Adds winning time, score and number of objects pushed to JSON file
    {
        Debug.Log("Write JSON");
        Player newPlayer = new Player();
        newPlayer.time = newTime;
        newPlayer.score = newScore;
        newPlayer.objPushed = newObjPushed;

        playerList.players.Add(newPlayer);

        string output = JsonUtility.ToJson(playerList);

        File.WriteAllText(Application.dataPath + "/SavedData.json", output);
    }

    private void Start() // Previous player stats are added to the player list
    {
        PlayerList tempPlayerList = new PlayerList();
        tempPlayerList = JsonUtility.FromJson<PlayerList>(jsonText.text);

        playerList = tempPlayerList;
    }

}
