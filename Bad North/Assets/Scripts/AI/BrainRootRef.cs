using UnityEngine;

public class BrainRootRef : MonoBehaviour
{
    [SerializeField] private SquadBrain squadBrain;

    public SquadBrain GetConnectedSB()
    {
        return squadBrain;
    }
}
