using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using Firebase.Extensions;
using UnityEngine.SceneManagement;


public class FireBaseAuthManager : MonoBehaviour
{
    private string tipo_usuario="";
    // Start is called before the first frame update
    Firebase.Auth.FirebaseAuth auth;
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignIn(string Tipo_Usuario)
    {
        this.tipo_usuario = Tipo_Usuario;
        SignInWithGoogle();
    }

    public async Task SignInWithGoogle()
    {
        Debug.Log("Iniciando login - sign in with google");
        ServiceManager.GetService<OpenIDConnectService>().LoginCompleted+= OnLoginCompleted;
        Debug.Log("Iniciando login - pós service, awaiting");
        await ServiceManager.GetService<OpenIDConnectService>().OpenLoginPageAsync();
        Debug.Log("Iniciando login - pós open page");
    }

    private void OnLoginCompleted(object sender, EventArgs e){
        Debug.Log("login completed");


        string acessToken = ServiceManager.GetService<OpenIDConnectService>().AccessToken;

        Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(idToken:null, acessToken);

        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });

        if (tipo_usuario == "professor")
        {
            Debug.Log("Logado como professor.");
            SwitchScenes("areaProfessor");

        }
        else if (tipo_usuario == "aluno")
        {
            Debug.Log("Logado como aluno.");
            SwitchScenes("RenderPolygons");
        }
    }
    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}