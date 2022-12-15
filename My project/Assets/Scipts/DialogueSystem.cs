using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    
    [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
    private float dialogueIntervalTime = 0.1f;
    [SerializeField, Header("開頭對話")]
    private DialogueData dialogueOpening;
    [SerializeField, Header("對話按鍵")]
    private KeyCode dialogueKey = KeyCode.Space;


    private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);
    private CanvasGroup groupDialogue;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textContent;
    private GameObject goTriangle;


    private void Awake()
    {
        groupDialogue = GameObject.Find("畫布對話系統").GetComponent<CanvasGroup>();
        textName = GameObject.Find("名稱").GetComponent<TextMeshProUGUI>();
        textContent = GameObject.Find("對話文").GetComponent<TextMeshProUGUI>();
        goTriangle = GameObject.Find("對話完成圖示");
        goTriangle.SetActive(false);

        StartCoroutine(FadeGroup());
        StartCoroutine(TypeEffect());
    }


    private IEnumerator FadeGroup(bool fadeIn = true)
    {
        float increase = fadeIn ? +0.1f : -0.1f;
        for (int i = 0; i < 10; i++)
        {
            groupDialogue.alpha += increase;
            yield return new WaitForSeconds(0.05f);
        }
    }


    private IEnumerator TypeEffect()
    {
        textName.text = dialogueOpening.dialogueName;

        for (int j = 0; j < dialogueOpening.dialogueContent.Length; j++)
        {
            textContent.text = "";
            goTriangle.SetActive(false);

            string dialogue = dialogueOpening.dialogueContent[j];

            for (int i = 0; i < dialogue.Length; i++)
            {
                textContent.text += dialogue[i];
                yield return dialogueInterval;
            }
        }


        while (Input.GetKeyDown(dialogueKey))
        {
            yield return null;
            // 等待玩家輸入
        }

        StartCoroutine(FadeGroup(false));
    }
}
