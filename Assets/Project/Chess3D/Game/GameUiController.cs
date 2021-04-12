using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Assets.Project.ChessEngine;
using Assets.Project.ChessEngine.Exceptions;
using Chess3D.Dependency;
using UnitySocketIO;
using UnitySocketIO.Events;

namespace Assets.Project.Chess3D
{
    public class GameUiController : MonoBehaviour
    {
        public Text ErrorText;
        public Text InputInfoText;
        public Text SearchInfoText;
        public Button EndButton;

        public GameObject btnSave;

        public GameObject gameOverWindow;
        public Text winnerText;

        public GameObject btn_home;
        public GameObject btn_restart;
        public GameObject btn_resume;
        SocketIOController socket;

        private void Start()
        {
            socket = SocketIOController.instance;
            gameOverWindow.SetActive(false);

            EndButton.onClick.AddListener(ToMenu);

            if (PlayerPrefs.GetInt("VsCPU", 1) == 1)
            {
                btnSave.SetActive(true);
                /* btn_home.SetActive(true);
                 btn_restart.SetActive(true);
                 btn_resume.SetActive(true);*/
            }
            else
            {
                btnSave.SetActive(false);
                /*   btn_home.SetActive(false);
                   btn_restart.SetActive(false);
                   btn_resume.SetActive(false);*/
            }
        }

        public void ShowErrorText(string text)
        {
            ErrorText.text = text;
        }

        public void HideErrorText()
        {
            ErrorText.text = string.Empty;
        }

        public void ShowInputInfoText(string text)
        {
            InputInfoText.text = text;
        }

        public void HideInputInfoText()
        {
            InputInfoText.text = string.Empty;
        }

        public void ShowSearchInfoText(string text)
        {
            SearchInfoText.text = text;
        }

        public void HideSearchInfoText()
        {
            SearchInfoText.text = string.Empty;
        }

        public void EndGame(string winner)
        {
            gameOverWindow.SetActive(true);
            winnerText.text = winner.ToUpper();
            if (winner.ToUpper() == "YOU WINS.")
            {
                Global.m_user.score++;
                socket.Emit("increaseScore", JsonUtility.ToJson(Global.m_user));
                if (PlayerPrefs.GetInt("VsCPU", 1) != 1)
                    socket.Emit("set winner", JsonUtility.ToJson(Global.m_user));
            }
            //InputInfoText.text = winner;
            //ErrorText.text = string.Empty;
            //SearchInfoText.text = string.Empty;
        }

        public void ClearAll()
        {
            InputInfoText.text = string.Empty;
            ErrorText.text = string.Empty;
        }

        public void ToMenu()
        {

            SceneManager.LoadScene("MainMenu");


        }

        public void Restart()
        {

        }
    }
}
