
namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;
    using Photon.Pun.Demo.PunBasics;
    using UnityEngine.UIElements;
    using System.Collections.Generic;

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
    public class TargetLauncher : MonoBehaviour
    {
        [SerializeField] private int osTargetNum = 4;
        [SerializeField] private int tsTargetNum = 2;
        [SerializeField] private int fsTargetNum = 1;
        [SerializeField] private GameObject ostargetPrefab;
        [SerializeField] private GameObject tstargetPrefab;
        [SerializeField] private GameObject fstargetPrefab;
        [SerializeField] private GameObject targets;
        [SerializeField] private List<Target> targetList = new List<Target>();
        [SerializeField] private Dictionary<string, int> targetScores = new Dictionary<string, int>();
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
            foreach(var target in targets.GetComponentsInChildren<Target>())
            {
                targetList.Add(target);
                targetScores.Add(target.GetID(), (int)target.GetType());
            }
        }
        void Start()
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    Vector3 origin = GetComponent<Camera>().transform.position;
            //    Vector3 forward = GetComponent<Camera>().transform.forward.normalized;

            //    GameObject parent = new GameObject("Targets");
            //    for (int i = 0; i < osTargetNum; i++)
            //    {
            //        GameObject obj = PhotonNetwork.Instantiate(ostargetPrefab.name, origin + forward * 5 + Vector3.up * 2 + Vector3.left * (i-osTargetNum/2) * 1, 
            //            Quaternion.identity);
            //        obj.transform.localScale = new Vector3((float)OneScoreTarget.scale / 5.0f, (float)OneScoreTarget.scale / 5.0f, (float)OneScoreTarget.scale) / 5.0f;
            //        obj.name = ostargetPrefab.name + i;
            //        obj.transform.parent = parent.transform;
            //        obj.GetComponent<Target>().SetID(obj.name);
            //    }

            //    for (int i = 0; i < tsTargetNum; i++)
            //    {
            //        GameObject obj = PhotonNetwork.Instantiate(tstargetPrefab.name, origin + forward * 5 + Vector3.down * 2 + Vector3.left * (i - tsTargetNum / 2) * 1, 
            //            Quaternion.identity);
            //        obj.transform.localScale = new Vector3((float)ThreeScoreTarget.scale / 5.0f, (float)ThreeScoreTarget.scale / 10.0f, (float)ThreeScoreTarget.scale) / 5.0f;
            //        obj.name = tstargetPrefab.name + i;
            //        obj.transform.parent = parent.transform;
            //        obj.GetComponent<Target>().SetID(obj.name);
            //    }

            //    for (int i = 0; i < fsTargetNum; i++)
            //    {
            //        GameObject obj = PhotonNetwork.Instantiate(fstargetPrefab.name, origin + forward * 5 + Vector3.left * (i - fsTargetNum / 2) * 1, 
            //            Quaternion.identity);
            //        obj.transform.localScale = new Vector3((float)FiveScoreTarget.scale / 5.0f, (float)FiveScoreTarget.scale / 5.0f, (float)FiveScoreTarget.scale / 5.0f);
            //        obj.name = fstargetPrefab.name + i;
            //        obj.transform.parent = parent.transform;
            //        obj.GetComponent<Target>().SetID(obj.name);
            //    }
            //}
            //else
            //{
            //    Debug.Log("I'm not Master");
            //}
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
            GameManager.Instance.BeginOverCountDown();
        }
    }
}
