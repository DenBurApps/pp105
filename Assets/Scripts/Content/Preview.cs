using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{
    public static Preview Instance;

    public Properties properties;

    [SerializeField] private ImagePicker Photo;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Location;
    [SerializeField] private TextMeshProUGUI Date;
    [SerializeField] private TextMeshProUGUI Rare;
    public TextMeshProUGUI Description;


    public void Init(Properties props)
    {
        properties = props;

        Photo.Init(props.Photo);

        Name.text = props.Name;
        Location.text = props.Location;
        Date.text = props.Date;
        Rare.text = props.Rare;
        Description.text = props.Description;
    }

    public void DeleteProject()
    {
        DataProcessor.Instance.DeletePlate(properties);
        Destroy(gameObject);
    }
    private void Awake()
    {
        Instance = this;
    }
    public EditPlate editPlateWindow;
    public void OpenEditWindow()
    {
        var obj = Instantiate(editPlateWindow, transform);
        obj.Init(properties,this);
    }
}
