using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    [Header("References"), Tooltip("탄환과 이펙트가 나오는 위치")]
    public Transform muzzle;
    public Transform muzzleVFX;

    [Header("Hit-scan"), Tooltip("히트 스캔 방식에 필요한 변수")]
    public float hitscanRange = 30f;
    public int hitDamage = 30;

    [Header("Ballistic"), Tooltip("탄도학 계산에 필요한 변수")]
    public float bulletSpeed = 800f;
    public float fireRate = 0.1f;
    float lastFireTime;

    [Header("Spread"), Tooltip("탄환 퍼짐 정도")]
    public float spreadAngle = 0.6f;

    private DamageSystem dms;
    private DamageSystem DMS
    {
        get
        {
            if(dms == null)
                dms = StaticRegistry.Find<DamageSystem>();
            return dms;
        }
    }

    public void Fire() 
    {
        if (Time.time < lastFireTime + fireRate) return;
        lastFireTime = Time.time;

        //Vector3 dir = GetSpreadDirection();
        Vector3 dir = GetSpreadDirection();

        Debug.DrawRay(muzzle.position, dir * hitscanRange, Color.red);
        if (Physics.Raycast(muzzle.position, dir, out var hit, hitscanRange))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var dmg))
            {
                DamageContext context = new()
                {
                    attacker = gameObject,
                    target = hit.collider.gameObject,
                    hitPoint = hit.point,
                    hitNormal = hit.normal,
                    damage = hitDamage,
                    distance = hit.distance,
                    damageType = DamageType.Bullet,
                    hitZone = DMS.ResolveHitZone(hit.collider)
                };
                
                DamageResult res = DMS.Pipeline.Calculate(context);
                dmg.ApplyDamage(res);
            }

            SpawnTracer(hit.point);
            return;
        }

        var bm = StaticRegistry.Find<BulletManger>();
        bm.SpawnBullet(muzzle.position, dir, bulletSpeed, hitDamage);

    }

    Vector3 GetSpreadDirection()
    {
        Vector3 dir = muzzle.forward;
        dir = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0f
        ) * dir;
        return dir.normalized;
    }


    void SpawnTracer(Vector3 endPoint)
    {
        // LineRenderer
    }
}
