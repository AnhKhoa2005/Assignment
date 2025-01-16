using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject Esc;
    bool esc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        esc = false;
    }

    // Update is called once per frame
    void Update()
    {
        ESC();
    }

    void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && esc == false)
        {
            Esc.SetActive(true);
            esc = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && esc == true)
        {
            Esc.SetActive(false);
            esc = false;
        }

    }
}
