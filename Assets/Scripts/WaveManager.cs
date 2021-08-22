using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private List<WaveSO> waves;

    [SerializeField]
    private int currentWaveIndex = -1;
    
    [SerializeField]
    private Gun playerGun;
    
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private GameObject NextWavePanel;
    private void Awake()
    {
        NextWave();
    }
    public void NextWave()
    {
        currentWaveIndex++;
        if(currentWaveIndex == waves.Count)
        {
            currentWaveIndex = waves.Count - 1;
            return;
        }
        NextWavePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Next Wave: " + (currentWaveIndex+1);
        NextWavePanel.SetActive(true);
        WaveSO waveSO = waves[currentWaveIndex];
        playerGun.gunSO = waveSO.gun; //kullanıcının silah özelliklerini değiştir.
        spawner.waveSO = waveSO; //Düşmanların oluşma süresini değiştir.
        Invoke("NextWavePanelClose", 3);
    }

    public WaveSO GetCurrentWaveSO()
    {
        return waves[currentWaveIndex];
    }

    private void NextWavePanelClose()
    {
        NextWavePanel.SetActive(false);
    }

    public string GetNextWaveCounter(int killCount)
    {
        if(currentWaveIndex == waves.Count-1)
        {
            return "Max";
        }
        return GetCurrentWaveSO().maxKill - killCount + "";
    }
}
