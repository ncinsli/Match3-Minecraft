using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Selector))]
public class DeleteObjectOnSelect : MonoBehaviour{
    private Selector selector;
    private void Awake(){ 
        selector = GetComponent<Selector>();
        selector.OnButtonUp += DestroySelected;
    }
    private void DestroySelected(SelectableResource[] resources){
        foreach (var resource in resources){ 
            if (resource == null) continue;
            LeanPool.Despawn(resource.gameObject);
        }
    }
}
