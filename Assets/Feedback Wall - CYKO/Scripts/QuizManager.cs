using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

[Serializable]
public class UserData
{
    public string date, uname, umobile, sessions, feedback1, feedback2, feedback3, feedback4, feedback5;
}

[Serializable]
public class UserDatas
{
    public List<UserData> users = new List<UserData>();
}


public class QuizManager : MonoBehaviour
{
    [SerializeField] private UserData userData = new UserData();
    public UserDatas userDatas = new UserDatas();
    string path;
    string feedbackPath;
    public TMP_Text question;
    public TMP_Text questionNum;
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    public TMP_Text optionD;
    public static int qNum = 0;
    string getAnswer;
    public Animator quizAnim;
    public GameObject bullet;
    public Transform bulletsParent;
    bool isOnce = true;
    GameObject firstBullet;
    public Sprite bulletDefault;
    int questionCount;
    public Sprite blank;
    public GameObject thankyou;
    public GameObject quiz;
    public GameObject inv;
    public string[] questionsData;
    public float timer = 0;
    public float timer2 = 0;
    public Slider slider;
    bool isFinished = false;
    bool isOnce2 = true;
    public Slider slider2;
    bool isSelected = false;
    bool isOnce3 = true;
    public GameObject session;
    public TMP_InputField namef;
    public TMP_InputField mobf;
    public string[] answers;

    private void Awake()
    {
        qNum = 0;
        feedbackPath = Application.dataPath + "/StreamingAssets/UserData.json";

        if (!File.Exists(feedbackPath))
        {
            File.Create(feedbackPath);

            byte[] s = File.ReadAllBytes(feedbackPath);
            var str = System.Text.Encoding.UTF8.GetString(s);

            if (str == "")
            {
                File.WriteAllText(feedbackPath, "{}");
            }
        }
    }

    void Start()
    {
        path = Application.dataPath + "/StreamingAssets/Questions.txt";
        StartCoroutine(GetData());

        byte[] s = File.ReadAllBytes(feedbackPath);
        var res = System.Text.Encoding.UTF8.GetString(s);
        userDatas = JsonUtility.FromJson<UserDatas>(res);
    }

    IEnumerator GetDataField(string opt)
    {
        WWW www = new WWW(path);
        yield return www;

        JSONObject j = new JSONObject(www.text);

        answers[qNum-1] = j.GetField("questions")[qNum-1].GetField(opt).str;
    }

    

    public void AnswerButton(string answer)
    {
       Invoke("RepeatAnim", 0.5f);

        qNum++;

        if (answer == "A")
        {
            optionA.transform.parent.GetComponent<Animator>().Play("AnswerBtnAnimation");
            StartCoroutine(GetDataField("OptionA"));
        }
        else if (answer == "B")
        {
            optionB.transform.parent.GetComponent<Animator>().Play("AnswerBtnAnimation");
            StartCoroutine(GetDataField("OptionB"));
        }
        else if (answer == "C")
        {
            optionC.transform.parent.GetComponent<Animator>().Play("AnswerBtnAnimation");
            StartCoroutine(GetDataField("OptionC"));
        }
        else if (answer == "D")
        {
            optionD.transform.parent.GetComponent<Animator>().Play("AnswerBtnAnimation");
            StartCoroutine(GetDataField("OptionD"));
        }

        if(qNum == 5)
        {
            Invoke("SaveData", 1f);
            thankyou.SetActive(true);
            quiz.SetActive(false);
            inv.SetActive(false);
            isFinished = true;
        }
    }

