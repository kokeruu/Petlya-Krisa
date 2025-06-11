using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookController : MonoBehaviour
{
    public Image notebookImage;

    public List<Sprite> framesWithoutBookmark;
    public List<Sprite> framesWithBookmark;

    public Button nextButton;
    public Button prevButton;
    public Button moveBookmarkButton;

    private int currentPage = 0;
    private int bookmarkPage = 0;

    private bool isAnimating = false;

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
        if (nextPage < 0 || nextPage > 5) return;

        StartCoroutine(PlayFlipAnimation(direction));
    }

    IEnumerator PlayFlipAnimation(int direction)
    {
        isAnimating = true;
        currentPage += direction;

        bool hasBookmark = currentPage == bookmarkPage;
        var frames = hasBookmark ? framesWithBookmark : framesWithoutBookmark;

        // Если направление назад — инвертируем порядок кадров
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

        // Показываем итоговый кадр — всегда первый
        notebookImage.sprite = frames[0];
        isAnimating = false;
    }


    void MoveBookmark()
    {
        bookmarkPage = currentPage;
        ShowCurrentPage();
    }

    void ShowCurrentPage()
    {
        notebookImage.sprite = (currentPage == bookmarkPage) ? framesWithBookmark[0] : framesWithoutBookmark[0];
    }
}


