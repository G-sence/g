using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    public GameObject smallDragon;
    public GameObject bigDragon;
    public Collider smallDragonCollider;
    public Collider bigDragonCollider;
    public Camera mainCamera;
    public float growthScaleFactor = 1.5f;//レベル変更比率
    private bool isBigDragon = false;
    public PlayerMove playerMove;
    public int growthThreshold = 100;

    void Start()
    {
        smallDragon.SetActive(true);//初期化
        bigDragon.SetActive(false);
        bigDragonCollider.enabled = false;
    }

    void Update()
    {
        // 成長制限
        if (playerMove.currentEXP >= growthThreshold && Input.GetKeyDown(KeyCode.Space) && !isBigDragon)
        {
            GrowToBigDragon();
            isBigDragon = true;
            GrowToBigDragon();
        }
    }

    void GrowToBigDragon()
    {
        smallDragon.SetActive(false);//成長
        bigDragon.SetActive(true);
        smallDragonCollider.enabled = false; // 小さいドラゴンホルダー
        bigDragonCollider.enabled = true;    // 大きいドラゴンホルダー
        mainCamera.orthographicSize *= growthScaleFactor; //カメラコントロール
        playerMove.canDash = false;

        isBigDragon = true;
    }
}