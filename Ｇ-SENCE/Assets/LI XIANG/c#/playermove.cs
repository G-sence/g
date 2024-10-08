using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//wasdで移丒、shiftでダッシュ、Mpを消費し小さいしか使えない
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public int dashCost = 1;
    public int cooltime = 1;
    public float minX, maxX, minY, maxY;  // boxcolliderが時々丒かないためコードで移动范围を制限すE

    public int maxHP = 5;
    public int maxMP = 10;
    public int currentHP;
    public int currentMP;
    public int currentEXP;
    public int level = 1;

    private Rigidbody rb;
    private bool isDashing = false;
    public bool canDash = true;
    public float dashCooldown = 1f;
    private Vector3 dashDirection;

    public GameObject hitpoint1;
    public GameObject hitpoint2;
    public GameObject hitpoint3;
    public GameObject hitpoint4;
    public GameObject hitpoint5;

    public GameObject Expslider;    //懱椡僎乕僕僆僽僕僃僋僩傪奿擺偡傞曄悢
    public GameObject Mpslider;    //懱椡僎乕僕僆僽僕僃僋僩傪奿擺偡傞曄悢


    Slider MpGauge;                             //丂UI偺Slider宆曄悢丂hpGauge傪梡堄偟傑偡
    Slider ExpGauge;                             //丂UI偺Slider宆曄悢丂hpGauge傪梡堄偟傑偡

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = 5;
        currentMP = maxMP;
        currentEXP = 0;

        MpGauge = Mpslider.GetComponent<Slider>();    //Slider傪庢傝崬傒傑偡
        MpGauge.minValue = currentMP;       
        
        ExpGauge = Expslider.GetComponent<Slider>();    //Slider傪庢傝崬傒傑偡
        ExpGauge.minValue = currentEXP;                             //懱椡僎乕僕偺嵟戝抣傪Slider偺嵟戝抣偵偟傑偡

    }

    void Update()
    {
        currentEXP += 1;//一旦1にすE
        if (!isDashing)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentMP >= dashCost && canDash)//ダッシュ条件
        {
            StartDash();
        }

        if (currentHP == 5)
        {
            hitpoint1.SetActive(true);
            hitpoint2.SetActive(true);
            hitpoint3.SetActive(true);
            hitpoint4.SetActive(true);
            hitpoint5.SetActive(true);
        }
        if (currentHP == 4)
        {
            hitpoint1.SetActive(true);
            hitpoint2.SetActive(true);
            hitpoint3.SetActive(true);
            hitpoint4.SetActive(true);
            hitpoint5.SetActive(false);
        }
        if (currentHP == 3)
        {
            hitpoint1.SetActive(true);
            hitpoint2.SetActive(true);
            hitpoint3.SetActive(true);
            hitpoint4.SetActive(false);
            hitpoint5.SetActive(false);
        }
        if (currentHP == 2)
        {
            hitpoint1.SetActive(true);
            hitpoint2.SetActive(true);
            hitpoint3.SetActive(false);
            hitpoint4.SetActive(false);
            hitpoint5.SetActive(false);
        }
        if (currentHP == 1)
        {
            hitpoint1.SetActive(true);
            hitpoint2.SetActive(false);
            hitpoint3.SetActive(false);
            hitpoint4.SetActive(false);
            hitpoint5.SetActive(false);
        }
        if (currentHP == 0)
        {
            hitpoint1.SetActive(false);
            hitpoint2.SetActive(false);
            hitpoint3.SetActive(false);
            hitpoint4.SetActive(false);
            hitpoint5.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="wall")
        {
            currentHP = currentHP - 1;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            currentHP = currentHP - 1;
            Destroy(collision.gameObject);
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
            Invoke("EndDash", dashDuration);//ダッシュ時間、遅延処利Hv数
            Invoke("ResetDashCooldown", dashCooldown);//クーE骏ぅ丒
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
            currentHP = 0;
        }
    }

    public void UseMana(int mana)
    {
        currentMP -= mana;
        if (currentMP < 0)
        {
            currentMP = 0;
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
        maxHP =5;
        maxMP += 10;
        currentHP = maxHP;
        currentMP = maxMP;
    }
}

