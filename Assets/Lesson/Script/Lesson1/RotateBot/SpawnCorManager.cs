using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using StringUtils;

namespace Lesson1
{
    public class SpawnCorManager : MonoBehaviour
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
        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine(Cor_SpawnDummy());
        }

        private IEnumerator Cor_SpawnDummy()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                SpawnDummy();
                yield return null;
            }
        }

        [Button]
        private void SpawnDummy()
        {
            RandomSpawnPos();
            GameObject dummyClone = Instantiate(dummy, spawnPos, Quaternion.identity, dummyContainer);
            dummyName = FunUtils.RandomName();
            dummyClone.name = dummyName;
            containerDummy.Add(dummyClone);
            PlayerCube_Cor pCube = dummyClone.GetComponent<PlayerCube_Cor>();
            if (willMove)
            {

                pCube.SetPoints(pillarPoints);
            }
            else
            {
                pCube.enabled = false;
            }
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
    }
}