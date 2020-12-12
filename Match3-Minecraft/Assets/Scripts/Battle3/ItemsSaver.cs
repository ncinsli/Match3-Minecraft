using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Selector))]
public class ItemsSaver : MonoBehaviour{
    private List<SelectableResource> resourcesTotal = new List<SelectableResource>();
    private Selector selector;
    private void Start(){
        selector = GetComponent<Selector>();
        selector.OnButtonUp += PushResources; 
    }
    private void PushResources(SelectableResource[] resources){
        foreach (var resource in resources)
            resourcesTotal.Add(resource);
    }

    private void SerializeResources(){
        
    }
}
