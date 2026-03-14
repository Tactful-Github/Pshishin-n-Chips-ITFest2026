using UnityEngine;

public class Phone_Script : MonoBehaviour
{

     public GameObject phonePanel;

     bool isOpen = false;

     public void TogglePhone()
     {
        isOpen = !isOpen;
        phonePanel.SetActive(isOpen);
        gameObject.SetActive(!isOpen);
     }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
