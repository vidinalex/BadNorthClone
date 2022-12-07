using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField] Transform moveTarget;
    private void Start()
    {
        moveTarget.position = LevelManager.Default.AskForHousePos();
    }
}
