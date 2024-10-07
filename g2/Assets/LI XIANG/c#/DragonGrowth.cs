using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    public GameObject smallDragon;
    public GameObject bigDragon;
    public Collider smallDragonCollider;
    public Collider bigDragonCollider;
    public Camera mainCamera;
    public float growthScaleFactor = 1.5f;//･・ﾙ･・荳・ﾈﾂﾊ
    private bool isBigDragon = false;
    public PlayerMove playerMove;
    public int growthThreshold = 100;

    void Start()
    {
        smallDragon.SetActive(true);//ｳﾚｻｯ
        bigDragon.SetActive(false);
        bigDragonCollider.enabled = false;
    }

    void Update()
    {
        // ｳﾉ餃ﾖﾆﾏﾞ
        if (playerMove.currentEXP >= growthThreshold && Input.GetKeyDown(KeyCode.Space) && !isBigDragon)
        {
            GrowToBigDragon();
            isBigDragon = true;
            GrowToBigDragon();
        }
    }

    void GrowToBigDragon()
    {
        smallDragon.SetActive(false);//ｳﾉ餃
        bigDragon.SetActive(true);
        smallDragonCollider.enabled = false; // ﾐ｡､ｵ､､･ﾉ･鬣ｴ･ﾛ･・ﾀｩ`
        bigDragonCollider.enabled = true;    // ｴｭ､､･ﾉ･鬣ｴ･ﾛ･・ﾀｩ`
        mainCamera.orthographicSize *= growthScaleFactor; //･ｫ･皈鬣ｳ･ﾈ･愰`･・
        playerMove.canDash = false;

        isBigDragon = true;
    }
}