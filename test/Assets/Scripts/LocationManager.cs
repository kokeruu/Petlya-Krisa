using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class LocationManager : MonoBehaviour
{
    [Header("Локации")]
    public GameObject[] locations;
    public Transform[] defaultSpawnPoints;
    public List<int> lockedLocations = new List<int>(); // Индексы закрытых локаций

    [Header("Настройки игрока")]
    public GameObject player;
    public float[] playerSpeedsPerLocation;

    [Header("Настройки камеры")]
    public CinemachineVirtualCamera[] virtualCameras;
    public float[] cameraOrthoSizePerLocation;

    private PlayerController playerMovement;

    private void Start()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerController>();
        }

        // Изначально деактивируем закрытые локации
        foreach (int index in lockedLocations)
        {
            if (index >= 0 && index < locations.Length)
            {
                locations[index].SetActive(false);
            }
        }
    }

    public void SwitchLocation(int locationIndex, Transform spawnPoint = null)
    {
        // Проверка индекса
        if (locationIndex < 0 || locationIndex >= locations.Length)
        {
            Debug.LogError($"Неверный индекс локации: {locationIndex}");
            return;
        }
        if (IsLocationLocked(locationIndex))
        {
            Debug.Log($"Локация {locationIndex} закрыта!");
            return;
        }
        // Отключаем все локации
        foreach (var loc in locations)
        {
            if (loc != null) loc.SetActive(false);
        }

        // Включаем нужную локацию
        locations[locationIndex].SetActive(true);

        // Перемещаем игрока
        if (player != null)
        {
            Transform targetPoint = spawnPoint != null
                ? spawnPoint
                : defaultSpawnPoints[locationIndex];

            if (targetPoint != null)
            {
                player.transform.position = targetPoint.position;
                Debug.Log($"Игрок перемещён в локацию {locationIndex}");
                PlayerController.target=targetPoint.position;
            }
            else
            {
                Debug.LogError("Точка спавна не назначена!");
            }
        }


        // Меняем скорость игрока
        if (playerMovement != null && playerSpeedsPerLocation.Length > locationIndex)
        {
            playerMovement.speed = playerSpeedsPerLocation[locationIndex];
            Debug.Log($"Скорость игрока изменена на: {playerSpeedsPerLocation[locationIndex]}");
        }

        // Меняем размер камеры
        if (virtualCameras.Length > locationIndex &&
            cameraOrthoSizePerLocation.Length > locationIndex)
        {
            foreach (var cam in virtualCameras)
            {
                cam.m_Lens.OrthographicSize = cameraOrthoSizePerLocation[locationIndex];
            }
            Debug.Log($"Размер камеры изменён на: {cameraOrthoSizePerLocation[locationIndex]}");
        }
        PlayerController.IsTalking = false;
    }
    public Transform GetDefaultSpawnPoint(int locationIndex)
    {
        if (locationIndex >= 0 && locationIndex < defaultSpawnPoints.Length)
        {
            return defaultSpawnPoints[locationIndex];
        }
        return null;
    }
    public bool IsLocationLocked(int locationIndex)
    {
        return lockedLocations.Contains(locationIndex);
    }

    public void UnlockLocation(int locationIndex)
    {
        if (lockedLocations.Contains(locationIndex))
        {
            lockedLocations.Remove(locationIndex);
            Debug.Log($"Локация {locationIndex} теперь открыта!");
            
            // Если нужно сразу активировать локацию при открытии:
            // locations[locationIndex].SetActive(true);
        }
    }

    public void LockLocation(int locationIndex)
    {
        if (!lockedLocations.Contains(locationIndex))
        {
            lockedLocations.Add(locationIndex);
            Debug.Log($"Локация {locationIndex} теперь закрыта!");
            
            // Деактивируем локацию при закрытии
            if (locationIndex >= 0 && locationIndex < locations.Length)
            {
                locations[locationIndex].SetActive(false);
            }
        }
    }

}