using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string linkName;
    
    public void OpenExternalLink()
    {
        Application.OpenURL(linkName);
    }
}
