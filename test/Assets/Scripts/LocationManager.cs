using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class LocationManager : MonoBehaviour
{
    [Header("Карты и локации")]
    public GameObject[] map1Locations; // Локации первой карты
    public GameObject[] map2Locations; // Локации второй карты
    public Transform[] defaultSpawnPoints;
    public List<int> lockedLocations = new List<int>();
    public int startLocationIndex = 0; // Индекс стартовой локации
    public int startMap = 1; // Стартовая карта (1 или 2)

    [Header("Настройки игрока")]
    public GameObject player;
    public float[] playerSpeedsPerLocation;

    [Header("Настройки камеры")]
    public CinemachineVirtualCamera[] virtualCameras;
    public float[] cameraOrthoSizePerLocation;

    private PlayerController playerMovement;
    private int currentMap = 1;
    private int currentLocationIndex = -1;

    private void Start()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerController>();
        }

        // Инициализация карт
        InitializeMaps();
        
        // Активируем стартовую локацию
        SwitchToStartLocation();
    }

    private void InitializeMaps()
    {
        // Деактивируем все локации на обеих картах
        foreach (var loc in map1Locations)
        {
            if (loc != null) loc.SetActive(false);
        }
        foreach (var loc in map2Locations)
        {
            if (loc != null) loc.SetActive(false);
        }
    }

    private void SwitchToStartLocation()
    {
        currentMap = startMap;
        SwitchLocation(startLocationIndex);
    }

    public void SwitchMap(int newMap)
    {
        if (newMap < 1 || newMap > 2) return;
        if (currentMap == newMap) return;

        // Скрываем текущую карту
        GameObject[] currentMapLocations = GetCurrentMapLocations();
        foreach (var loc in currentMapLocations)
        {
            if (loc != null) loc.SetActive(false);
        }

        currentMap = newMap;

        // Показываем текущую локацию на новой карте
        if (currentLocationIndex >= 0)
        {
            GetCurrentMapLocations()[currentLocationIndex].SetActive(true);
        }
    }

   public void SwitchLocation(int locationIndex, Transform spawnPoint = null)
{
    GameObject[] currentLocations = GetCurrentMapLocations();

    // Проверка индекса
    if (locationIndex < 0 || locationIndex >= currentLocations.Length)
    {
        Debug.LogError($"Неверный индекс локации: {locationIndex}");
        return;
    }

    // Проверка блокировки
    if (IsLocationLocked(locationIndex))
    {
        Debug.Log($"Локация {locationIndex} закрыта!");
        return;
    }

    // Отключаем все локации
    foreach (var loc in currentLocations)
    {
        if (loc != null) loc.SetActive(false);
    }

    // Включаем нужную локацию
    currentLocations[locationIndex].SetActive(true);
    currentLocationIndex = locationIndex;

    // Перемещаем игрока
    if (player != null)
    {
        if (spawnPoint == null && defaultSpawnPoints.Length > locationIndex)
        {
            spawnPoint = defaultSpawnPoints[locationIndex];
        }
        
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            if (playerMovement != null)
            {
                PlayerController.target = spawnPoint.position;
            }
        }
    }

    // Обновляем скорость игрока
    if (playerMovement != null && playerSpeedsPerLocation.Length > locationIndex)
    {
        playerMovement.speed = playerSpeedsPerLocation[locationIndex];
    }

    // Обновляем камеру (исправленная версия)
    if (virtualCameras != null && 
        virtualCameras.Length > 0 &&
        cameraOrthoSizePerLocation != null &&
        cameraOrthoSizePerLocation.Length > locationIndex)
    {
        foreach (var cam in virtualCameras)
        {
            if (cam != null)
            {
                cam.gameObject.SetActive(true);
                cam.m_Lens.OrthographicSize = cameraOrthoSizePerLocation[locationIndex];
                Debug.Log($"Установлен размер камеры: {cameraOrthoSizePerLocation[locationIndex]} для локации {locationIndex}");
            }
        }
    }
    else
    {
        Debug.LogError("Не удалось обновить камеру: проверьте назначение камер и размеров");
    }

    PlayerController.IsTalking = false;
}

    private GameObject[] GetCurrentMapLocations()
    {
        return currentMap == 1 ? map1Locations : map2Locations;
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
            
            // Если разблокировали текущую локацию - активируем ее
            if (currentLocationIndex == locationIndex)
            {
                GetCurrentMapLocations()[locationIndex].SetActive(true);
            }
        }
    }

    public void LockLocation(int locationIndex)
    {
        if (!lockedLocations.Contains(locationIndex))
        {
            lockedLocations.Add(locationIndex);
            Debug.Log($"Локация {locationIndex} теперь закрыта!");
            
            // Деактивируем локацию на обеих картах
            if (locationIndex >= 0 && locationIndex < map1Locations.Length)
            {
                map1Locations[locationIndex].SetActive(false);
            }
            if (locationIndex >= 0 && locationIndex < map2Locations.Length)
            {
                map2Locations[locationIndex].SetActive(false);
            }
        }
    }
}