using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    public GameObject bg1, bg2, bg3;
    public float backgroundScaleFactor = 0.5f; // 縮小目標の比率
    public float backgroundShrinkDuration = 2f; // 背景を縮小する時間
    public GameObject smallDragon;
    public GameObject bigDragon;
    public Collider smallDragonCollider;
    public Collider bigDragonCollider;
    public Camera mainCamera;
    public float growthScaleFactor = 1.5f; // レベル変更の比率
    public bool isBigDragon = false;
    public PlayerMove playerMove;
    public CreateStage createStage;
    public int growthThreshold = 1000; // 成長のための経験値の閾値
    public float cameraGrowthDuration = 2f; // カメラ拡大にかかる時間
    private Coroutine expFlashCoroutine; // 経験値スライダーのフラッシュ用コルーチン
    public float expGrowthRate = 0.1f; // 小龍の時の経験値の成長率
    public float newMinX, newMaxX, newMinY, newMaxY; // 成長後の新しい活動範囲

    void Start()
    {
        smallDragon.SetActive(true); // 初期化として小龍を有効にする
        bigDragon.SetActive(false);  // 大龍は無効にする
        bigDragonCollider.enabled = false; // 大龍のコライダーを無効にする
    }

    void Update()
    {
        if (!isBigDragon)
        {
            playerMove.GainEXP((int)(expGrowthRate * Time.deltaTime * 1000)); // 小龍の時、自動的に経験値を成長させる
        }

        if (playerMove.currentEXP >= growthThreshold && Input.GetKeyDown(KeyCode.Space) && !isBigDragon)
        {
            StartCoroutine(GrowToBigDragonSequence()); // 成長のシーケンスを開始する
            isBigDragon = true;
        }

        if (!isBigDragon && playerMove.currentEXP >= growthThreshold)
        {
            if (expFlashCoroutine == null)
            {
                expFlashCoroutine = StartCoroutine(FlashExpGauge()); // 経験値ゲージを点滅させる
            }
        }
        else if (isBigDragon && expFlashCoroutine != null)
        {
            StopExpFlash(); // 大龍になったら経験値ゲージの点滅を停止する
        }
    }

    System.Collections.IEnumerator GrowToBigDragonSequence()
    {
        // 小龍を非表示にする
        smallDragon.SetActive(false);

        // 背景を縮小する
        yield return StartCoroutine(ShrinkBackground(bg1));
        yield return StartCoroutine(ShrinkBackground(bg2));
        yield return StartCoroutine(ShrinkBackground(bg3));

        // 大龍に切り替える
        bigDragon.SetActive(true);
        smallDragonCollider.enabled = false;
        bigDragonCollider.enabled = true;

        // プレイヤーの状態を更新する
        StartCoroutine(GrowCamera()); // カメラを拡大する
        playerMove.canDash = false;
        playerMove.maxEXP += 500; // レベルアップ時に最大経験値を増加させる
        playerMove.currentEXP = 0; // 経験値をリセットする
        playerMove.maxHP += 5;  // 大幅にHPを増加
        playerMove.maxMP += 5;  // 大幅にMPを増加
        playerMove.attackPower += 20;  // 攻撃力を大幅に増加
        playerMove.currentHP = playerMove.maxHP;
        playerMove.currentMP = playerMove.maxMP;
        playerMove.moveSpeed = playerMove.moveSpeed; // 移動速度を設定する
        // プレイヤーの活動範囲を更新する
        playerMove.minX = newMinX;
        playerMove.maxX = newMaxX;
        playerMove.minY = newMinY;
        playerMove.maxY = newMaxY;

        createStage.isBigDragon = true;
        playerMove.isBigDragon = true;
        isBigDragon = true;
    }

    System.Collections.IEnumerator GrowCamera()
    {
        float elapsedTime = 0f;
        float initialSize = mainCamera.orthographicSize;
        float targetSize = initialSize * growthScaleFactor;

        while (elapsedTime < cameraGrowthDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime / cameraGrowthDuration); // カメラサイズを補間する
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetSize; // 最終的なカメラサイズを設定する
    }

    System.Collections.IEnumerator FlashExpGauge()
    {
        while (!isBigDragon)
        {
            playerMove.Expslider.SetActive(!playerMove.Expslider.activeSelf); // 経験値ゲージを点滅させる
            yield return new WaitForSeconds(0.5f);
        }
        playerMove.Expslider.SetActive(true); // 経験値ゲージを有効にする
    }

    System.Collections.IEnumerator ShrinkBackground(GameObject background)
    {
        Vector3 initialScale = background.transform.localScale;
        Vector3 targetScale = initialScale * backgroundScaleFactor;

        float elapsedTime = 0f;

        while (elapsedTime < backgroundShrinkDuration)
        {
            background.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / backgroundShrinkDuration); // 背景のスケールを補間する
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        background.transform.localScale = targetScale; // 最終的なスケールを設定する
    }

    void StopExpFlash()
    {
        if (expFlashCoroutine != null)
        {
            StopCoroutine(expFlashCoroutine);  // 経験値ゲージの点滅を停止
            expFlashCoroutine = null;
        }
        playerMove.Expslider.SetActive(true); // 経験値ゲージを有効にする
    }
}