using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    private void Awake()
    {
        // Убедимся, что это единственная камера в сцене
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
        {
            Destroy(gameObject); // Удаляем дубликат
            return;
        }

        DontDestroyOnLoad(gameObject); // Делаем камеру неуничтожаемой
    }
}