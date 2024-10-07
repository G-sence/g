using UnityEngine;
//wasdで移動、shiftでダッシュ、Mpを消費し小さい頃しか使えない
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public int dashCost = 1;
    public int cooltime = 1;
    public float minX, maxX, minY, maxY;  // boxcolliderが時々効かないためコードで移动范围を制限する

    public int maxHP = 100;
    public int maxMP = 50;
    public int currentHP;
    public int currentMP;
    public int currentEXP;
    public int level = 1;

    private Rigidbody rb;
    private bool isDashing = false;
    public bool canDash = true;
    public float dashCooldown = 1f;
    private Vector3 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;
        currentMP = maxMP;
        currentEXP = 0;
    }

    void Update()
    {
        currentEXP += 1;//一旦1にする
        if (!isDashing)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentMP >= dashCost && canDash)//ダッシュ条件
        {
            StartDash();
        }
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

        // 移动范围制限                                           
        //if (transform.position.x < minX) transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        //if (transform.position.x > maxX) transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        //if (transform.position.y < minY) transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        //if (transform.position.y > maxY) transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        //transform.position = clampedPosition;
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
            dashDirection = rb.velocity.normalized;
            rb.velocity = dashDirection * dashSpeed;
            currentMP -= dashCost;
            Invoke("EndDash", dashDuration);//ダッシュ時間、遅延処理関数
            Invoke("ResetDashCooldown", dashCooldown);//クールタイム
        }
    }

    void EndDash()
    {
        isDashing = false;
    }

    void ResetDashCooldown()
    {
        canDash = true;
    }
    ///戦闘相関
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;///////////////////////////////////////////////
        }
    }

    public void UseMana(int mana)
    {
        currentMP -= mana;
        if (currentMP < 0)
        {
            currentMP = 0;///////////////////////////////////
        }
    }

    public void GainEXP(int exp)
    {
        currentEXP += exp;
        if (currentEXP >= 100)
        {
            LevelUp();
            currentEXP = 0;
        }
    }

    void LevelUp()
    {
        level++;
        maxHP += 20;
        maxMP += 10;
        currentHP = maxHP;
        currentMP = maxMP;
    }
}

