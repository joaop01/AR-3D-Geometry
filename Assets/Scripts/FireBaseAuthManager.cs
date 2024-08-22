using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using Firebase.Extensions;


public class FireBaseAuthManager : MonoBehaviour
{
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

    public void SignIn(){
        SignInWithGoogle();
    }

    private async Task SignInWithGoogle(){
        ServiceManager.GetService<OpenIDConnectService>().LoginCompleted+= OnLoginCompleted;
        await ServiceManager.GetService<OpenIDConnectService>().OpenLoginPageAsync();
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
    }
}
