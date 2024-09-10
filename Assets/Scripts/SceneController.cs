using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    Button aluno;
    Button professor;
    //public FireBaseAuthManager authMan;

    private vars v;
    
    void Start()
    {
        //authMan = new FireBaseAuthManager();
        //authMan.Start();
        v = GameObject.Find("vars").GetComponent<vars>();
        Debug.Log("start");
        aluno = GameObject.Find("ButtonAluno").GetComponent<Button>();
        aluno.onClick.AddListener(AlunoUpdate);
        
        professor = GameObject.Find("ButtonProf").GetComponent<Button>();
        professor.onClick.AddListener(ProfessorUpdate);
    }

    void AlunoUpdate()
    {
        Debug.Log("aluno update");
        v.tipo_user = "aluno";
        Debug.Log("Sign in" + v.tipo_user);
        //authMan.SignIn("aluno");
        Debug.Log("Switch");
        SwitchScenes("login");
    }
    
    void ProfessorUpdate()
    {
        Debug.Log("professor update");
        v.tipo_user = "professor";
        Debug.Log("Sign in" + v.tipo_user);
        //authMan.SignIn("professor");
        SwitchScenes("login");
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
