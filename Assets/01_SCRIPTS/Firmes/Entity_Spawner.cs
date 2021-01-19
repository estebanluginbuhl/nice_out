using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity_Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject entityToSpawn;
    [HideInInspector]
    public Wave_Manager waveManager;
    [SerializeField]
    int nbEntityToSpawn;
    [SerializeField]
    float timeBetweenSpawn;
    float cptTimeBetweenSpawn;
    [SerializeField]
    float spawnRange;

    Animator anm;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponentInChildren<Animator>();
        cptTimeBetweenSpawn = timeBetweenSpawn;
    }
    void SpawnEntity(Vector3 _spawnPoint)
    {
        anm.SetTrigger("Spawn");
        waveManager.AddRemoveEntity(true);
        GameObject newEntity = Instantiate(entityToSpawn, _spawnPoint, Quaternion.identity);
        newEntity.GetComponent<Entity_Stats>().InitializeEntity(GetComponent<Firme_Stats>().firmeType, 0, waveManager);
    }
    Vector3 ChooseSpawnPoint(float _radius)//choisi un point dans la range de la firme sur le navmesh pour faire spawn l'entité
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * _radius;
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
        if(waveManager.play == true)
        {
            if (waveManager.nbEntity < waveManager.nbMaxEntity[waveManager.waveIndex])
            {
                if (cptTimeBetweenSpawn > 0)
                {
                    cptTimeBetweenSpawn -= Time.deltaTime;
                }
                else
                {
                    SpawnEntity(ChooseSpawnPoint(spawnRange));
                    cptTimeBetweenSpawn = timeBetweenSpawn / nbEntityToSpawn;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * (transform.localScale.y / 2), spawnRange);
    }
}
