using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject mainButtonsMenu;
    public GameObject aboutGameMenu;
    private GameObject storyPanel;
    public TextMeshProUGUI storyText;
    private bool startGameBool = false;
    private float counter = 0;
    private float pocetPribehov = 4;
    private float timer = 0;
    void Start()
    {
        storyPanel = GameObject.FindGameObjectWithTag("EndScenePanel");
        Time.timeScale = 1f;
        storyPanel.SetActive(false);
        timer = 0;
        counter = 0;
        startGameBool = false;

    }
    private void Update()
    {
        if(startGameBool )
        {
            storyPanel.SetActive(true);
            if (counter == pocetPribehov)
            {

                startGame();
            }
            else
            {
                


                timer += Time.deltaTime;


                    if (timer > 3.0f)
                    {


                        timer = 0.0f;
                        counter++;
                        Debug.Log("ide to" + counter);

                    switch (counter)
                    {
                        case 1:
                            storyText.text = "Once upon a time, in a world where magic still existed, ";
                            break;
                        case 2:
                            storyText.text = "there was beautiful nature where life functioned peacefully and in harmony. ";
                            break;
                        case 3:
                            storyText.text = "But everything suddenly changed. ";
                            break;
                        case 4:
                            storyText.text = "Become a hero and save your forest.";
                            break;
                        default:
                            
                            break;
                    }

                }

                
            }

            
        }
    }

    public void PlayButton()
    {

        startGameBool = true;

        //storyPanel.SetActive(true);
        //Time.timeScale = 1f;
        //playStory();

        
    }
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1f;


    }

    public void playStory()
    {
              float timer = 0;
              

             timer += Time.deltaTime;


            while(counter != pocetPribehov)
        {
            if (timer > 3.0f)
            {


                timer = 0.0f;
                counter++;
                Debug.Log("ide to" + counter);

            }

        }

    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void InfoButton()
    {
        mainButtonsMenu.SetActive(false);
        aboutGameMenu.SetActive(true);
    }
    public void ReturnToMenuButton()
    {
        mainButtonsMenu.SetActive(true);
        aboutGameMenu.SetActive(false);
    }

}
