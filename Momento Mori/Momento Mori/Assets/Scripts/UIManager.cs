using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //UI
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject transitionPanel;
    public Slider volume;
    public Slider brightness;

    //Fade to black
    public PostProcessVolume postProcessing;
    public float transitionTime;
    public float toOptionsAnimTime;
    public float toMainAnimTime;
    private float currentTime;

    private bool playHit;
    private bool optionsHit;
    private bool mainHit;
    private bool levelStart;

    private Image fadeToBlackImage;
    private Scene scene;

    //Camera transitions
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        postProcessing.weight = 0.0f;
        currentTime = 0.0f;
        playHit = false;
        fadeToBlackImage = transitionPanel.GetComponent<Image>();
        levelStart = true;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustBrightness();

        if (levelStart && scene.name == "Main Scene")
            FadeFromBlack();

        if (playHit)
        {
            FadeToBlack();
        }
        else if(optionsHit)
        {
            currentTime += Time.deltaTime;

            if(currentTime > toOptionsAnimTime)
            {
                optionsPanel.SetActive(true);
                currentTime = 0.0f;
                optionsHit = false;
            }
        }
        else if(mainHit)
        {
            currentTime += Time.deltaTime;

            if(currentTime > toMainAnimTime)
            {
                mainMenuPanel.SetActive(true);
                currentTime = 0.0f;
                mainHit = false;
            }
        }
    }

    public void Play()
    {
        playHit = true;     
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToOptions()
    {
        animator.SetBool("BackHit", false);

        mainMenuPanel.SetActive(false);

        animator.SetBool("OptionsHit", true);

        optionsHit = true;


    }

    public void ToMainMenu()
    {
        optionsPanel.SetActive(false);

        animator.SetBool("BackHit", true);  

        animator.SetBool("OptionsHit", false);

        mainHit = true;
    }

    void AdjustBrightness()
    {
        float gammaValue = brightness.value;
        RenderSettings.ambientLight = new Color(gammaValue, gammaValue, gammaValue, 1);
    }

    void FadeToBlack()
    {
        currentTime += Time.deltaTime;
        float transition = (currentTime / transitionTime);
        postProcessing.weight = (transition);
        fadeToBlackImage.color = new Color(0.0f, 0.0f, 0.0f, transition);


        if (currentTime > transitionTime)
        {
            currentTime = 0.0f;
            playHit = false;
            SceneManager.LoadScene(1);
        }
    }

    void FadeFromBlack()
    {
        currentTime = transitionTime - Time.deltaTime;
        Debug.Log(currentTime);
        float transition = (currentTime / transitionTime);
        postProcessing.weight = (transition);
        fadeToBlackImage.color = new Color(0.0f, 0.0f, 0.0f, transition);

        if(currentTime < 0.0f)
        {
            levelStart = false;
        }
    }


}

