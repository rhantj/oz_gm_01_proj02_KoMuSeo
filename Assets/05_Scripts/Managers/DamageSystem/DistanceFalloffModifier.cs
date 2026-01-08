using UnityEngine;

public class DistanceFalloffModifier : IDamageModifier
{
    private float minDistance;
    private float falloffRate;

    public DistanceFalloffModifier(float mindist, float rate)
    {
        minDistance = mindist;
        falloffRate = rate;
    }

    public void Modify(ref DamageContext context, ref DamageResult result)
    {
        if (context.distance <= minDistance) return;
        float falloff = 1f - (context.distance - minDistance) * falloffRate;
        falloff = Mathf.Max(falloff, 0f);

        result.finalDamage *= falloff;

    }
}