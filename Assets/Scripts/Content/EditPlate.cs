using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlate : MonoBehaviour
{
    public Properties properties = new Properties();
    [Header("Base Window")]
    [SerializeField] private InputFieldChanger Name;
    [SerializeField] private InputFieldChanger Location;
    [SerializeField] private InputFieldChanger Description;

    [SerializeField] private ImagePicker Photo;

    [SerializeField] private TextMeshProUGUI Date;

    public CheckMark[] marks;

    public Calendar calendar;
    public Button ContinueButton;
    private Preview preview;

    [SerializeField] private Button button;
    private void FixedUpdate()
    {
        if(Name.text == "")
        {
            button.interactable = false;
            return;
        }
        if (Location.text == "")
        {
            button.interactable = false;
            return;
        }
        if (ChoosedDay == "")
        {
            button.interactable = false;
            return;
        }
        button.interactable = true;
    }
    public void Init(Properties props,Preview preview)
    {
        properties = props;
        this.preview = preview;


        Photo.Init(props.Photo);
        choosedPhoto = props.Photo;

        if (props.Date != "")
        {
            DateTime.TryParse(props.Date, out DateTime date);
            calendar.Init(ChangeDate, 1, date);

            ChoosedDay = props.Date;
            Date.text = ChoosedDay;
        }
        else
            calendar.Init(ChangeDate, 1);



        Name.ChangeText(props.Name);
        Location.ChangeText(props.Location);
        Description.ChangeText(props.Description);

        foreach (var item in marks)
        {
            if (item.status.text == properties.Rare)
                item.ReturnStatus();
            else
                item.DeactivateCheck();
        }

        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => 
        {
            SavePlateData();
            Destroy(gameObject);
        });
    }
    public void OnClickPhotoChange()
    {
        GetImageFromGallery.PickImage(ChangePhoto);
    }
    private string choosedPhoto;
    private void ChangePhoto(string str)
    {
        if(str !="" && str != null)
        {
            choosedPhoto = str;
            Photo.Init(choosedPhoto);
        }
    }
    private string ChoosedDay = "";
    private void ChangeDate(Day day)
    {
        calendar.choosedDays[0] = day.DateTime;
        ChoosedDay = day.DateTime.ToString().Remove(10);
        Date.text = ChoosedDay;
        calendar.SetDayStates();
    }
    private void Awake()
    {
        ContinueButton.onClick.AddListener(() =>
        {
            CreatePlateData();
            Destroy(gameObject);
        });
        calendar.Init(ChangeDate, 1);
        foreach(var mark in marks)
        {
            mark.GetComponent<Button>().onClick.AddListener(() => 
            { ChangeStatus(mark); });
        }
        ChangeStatus(marks[0]);
    }
    private string Rare;
    public void ChangeStatus(CheckMark checkedMark)
    {
        foreach (var mark in marks)
        {
            mark.DeactivateCheck();
        }
        Rare = checkedMark.ReturnStatus();
    }
    public void SavePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.EditPlate(properties);
        preview.Init(properties);
    }

    public void CreatePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.AddNewPlate(properties);
    }

    private void FillPlateData()
    {
        properties.Photo = choosedPhoto;
        properties.Date = ChoosedDay;
        properties.Name = Name.text;
        properties.Location = Location.text;
        properties.Rare = Rare;
        properties.Description = Description.text;
        Destroy(gameObject);
    }
}
