using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro   UI

public class PlayControl : MonoBehaviour
{
    public GameObject bg1; // BG1�α���
    public GameObject bg2; // BG2�α���
    public GameObject bg3; // BG3�α���

    public TextMeshProUGUI timerText; // �r�g���ʾ����TextMeshPro��UI�ƥ�����

    public static int currentLevel = 1; // ���Υ�����ץȤ�int currentLevel = PlayControl.currentLevel��ʹ��;

    private GameObject currentBg; // �F�ڤα���
    private float timer = 0f; // �����ީ`���U�^�r�g��ӛ�h

    void Start()
    {
        // ��������ڻ�
        currentBg = bg1;
        ActivateBackground(currentBg);
    }

    void Update()
    {
        // �F�ڤα����򥹥���`�뤵����
        currentBg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Time.time / 5, 0));

        // �����ީ`�����
        timer += Time.deltaTime;

        // �֤����Ӌ��
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);

        // �����ϤΕr�g��ʾ�����
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);/////////UI�ȥ�֥롢�����趨

        // 5�֤��Ȥ˴ΤΥ�٥���Ф��椨�롡��������������������////////�ƥ��ȤΤ��ᡢ3����O��///////
        if (timer >= 3f)
        {
            timer = 0f; // �����ީ`��ꥻ�å�
            currentLevel++;

            //// ��٥뤬��󂎤򳬤���������ˑ���////
            if (currentLevel > 3)
            {
                currentLevel = 1;
            }

            // ��٥���Ф��椨��
            ChangeLevel(currentLevel);
        }
    }

    // ��٥���������������Ф��椨��
    void ChangeLevel(int level)
    {
        currentLevel = level;

        // ��٥�ˏꤸ�Ʊ������x�k
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

        // �¤��������򥢥��ƥ��ֻ��������α�����ǥ����ƥ��ֻ�
        ActivateBackground(currentBg);
    }

    // ָ�����������򥢥��ƥ��ֻ��������α�����ǥ����ƥ��ֻ�
    void ActivateBackground(GameObject bgToActivate)
    {
        bg1.SetActive(bgToActivate == bg1);
        bg2.SetActive(bgToActivate == bg2);
        bg3.SetActive(bgToActivate == bg3);
    }
}
