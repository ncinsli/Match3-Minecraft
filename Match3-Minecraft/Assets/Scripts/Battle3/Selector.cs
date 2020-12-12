﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Singleton<Selector>{
    public event Action<SelectableResource[]> OnButtonUp;
    public SelectableResource firstSelected; //То есть, тип объекта, который мы выбрали
    [SerializeField] private List<SelectableResource> selectedResources = new List<SelectableResource>();
    private float mouseDeltaModifier = 0.15f; //Так как дельта мыши высчитывается странно, введём магическое число
    private Vector2 mousePositionSinceClick;
    private bool canSelect;
    private RaycastHit hit; //Теперь ресурсы сами подписываются на селект их
    private void Update() {
        if (Input.GetMouseButtonUp(0)){ 
            OnButtonUp?.Invoke(selectedResources.ToArray());
            DeselectAll();
        }
        if (Input.GetMouseButton(0)) canSelect = true; 
    }

    public void SelectResource(SelectableResource resource){
        if (canSelect){ 
            resource.Select();
            selectedResources.Add(resource); 
            if (firstSelected == null) firstSelected = resource;
        }
    }
    private void OnDestroy(){
        foreach (var resource in selectedResources)
            if (resource != null) resource.OnSelect -= SelectResource;
    }
    public void DeselectAll(){
        canSelect = false;
        foreach (var resource in selectedResources)
            if (resource != null) resource.Deselect();
        selectedResources.Clear();    
        firstSelected = null;
    }    
    public void FetchSelectables() => selectedResources = FindObjectsOfType<SelectableResource>().ToList();
}
