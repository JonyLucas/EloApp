using UnityEngine;
using UnityEngine.UI;

public class FormLine : MonoBehaviour
{
    [SerializeField]
    private Toggle yesToggle;

    [SerializeField]
    private Toggle noToggle;

    public Toggle YesToggle => yesToggle;
    public Toggle NoToggle => noToggle;
}
