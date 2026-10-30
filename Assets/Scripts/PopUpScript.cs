using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public GameObject popupPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopup();
        }
    }
}
