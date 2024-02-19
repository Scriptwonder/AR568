using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    One,
    Three,
    Five,
}

namespace MyFirstARGame
{
    public class Target : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private TargetType type;
        [SerializeField] private string id;
        [SerializeField] NetworkCommunication networkCommunication;
        void Start()
        {
            networkCommunication = FindObjectOfType<NetworkCommunication>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.GetComponent<ProjectileBehaviour>() != null)
            {
                Debug.Log("Hit : " +  gameObject.name + " : " + type);
                if(networkCommunication != null)
                {
                    networkCommunication.UpdateTargetScore(this);
                }
                other.GetComponent<ProjectileBehaviour>().Bounce();
            }
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public string GetID()
        {
            return id;
        }
    }
}
