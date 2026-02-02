using UnityEngine;
using System.Collections;

public class stage2_Boss_Attack : MonoBehaviour
{
    [Header("攻撃プレハブ")]
    //攻撃のプレハブ（遠距離系のみ）
    public GameObject fistPrefab;
    public GameObject tornadoPrefab;

    [Header("攻撃間隔")]
    //落ちてくる間隔（０だととんでもない量降ってくる）
    public float attackInterval = 2.5f;

    [Header("こぶしパラメータ")]
    //落下速度
    public float fistFallSpeed = 6.0f;

    //ターゲット設定（基本的にプレイヤー）
    [Header("ターゲット設定")]
    public Transform player; // プレイヤーをInspectorで指定

    [Header("竜巻用変数")]
    public float tornadoDelay = 0.7f;
    public float tornadoLifeTime = 3f;

    [Header("竜巻サイズ")]
    public Vector3 tornadoScale = new Vector3(3f, 3f, 3f);

    [Header("地面のY座標")]
    public float groundY = -3.7f;// ここは地面の高さに合わせて調整
    public float groundX = 0;

    private float attackTimer;
    private int lastAttack = -1;
    private Camera cam;
    private Animator animator;

    //SE
    public AudioSource audioSource;
    public AudioClip sundArm;
    public AudioClip sundArm2;
    public AudioClip mosionSE;
    public AudioClip TornadoSE;


    void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        //初撃までのインターバル（初期は2.5秒から０秒までからのランダム）
        attackTimer = Random.Range(0f, attackInterval);
    }

    void Update()
    {
        //プレイヤーのところに何もないと値を返す。
        if (player == null) return;

        //前回のフレームからの経過時間をattacktimerに足す
        attackTimer += Time.deltaTime;

        //attacktimerがインターバルを上回ったらattacktimerを０にして、攻撃用関数を呼び出す。
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            DoRandomAttack();
        }
    }

    void DoRandomAttack()
    {
        int attackType = Random.Range(0, 2);

        if (attackType == 0)
            SpawnFistAtPlayer();
        else
            StartTornadoAttack();
    }

    // ===== こぶし攻撃 =====
    void SpawnFistAtPlayer()
    {
        //プレイヤートランスフォームで取得した、プレイヤーのX座標の真上の画面外にこぶしを生成
        Vector3 spawnPos = new Vector3(player.position.x, cam.orthographicSize + 1f, 0);
        //その位置にこぶしのプレハブを生成し、まっすぐ落とす。
        GameObject fist = Instantiate(fistPrefab, spawnPos, Quaternion.identity);

        if (audioSource != null&&sundArm!=null)
        {
            audioSource.PlayOneShot(sundArm);
            audioSource.PlayOneShot(sundArm2);
        }

        // ここで拳の大きさを変更してみる
        fist.transform.localScale = new Vector3(3f, 3f, 3f); // 好きな大きさに変更

        //フォールダウンの数値に合わせて落下
        StartCoroutine(FallDown(fist.transform));
    }

    IEnumerator FallDown(Transform fist)
    {
        //こぶしがあるかつこぶしのY座標が下の画面外に出たらコンルーチン（時間経過と一緒に処理する）を一時停止させる
        while (fist != null && fist.position.y > -cam.orthographicSize - 2f)
        {
            fist.position += Vector3.down * fistFallSpeed * Time.deltaTime;//毎フレームこぶしを下に落とす
            yield return null;//１フレーム待つ
        }
        //こぶしがあるならこぶしを消す。
        if (fist != null) Destroy(fist.gameObject);
    }

    void StartTornadoAttack()
    {
        animator.SetTrigger("TornadoWarn");
    }

    // Animation Eventから呼ばれる
    public void OnTornadoWarnEnd()
    {
        if (audioSource != null && mosionSE != null)
        {
            audioSource.PlayOneShot(mosionSE);
        }
        StartCoroutine(SpawnTornadoDelayed(tornadoDelay));
    }

    void SpawnTornado()
    {
        groundX = Random.Range(-100, 40);

        Vector2 spawnPos = new Vector2(groundX, groundY + 0.2f);
    
        GameObject t = Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);

        t.transform.localScale = tornadoScale;

        if (audioSource != null && TornadoSE != null)
        {
            audioSource.PlayOneShot(TornadoSE);
        }
        Tornado tornado = t.GetComponent<Tornado>();
        if (tornado != null)
            tornado.Init(player);
    }

    IEnumerator SpawnTornadoDelayed(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        SpawnTornado();
    }

    
}