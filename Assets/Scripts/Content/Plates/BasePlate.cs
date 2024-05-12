using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePlate : MonoBehaviour
{
    public Properties properties;

    [SerializeField] private ImagePicker Photo;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Location;
    [SerializeField] private TextMeshProUGUI Date;
    [SerializeField] private TextMeshProUGUI Rare;


    [SerializeField] private Preview preview;
    private Transform previewSpawnPlace;
    public void Init(Properties props,Transform previewSpawnPlace)
    {
        properties = props;
        this.previewSpawnPlace = previewSpawnPlace;

        Photo.Init(props.Photo);

        Name.text = props.Name;
        Location.text = props.Location;
        Date.text = props.Date;
        Rare.text = props.Rare;
    }

    public void OpenPreview()
    {
        var obj = Instantiate(preview,previewSpawnPlace);
        obj.Init(properties);
    }
}
