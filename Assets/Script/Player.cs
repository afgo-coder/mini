using UnityEngine;

public class Player : MonoBehaviour
{
    int hp = 100;
    float moveSpeed = 3f;

    void Start()
    {
        
    }
    public void Heal(int amount)
    {
        hp += amount;
    }

    void Update()
    {
        
    }
}
