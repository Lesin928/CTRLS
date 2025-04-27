using UnityEngine;

public class OptionUIButton : MonoBehaviour
{
    public GameObject OptionUI;

    public void OnClickOptionButton()
    {
        if (OptionUIManager.Instance != null)
        {
            OptionUIManager.Instance.ToggleOptionUI();
        }
    }

    public void OnClickCloseOption()
    {
        if (OptionUIManager.Instance != null)
        {
            OptionUIManager.Instance.CloseOptionUI();
        }
    }
}
