using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public int targetLocationIndex;
    public Transform customSpawnPoint;
    public LocationManager locationManager;
    public bool showLockedMessage = true;

    private void OnMouseDown()
    {
        if (locationManager == null) return;

        if (locationManager.IsLocationLocked(targetLocationIndex))
        {
            if (showLockedMessage)
            {
                Debug.Log("Эта локация пока недоступна!");
                // Здесь можно добавить визуальное сообщение для игрока
            }
            return;
        }

        Transform spawnPoint = customSpawnPoint != null
            ? customSpawnPoint
            : locationManager.GetDefaultSpawnPoint(targetLocationIndex);

        PlayerController.IsTalking = true;
        locationManager.SwitchLocation(targetLocationIndex, spawnPoint);
    }
}