    void SaveData()
    {
        SaveIntoJson();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveIntoJson()
    {
        userData.date = DateTime.Now.ToString();
        userData.uname = namef.text;
        userData.umobile = mobf.text;
        userData.sessions = sessionNames;
        userData.feedback1 = answers[0];
        userData.feedback2 = answers[1];
        userData.feedback3 = answers[2];
        userData.feedback4 = answers[3];
        userData.feedback5 = answers[4];

        userDatas.users.Add(userData);
        string data = JsonUtility.ToJson(userDatas);
        File.WriteAllText(feedbackPath, data);

        byte[] s = File.ReadAllBytes(feedbackPath);
        var res = System.Text.Encoding.UTF8.GetString(s);
        userDatas = JsonUtility.FromJson<UserDatas>(res);

        Debug.Log("Success!");
    }

    private void RepeatAnim()
    {
        optionA.transform.parent.GetComponent<Animator>().Rebind();
        optionB.transform.parent.GetComponent<Animator>().Rebind();
        optionC.transform.parent.GetComponent<Animator>().Rebind();
        optionD.transform.parent.GetComponent<Animator>().Rebind();
        question.text = "";
        quizAnim.Play("QuizAnimation", -1, 0);
        firstBullet.transform.SetSiblingIndex(qNum);

        if (qNum < 5)
            StartCoroutine(GetData());
    }

    // Update is called once per frame
    void Update()
    {
        if(isFinished)
        {
            timer += Time.deltaTime * 10f;
            slider.value = timer / 100f;

            if(slider.value >= 1)
            {
                Debug.Log("Done!");
                if(isOnce2)
                {
                    ResetScene();
                    isOnce2 = false;
                }
            }
        }

        if (isSelected)
        {
            timer2 += Time.deltaTime * 20f;
            slider2.value = timer2 / 100f;

            if (slider2.value >= 1)
            {
                
                if (isOnce3)
                {
                    Debug.Log("Done!");
                    session.SetActive(false);
                    quiz.SetActive(true);
                    isOnce3 = false;
                }
            }
        }

    }

    int selectCount = 0;
    bool isOnce4 = true;
    string sessionNames,k1,j1,s1,d1,o1,so1;

    public void SelectSession(string sessionName)
    {
        slider2.gameObject.SetActive(true);
        isSelected = true;
        selectCount++;

        if(selectCount > 1 && isOnce4)
        {
            Invoke("GotoQuiz", 0.5f);
            isOnce4 = false;
        }
        
        if (sessionName == "HDRP")
            k1 = sessionName;
        if (sessionName == "URP")
            j1 = sessionName;
        if (sessionName == "Lighting")
            s1 = sessionName;
        if (sessionName == "Photon")
            d1 = sessionName;
        if (sessionName == "UNet")
            o1 = sessionName;
        if (sessionName == "Social")
            so1 = sessionName;

        sessionNames = k1 +" "+ j1 + " " + s1 + d1 + " " + o1 + " " + so1;
        Debug.Log(sessionNames);
    }

    void GotoQuiz()
    {
        session.SetActive(false);
        quiz.SetActive(true);
    }

    IEnumerator GetData()
    {
        WWW www = new WWW(path);
        yield return www;

        JSONObject j = new JSONObject(www.text);
        string qnum = j.GetField("questions")[qNum].GetField("id").str;
        questionNum.text = int.Parse(qnum).ToString();
        string count = j.GetField("questions").Count.ToString();
        questionCount = int.Parse(count);
        optionA.text = j.GetField("questions")[qNum].GetField("OptionA").str;
        optionB.text = j.GetField("questions")[qNum].GetField("OptionB").str;
        optionC.text = j.GetField("questions")[qNum].GetField("OptionC").str;
        optionD.text = j.GetField("questions")[qNum].GetField("OptionD").str;

        if (isOnce)
        {
            for (int i = 0; i < int.Parse(count); i++)
            {
                GameObject childObject = Instantiate(bullet) as GameObject;
                childObject.transform.SetParent(bulletsParent,false);
                childObject.name = childObject.transform.GetSiblingIndex().ToString();
            }

            for (int o = 0; o < int.Parse(count); o++)
            {
                questionsData[o] = j.GetField("questions")[o].GetField("question").str;
            }

            firstBullet = bulletsParent.transform.GetChild(0).gameObject;
            firstBullet.GetComponent<Image>().sprite = bulletDefault;
            isOnce = false;
        }
    }
}
