using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;

namespace MyFirstARGame
{
    public class Scoreboard : MonoBehaviourPunCallbacks
    {
        private Dictionary<string, int> scores = new Dictionary<string, int>();
        //<Cube ID, PlayerName>
        private Dictionary<string, string> targetScores = new Dictionary<string, string>();
        private Dictionary<string, int> result = new Dictionary<string, int>();

        public string winner;

        [SerializeField] private Text text;
        
        // Start is called before the first frame update
        void Start()
        {
            text = GameObject.Find("ScoreText").GetComponent<Text>();
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

        public void SetScore(string playerName, string t)
        {
            if (targetScores.ContainsKey(t))
            {
                targetScores[t] = playerName;
            }
            else
            {
                targetScores.Add(t, playerName);
            }
            if (!result.ContainsKey(playerName))
            {
                result.Add(playerName, 0);
            }
            //Debug.Log(result.Count + " : " + playerName);
            TargetLauncher.Instance.CheckTargetsStatus();
            //Debug.Log("SetScore: " + targetScores.Count + t);
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

        public void SetScoreText()
        {
            //text.text = "Score:\n";
            //sort targetScores based on value

            //Debug.Log("SetScoreText: " + targetScores.Count);
            foreach (var targetScore in targetScores)
            {
                //Debug.Log(targetScore.Key + " : " + targetScore.Value);
                //Debug.Log(TargetLauncher.Instance.targetList.Count);
                //Debug.Log(TargetLauncher.Instance.targetScores.Count);

                if (TargetLauncher.Instance.targetScores.ContainsKey(targetScore.Key)) {
                    //Debug.Log(targetScore.Value + " : " + TargetLauncher.Instance.targetScores[targetScore.Key]);
                    result[targetScore.Value] += TargetLauncher.Instance.targetScores[targetScore.Key];
                }
                //Debug.Log(targetScore.Value + " : " + result[targetScore.Value]);
            }

            var targetResults = result.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            winner = targetResults.First().Key;
            foreach (var player in targetResults) 
            {
                text.text = player.Key + ": " + player.Value + "\n";
            }
        }

        public Dictionary<string, string>  GetResult()
        {
            return targetScores;
        }

        public string GetScore(Target target)
        {
            if (scores.ContainsKey(target.GetID()))
            {
                return this.targetScores[target.GetID()];
            }
            return "Draw";
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
