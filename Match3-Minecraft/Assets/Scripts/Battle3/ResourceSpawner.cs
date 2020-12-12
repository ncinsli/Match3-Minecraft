using Lean.Pool;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceSpawner : Singleton<ResourceSpawner>{
    [SerializeField] private GameObject[] resourcesAvaiable;
    private void Awake(){ 
        BattleGrid.instance.ForEachCell((x, y, absPosition) => {
            transform.position = absPosition;
            var resource = Instantiate(RandomChoice(resourcesAvaiable));
            resource.transform.position = absPosition;
        });
    }
    public void SpawnObjectUpwards(Transform objectDeselectedTransform){
        Debug.Log("Spawned upwards");
        objectDeselectedTransform.position += Vector3.up * transform.position.y * 7f;
        var rs = Instantiate(RandomChoice(resourcesAvaiable));
        rs.transform.position = objectDeselectedTransform.position; //Временно 
    }
    private GameObject RandomChoice(GameObject[] arr) => arr[Random.Range(0, arr.Length)];
}
