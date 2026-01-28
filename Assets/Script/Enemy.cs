using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject idle;
    public GameObject attack;
    public GameObject move;
    public GameObject die;
    public GameObject hit;

    private EnemyState state = EnemyState.Idle;
    private Transform player;
    public float speed = 2f;
    public int hp = 10;
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Hit:
                break;

            case EnemyState.Die:
                break;
        }
    }

    // ================== 상태별 행동 ==================

    void Idle()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < 7f)
        {
            ChangeState(EnemyState.Move);
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < 1.5f)
        {
            ChangeState(EnemyState.Attack);
        }
    }

    void Attack()
    {
        Debug.Log("공격!");

        ChangeState(EnemyState.Idle);
    }

    // ================== 데미지 처리 ==================

    public void TakeDamage(int dmg)
    {
        if (state == EnemyState.Die) return;

        hp -= dmg;

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HitEffect());
        }
    }

    void Die()
    {
        ChangeState(EnemyState.Die);

        Destroy(gameObject, 1.5f);
    }

    // ================== 상태 변경 ==================

    void ChangeState(EnemyState newState)
    {
        state = newState;

        // 전부 끄기
        idle.SetActive(false);
        move.SetActive(false);
        attack.SetActive(false);
        hit.SetActive(false);
        die.SetActive(false);

        // 현재 상태만 켜기
        switch (state)
        {
            case EnemyState.Idle:
                idle.SetActive(true);
                break;

            case EnemyState.Move:
                move.SetActive(true);
                break;

            case EnemyState.Attack:
                attack.SetActive(true);
                break;

            case EnemyState.Hit:
                hit.SetActive(true);
                break;

            case EnemyState.Die:
                die.SetActive(true);
                break;
        }
    }

    // ================== 피격 이펙트 ==================

    System.Collections.IEnumerator HitEffect()
    {
        ChangeState(EnemyState.Hit);

        yield return new WaitForSeconds(0.3f);

        ChangeState(EnemyState.Idle);
    }
}

