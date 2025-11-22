using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Collections.Generic;
using Unity.Services.Multiplayer;

public class MultiplayerServiceManager : MonoBehaviour
{
    private static MultiplayerServiceManager instance;

    public static MultiplayerServiceManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public async Task InitializeServiceAsync()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log($"Inicializó correctamente {AuthenticationService.Instance.PlayerId}");
    }

    public async Task<IHostSession> CreateSessionAsync()
    {
        SessionOptions options = new SessionOptions { MaxPlayers = 4 }.WithDistributedAuthorityNetwork();
        IHostSession session = await MultiplayerService.Instance.CreateSessionAsync(options);
        Debug.Log($"Sesión creada con ID: {session.Id}");
        return session;
    }

    public async Task<IList<ISessionInfo>>  QuerySessionAsync()
    {
        QuerySessionsOptions options = new QuerySessionsOptions { };
        QuerySessionsResults results = await MultiplayerService.Instance.QuerySessionsAsync(options);
        return results.Sessions;
    }

    public async Task JoinSessionByIdAsync(string sessionId)
    {
        await MultiplayerService.Instance.JoinSessionByIdAsync(sessionId);
    }


}
