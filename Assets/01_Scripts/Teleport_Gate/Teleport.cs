using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject A, B;
    [SerializeField] Animator transition;
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
    }

    public void TeleToA()
    {
        transition.SetTrigger("end");
        SoundManager.instance.PlaySFX(SoundManager.instance.transition);
        StartCoroutine(Wait(A));
    }

    public void TeleToB()
    {
        transition.SetTrigger("end");
        SoundManager.instance.PlaySFX(SoundManager.instance.transition);
        StartCoroutine(Wait(B));
    }

    IEnumerator Wait(GameObject point)
    {
        yield return new WaitForSeconds(1);
        player.transform.position = point.transform.position;
        yield return new WaitForSeconds(1);
        transition.SetTrigger("start");
    }
}
