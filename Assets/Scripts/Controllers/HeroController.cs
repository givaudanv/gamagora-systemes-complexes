using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HeroStates hStates;
    public Text text;
    private bool seeEnemy = false;
    private bool inAttackRange = false;
    private bool superAttackReady = false;

    private GameObject target = null;
    private Vector3 roamTarget;
    private float autoAttackReload;
    private float superAttackReload;

    public float speed;
    public float attackRange;
    public float autoAttackSpeed;
    public float superAttackSpeed;
    public int attack;
    public int superAttack;

    void Start()
    {
        hStates = HeroStates.Roaming;
        roamTarget = transform.position;
    }

    void Update()
    {
        LookAround();
        Reload();
        switch (hStates)
        {
            case HeroStates.Roaming:
                Roam();
                if (seeEnemy) hStates = HeroStates.Chasing;
                break;
            case HeroStates.Chasing:
                Chase();
                if (inAttackRange) hStates = HeroStates.Attacking;
                else if (!seeEnemy) hStates = HeroStates.Roaming;
                break;
            case HeroStates.Attacking:
                Attack();
                if (superAttackReady) hStates = HeroStates.SuperAttacking;
                else if (!inAttackRange) hStates = HeroStates.Chasing;
                break;
            case HeroStates.SuperAttacking:
                SuperAttack();
                if (!superAttackReady) hStates = HeroStates.Attacking;
                break;
        }
        text.text = "State : " + hStates.ToString();
    }

    private void Roam()
    {
        if (transform.position == roamTarget)
        {
            roamTarget = new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20));
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, roamTarget, step);
    }

    private void Chase()
    {
        if (target)
        {
            float step = speed * Time.deltaTime;
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            else
                inAttackRange = true;
        }
    }

    private void Attack()
    {
        if (target && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            if (autoAttackReload >= autoAttackSpeed)
            {
                autoAttackReload = 0;
                target.GetComponent<StatController>().Damage(attack);
            }
        }
        else
            inAttackRange = false;
    }

    private void SuperAttack()
    {
        if (target)
        {
            superAttackReady = false;
            superAttackReload = 0;
            target.GetComponent<StatController>().Damage(superAttack);
        }
    }

    private void LookAround()
    {
        if (!target)
        {
            seeEnemy = false;
            int layer = 1 << LayerMask.NameToLayer("Enemies");
            Collider[] enemies = Physics.OverlapSphere(transform.position, 20f, layer);
            if (enemies.Length > 0)
            {
                target = enemies[0].gameObject;
                seeEnemy = true;
            }
        }
    }

    private void Reload()
    {
        if (autoAttackReload < autoAttackSpeed) autoAttackReload += Time.deltaTime;
        if (superAttackReload < superAttackSpeed) superAttackReload += Time.deltaTime;
        else superAttackReady = true;
    }
}

public enum HeroStates
{
    Roaming,
    Chasing,
    Attacking,
    SuperAttacking,
}
