using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Singleton<Selector>{
    [SerializeField] private List<SelectableResource> resources; 
    public event Action<SelectableResource[]> OnButtonUp;
    public InventoryElement objectSelectInfo; //То есть, тип объекта, который мы выбрали
    private List<SelectableResource> selectedResources = new List<SelectableResource>();
    private float mouseDeltaModifier = 0.15f; //Так как дельта мыши высчитывается странно, введём магическое число
    private Vector2 mousePositionSinceClick;
    private bool canSelect;
    private RaycastHit hit;
    private void Awake(){ 
        foreach (var resource in resources)
            resource.OnSelect += SelectResource;

        OnButtonUp += resourcesArray => {
            canSelect = false;
            foreach (var resource in resourcesArray)
                if (resource != null) resource.Deselect();
        };
    }
    private void Update() {
        if (Input.GetMouseButtonUp(0)) OnButtonUp?.Invoke(selectedResources.ToArray());
        if (Input.GetMouseButton(0)) canSelect = true; 
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
