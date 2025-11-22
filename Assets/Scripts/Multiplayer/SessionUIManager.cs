using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

public class SessionUIManager : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private GameObject sessionCanvas;
    [SerializeField] private GameObject sessionButtonPrefab;
    [SerializeField] private GameObject sessionContainer;

    private void Awake()
    {
        hostButton.onClick.AddListener(OnHostButtonClicked);
        refreshButton.onClick.AddListener(OnRefreshButtonClicked);
    }

    private async void Start()
    {
        await MultiplayerServiceManager.Instance.InitializeServiceAsync();
    }

    private async void OnHostButtonClicked()
    {
        try
        {
            LoadingScreen.Instance.Show();
            await MultiplayerServiceManager.Instance.CreateSessionAsync();
            Destroy(sessionCanvas);
            LoadingScreen.Instance.End();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            LoadingScreen.Instance.End();
        }
    }

    private async void OnRefreshButtonClicked()
    {
        try
        {
            LoadingScreen.Instance.Show();
            foreach (Transform t in sessionContainer.GetComponentInChildren<Transform>())
            {
                if (t != sessionContainer.transform)
                {
                    Destroy(t.gameObject);
                }
            }

            IList<ISessionInfo> sessions = await MultiplayerServiceManager.Instance.QuerySessionAsync();
            if (sessions.Count > 0)
            {
                foreach (ISessionInfo session in sessions)
                {
                    GameObject button = Instantiate(sessionButtonPrefab, sessionContainer.transform);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"Join {session.Id} {session.AvailableSlots}/{session.MaxPlayers}";
                    button.GetComponent<Button>().onClick.AddListener(() => OnJoinButtonClicked(session.Id));

                }
            }
            else
            {
                Debug.Log("No hay sesiones");
            }
            LoadingScreen.Instance.End();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            LoadingScreen.Instance.End();
        }

    }

    private async void OnJoinButtonClicked(string sessionId)
    {
        try
        {
            LoadingScreen.Instance.Show();
            await MultiplayerServiceManager.Instance.JoinSessionByIdAsync(sessionId);
            Destroy(sessionCanvas);
            LoadingScreen.Instance.End();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            LoadingScreen.Instance.End();
        }
    }
}
