using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour {

    public APawn[] pawns;
    List<APawn> menacingEnemies;

    public APawn[] GetAllEnemies()
    {
        return pawns;
    }

    public List<APawn> GetMenacingEnemies()
    {
        menacingEnemies.Clear();
        return menacingEnemies;
    }

    public APawn GetClosestEnemies(List<APawn> menacingEnemies)
    {
        return menacingEnemies[0];
    }
}
