using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro   UI

public class PlayControl : MonoBehaviour
{
    public GameObject bg1; // BG1の背景
    public GameObject bg2; // BG2の背景
    public GameObject bg3; // BG3の背景

    public TextMeshProUGUI timerText; // 時間を表示するTextMeshProのUIテキスト

    public static int currentLevel = 1; // 他のスクリプトでint currentLevel = PlayControl.currentLevelで使う;

    private GameObject currentBg; // 現在の背景
    private float timer = 0f; // タイマー、経過時間を記録

    void Start()
    {
        // 背景を初期化
        currentBg = bg1;
        ActivateBackground(currentBg);
    }

    void Update()
    {
        // 現在の背景をスクロールさせる
        currentBg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Time.time / 5, 0));

        // タイマーを更新
        timer += Time.deltaTime;

        // 分と秒を計算
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);

        // 画面上の時間表示を更新
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);/////////UIトラブル、修正予定

        // 5分ごとに次のレベルに切り替える　　　　　　　　　　　////////テストのため、3秒に設定///////
        if (timer >= 3f)
        {
            timer = 0f; // タイマーをリセット
            currentLevel++;

            //// レベルが最大値を超えたら最初に戻る////
            if (currentLevel > 3)
            {
                currentLevel = 1;
            }

            // レベルを切り替える
            ChangeLevel(currentLevel);
        }
    }

    // レベルを変更し、背景を切り替える
    void ChangeLevel(int level)
    {
        currentLevel = level;

        // レベルに応じて背景を選択
        switch (currentLevel)
        {
            case 1:
                currentBg = bg1;
                break;
            case 2:
                currentBg = bg2;
                break;
            case 3:
                currentBg = bg3;
                break;
            default:
                currentBg = bg1;
                break;
        }

        // 新しい背景をアクティブ化し、他の背景を非アクティブ化
        ActivateBackground(currentBg);
    }

    // 指定した背景をアクティブ化し、他の背景を非アクティブ化
    void ActivateBackground(GameObject bgToActivate)
    {
        bg1.SetActive(bgToActivate == bg1);
        bg2.SetActive(bgToActivate == bg2);
        bg3.SetActive(bgToActivate == bg3);
    }
}
