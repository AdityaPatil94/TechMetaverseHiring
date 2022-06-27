using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
 
 public class TextTyper : MonoBehaviour {
 
     public float letterPause = 0.2f;
 
    string f1b1text;
    public TMP_Text f1b1;
    string path;

    // Use this for initialization
    void OnEnable () {
        path = Application.dataPath + "/StreamingAssets/Questions.txt";
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        WWW www = new WWW(path);
        yield return www;

        JSONObject j = new JSONObject(www.text);
        string q = j.GetField("questions")[QuizManager.qNum].GetField("question").str;

        f1b1.text = "";
        f1b1text = q;

        StartCoroutine(TypeTextf1b1());
    }

    IEnumerator TypeTextf1b1() {
         foreach (char letter in f1b1text.ToCharArray()) {
            f1b1.text += letter;

            yield return 0;
            yield return new WaitForSeconds (letterPause);

         }
     }

    public void StartAnimf1b1()
    {
        f1b1.text = "";
        f1b1text = GetComponent<TMP_Text>().text;

        StartCoroutine(TypeTextf1b1());
    }

    private void Update()
    {
    
    }

    public void EmptyText()
    {
        f1b1.text = "";
    }
}