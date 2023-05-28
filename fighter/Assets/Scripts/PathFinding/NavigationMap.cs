using UnityEngine;
using UnityEngine.Serialization;

public class NavigationMap : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int depth;
    public bool[,,] MapWithNotFreeCells { get; private set; }
    public Vector3 Center => new Vector3(width, height, depth) / 2;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, depth));
    }

    private void OnEnable()
    {
        MapWithNotFreeCells = new bool[width, height, depth];
        var center = new Vector3(width, height, height) / 2;
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                for (var k = 0; k < depth; k++)
                {
                    var vector = new Vector3(i, j, k);

                    var position = transform.position + (vector - center);

                    if (Physics.CheckBox(position, new Vector3(1, 1, 1), Quaternion.identity))
                    {
                        MapWithNotFreeCells[i, j, k] = true;
                    }
                }
            }
        }
    }
}
