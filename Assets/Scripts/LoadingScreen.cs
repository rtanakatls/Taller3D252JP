using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;

    public static LoadingScreen Instance
    {
        get
        {
            if(instance==null)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/LoadingScreenCanvas"));
                instance = obj.GetComponent<LoadingScreen>();
            }
            return instance;
        }
    }

    public void Show()
    {

    }

    public void End()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
