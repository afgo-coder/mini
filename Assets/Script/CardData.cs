using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;

    [TextArea]
    public string description; // ⭐ 설명

    public int damage;
    public int heal;
    public int cost;
}
