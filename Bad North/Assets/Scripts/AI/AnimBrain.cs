using UnityEngine;

public class AnimBrain : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void SetState(int state)
    {
        animator.SetInteger("State", state);
    }
}
