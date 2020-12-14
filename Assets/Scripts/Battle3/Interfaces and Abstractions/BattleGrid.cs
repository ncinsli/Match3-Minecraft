using UnityEngine;

public class BattleGrid : Singleton<BattleGrid>
{
    private  Vector3[,] _cells;
    [SerializeField] private float _cellSize = 1f;
    [SerializeField] private Vector2Int _cellsCount = new Vector2Int(7, 7);
    [SerializeField] private Bounds _bounds;
    [SerializeField] private SelectableResource _resourcePrefab;
    [SerializeField] private InventoryElement[] _levelElements;
    

    private void Awake()
    {
        var currentPosition = _bounds.min + new Vector3(1, 1) * 0.5f * _cellSize;

        _cells = new Vector3[_cellsCount.x, _cellsCount.y];

        for (int x = 0; x < _cellsCount.x; x++)
        {
            for (int y = 0; y < _cellsCount.y; y++)
            {
                var resource = Instantiate(_resourcePrefab, transform);
                InitResource(resource, currentPosition);
                
                _cells[x, y] = currentPosition;
                currentPosition.y += _cellSize;
            }

            currentPosition.x += _cellSize;
            currentPosition.y = _bounds.min.y + _cellSize * 0.5f;
        }
        SelectableResource.OnDeselected += SpawnObjectUpwards;
    }

    private void OnDestroy()
    {
        SelectableResource.OnDeselected -= SpawnObjectUpwards;
    }

    private void InitResource(SelectableResource resource, Vector2 absPosition)
    {
        var element = RandomChoice(_levelElements);
        resource.Init(element);
        resource.transform.position = absPosition;
    }

    private void SpawnObjectUpwards(SelectableResource deselectedResource)
    {
        var position = deselectedResource.transform.position + Vector3.up * _cellSize * _cellsCount.y;
        InitResource(deselectedResource, position);
    }

    private T RandomChoice<T>(T[] arr)
        where T : class =>
        arr[Random.Range(0, arr.Length)];


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        if (_cells == null) return;
        for (int x = 0; x < _cellsCount.x; x++)
        {
            for (int y = 0; y < _cellsCount.y; y++)
            {
                Vector2 current = _cells[x, y];
                Gizmos.DrawWireCube(current, Vector2.one * _cellSize);
            }
        }
    }
}