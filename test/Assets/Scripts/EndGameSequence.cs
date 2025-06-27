using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndGameSequence : MonoBehaviour
{
    [Header("Настройки затемнения")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 3f;
    
    [Header("Финальный экран")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private Button quitButton;
    
    [Header("Текст")]
    [SerializeField] private string prologueEndText = "КОНЕЦ ПРОЛОГА";
    [SerializeField] private string thanksText = "Спасибо за игру";
    
    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
        endScreen.SetActive(false);

        titleText.text = prologueEndText;
        subtitleText.text = thanksText;

        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartEndSequence()
    {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = Color.black;
        endScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
