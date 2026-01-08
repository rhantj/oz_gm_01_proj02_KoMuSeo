using UnityEngine;


// modifier와 데미지 계산 파이프라인 제공
public class DamageSystem : MonoBehaviour
{
    public DamagePipeline Pipeline { get; private set; }

    private void Awake()
    {
        Pipeline = new DamagePipeline();
        StaticRegistry.Add(this);

        Pipeline.Add(new DistanceFalloffModifier(30f, 0.01f));
        Pipeline.Add(new HeadShotModifier());
        //Pipeline.Add(new ArmorModifier());
    }

    // 맞은 위치
    public HitZone ResolveHitZone(Collider col)
    {
        if (col.CompareTag("Head")) return HitZone.Head;
        if (col.CompareTag("Limb")) return HitZone.Limb;
        return HitZone.Body;
    }
}