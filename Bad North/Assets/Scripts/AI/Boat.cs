using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string landLayer;

    [SerializeField] private SquadBrain squadBrain;

    private bool isOnLand = false;
    private void Update()
    {
        if (isOnLand)
            return;

        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(landLayer))
        {
            isOnLand = true;
            squadBrain.gameObject.SetActive(true);
            squadBrain.SetNavMeshAgent(true);
        }
    }
}
