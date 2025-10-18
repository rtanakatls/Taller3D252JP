using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Pattern", menuName = "GameSettings/Pattern")]
public class Pattern : ScriptableObject
{
    public List<GameObject> objectPrefabs;
}
