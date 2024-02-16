using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Scoreboard : MonoBehaviour
    {
        private Dictionary<string, int> scores = new Dictionary<string, int>();
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetScore(string playerName, int score)
        {
            if (scores.ContainsKey(playerName))
            {
                scores[playerName] = score;
            }
            else
            {
                scores.Add(playerName, score);
            }
        }

        public int GetScore(string playerName)
        {
            if (scores.ContainsKey(playerName))
            {
                return this.scores[playerName];
            }
            else
            {
                return 0;
            }
        }

        public void RemovePlayer(string playerName)
        {
            if (scores.ContainsKey(playerName))
            {
                scores.Remove(playerName);
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            foreach (var player in scores)
            {
                GUILayout.Label(player.Key + ": " + player.Value, new GUIStyle() {normal = new GUIStyleState {textColor = Color.black},  fontSize = 30 });
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
