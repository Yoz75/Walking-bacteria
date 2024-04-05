
using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FoodPrefab;

    [SerializeField] private float Timeout;
    [SerializeField] private int MaximalFoodCount;
    [SerializeField] private float RecountTimeout;

    [SerializeField] Rect SpawnRect;

    private int Spawned;

    private void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(RecountSpawnedFood());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {

            if(Spawned >= MaximalFoodCount)
            {
                yield return null;
            }
            Spawned++;

            var food = Instantiate(FoodPrefab);

            Vector3 foodPosition = new Vector3(Random.RandomRange(SpawnRect.xMin, SpawnRect.xMax), Random.RandomRange(SpawnRect.yMin, SpawnRect.yMax));

            food.transform.position = foodPosition;

            yield return new WaitForSeconds(Timeout);
        }
    }

    private IEnumerator RecountSpawnedFood()
    {
        while(true)
        {
            Spawned = GameObject.FindGameObjectsWithTag(FoodPrefab.tag).Length;
            yield return new WaitForSeconds(RecountTimeout);
        }
    }
}
