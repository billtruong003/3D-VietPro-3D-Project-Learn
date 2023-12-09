using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using StringUtils;

namespace Lesson1
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Refference")]
        [SerializeField] private GameObject dummy;
        [SerializeField] private Transform dummyContainer;
        [SerializeField] private Transform[] pillarPoints;

        [Space(2)]
        [Header("Assign Value")]
        [SerializeField] private bool willMove;
        [SerializeField] private string dummyName;
        [SerializeField] private Vector3 spawnPos;
        [SerializeField] private List<GameObject> containerDummy;

        // Private
        private float xPos;
        private float zPos;
        private float timing = 2f;

        [Button]
        private void SpawnDummy()
        {
            RandomSpawnPos();
            GameObject dummyClone = Instantiate(dummy, spawnPos, Quaternion.identity, dummyContainer);
            dummyClone.name = FunUtils.RandomName();
            containerDummy.Add(dummyClone);


            PlayerCube pCube = dummyClone.GetComponent<PlayerCube>();
            pCube.SetPoints(pillarPoints);
        }

        private void RandomSpawnPos()
        {
            xPos = Random.Range(-16, 16);
            zPos = Random.Range(-16, 16);
            spawnPos = new Vector3(xPos, 0, zPos);
        }

        [Button]
        private void ClearDummy()
        {
            foreach (var item in containerDummy)
            {
                DestroyImmediate(item);
            }
            containerDummy.Clear();
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > timing)
            {
                SpawnDummy();
                timing = Time.time + 2;
            }
        }
    }
}