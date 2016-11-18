//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class LifeLootManager : MonoBehaviour
//{
//    public int initialSize;
//    public GameObject enemyPrefab;

//    private List<Transform> spawningPlaces;

//    public Transform target;

//    private int pos = 0;

//    public float startingTimeBetweenWaves = 10f;
//    private float timeSinceLastWave;
//    public int startingMinNumInWave = 3;
//    public int startingMaxNumInWave = 10;
//    public float timeCleaner = 20f;
//    public float timeFat = 30f;
//    public float timeGordon = 40f;
//    public float timeBetweenWavesDecreaseRate;



//    void Start()
//    {
//        timeSinceLastWave = startingTimeBetweenWaves;
//        enemyPool = new ObjectPool();
//      //  enemyPool.InitPool(enemyPrefab, initialSize);

//        spawningPlaces = new List<Transform>();
//        foreach (Transform t in transform)
//        {
//            spawningPlaces.Add(t);
//        }
//    }


//    void Update()
//    {

//        //timeSinceLastWave += Time.deltaTime;
//        //if (timeSinceLastWave > startingTimeBetweenWaves)
//        //{
//        //    if (startingTimeBetweenWaves > 0) --startingTimeBetweenWaves;

//        //    if (Time.timeSinceLevelLoad < timeCleaner)
//        //    {
//        //        SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[0].position);
//        //    }
//        //    else if (Time.timeSinceLevelLoad < timeFat)
//        //    {
//        //        SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[0].position);
//        //    }
//        //}


        
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            switch(pos)
//            {
//                case 0:
//                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[0].position);
//                    break;
//                case 1:
//                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[1].position);
//                    break;
//                case 2:
//                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[2].position);
//                    break;
//                case 3:
//                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPlaces[3].position);
//                    break;
//            }
//            pos = pos + 1 % 4;
//        }
//    }


//    public void SpawnWave(ENEMY_TYPE type, int numberOfEnemy, Vector3 position)
//    {
//        for (int i = 0; i < numberOfEnemy; ++i)
//        {
//            //GameObject enemy = enemyPool.GetPooledObject();
//            //enemy.transform.position = position;
//            //enemy.GetComponent<Enemy>().Target = target;
//            //enemy.SetActive(true);

//            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
//            enemy.transform.position = position;
//            enemy.GetComponent<Enemy>().Target = target;
//            enemy.SetActive(true);
//        }
        
//    }
//}
