using UnityEngine;

public class Close_Phone_Script : MonoBehaviour
{

    public GameObject SmallPhone;
    public GameObject BigPhone;

    public void TogglePhone()
    {
        SmallPhone.SetActive(true);
        BigPhone.SetActive(false);
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
