using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(string.Empty);

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Material ownerMaterial;
    [SerializeField] private Material otherMaterial;

    [SerializeField] private TextMeshPro nameText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        nameText.text = playerName.Value.ToString();
        playerName.OnValueChanged += (oldValue, newValue) =>
        {
            nameText.text = newValue.ToString();
        };
    }

    public void SetName(string name)
    {
        if(IsOwner)
        {
            SendNameToServerRpc(name);
        }
    }

    [Rpc(SendTo.Server)]
    private void SendNameToServerRpc(string name)
    {
        playerName.Value = name;
        SendNameToClientRpc(name);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SendNameToClientRpc(string name)
    {
        nameText.text = name;
    }



    private void Start()
    {
        if (IsOwner)
        {
            GetComponent<Renderer>().material = ownerMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = otherMaterial;
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            rb.linearVelocity = new Vector3(h * speed, rb.linearVelocity.y, v * speed);
        }
    }
}