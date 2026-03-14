using UnityEngine;

public class Phone_Script : MonoBehaviour
{

     public GameObject phonePanel;

     public void TogglePhone()
     {
        phonePanel.SetActive(true);
        gameObject.SetActive(false);
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
