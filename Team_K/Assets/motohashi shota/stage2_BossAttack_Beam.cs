using System.Collections;
using UnityEngine;

public class stage2_BossAttack_Beam : MonoBehaviour
{
    [Header("Settings")]
    public LineRenderer beamLine;
    public float damagePerSecond = 10f;
    public float duration = 2f;
    public float width = 0.2f;
    public LayerMask playerMask;

    public IEnumerator DoAttack()
    {
        Debug.Log("Beam attack!");

        float timer = 0f;
        beamLine.enabled = true;
        beamLine.startWidth = beamLine.endWidth = width;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f, playerMask);
            Vector3 endPos = transform.position + transform.right * 10f;
            if (hit)
            {
                endPos = hit.point;
                // ƒ_ƒ[ƒW
                // hit.collider.GetComponent<PlayerHealth>()?.TakeDamage(damagePerSecond * Time.deltaTime);
            }

            beamLine.SetPosition(0, transform.position);
            beamLine.SetPosition(1, endPos);

            yield return null;
        }

        beamLine.enabled = false;
    }
}
