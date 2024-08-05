using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    Button aluno;

    void Start()
    {
        aluno = GameObject.Find("ButtonAluno").GetComponent<Button>();
        aluno.onClick.AddListener(AlunoUpdate);
    }

    void AlunoUpdate()
    {
        SwitchScenes("RenderPolygons");
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
