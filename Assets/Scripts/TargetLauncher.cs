
namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;
    using UnityEngine.UIElements;
    using System.Collections.Generic;
    using static UnityEngine.GraphicsBuffer;
    using UnityEngine.UI;
    using System.Linq;

    enum OneScoreTarget
    {
       scale = 5,
       score = 1
    }

    enum ThreeScoreTarget
    {
        scale = 10,
        score = 3
    }

    enum FiveScoreTarget
    {
        scale = 18,
        score = 5
    }

    [RequireComponent(typeof(Camera))]
    public class TargetLauncher : MonoBehaviourPun
    {
        [SerializeField] private int osTargetNum = 4;
        [SerializeField] private int tsTargetNum = 2;
        [SerializeField] private int fsTargetNum = 1;
        [SerializeField] private GameObject ostargetPrefab;
        [SerializeField] private GameObject tstargetPrefab;
        [SerializeField] private GameObject fstargetPrefab;
        [SerializeField] private GameObject targets;
        [SerializeField] public List<Target> targetList = new List<Target>();
        public Dictionary<string, int> targetScores = new Dictionary<string, int>();
        public static TargetLauncher Instance { get; private set; }

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
            targets.SetActive(false);
        }

        void OnEnable()
        {
            //targets.SetActive(false);
            //Debug.Log("1111" + targetScores.Count);
            targetList = targets.GetComponentsInChildren<Target>().ToList();
            //int i = 1;
            foreach(var target in targetList)
            {
                //targetList.Add(target);
                //Debug.Log(i++);
                //Debug.Log(target.gameObject.name + " : " + (int)target.GetType());
                targetScores.Add(target.gameObject.name, (int)target.GetType());
            }
            //Debug.Log("1111" + targetList.Count + " : " + targetScores.Count);
        }

        public void LoadTargets()
        {
            targets.SetActive(true);
        }

        public void CheckTargetsStatus()
        {
            foreach(var target in targetList)
            {
                if(!target.beShooted)
                {
                    return;
                }
            }
            
            photonView.RPC("CheckOverCountDown", RpcTarget.All);
            
        }
        [PunRPC]
        public void CheckOverCountDown()
        {
            GameManager.Instance.BeginOverCountDown();
        }
    }
}
