using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    public GameObject smallDragon;
    public GameObject bigDragon;
    public Collider smallDragonCollider;
    public Collider bigDragonCollider;
    public Camera mainCamera;
    public float growthScaleFactor = 1.5f;//��E٥�E丁E���
    private bool isBigDragon = false;
    public PlayerMove playerMove;
    public int growthThreshold = 100;

    void Start()
    {
        smallDragon.SetActive(true);//���ڻ�
        bigDragon.SetActive(false);
        bigDragonCollider.enabled = false;
    }

    void Update()
    {
        // ���L����
        if (playerMove.currentEXP >= growthThreshold && Input.GetKeyDown(KeyCode.Space) && !isBigDragon)
        {
            GrowToBigDragon();
            isBigDragon = true;
            GrowToBigDragon();
        }
    }

    void GrowToBigDragon()
    {
        smallDragon.SetActive(false);//���L
        bigDragon.SetActive(true);
        smallDragonCollider.enabled = false; // С�����ɥ饴��ۥ�E��`
        bigDragonCollider.enabled = true;    // �󤭤��ɥ饴��ۥ�E��`
        mainCamera.orthographicSize *= growthScaleFactor; //����饳��ȥ��`��E
        playerMove.canDash = false;

        isBigDragon = true;
    }
}