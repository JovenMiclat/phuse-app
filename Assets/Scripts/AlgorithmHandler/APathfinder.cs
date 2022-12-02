using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts.AlgorithmHandler
{
    public class APathfinder : MonoBehaviour
    {
        public static APathfinder Instance;

        [Header("SETUP")]
        public GameObject object_client;
        public TMPro.TMP_Text DistanceTravel;
        public TMPro.TMP_Text TimeTravel;

        public Transform defaultLocation;
        public Transform counterLocation;

        [Space(10)]
        public float radius = 0;
        public GameObject[] targets;
        public GameObject[] tempTargets;
        [HideInInspector] public Image[] indecator;
        [HideInInspector] public GameObject[] _objects;

        UIHandler itemHandler;

        private void Awake()
        {
            Instance = this;
        }
        [Obsolete]

        int tempTargetCount = 0;

        [Obsolete]
        private void Start()
        {
            itemHandler = GetComponent<UIHandler>();
            List<int> selectedItems = itemHandler.item_arr2;
            tempTargets = targets;
            int i, j;

            for (i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.SetActive(false);
            }

            for (i = 0; i < selectedItems.Count; i++)
            {
                for (j = 0; j < targets.Length; j++)
                {
                    if (selectedItems[i] == j)
                    {
                        targets[j].gameObject.SetActive(true);
                        break;
                    }
                }
            }
            for (i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.active == true)
                {
                    tempTargets[tempTargetCount] = targets[i];
                    tempTargetCount++;
                }
            }
            AddRoute();
        }
        public int cutIndex;
        public int[] visitedTarget = new int[90];
        public int visitedTargetCount = 0;
        public double totalDistance = 0;

        [Obsolete]
        public void AddRoute()
        {
            cutIndex = targets.Length - tempTargetCount;
            int targetIndex = 0;
            Vector3 location = defaultLocation.position;
            GameObject obj = Instantiate(object_client, location, Quaternion.identity);
            int ctr = 0;
            for (ctr = 0; ctr < tempTargets.Length - cutIndex; ctr++)
            {
                obj = Instantiate(object_client, location, Quaternion.identity);
                targetIndex = getShortestDistance(obj);
                InstantiateObject(targetIndex, location);
                location = tempTargets[targetIndex].transform.position;
            }
            obj = Instantiate(object_client, location, Quaternion.identity);
            GameObject counter = Instantiate(object_client, counterLocation.position, Quaternion.identity);
            NavMeshAgent Agent = obj.GetComponent<NavMeshAgent>();
            Agent.SetDestination(counterLocation.position);
            NavMeshPath Path = new NavMeshPath();
            float getDistance = 0;
            if (NavMesh.CalculatePath(obj.transform.position, counter.transform.position, Agent.areaMask, Path))
            {
                getDistance = Vector3.Distance(obj.transform.position, counter.transform.position);
                for (int k = 1; k < Path.corners.Length; k++)
                {
                    getDistance += Vector3.Distance(Path.corners[k - 1], Path.corners[k]);
                }
            }
            totalDistance += getDistance;
			totalDistance = totalDistance/18.88852;
			double roundedDistance = Math.Round(totalDistance,2);
            double TotalTime = (totalDistance / 0.8f) + (visitedTargetCount*5);
            int Timemins = (int)TotalTime / 60;
            int Timesecs = (int)TotalTime % 60;

            Debug.Log(String.Format("Total Distance: {0} meters", totalDistance));
            Debug.Log(String.Format("Total Time: {0} Mins {1} Secs", Timemins, Timesecs));
            DistanceTravel.text = "Total Distance: " + roundedDistance + " meters";
            TimeTravel.text = "Total Time: " + Timemins + " min/s " + Timesecs + " sec/s";

        }

        public int getShortestDistance(GameObject obj)
        {

            int i = 0, j = 0;
            int flag = 0;
            float tempDistance = float.MaxValue;
            float getDistance = 0;
            NavMeshPath Path = new NavMeshPath();
            int indexPos = 0;
            for (i = 0; i < tempTargets.Length - cutIndex; i++)
            {
                NavMeshAgent Agent = obj.GetComponent<NavMeshAgent>();
                flag = visitedTargetCount;
                while (flag > 0)
                {
                    for (j = 0; j < visitedTargetCount; j++)
                    {
                        if (i == visitedTarget[j])
                        {
                            i++;
                        }
                    }
                    flag--;
                }
                if (i == tempTargets.Length - cutIndex)
                {
                    break;
                }
                if (NavMesh.CalculatePath(obj.transform.position, tempTargets[i].transform.position, Agent.areaMask, Path))
                {
                    getDistance = Vector3.Distance(obj.transform.position, tempTargets[i].transform.position);
                    for (int k = 1; k < Path.corners.Length; k++)
                    {
                        getDistance += Vector3.Distance(Path.corners[k - 1], Path.corners[k]);
                    }
                }

                if (getDistance < tempDistance)
                {
                    indexPos = i;
                    tempDistance = getDistance;
                }
                else
                {
                    tempDistance = tempDistance;
                }
            }
            visitedTarget[visitedTargetCount] = indexPos;
            visitedTargetCount++;

            totalDistance += tempDistance;
            return indexPos;
        }

        public void RemoveRoute()
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                Destroy(_objects[i].gameObject);

                for (int j = 0; j < indecator.Length; j++)
                {
                    indecator[j].gameObject.SetActive(false);
                }

            }
        }

        private void Update()
        {
            _objects = GameObject.FindGameObjectsWithTag("Player");
        }

        public void InstantiateObject(int Index, Vector3 location)
        {
            Dictionary<float, GameObject> distDic = new Dictionary<float, GameObject>();

            Vector3 pos = location;
            GameObject obj = Instantiate(object_client, pos, Quaternion.identity);
            ObjectController objectController = obj.GetComponent<ObjectController>();
            NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();

            agent.SetDestination(tempTargets[Index].transform.position);


            StartCoroutine(testCol());

            IEnumerator testCol()
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

}
