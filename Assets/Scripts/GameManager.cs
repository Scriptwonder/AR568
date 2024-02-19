using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MyFirstARGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int MaxPlayerNum = 2;
        [SerializeField] private float StartCountDownTime = 3.0f;
        [SerializeField] private float GameCountDownTime = 30.0f;
        [SerializeField] private float OverCountDownTime = 10.0f;
        [SerializeField] private ProjectileLauncher ProjectileBehaviour;
        [SerializeField] private Scoreboard scoreboard;
        public static GameManager Instance { get; private set; }
        private bool startCountDown = false;
        private bool gameStarted = false;
        private bool overCountDown = false;
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
                if(timer > GameCountDownTime)
                {
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
        }
    }
}
