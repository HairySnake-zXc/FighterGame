using UnityEngine;

public class NavigationMap : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _depth;
    [SerializeField] private GameObject _checker;
    public bool[,,] MapWithNotFreeCells { get; set; }
    public Vector3 Center => new Vector3(_width, _height, _depth) / 2;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height, _depth));
    }

    private void Awake()
    {
        MapWithNotFreeCells = new bool[_width, _height, _depth];
        var center = new Vector3(_width, _height, _height) / 2;
        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                for (var k = 0; k < _depth; k++)
                {
                    var vector = new Vector3(i, j, k);

                    var position = transform.position + (vector - center);

                    if (Physics.CheckBox(position, new Vector3(1, 1, 1), Quaternion.identity))
                    {
                        MapWithNotFreeCells[i, j, k] = true;
                        //Instantiate(_checker, position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
