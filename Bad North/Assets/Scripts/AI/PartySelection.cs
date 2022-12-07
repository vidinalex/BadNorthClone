using UnityEngine;

public class PartySelection : MonoBehaviour
{
    [SerializeField] private Transform moveTarget;
    [SerializeField] private GameObject selectionCircle;

    private GameObject storedWalkablePlane;
    public void SetDestination(Transform target)
    {
        if (!target)
            return;
        moveTarget.position = target.position;
        if (storedWalkablePlane)
            storedWalkablePlane.SetActive(true);
        storedWalkablePlane = target.gameObject;
        storedWalkablePlane.SetActive(false);
    }
    public void SetSelectSprite(bool state)
    {
        selectionCircle.SetActive(state);
    }
}
