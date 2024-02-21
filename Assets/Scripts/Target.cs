using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    One = 1,
    Three = 3,
    Five = 5,

    Jackpot = 20
}

namespace MyFirstARGame
{
    public class Target : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private TargetType type;
        [SerializeField] private string id;
        [SerializeField] public bool beShooted;
        [SerializeField] NetworkCommunication networkCommunication;
        [SerializeField] private Material material;
        void Start()
        {
            networkCommunication = FindObjectOfType<NetworkCommunication>();
            //int i = this.gameObject.getComponent<PhotonView>().ViewID;
            SetID(this.gameObject.name);
            beShooted = false;
            material = GetComponent<MeshRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerStay(Collider other)
        {
            if (beShooted && type == TargetType.Jackpot)
            {
                return;
            }
            if(other.GetComponent<ProjectileBehaviour>() != null)
            {
                Debug.Log("Hit : " +  gameObject.name + " : " + type);
                if(networkCommunication != null)
                {
                    networkCommunication.UpdateTargetScore(this.GetID());
                }
                other.GetComponent<ProjectileBehaviour>().Bounce();
                material.color = other.GetComponent<MeshRenderer>().material.color;
                beShooted = true;
            }
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public string GetID()
        {
            return this.id;
        }

        new public TargetType GetType()
        {
            return type;
        }
    }
}
