using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// stageコードを鹠化済み
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

    void Start()
    {
        //初期化
        previousStage = Instantiate(stagePrefab, new Vector3(0f, 5.5f, 0), Quaternion.identity);
        currentStage = previousStage;

        InvokeRepeating("GenerateNewStage", 0f, 3f); // 生成時間（始まE間隔）
    }

    void Update()
    {//ステージ移丒
        if (currentStage != null)
        {
            currentStage.transform.Translate(-0.01f, 0, 0);
        }
        if (previousStage != null)
        {
            previousStage.transform.Translate(-0.01f, 0, 0);
        }
    }
    //ステージ生成メソッド
    void GenerateNewStage()
    {
        int randomStageIndex = Random.Range(1, stages.Count);
        currentStageState = (Stage)randomStageIndex;

        Vector3 newPosition = spawnPoint.position + spawnOffset;

        previousStage = currentStage;
        currentStage = Instantiate(stages[(int)currentStageState - 1], newPosition, Quaternion.identity);

        StartCoroutine(DestroyPreviousStageAfterDelay());//旧ステージを遅延して破壊
    }

    IEnumerator DestroyPreviousStageAfterDelay()
    {
        yield return new WaitForSeconds(20f);
        if (previousStage != null)
        {
            Destroy(previousStage);
        }
    }
}