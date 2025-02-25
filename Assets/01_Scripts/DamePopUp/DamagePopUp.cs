using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI DamePopUp;

    float TimeLost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeLost = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLost -= Time.deltaTime;
        if (TimeLost <= 0) Destroy(gameObject);
    }

    public void init(float dame)
    {
        DamePopUp = GetComponentInChildren<TextMeshProUGUI>();
        DamePopUp.text = "-" + dame.ToString();
    }


}
