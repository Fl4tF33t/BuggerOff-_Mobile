using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugTakeDamage
{
    public void BugTakeDamage(int damage);
    public void BugSlow();

    public int GetHealth();

    public bool GetIsAttackable();

    public void SetIsAttackable(bool isAttackable);
}
