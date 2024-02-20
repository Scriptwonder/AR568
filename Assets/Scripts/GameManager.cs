using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace MyFirstARGame
{
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField] private int MaxPlayerNum = 1;
        [SerializeField] private float StartCountDownTime = 3.0f;
        [SerializeField] private float GameCountDownTime = 30.0f;
        [SerializeField] private float OverCountDownTime = 10.0f;
        [SerializeField] private ProjectileLauncher ProjectileBehaviour;
        [SerializeField] private Scoreboard scoreboard;
        public static GameManager Instance { get; private set; }
        private bool startCountDown = false;
        private bool gameStarted = false;
        private bool overCountDown = false;

        public Text countdownText;
        float timer = 0;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            ProjectileBehaviour = FindObjectOfType<ProjectileLauncher>();
            scoreboard = FindObjectOfType<Scoreboard>();
            startCountDown = false;
            gameStarted = false;
            overCountDown = false;
        }

        private void Start()
        {
            if(PhotonNetwork.PlayerList.Length == MaxPlayerNum)
            {
                TargetLauncher.Instance.LoadTargets();
                PhotonNetwork.CurrentRoom.IsOpen = false;
                startCountDown = true;
            }
        }
        void Update()
        {
            if(startCountDown)
            {
                timer += Time.deltaTime;
                
                if(timer > StartCountDownTime)
                {
                    
                    timer = 0;
                    startCountDown = false;
                    gameStarted = true;
                    ProjectileBehaviour.OpenFire();
                }
            }

            if(gameStarted)
            {
                timer += Time.deltaTime;
                countdownText.text = ((int)(GameCountDownTime - timer)).ToString();
                if(timer > GameCountDownTime)
                {
                    countdownText.text = "00";
                    timer = 0;
                    gameStarted = false;
                    GameOver();
                }
                if(GameCountDownTime - timer < OverCountDownTime)
                {
                    overCountDown = true;
                }
            }
        }

        public void BeginOverCountDown()
        {
            if(!gameStarted) { return; }
            timer = GameCountDownTime - OverCountDownTime;
            overCountDown = true;
        }

        public void GameOver()
        {
            ProjectileBehaviour.StopFire();
            Dictionary<string, string> targetScores = scoreboard.GetResult();

            //Show UI Changes
            scoreboard.SetScoreText();
            var playerName = $"Player {photonView.Owner.ActorNumber}";
            if (scoreboard.winner == playerName)
            {
                countdownText.text = "You Win!";
            }
            else
            {
                countdownText.text = "You Lose!";
            }

        }

        public void RestartGame()
        {
            // ProjectileBehaviour.StopFire();
            // TargetLauncher.Instance.LoadTargets();
            // startCountDown = true;
        }
    }
}
