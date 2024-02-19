namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;

    /// <summary>
    /// Controls projectile behaviour. In our case it currently only changes the material of the projectile based on the player that owns it.
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Material[] projectileMaterials;

        [SerializeField] private int bounceCount = 0;
        [SerializeField] private int maxBounceTime = 3;
        [SerializeField] private float destroyTime = 6.0f;
        private float timer;
        private void Start()
        {
            timer = destroyTime;
        }
        private void Awake()
        {
            // Pick a material based on our player number so that we can distinguish between projectiles. We use the player number
            // but wrap around if we have more players than materials. This number was passed to us when the projectile was instantiated.
            // See ProjectileLauncher.cs for more details.
            var photonView = this.transform.GetComponent<PhotonView>();
            var playerId = Mathf.Max((int)photonView.InstantiationData[0], 0);
            if (this.projectileMaterials.Length > 0)
            {
                var material = this.projectileMaterials[playerId % this.projectileMaterials.Length];
                this.transform.GetComponent<Renderer>().material = material;
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer < 0 )
            {
                Destroy(gameObject);
            }
        }
        public void Bounce()
        {
            bounceCount++;
            if(bounceCount > maxBounceTime)
            {
                Destroy(gameObject, 1);
            }
        }
    }
}
