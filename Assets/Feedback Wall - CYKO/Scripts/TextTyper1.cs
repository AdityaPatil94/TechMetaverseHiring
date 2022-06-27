using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
 
 public class TextTyper1 : MonoBehaviour {
 
     public float letterPause = 0.2f;
 
    string f1b1text;
    public TMP_Text f1b1;
    string path;

    // Use this for initialization
    void OnEnable () {
        path = Application.dataPath + "/StreamingAssets/Questions.txt";
        GetData();
    }

    public void GetData()
    {
        string q = "Please specify the sessions that you attended?";
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