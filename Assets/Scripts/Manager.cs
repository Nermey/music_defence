using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager>
{
    [SerializeField] // не дает доступ с других скриптов
    GameObject spawnPoint;
    [SerializeField]
    GameObject[] Enemies;
    [SerializeField]
    int maxEnemiesOnScreen;
    [SerializeField]
    int totalEnemies;
    [SerializeField]
    int enemiesPerSpawn;

    public List<Enemy> EnemyList = new List<Enemy>();


    const float spawnDelay = 1.5f; // задержка в появлении противников

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(Enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }


    public void DesstroyEnemies()
    {
        foreach (Enemy enemy in EnemyList) 
        {
            Destroy(enemy.gameObject);
        }
        
        EnemyList.Clear();
    }
}
