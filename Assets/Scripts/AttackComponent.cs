using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    public void Attack(APawn target)
    {
        print("Attack on " + target.name);
    }
}
