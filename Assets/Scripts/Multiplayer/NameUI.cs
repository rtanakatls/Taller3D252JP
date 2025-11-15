using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button setNameButton;

    private void Awake()
    {
        setNameButton.onClick.AddListener(SetName);
    }

    private void SetName()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in player)
        {
            p.GetComponent<PlayerNetwork>().SetName(nameInputField.text);
        }
        Destroy(transform.parent.gameObject);
    }

}
