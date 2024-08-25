using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;


public class GoogleSignIn : BaseServiceBootstrapper
{
    [SerializeField]private ClientDataObject googleClientDataObject;
    [SerializeField]private ClientDataObject googleClientDataObjectEditorOnly;

    protected override void RegisterServices(){
        OpenIDConnectService oidc = new OpenIDConnectService();
        oidc.OidcProvider = new GoogleOidcProvider();
#if !UNITY_EDITOR
        oidc.OidcProvider.ClientData = googleClientDataObject.clientData;
        oidc.RedirectURI = "com.ARGeometry.AR3DGeometry:/";
#else
        oidc.OidcProvider.ClientData = googleClientDataObjectEditorOnly.clientData;
        oidc.RedirectURI = "https://google.com";
        oidc.ServerListener.ListeningUri = "http://127.0.0.1:52229/";

#endif
        ServiceManager.RegisterService(oidc);
    }

    protected override void UnRegisterServices(){
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
