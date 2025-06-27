using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    // Массив MonoBehaviour-скриптов для включения/выключения
    [SerializeField] private MonoBehaviour[] targetScripts;

    // Включить/выключить все скрипты в массиве
   public void SetScriptsActive(bool isActive)
{
    foreach (MonoBehaviour script in targetScripts)
    {
        if (script != null)
        {
            script.enabled = isActive;
            
            // Дополнительно останавливаем все корутины
            if (!isActive && script is MonoBehaviour behaviour)
            {
                behaviour.StopAllCoroutines();
            }
        }
    }

}
}