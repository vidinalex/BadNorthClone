using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private void Start()
    {
        targetCamera = LevelManager.Default.mainCamera;
    }
    private void Update()
    {
        transform.LookAt(targetCamera.transform);
    }
}
