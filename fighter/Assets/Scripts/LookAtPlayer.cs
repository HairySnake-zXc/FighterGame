using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    void LateUpdate()
    {
        transform.LookAt(camera);
    }
}
