using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameContoller : MonoBehaviour
{
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;
    private bool isPaused;
    public GameObject pauseMenu;
    private bool waitingForRespawn;
    private InventoryUI invUI;
    private PlayerController playerController;

    void Start()
    {
        invUI = GetComponent<InventoryUI>();
        playerController = GetComponent<PlayerController>();
    }

    public void ExitButton()
    {


        Application.Quit();
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        GameResumed.Invoke();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        invUI.UpdateUIPanels(false, true, true, true, true,false);
        
       
            
       

    }
    public void ResetovanieLevelu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        playerController.SaveInventory();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                GamePaused.Invoke();
                pauseMenu.SetActive(true);
                invUI.UpdateUIPanels(false, false, false, false,false,true);
            }
            else
            {
                Time.timeScale = 1;
                GameResumed.Invoke();
                pauseMenu.SetActive(false);
                invUI.UpdateUIPanels(false, true, true, true,true,false);
            }
        }
    }
    public void PauseGame()
    {
        if (!waitingForRespawn)
        {

            isPaused = !isPaused;
            Time.timeScale = 0;
            GamePaused.Invoke();
        }


    }
    public void Respawn() {

        
        isPaused = !isPaused;
        GameResumed.Invoke();
        //pauseMenu.SetActive(false);
        Time.timeScale = 1;
        // pauseMenu.SetActive(false);
        Debug.Log(isPaused);
        Debug.Log(Time.timeScale);
        Debug.Log("klik resume");
        waitingForRespawn = false;

    }
    public bool getWaitingforRespawn()
    {
        return waitingForRespawn;
    }
}
