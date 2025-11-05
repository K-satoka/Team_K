using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2_BossController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public stage2_BossAttack_Slash slashAttack;
    public stage2_BossAttack_Beam beamAttack;

    [Header("Attack Settings")]
    public List<stage2_AttackData> attacks = new List<stage2_AttackData>();

    private Dictionary<stage2_AttackData, float> lastUsed = new Dictionary<stage2_AttackData, float>();
    public float globalInterval = 1.0f;

    void Start()
    {
        foreach (var a in attacks)
            lastUsed[a] = -999f;

        StartCoroutine(AILoop());
    }

    IEnumerator AILoop()
    {
        while (true)
        {
            var atk = PickAttack();
            if (atk != null)
            {
                Debug.Log("Selected attack: " + atk.type);
                yield return new WaitForSeconds(atk.minDelay); // チャージ演出など

                switch (atk.type)
                {
                    case AttackType.Slash:
                        slashAttack.DoAttack();
                        break;
                    case AttackType.Beam:
                        StartCoroutine(beamAttack.DoAttack());
                        break;
                }

                lastUsed[atk] = Time.time;
            }
            yield return new WaitForSeconds(globalInterval);
        }
    }

    stage2_AttackData PickAttack()
    {
        List<stage2_AttackData> candidates = new List<stage2_AttackData>();
        float sum = 0;

        foreach (var a in attacks)
        {
            if (Time.time - lastUsed[a] >= a.cooldown)
            {
                candidates.Add(a);
                sum += a.weight;
            }
        }

        if (candidates.Count == 0) return null;

        float r = Random.Range(0, sum);
        float acc = 0;
        foreach (var a in candidates)
        {
            acc += a.weight;
            if (r <= acc) return a;
        }
        return candidates[candidates.Count - 1];
    }
}
