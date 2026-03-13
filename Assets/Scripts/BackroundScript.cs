using UnityEngine;
using UnityEngine.UIElements;

public class BackroundScript : MonoBehaviour
{
    public GameObject backgrounds; 
    private GameObject[] scenes;
    private int crr = 0;

    public void EnableScene(string sceneName)
    {
        Transform scene = backgrounds.transform.Find(sceneName);

        if (scene != null)
            scene.gameObject.SetActive(true);

    }

    public void DisableScene(string sceneName)
    {
        Transform scene = backgrounds.transform.Find(sceneName);

        if (scene != null)
            scene.gameObject.SetActive(false);
    }

    public void ShowOnlyScene(string sceneName)
    {
        foreach (Transform child in backgrounds.transform)
        {
            child.gameObject.SetActive(child.name == sceneName);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int count = backgrounds.transform.childCount;
        scenes = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            scenes[i] = backgrounds.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ShowOnlyScene("Scene 3");
            
        }
    }
}
