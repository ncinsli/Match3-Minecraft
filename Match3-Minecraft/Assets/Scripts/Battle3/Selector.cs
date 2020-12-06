using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour{
    [SerializeField] private List<SelectableResource> resources; 
    protected float mouseDeltaModifier = 0.15f; //Так как дельта мыши высчитывается странно, введём магическое число
    protected Vector2 mousePositionSinceClick;
    protected RaycastHit hit;
    public Transform t;
    private void Awake() => resources = new List<SelectableResource>();

    private void Update(){  //ScreenToWorldPoint - дорогая операция, потому кэшируем её, а потом меняем через Mouse Axis
        if (Input.GetKeyDown(KeyCode.Mouse0)) BeforeSelect();
        if (Input.GetKey(KeyCode.Mouse0)) TrySelect();
        if (Input.GetKeyUp(KeyCode.Mouse0)) AfterSelect();
        t.position = mousePositionSinceClick;
        t.position = new Vector3(t.position.x, t.position.y, 3);
    }

    private void TrySelect(){
        mousePositionSinceClick += Time.deltaTime * 40f * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 mouse = mousePositionSinceClick;
        Vector3 direction = new Vector3(mouse.x, mouse.y, 0f); //Чтобы не было наклона луча
            Debug.DrawRay(mouse, direction); 
        if (Physics.Raycast(mouse, direction, out hit)){
            var target = hit.transform.gameObject;
            var resource = target.GetComponent<SelectableResource>();
            if (resources.Count <= 0) Select(resource);
            else if (CheckSelect(resource)) Select(resource); 
        }
    }

    private void Select(SelectableResource res){
        if (!resources.Contains(res)) resources.Add(res);
        res?.Select();
    }

    private void BeforeSelect(){
        resources = new List<SelectableResource>(); //Зануляем список
        foreach (var i in resources) 
            if (i != null) i.Deselect();
        mousePositionSinceClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    private void AfterSelect(){
        foreach (var i in resources) Destroy(i.gameObject);
    }

    //Не принимает нулл 
    private bool CheckSelect(SelectableResource currentResource){
        var lastResource = resources[resources.Count - 1];
        var positionDelta = (lastResource.gridPosition - currentResource.gridPosition);
        if (Mathf.Abs(positionDelta.x) < 3f && Mathf.Abs(positionDelta.y) < 3f)
            return lastResource.info.id == currentResource.info.id;
        return false;
    }
}
