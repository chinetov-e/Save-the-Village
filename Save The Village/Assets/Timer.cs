using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float maxTime;
    public bool Tick;

    private Image img;
    private AudioSource sound;
    private float currentTime;

    void Start()
    {
        img = GetComponent<Image>();
        sound = GetComponent<AudioSource>();
        currentTime = maxTime;
    }   

    void Update()
    {
        Tick = false;
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            Tick = true;
            currentTime = maxTime;
            sound.Play();
        }
        img.fillAmount = currentTime / maxTime;
    }
}
