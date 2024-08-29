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
    
    void Start()
    {
        //authMan = new FireBaseAuthManager();
        //authMan.Start();
        Debug.Log("start");
        //aluno = GameObject.Find("ButtonAluno").GetComponent<Button>();
        //aluno.onClick.AddListener(AlunoUpdate);
        
        //professor = GameObject.Find("ButtonProfessor").GetComponent<Button>();
        //professor.onClick.AddListener(ProfessorUpdate);
    }

    void AlunoUpdate()
    {
        Debug.Log("aluno update");
        Debug.Log("Sign in");
        //authMan.SignIn("aluno");
        Debug.Log("Switch");
        //SwitchScenes("RenderPolygons");
    }
    
    void ProfessorUpdate()
    {
        //authMan.SignIn("professor");
        //SwitchScenes("RenderPolygons");
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
