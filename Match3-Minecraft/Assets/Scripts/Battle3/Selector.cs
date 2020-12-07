using System;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour{
    [SerializeField] private List<SelectableResource> resources; 
    private List<SelectableResource> selectedResources = new List<SelectableResource>();
    private float mouseDeltaModifier = 0.15f; //Так как дельта мыши высчитывается странно, введём магическое число
    private Vector2 mousePositionSinceClick;
    private bool canSelect;
    private RaycastHit hit;
    private void Awake(){ 
        foreach (var resource in resources)
            resource.OnSelect += SelectResource;
    }
    private void Update() {
        if (Input.GetMouseButtonUp(0)){ 
            canSelect = false;
            DeselectAll();
        }
        if (Input.GetMouseButtonDown(0)){
            canSelect = true; 
        }
    }

    private void DeselectAll(){
        foreach (var resource in selectedResources)
            resource.Deselect();
    }

    private void SelectResource(SelectableResource resource){
        if (canSelect){ 
            resource.Select();
            selectedResources.Add(resource);
        }
    }

    private void OnDestroy(){
        foreach (var resource in resources)
            resource.OnSelect -= SelectResource;
    }
}
