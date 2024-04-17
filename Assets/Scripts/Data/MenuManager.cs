using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

namespace GenshintImpact2
{
    public class MenuManager : MonoBehaviour
    {
        public PlayerInput input { get; private set; }
        void Awake()
        {
            input = FindObjectOfType<PlayerInput>();
        }
        public void StartGame()
        {
            DataManager.instance.LoadData();
            SceneManager.LoadScene("MainScene");

            AudioManager.instance.StopPlaying("MainMenu");
            AudioManager.instance.Play("Main");

        }

        public void MainMenu()
        {

            AudioManager.instance.StopPlaying("Main");
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ChangeTime()
        {
            input.playerActions.ToggleInventory.Enable();
            Time.timeScale = 1f;
        }
    }
}
