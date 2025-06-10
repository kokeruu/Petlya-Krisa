using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerOnClick : MonoBehaviour
{
    // Название сцены, которую нужно загрузить
    public string sceneName;

    // Этот метод срабатывает при клике мыши по объекту с Collider'ом
    private void OnMouseDown()
    {
        // Загружаем сцену с заданным именем
        SceneManager.LoadScene(sceneName);
        Debug.Log("fsh!");
    }
}