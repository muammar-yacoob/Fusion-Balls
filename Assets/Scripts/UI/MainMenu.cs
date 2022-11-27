using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Environment;

namespace Spark.FusionTest
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button teacherButton;
        [SerializeField] private Button studentButton;

        [SerializeField] private TMP_InputField nickNameUI;

        private const string NICK_NAME_PREF = "NICK_NAME";

        private void Awake()
        {
            teacherButton.onClick.AddListener(LoginAsTeacher);
            studentButton.onClick.AddListener(LoginAsStudent);

            nickNameUI.text = LoadNickName();
        }

        private void LoginAsTeacher()
        {
            SaveNickName();
            SceneManager.LoadScene(App.Instance.TeacherScene);
        }

       private void LoginAsStudent()
        {
            SaveNickName();
            SceneManager.LoadScene(App.Instance.StudentScene);
        }

       private string LoadNickName()
       {
           var nickName = PlayerPrefs.GetString(NICK_NAME_PREF);
           nickName = String.IsNullOrEmpty(nickName) ? UserName : nickName;
           return nickName;
       }
       
       private void SaveNickName()
       {
           var nickName = nickNameUI.text;
           nickName = String.IsNullOrEmpty(nickName) ? UserName : nickName;
           nickName = nickName.ToUpper();
           PlayerPrefs.SetString(NICK_NAME_PREF, nickName);
       }
    }
}