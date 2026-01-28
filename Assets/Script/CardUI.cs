using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI effectText;

    public CardData data;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (data == null) return;

        nameText.text = data.cardName;
        effectText.text = data.description;
    }
}