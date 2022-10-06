using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeaGamePanel : MonoBehaviour
{
    [SerializeField]
    Task ownerTask;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Slider tempSlider;

    [SerializeField]
    TextMeshProUGUI tempText;

    private float tempIncreaseSpeed = 10;
    private float temperature;
    private bool teaIsBrewing;

    private GameObject lastFirstSelectedGameObject;

    void Start()
    {
        startButton.onClick.AddListener(StartButtonPressed);
        ResetToDefault();
    }

    private void ResetToDefault() //Resets stuff to default stuff
    {
        temperature = 12;
        teaIsBrewing = false;
        tempSlider.value = temperature;
        tempText.text = ((int)temperature).ToString() + " C°";
    }

    void StartButtonPressed()
    {
        if (teaIsBrewing == true)
        {
            if (temperature > 95 && temperature < 105) //Player finish heating the water
            {
                teaIsBrewing = false;
                ownerTask.SetAsResolved();

                Invoke("Hide", 1);
            }
            else //Player lose
            {
                ResetToDefault();
                Invoke("Hide", 0);
            }
        }
        else
        {
            teaIsBrewing = true;
        }
    }

    private void Update()
    {
        if (teaIsBrewing)
        {
            temperature += tempIncreaseSpeed * Time.deltaTime;
            tempSlider.value = temperature;
            tempText.text = ((int)temperature).ToString() + " C°";
        }
    }


    public void Show()
    {
        gameObject.SetActive(true);
        lastFirstSelectedGameObject = GameManager.Instance.EventSystem.firstSelectedGameObject;
        GameManager.Instance.EventSystem.firstSelectedGameObject = gameObject;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GameManager.Instance.EventSystem.firstSelectedGameObject = lastFirstSelectedGameObject;
    }
}
