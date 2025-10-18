using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Pattern> patterns;
    private Pattern currentPattern;
    private int currentPrefabIndex;
    private int seed;
    [SerializeField] private Transform targetReference;
    [SerializeField] private NavMeshSurface navmesh;

    private void Awake()
    {
        seed = Random.Range(0, 10000);
        currentPattern = patterns[seed % patterns.Count];
    }

    private void Update()
    {
        if (Vector3.Distance(targetReference.position,transform.position) <= 50f)
        {
            Spawn();    
        }
    }

    private void Spawn()
    {
        GameObject obj = Instantiate(currentPattern.objectPrefabs[currentPrefabIndex]);
        obj.transform.position = transform.position;
        currentPrefabIndex++;
        if(currentPrefabIndex>= currentPattern.objectPrefabs.Count)
        {
            currentPrefabIndex = 0;
        }
        transform.position+= new Vector3(0, 0, 10f);
        if(navmesh.navMeshData!=null)
        {
            navmesh.UpdateNavMesh(navmesh.navMeshData);
        }
        else
        {
            navmesh.BuildNavMesh();
        }
    }




}
