using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectableResource))]
public class DeleteObjectOnSelect : MonoBehaviour{
    private SelectableResource resourceAttached;
    private void Awake(){ 
        resourceAttached = GetComponent<SelectableResource>();
        Selector.instance.OnButtonUp += resources => {
            foreach (var resource in resources){ 
                if (resource != null) Destroy(resource.gameObject);
            }
        }; 
    }
    private void OnDisable() => 
        Selector.instance.OnButtonUp -= resources => {
            foreach (var resource in resources) Destroy(resource.gameObject);
        }; 
}
