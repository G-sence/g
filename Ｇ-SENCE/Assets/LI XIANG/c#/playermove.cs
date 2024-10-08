// UTF-8に修正
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WASDで移動、Shiftでダッシュ、MPを消費し小さい頃しか使えない
public class PlayerMove : MonoBehaviour
{
    public DragonGrowth dragonGrowth;
    public int WallDamage = 1;  // 壁に衝突した際のダメージ量
    public float moveSpeed = 10f;  // 通常の移動速度
    public float dashSpeed = 20f;  // ダッシュ時の速度
    public float dashDuration = 0.2f;  // ダッシュの継続時間
    public int dashCost = 1;  // ダッシュの際に消費するMP
    public int cooltime = 1;  // クールタイム
    public float minX, maxX, minY, maxY;   // BoxColliderが時々効かないためコードで移動範囲を制限する
    public float knockbackForce = 5f;  // 壁に衝突した際のノックバック力

// HEAD
    public int maxHP = 3;  // 最大HP
//
// parent of 0e42022b (敵の攻撃の調整)
    public int maxMP = 10;  // 最大MP
    public int currentHP;  // 現在のHP
    public int currentMP;  // 現在のMP
    public int currentEXP;  // 現在の経験値
    public int level = 1;  // 現在のレベル
    public int maxEXP = 1000; // 初期の最大経験値
    public int attackPower = 10;  // 攻撃力

    private Rigidbody rb;  // Rigidbodyの参照
    private bool isDashing = false;  // 現在ダッシュしているかどうかのフラグ
    public bool canDash = true;  // ダッシュが可能かどうか
    public float dashCooldown = 1f;  // ダッシュのクールダウン時間
    private Vector3 dashDirection;  // ダッシュの方向
    private bool hasCollidedWithWall = false;  // 壁に衝突済みかどうか
    public bool isBigDragon = false;
    public bool isInvincible = false;  // 霸体状態のフラグ
    public bool isExpLocked = false;  // 経験値がロックされているかどうかのフラグ
    /// <summary>
    /// 
    /// </summary>
    public GameObject laserPrefab;  // レーザー攻撃のプレハブ
    public Transform laserSpawnPoint;  // レーザーの発射位置
    public GameObject flamePrefab;  // 火炎攻撃のプレハブ
    public Transform flameSpawnPoint;  // 火炎の発射位置
    public Animator animator;  // プレイヤーのアニメーター

    public GameObject hitpoint1;  // ヒットポイントのUIオブジェクト
    public GameObject hitpoint2;  // ヒットポイントのUIオブジェクト
    public GameObject hitpoint3;  // ヒットポイントのUIオブジェクト
    public GameObject hitpoint4;  // ヒットポイントのUIオブジェクト
    public GameObject hitpoint5;  // ヒットポイントのUIオブジェクト

    private Collider playerCollider;  // プレイヤーのコライダー

    public GameObject Expslider;    // 経験値スライダー
    public GameObject Mpslider;    // MPスライダー

    Slider MpGauge; // UIのスライダー
    Slider ExpGauge; // UIのスライダー

    private Coroutine expFlashCoroutine; // 経験値スライダーのフラッシュ用コルーチン

    [SerializeField, Header("ダメージ")]
    private GameObject getsound;
    [SerializeField, Header("Lv.up")]
    private GameObject getsound2;
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyコンポーネントを取得
        playerCollider = GetComponent<Collider>();  // Colliderコンポーネントを取得
        currentHP = maxHP;  // HPを最大に設定
        currentMP = maxMP;  // MPを最大に設定
        currentEXP = 0;  // 初期経験値を0に設定

        MpGauge = Mpslider.GetComponent<Slider>();    // Sliderコンポーネントを取得
        MpGauge.maxValue = 1;
        MpGauge.value = (float)currentMP / maxMP;

        ExpGauge = Expslider.GetComponent<Slider>();    // Sliderコンポーネントを取得
        ExpGauge.maxValue = 1;
        ExpGauge.value = (float)currentEXP / maxEXP;

