using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeaGamePanel : MonoBehaviour
{
    public Task ownerTask;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Slider tempSlider;

    [SerializeField]
    TextMeshProUGUI tempText;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Sprite[] buttonSprites;

    [SerializeField]
    Image teaCupImage;

    [SerializeField]
    Sprite bagInTeaSprite;

    [SerializeField]
    GameObject teaBag;

    private float tempIncreaseSpeed = 13;
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
        startButton.image.sprite = buttonSprites[0];
        temperature = 12;
        teaIsBrewing = false;
        tempSlider.value = temperature;
        tempText.text = ((int)temperature).ToString() + " C°";
    }

    void StartButtonPressed()
    {
        if (teaIsBrewing == true) //If tea is already brewing
        {
            startButton.image.sprite = buttonSprites[0];
            if (temperature > 95 && temperature < 115) //Player finish heating the water
            {
                teaIsBrewing = false;
                animator.SetTrigger("Slide");
            }
            else //Player lose
            {
                ResetToDefault();
                Invoke("Hide", 0);
            }
        }
        else //If player starts brewing the tea
        {
            startButton.image.sprite = buttonSprites[1];
            teaIsBrewing = true;
        }
    }

    public void TeaBagInCup()
    {
        teaBag.SetActive(false);
        teaCupImage.sprite = bagInTeaSprite;
        ownerTask.SetAsResolved();

        Invoke("Hide", 2);
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

    public void TeaBrewAnimFinished()
    {
        animator.enabled = false;
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
