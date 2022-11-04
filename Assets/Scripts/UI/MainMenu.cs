using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button teacherButton;
        [SerializeField] private Button studentButton;

        [SerializeField] private TMP_InputField nickNameUI;

        private int teacherSceneIndex = 1;
        private int studentSceneIndex = 2;
        private const string nickNamePref = "NICK_NAME";

        private void Awake()
        {
            teacherButton.onClick.AddListener(LoginAsTeacher);
            studentButton.onClick.AddListener(LoginAsStudent);

            nickNameUI.text = LoadNickName();
        }

        private void LoginAsTeacher()
        {
            SaveNickName();
            SceneManager.LoadScene(teacherSceneIndex);
        }

       private void LoginAsStudent()
        {
            SaveNickName();
            SceneManager.LoadScene(studentSceneIndex);
        }

       private string LoadNickName()
       {
           var nickName = PlayerPrefs.GetString(nickNamePref);
           nickName = String.IsNullOrEmpty(nickName) ? System.Environment.UserName : nickName;
           return nickName;
       }
       
       private void SaveNickName()
       {
           var nickName = nickNameUI.text;
           nickName = String.IsNullOrEmpty(nickName) ? System.Environment.UserName : nickName;
           nickName = nickName.ToUpper();
           PlayerPrefs.SetString(nickNamePref, nickName);
       }
    }
}