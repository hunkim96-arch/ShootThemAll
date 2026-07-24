using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform firePostion;
    [SerializeField] GameObject bullet; // 프리팹으로 만들어서 넣을 예정

    [SerializeField] float attackDelay;
    [SerializeField] public int level;

    private float nextAttackTime;

    private Vector3 offset;

    private void Start()
    {
        nextAttackTime = Time.time;
        offset = new Vector3(0, 0.2f,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame
            && Time.time>= nextAttackTime /* 공격 딜레이가 지났는가*/)
        {
            // 공격
            Fire(level);
        }
    }

    void Fire(int level = 0)
    {
        // 레벨에 따른 총알 생성
        // 공격 다시 사용 가능한 시간 설정

        switch(level)
        {
            case 0:
            {
                ObjectPoolManager.instance.Get(bullet, firePostion.position, Quaternion.identity);
                break;
            }
            case 1:
            {
                ObjectPoolManager.instance.Get(bullet, firePostion.position+offset, Quaternion.identity);
                ObjectPoolManager.instance.Get(bullet, firePostion.position-offset, Quaternion.identity);
                break;
            }
            case 2:
            {
                ObjectPoolManager.instance.Get(bullet, firePostion.position, Quaternion.identity);
                ObjectPoolManager.instance.Get(bullet, firePostion.position+ offset, Quaternion.identity);
                ObjectPoolManager.instance.Get(bullet, firePostion.position- offset, Quaternion.identity);
                break;
            }
        }

        SoundManager.instance.PlaySFX("Player_Shoot");
        nextAttackTime = Time.time + attackDelay;
    }

    public void WeaponLevelup()
    {
        level++;

        level = Mathf.Clamp(level, 0, 2);
    }
}
