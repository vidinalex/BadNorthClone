using UnityEngine;

public class GetAvgPos : MonoBehaviour
{
    [SerializeField] private Transform[] solders;

    private void Update()
    {
        transform.position = GetCenter();
    }

    private Vector3 GetCenter()
    {
        Vector3 soldersCenter = Vector3.zero;
        int count = 0;
        foreach (Transform t in solders)
        {
            if (t.gameObject.activeInHierarchy)
            {
                soldersCenter += t.position;
                count++;
            }
        }
        soldersCenter /= count;

        return soldersCenter;
    }
}
