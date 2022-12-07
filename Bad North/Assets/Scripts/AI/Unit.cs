using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class DeathEvent : UnityEvent<Unit> { }
public class Unit : MonoBehaviour
{
    [HideInInspector] public DeathEvent deathEvent;

    [SerializeField] private SquadBrain brain;
    [SerializeField] private AnimBrain brainAnim;
    [SerializeField] private Transform targetNav;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Image hpBar;
    [SerializeField] private StatsData stats;

    private bool isCooldown = false;
    private float hp;
    private float dmg;
    private float cooldown;

    private Unit unitTarget;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        brain.newTargetsFound.AddListener(SetNewTarget);
        hp = stats.health;
        dmg = stats.dmg;
        cooldown = stats.cooldown;
    }
    private void SetNewTarget()
    {
        if (!unitTarget)
        {
            unitTarget = brain.AskForTarget();
            if (unitTarget)
                unitTarget.deathEvent.AddListener(ClearCurrentTarget);
        }
    }
    private void ClearCurrentTarget(Unit unit)
    {
        unitTarget = null;
        SetNewTarget();
    }
    private void Update()
    {
        DetectAnimationState();
    }
    private void DetectAnimationState()
    {
        if (!agent.isActiveAndEnabled)
            return;
        if (!unitTarget)
        {
            agent.stoppingDistance = 0.1f;
            agent.SetDestination(targetNav.position);

            if (agent.remainingDistance < 0.2f)
            {
                brainAnim.SetState(0);
            }
            else
            {
                brainAnim.SetState(1);
            }
        }
        else
        {
            agent.stoppingDistance = 1.5f;
            agent.SetDestination(unitTarget.transform.position);

            if (agent.remainingDistance < 1.6f)
            {
                brainAnim.SetState(2);
                transform.LookAt(unitTarget.transform);
                Attack();
            }
            else
            {
                brainAnim.SetState(1);
            }
        }
    }

    private void Attack()
    {
        if (isCooldown)
            return;

        unitTarget.TakeDamage(dmg);

        StartCoroutine(CooldownAttack());
    }

    IEnumerator CooldownAttack()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Death();
        UpdateUI();
    }
    private void UpdateUI()
    {
        hpBar.fillAmount = 1 - (stats.health - hp) / stats.health;
    }
    [ContextMenu("Manual Death")]
    private void Death()
    {
        deathEvent.Invoke(this);
        gameObject.SetActive(false);
    }
}
