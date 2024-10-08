//カメラやステージ変更より背景の大きさを変更する方が手間が省けるため、そうする。
//前はステージを操作するコードを書いたから余ったコードが出てくる、気にしないでください。
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// stageコードを最適化化済み
public enum Stage
{
    Normal,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Stage6,
}

public class CreateStage : MonoBehaviour
{
    public Vector3 spawnOffset = new Vector3(0f, 6f, 0f);  // 新しいステージの偏移量
    private GameObject currentStage;
    private GameObject previousStage;

    public GameObject stagePrefab;
    public List<GameObject> stages;
    public Transform spawnPoint; //ステージ定位

    private Stage currentStageState = Stage.Normal;
    public bool isBigDragon = false; // 龍が成長したかどうかのフラグ

    void Start()
    {
        // 初期化
        previousStage = Instantiate(stagePrefab, new Vector3(0f, 5.5f, 0), Quaternion.identity);
        currentStage = previousStage;

        InvokeRepeating("GenerateNewStage", 0f, 10f); // 生成時間（始まる時間と間隔）
    }

    void Update()
    {
        // ステージ移動
        if (currentStage != null)
        {
            currentStage.transform.Translate(-0.008f, 0, 0);
        }
        if (previousStage != null)
        {
            previousStage.transform.Translate(-0.1f, 0, 0);
        }
    }

    // ステージ生成メソッド
    void GenerateNewStage()
    {
        List<GameObject> availableStages = new List<GameObject>();

        // ステージ選択のロジック
        if (isBigDragon)
        {
            // 大きなドラゴンの場合、ステージを選択
            availableStages = stages.GetRange(0, stages.Count ); 
        }
        else
        {
            // 小さなドラゴンの場合、ステージを選択
            availableStages = stages.GetRange(0, stages.Count); 
        }

        // ランダムにステージを選択
        int randomStageIndex = Random.Range(0, availableStages.Count);
        GameObject newStage = availableStages[randomStageIndex];

        Vector3 newPosition = spawnPoint.position + spawnOffset;

        previousStage = currentStage;
        currentStage = Instantiate(newStage, newPosition, Quaternion.identity);

        StartCoroutine(DestroyPreviousStageAfterDelay()); // 旧ステージを遅延して破壊
    }

    IEnumerator DestroyPreviousStageAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        if (previousStage != null)
        {
            Destroy(previousStage);
        }
    }

    public void SetBigDragonStage()
    {
        isBigDragon = true; // 龍が成長した後に呼び出して、ステージを更新する
    }
}
