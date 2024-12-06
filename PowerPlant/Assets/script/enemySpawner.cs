using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 12f;
    [SerializeField] private float minSpawnRate = 8f;
    [SerializeField] private int maxSpiders = 1; // Max number of spiders allowed in the scene
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;

    private int currentSpiders = 0;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);

            // Check if the current number of spiders is below the maximum
            if (currentSpiders < maxSpiders)
            {
                //Debug.Log("Current" + currentSpiders);
                //Debug.Log("max" + maxSpiders);
                //Debug.Log("plants "+Plant.plantsNum);

                int rand = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyToSpawn = enemyPrefabs[rand];
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

                // Increment the counter and add a destroy event to decrement it
                currentSpiders++;
                spawnedEnemy.GetComponent<SpiderScript>().OnDestroyed += () => currentSpiders--;

                // Adjust spawn rate
                spawnRate = Mathf.Max(spawnRate - 1f, minSpawnRate);
                Debug.Log(currentSpiders);
                Debug.Log("Max" + maxSpiders);
            }
        }
    }
    private void Update(){
        if (Plant.plantsNum ==2){
            maxSpiders=4;
        }
        if (Plant.plantsNum >3){
            maxSpiders=5;
        }
    }
}
