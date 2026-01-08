using System.Collections.Generic;
using UnityEngine;

// 탄도 계산 및 탄환 스폰 관리
public class BulletManger : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 1)] public float gravityScale = 0.3f;
    [Range(0, 3)] public float maxLifeTime = 3f;
    public float damage;

    List<Bullet> bullets = new();
    DamageSystem dms;
    DamageSystem DMS
    {
        get
        {
            if(dms == null)
                dms = StaticRegistry.Find<DamageSystem>();

            return dms;
        }
    }

    private void Awake()
    {
        StaticRegistry.Add(this);
        dms = StaticRegistry.Find<DamageSystem>();
    }

    private void Update()
    {
        BallisticCalculation(Time.deltaTime);
    }

    // 탄환 소환 및 초기화
    public void SpawnBullet(Vector3 pos, Vector3 dir, float speed, float damage)
    {
        bullets.Add(new Bullet
        {
            position = pos,
            velocity = dir * speed,
            lifeTime = 0
        });
    }

    // 탄도학 계산
    void BallisticCalculation(float dt)
    {
        //유니티 내부 중력값 사용
        Vector3 gravity = Physics.gravity * gravityScale;

        // 생성된 탄환의 탄도학 계산
        for (int i = 0; i < bullets.Count; ++i)
        {
            var b = bullets[i];
            Vector3 prevPos = b.position;

            b.position += b.velocity * dt;
            b.velocity += gravity * dt;
            b.lifeTime += dt;

            var move = b.position - prevPos;
            float dist = move.magnitude;

            // 탄환이 목표에 맞으면 데미지 계산 후 제거
            if (dist > 0f && Physics.Raycast(prevPos, move.normalized, out var hit, dist))
            {
                HandleHit(hit, prevPos);
                bullets.RemoveAt(i);
                continue;
            }

            // 최대 시간을 넘어가면 제거
            if (b.lifeTime > maxLifeTime)
            {
                bullets.RemoveAt(i);
                continue;
            }

            bullets[i] = b;
        }
    }

    // 데미지 적용 메서드
    void HandleHit(RaycastHit hit, Vector3 prevPos)
    {
        if (!hit.collider.TryGetComponent<IDamageable>(out var dmg)) return;

        float dist = (prevPos - hit.point).magnitude;

        DamageContext ctx = new()
        {
            attacker = gameObject,
            target = hit.collider.gameObject,
            hitPoint = hit.point,
            hitNormal = hit.normal,
            damage = 1f,
            distance = dist,
            damageType = DamageType.Bullet,
            hitZone = DMS.ResolveHitZone(hit.collider)
        };

        DamageResult res = DMS.Pipeline.Calculate(ctx);
        dmg.ApplyDamage(res);
    }
}