using UnityEngine;

[System.Serializable]
public class stage2_AttackData
{
    public string name;
    public float weight = 1f;
    public float cooldown = 3f;
    public float minDelay = 0.5f;   // 攻撃前のチャージ演出時間
    public AttackType type = AttackType.Slash;
}

public enum AttackType
{
    Slash,
    Beam
}
