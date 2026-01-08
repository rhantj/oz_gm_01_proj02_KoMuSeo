using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public int hp = 100;
    public void ApplyDamage(DamageResult res)
    {
        if(hp <= 0)
        {
            hp = 0;
            return;
        }

        hp -= (int)res.finalDamage;
    }
}
