using UnityEngine;

public class Close_Phone_Script : MonoBehaviour
{

    public GameObject SmallPhone;
    public GameObject BigPhone;

    bool isOpen = false;

    public void TogglePhone()
    {
        isOpen = !isOpen;
        SmallPhone.SetActive(isOpen);
        BigPhone.SetActive(!isOpen);
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
