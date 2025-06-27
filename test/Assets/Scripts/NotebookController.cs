using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookController : MonoBehaviour
{
    public Image notebookImage;

    public List<Sprite> framesWithoutBookmark;
    public List<Sprite> framesWithBookmark;
    [SerializeField] LocationManager locationManager;

    public Button nextButton;
    public Button prevButton;
    public Button moveBookmarkButton;
    public TMPro.TextMeshProUGUI notebookText;

    private int currentPage = 0;      // может быть 0 или 1
    private int bookmarkPage = 0;

    private bool isAnimating = false;

    // ✅ ТЕКСТЫ ДЛЯ СТРАНИЦ
    private string[] pageTexts = new string[]
    {
        "07.09.2007",
        "06.06.2004\nВ палату подселили нового — Кирилл.\nМолчит, не лезет в душу. Странно, но с ним рядом тихо — не внешне, внутри.\nУ него взгляд такой… будто понимает больше, чем говорит.\nПошутили про «местный театр», он чуть улыбнулся. И этого почему-то хватило.\nНе хочу сглазить, но кажется, с ним проще дышать.\n\n      не проебать бы\n\n"
    };

    void Start()
    {
        nextButton.onClick.AddListener(() => TryTurnPage(1));
        prevButton.onClick.AddListener(() => TryTurnPage(-1));
        moveBookmarkButton.onClick.AddListener(MoveBookmark);

        ShowCurrentPage();
    }

    void TryTurnPage(int direction)
    {
        if (isAnimating) return;

        int nextPage = currentPage + direction;

        // Ограничиваем страницы от 0 до 1
        if (nextPage < 0 || nextPage > 1) return;

        StartCoroutine(PlayFlipAnimation(currentPage, nextPage, direction));
    }

    IEnumerator PlayFlipAnimation(int fromPage, int toPage, int direction)
    {
        isAnimating = true;

        bool hasBookmark = toPage == bookmarkPage;
        var frames = hasBookmark ? framesWithBookmark : framesWithoutBookmark;

        if (direction < 0)
        {
            for (int i = frames.Count - 1; i >= 0; i--)
            {
                notebookImage.sprite = frames[i];
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < frames.Count; i++)
            {
                notebookImage.sprite = frames[i];
                yield return new WaitForSeconds(0.05f);
            }
        }

        currentPage = toPage;
        ShowCurrentPage();

        isAnimating = false;
    }

    void MoveBookmark()
    {
        bookmarkPage = currentPage;
        ShowCurrentPage();
        locationManager.SwitchMap(bookmarkPage+1);
    }

    void ShowCurrentPage()
    {
        notebookImage.sprite = (currentPage == bookmarkPage) ? framesWithBookmark[0] : framesWithoutBookmark[0];

        // ✅ Обновляем текст
        if (notebookText != null && currentPage < pageTexts.Length)
        {
            notebookText.text = pageTexts[currentPage];
        }
    }
}