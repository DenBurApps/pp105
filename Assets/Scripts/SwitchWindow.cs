using UnityEngine;
using UnityEngine.UI;

public class SwitchWindow : MonoBehaviour
{
    [SerializeField] private GameObject closeThisWindow;
    [SerializeField] private GameObject openThisWindow;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            closeThisWindow.SetActive(false);
            openThisWindow.SetActive(true);
        });
    }

}