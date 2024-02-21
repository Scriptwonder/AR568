namespace MyFirstARGame
{
    using Photon.Pun;
    using UnityEngine;
    
    /// <summary>
    /// You can use this class to make RPC calls between the clients. It is already spawned on each client with networking capabilities.
    /// </summary>
    public class NetworkCommunication : MonoBehaviourPun
    {
        [SerializeField]
        private Scoreboard scoreboard;
        // Start is called before the first frame update
        void Start()
        {
            //var playerName = $"Player {photonView.Owner.ActorNumber}";
            //GameManager.Instance.playerName = playerName;

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementScore()
        {
            var playerName = $"Player {photonView.Owner.ActorNumber}";
            //GameManager.Instance.playerName = playerName;
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetScore", RpcTarget.All, playerName, currentScore + 1);
        }

        public void UpdateTargetScore(string target)
        {
            //var playerName = $"Player {photonView.Owner.ActorNumber}";
            var playerName = GameManager.Instance.playerName;
            this.photonView.RPC("Network_SetTargetScore", RpcTarget.All, playerName, target);
        }

        public string GetPlayerName()
        {
            return $"Player {photonView.Owner.ActorNumber}";
        }

        [PunRPC]
        public void Network_SetTargetScore(string playerName, string target)
        {
            Debug.Log($"Target {target} belong to {playerName}");
            this.scoreboard.SetScore(playerName, target);
            //this.scoreboard.SetScoreText();
        }

        [PunRPC]
        public void Network_SetScore(string playerName, int score)
        {
            Debug.Log($"Player {playerName} has a score of {score}");
            this.scoreboard.SetScore(playerName, score);
            this.scoreboard.SetScoreText();
        }

        public void UpdateForNewPlayer(Photon.Realtime.Player newPlayer)
        {
            var playerName = $"Player {newPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetScore", newPlayer, playerName, currentScore);
        }
    }

}