        audioSource.Play();
    }

    void Update()
    {
        MpGauge.value = (float)currentMP / maxMP;  // MPスライダーの値を更新
        ExpGauge.value = (float)currentEXP / maxEXP;  // 経験値スライダーの値を更新

        if (!isDashing)  // ダッシュ中でない場合のみ移動可能
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentMP >= dashCost && canDash)  // ダッシュ条件
        {
            StartDash();
            UseMana(dashCost);
        }

        if (Input.GetKeyDown(KeyCode.J) && currentMP > 0)  // Jキーでレーザー攻撃（キーを押した瞬間だけ）
        {
            StartCoroutine(FireLaser());
        }

        if (Input.GetKeyDown(KeyCode.K) && currentMP > 0)  // Kキーで火炎攻撃（キーを押した瞬間だけ）
        {
            StartCoroutine(FireFlame());
        }

        UpdateHitPoints();  // ヒットポイントUIの更新
    }

    void UpdateHitPoints()
    {
        hitpoint1.SetActive(currentHP >= 1);  // ヒットポイントが1以上なら表示
        hitpoint2.SetActive(currentHP >= 2);  // ヒットポイントが2以上なら表示
        hitpoint3.SetActive(currentHP >= 3);  // ヒットポイントが3以上なら表示
        hitpoint4.SetActive(currentHP >= 4);  // ヒットポイントが4以上なら表示
        hitpoint5.SetActive(currentHP >= 5);  // ヒットポイントが5以上なら表示

        if (currentHP == 0)
        {
            Time.timeScale = 0f; // 死亡処理
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") && !hasCollidedWithWall)  // 壁に初めて衝突したときのみダメージを受ける
        {
            if (!isBigDragon && !isInvincible) // 小さなドラゴンで且つ霸体状態でない場合のみダメージを受ける
            {
                TakeDamage(WallDamage);
                Knockback(collision.contacts[0].normal);
                Debug.Log("壁に衝突しました。現在のHP: " + currentHP);
                hasCollidedWithWall = true;
                Invoke("ResetWallCollision", 1f);
                Instantiate(getsound);
            }
            else if (isBigDragon)
            {
                Destroy(collision.gameObject); // 大きなドラゴンの場合、壁を破壊する
            }
        }
        if (collision.gameObject.tag == "ammo")
        {
            Destroy(collision.gameObject);
        }
    }

    void Knockback(Vector3 collisionNormal)
    {
        Vector3 knockbackDirection = -collisionNormal * knockbackForce;
        rb.AddForce(knockbackDirection, ForceMode.Impulse);
    }

    void ResetWallCollision()
    {
        hasCollidedWithWall = false;
    }

    void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        Vector3 movement = new Vector3(moveX, moveY, 0f) * moveSpeed;
        rb.velocity = movement;

        // 移動範囲制限
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
        transform.position = clampedPosition;
    }

    void StartDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            playerCollider.enabled = false;
            dashDirection = rb.velocity.normalized;
            rb.velocity = dashDirection * dashSpeed;
            currentMP -= dashCost;
            Invoke("EndDash", dashDuration);
            Invoke("ResetDashCooldown", dashCooldown);
        }
    }

    void EndDash()
    {
        isDashing = false;
        playerCollider.enabled = true; // ダッシュ終了後にColliderを有効にする
    }

    void ResetDashCooldown()
    {
        canDash = true;
    }

    /// 戦闘関連
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                currentHP = 0;
                Time.timeScale = 0f;
            }
        }
    }

    public void UseMana(int mana)
    {
        currentMP -= mana;
        if (currentMP < 0)
        {
            currentMP = 0;
        }
        MpGauge.value = (float)currentMP / maxMP;  // MPスライダーの値を更新
    }

    public void GainEXP(int exp)
    {
        if (!isExpLocked)  // 経験値がロックされている場合、経験値を増加させない
        {
            currentEXP += exp;
            if (currentEXP >= maxEXP)
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        level++;
        maxHP += 5;
        maxMP += 10;
        attackPower += 5;  // 攻撃力を増加
        currentHP = maxHP;
        currentMP = maxMP;
        currentEXP = 0;
        maxEXP += 500;

        Instantiate(getsound2);

        MpGauge.value = (float)currentMP / maxMP;  // MPスライダーの値を更新
        ExpGauge.value = (float)currentEXP / maxEXP;  // 経験値スライダーの値を更新

        StartCoroutine(TemporaryInvincibility());  // レベルアップ時に短時間の霸体を付与
        Invoke("EnableColliderAfterLevelUp", 3f); // レベルアップ後にColliderを有効にする

        if (isBigDragon)
        {
            isExpLocked = false;  // 大龍になったら経験値ロックを解除
        }
    }

    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(3f);  // 3秒間の霸体状態
        isInvincible = false;
    }

    IEnumerator FireLaser()
    {
        // レーザー攻撃アニメーションを再生
        animator.SetTrigger("FireLaser");
        // 2秒待機
        yield return new WaitForSeconds(1.3f);
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Destroy(laser, 3f);

        currentMP -= 1;
        MpGauge.value = (float)currentMP / maxMP;  // MPスライダーの値を更新
    }

    IEnumerator FireFlame()
    {
        // 火炎攻撃アニメーションを再生
        animator.SetTrigger("FireFlame");
        // 2秒待機
        yield return new WaitForSeconds(1.3f);
        GameObject flame = Instantiate(flamePrefab, flameSpawnPoint.position, flameSpawnPoint.rotation);
        Destroy(flame, 3f);

        currentMP -= 2;  // 火炎攻撃の際に消費するMP
        MpGauge.value = (float)currentMP / maxMP;  // MPスライダーの値を更新
    }
}