using UnityEngine;
using UnityEngine.UI;

public class OpacitySetterScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image panelImage;
    void Start()
    {
        Color c = panelImage.color;
        c.a = 1.0f; // 50% opacity
        panelImage.color = c;
    }

// Update is called once per frame
void Update()
    {
        
    }
}
