using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private List<BasePlate> AllPlates = new List<BasePlate>();

    [SerializeField] private BasePlate plate;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform spawnPreviewPoint;
    [SerializeField] private GameObject emptyObject;
    [SerializeField] private GameObject recentEmptyObject;

    [SerializeField] private EditPlate CreateDataWindow;
    [SerializeField] private Transform CreateDataSpawnPlace;

    [SerializeField] private TextMeshProUGUI statisticAll;
    [SerializeField] private TextMeshProUGUI statisticVeryRare;
    [SerializeField] private TextMeshProUGUI statisticRare;
    [SerializeField] private TextMeshProUGUI statisticNonRare;

    public void SpawnCreateDataWindow()
    {
        Instantiate(CreateDataWindow, CreateDataSpawnPlace);
    }
    public void Start()
    {
        if (DataProcessor.Instance != null)
            SpawnAllPlates();
    }

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnAllPlates()
    {
        ClearPlates();
        {
            statisticAll.text = DataProcessor.Instance.allData.properties.Count.ToString();
            int veryRare = 0;
            int rare = 0;
            int nonRare = 0;

            foreach (var item in DataProcessor.Instance.allData.properties)
            {
                if (item.Rare == "Very rare")
                {
                    veryRare++;
                    continue;
                }

                if (item.Rare == "Rare")
                {
                    rare++;
                    continue;
                }
                if (item.Rare == "Not rare")
                {
                    nonRare++;
                    continue;
                }
            }

            statisticVeryRare.text = veryRare.ToString();
            statisticRare.text = rare.ToString();
            statisticNonRare.text = nonRare.ToString();
        }
        if (SpawnState)
            SpawnAll();
        else
            SpawnRecentPlates();
    }
    private void SpawnAll()
    {
        foreach (var item in DataProcessor.Instance.allData.properties)
        {
            if (Filter(item))
            {
                var obj = Instantiate(plate, spawnPoint);
                obj.Init(item, spawnPreviewPoint);
                AllPlates.Add(obj);
                //obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
            }
        }
        recentEmptyObject.SetActive(false);
        if (AllPlates.Count == 0) emptyObject.SetActive(true); else emptyObject.SetActive(false);
    }
    private void SpawnRecentPlates()
    {
        var list = DataProcessor.Instance.allData.properties;
        for (int i = list.Count - 1; i > list.Count - 3;i--)
        {
            if(i >= 0)
            {
                var obj = Instantiate(plate, spawnPoint);
                obj.Init(list[i], spawnPreviewPoint);
                AllPlates.Add(obj);
            }

            //obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
        }
        emptyObject.SetActive(false);
        if (AllPlates.Count == 0) recentEmptyObject.SetActive(true); else recentEmptyObject.SetActive(false);
    }
    private void ClearPlates()
    {
        foreach (var plate in AllPlates)
            Destroy(plate.gameObject);
        AllPlates.Clear();
    }
    public void ChangeSpawnState(bool state)
    {
        SpawnState = state;
        SpawnAllPlates();
    }
    public bool SpawnState = false;

    [SerializeField] private TMP_InputField search;
    private bool Filter(Properties props)
    {
        if(!props.Name.Contains(search.text))
            return false;
        return true;
    }
}
