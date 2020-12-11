using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity_Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject entityToSpawn;
    Wave_Manager waveManager;
    public int type;
    [SerializeField]
    int nbEntityToSpawn;
    [SerializeField]
    float timeBetweenSpawn;
    float cptTimeBetweenSpawn;
    [SerializeField]
    float spawnRange;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("PFB_Game_Manager").GetComponent<Wave_Manager>();
        cptTimeBetweenSpawn = timeBetweenSpawn;
    }

    void SpawnEntity(Vector3 _spawnPoint)
    {
        waveManager.AddRemoveEntity(true);
        GameObject newEntity = Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
        print("Spawned");
    }
    Vector3 ChooseSpawnPoint(float _radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * _radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, _radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveManager.nbEntity < waveManager.nbMaxEntity[waveManager.nbMaxWaves])
        {
            if (cptTimeBetweenSpawn > 0)
            {
                cptTimeBetweenSpawn -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < nbEntityToSpawn; i++)
                {
                    if (waveManager.nbEntity < waveManager.nbMaxEntity[waveManager.nbMaxWaves])
                    {
                        SpawnEntity(ChooseSpawnPoint(spawnRange));
                        cptTimeBetweenSpawn = timeBetweenSpawn;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * (transform.localScale.y / 2), spawnRange);
    }
}
