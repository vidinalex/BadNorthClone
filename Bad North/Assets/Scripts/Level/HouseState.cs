using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseState : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SquadBrain>().partyDestroyed.AddListener(HouseDead);
    }
    private void HouseDead()
    {
        LevelManager.Default.EndGame(false);
    }
}
