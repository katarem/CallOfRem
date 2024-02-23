using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject bigEnemyPrefab;
    [SerializeField]
    private GameObject[] spawnPosition;
    
    private GameObject enemyContainer;

    private int ronda = 1;
    private int limiteRonda = 4;
    private int spawnTime = 5;

    [SerializeField]
    private bool isSpawning = true;
    void Start()
    {
        enemyContainer = GameObject.Find("EnemyContainer");
        if(isSpawning)
            StartCoroutine(enemySpawnRoutine());
    }
    /*
        It spawns an enemy each 5s till 5 mins have passed, after that the difficulty will be harder, as the spawnTime is lower and the speed is higher. Each enemy will be inside the container, so if the game finishes and we want to do something more before jumping to the other scene, we can easily destroy all spawned enemies destroying the container.
    */
    IEnumerator enemySpawnRoutine()
    {
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();
        BigEnemy bigEnemy = bigEnemyPrefab.GetComponent<BigEnemy>();

        float startTime = Time.time;
        while (true)
        {
            Vector3 spawnPoint = spawnPosition[Random.Range(0, 4)].transform.position;
            GameObject e = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            e.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(spawnTime);

            if (Time.time - startTime > 30)
            {
                //vuelve a contar otros 30 seg y spawnea uno tocho
                GameObject bigE = Instantiate(bigEnemyPrefab, spawnPoint, Quaternion.identity);
                bigE.transform.parent = enemyContainer.transform;
                startTime = Time.time;
                enemy.accelerate();
                bigEnemy.accelerate();
                ronda++;
            }
            //aqui se pone chunga la cosa
            if (ronda > limiteRonda)
            {
                limiteRonda += 4; //cada 4 rondas se pone mas complicado
                if (spawnTime > 1)
                    spawnTime--;
            }
        }
    }

    //stops spawning
    public void stopSpawning()
    {
        StopAllCoroutines();
        Destroy(enemyContainer);
    }


}
