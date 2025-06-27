using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    // Массив объектов для включения/выключения
    [SerializeField] private GameObject[] targetObjects;

    // Включить/выключить все объекты в массиве
    public void SetObjectsActive(bool isActive)
    {
        if (targetObjects == null || targetObjects.Length == 0)
        {
            Debug.LogWarning("Нет назначенных объектов!", this);
            return;
        }

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
                obj.SetActive(isActive);
            else
                Debug.LogWarning("Один из объектов не назначен!", this);
        }
    }
}