using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAnim : MonoBehaviour
{
    public QuizManager qm;
    public GameObject rcb;
    // Start is called before the first frame update
    void QuizAnimation()
    {
        GetComponent<Animator>().Play("OptionsAnimation");
    }

    public void RaycastBlocker()
    {
        rcb.SetActive(false);
    }
}
