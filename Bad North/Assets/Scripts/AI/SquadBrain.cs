using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SquadBrain : MonoBehaviour
{
    [SerializeField] private string targetLayer;
    [SerializeField] private List<Unit> units;

    private List<Unit> targetUnits = new List<Unit>();

    [HideInInspector] public UnityEvent newTargetsFound;
    [HideInInspector] public UnityEvent partyDestroyed;

    private void Start()
    {
        foreach (var unit in units)
        {
            unit.deathEvent.AddListener(CheckPartyExist);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            List<Unit> temp = other.GetComponent<SquadBrain>().GetUnitList();
            foreach (Unit unit in temp)
            {
                unit.deathEvent.AddListener(ClearTargets);
            }
            targetUnits.AddRange(temp);

            newTargetsFound.Invoke();
        }
    }

    public List<Unit> GetUnitList()
    {
        return units;
    }
    public Unit AskForTarget()
    {
        if (targetUnits.Count != 0)
            return targetUnits[Random.Range(0, targetUnits.Count)];
        return null;
    }
    public void SetNavMeshAgent(bool state)
    {
        foreach (var unit in units)
        {
            unit.GetComponent<NavMeshAgent>().enabled = state;
        }
    }
    private void ClearTargets(Unit unit)
    {
        targetUnits.Remove(unit);
    }
    private void CheckPartyExist(Unit unit)
    {
        units.Remove(unit);
        if (units.Count == 0)
        {
            Debug.Log("Destroyed");
            partyDestroyed.Invoke();
            gameObject.SetActive(false);
        }
    }
}
