using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPos;
    Canvas canvas;

    public CardData data; // 카드 정보
    public CardUI ui;


 
    void Start()
    {
        if (ui == null)
            ui = GetComponent<CardUI>();
        canvas = GetComponentInParent<Canvas>();
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        transform.SetAsLastSibling(); // 맨 위로
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        transform.position +=
            (Vector3)eventData.delta / canvas.scaleFactor;
    }

    // 드롭
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = GetTarget();

        if (target != null)
        {
            UseCard(target);
            Destroy(gameObject); // 카드 소비
        }
        else
        {
            transform.position = startPos; // 원위치
        }
    }

    // ==============================

    GameObject GetTarget()
    {
        Vector2 pos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit =
            Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider != null)
            return hit.collider.gameObject;

        return null;
    }

    void UseCard(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            target.GetComponent<Enemy>()?.TakeDamage(data.damage);
            Debug.Log("15 데미지!");
        }

        if (target.CompareTag("Player"))
        {
            target.GetComponent<Player>()?.Heal(data.heal);
            Debug.Log("15 회복");
        }
    }
}