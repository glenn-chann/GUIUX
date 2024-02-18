using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    PlayerMovement playerMovement;

    public Image stamBar;
    public Image visibilityBar;
    public Image soundBar;
    public Image foodBar;
    public Image waterBar;

    public int sampleWindow = 128;

    AudioClip microphoneClip;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        visibilityBar.rectTransform.sizeDelta = new Vector2(visibilityBar.rectTransform.sizeDelta.x, 0);
        soundBar.rectTransform.sizeDelta = new Vector2(soundBar.rectTransform.sizeDelta.x, 0);
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
        UpdateFood();
        UpdateWater();
        UpdateVisibility();
        UpdateSound();
    }

    void UpdateStamina()
    {
        if (playerMovement.isRunning && stamBar.rectTransform.sizeDelta.x > 0)
        {
            float currentValue;
            currentValue = stamBar.rectTransform.sizeDelta.x;
            stamBar.rectTransform.sizeDelta = new Vector2(currentValue -= (10 * Time.deltaTime), stamBar.rectTransform.sizeDelta.y);
        }
        else if (stamBar.rectTransform.sizeDelta.x < 100)
        {
            float currentValue;
            currentValue = stamBar.rectTransform.sizeDelta.x;
            stamBar.rectTransform.sizeDelta = new Vector2(currentValue += (10 * Time.deltaTime), stamBar.rectTransform.sizeDelta.y);
        }
    }

    void UpdateFood()
    {
        foodBar.fillAmount -= (0.01f * Time.deltaTime);
    }

    void UpdateWater()
    {
        waterBar.fillAmount -= (0.01f * Time.deltaTime);
    }

    void UpdateVisibility()
    {
        visibilityBar.rectTransform.sizeDelta = new Vector2(visibilityBar.rectTransform.sizeDelta.x,CalculateVisibility());  
    }

    float CalculateVisibility()
    {
        float finalValue = 0;
        if (playerMovement.isCrouching)
        {
            finalValue += 50;
        }
        if (!playerMovement.isMoving)
        {
            finalValue += 20;
        }
        if (playerMovement.isProning)
        {
            finalValue += 80;
        }
        return Mathf.Clamp(finalValue, 0 ,100);
    }

    void UpdateSound()
    {
        float loudness = GetVolume(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);

        soundBar.rectTransform.sizeDelta = new Vector2(soundBar.rectTransform.sizeDelta.x, Mathf.Clamp((loudness * 2000),0,100));
    }
    
    void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    float GetVolume(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;

    }
}
