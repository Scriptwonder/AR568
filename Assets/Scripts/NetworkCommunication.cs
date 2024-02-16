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

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementScore()
        {
            var playerName = $"Player {photonView.Owner.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetScore", RpcTarget.All, playerName, currentScore + 1);
        }

        [PunRPC]
        public void Network_SetScore(string playerName, int score)
        {
            Debug.Log($"Player {playerName} has a score of {score}");
            this.scoreboard.SetScore(playerName, score);
        }

        public void UpdateForNewPlayer(Photon.Realtime.Player newPlayer)
        {
            var playerName = $"Player {newPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetScore", newPlayer, playerName, currentScore);
        }
    }

}