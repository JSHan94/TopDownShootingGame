using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State{Idle,Chasing,Attacking};
    State currentState;
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    Material skinMaterial;
    Color originalColor;

    float attackDistanceThreshold =.5f;
    float timeBetweenAttacks =1;
    float nextAttackTime;
    float damage = 1;

    float myCollisionRadius;
    float targetCollisionRadius;

    LivingEntity targetEntity;
    bool hasTarget;
protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = State.Chasing;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;


            skinMaterial = GetComponent<Renderer>().material;
            originalColor = skinMaterial.color;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = GetComponent<CapsuleCollider>().radius;
            StartCoroutine(UpdatePath());
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }
    void Update()
    {
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDstToTarge = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarge < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }
    //Enemy가 Player를 공격하는 method
    IEnumerator Attack(){
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position-transform.position).normalized;
        Vector3 attackPosition =target.position - dirToTarget*(myCollisionRadius+targetCollisionRadius); //Attack을 실시하는 position
       
        float attackSpeed =3;
        float percent = 0;
        skinMaterial.color = Color.red; //공격 시 변하는 색깔
        bool hasAppliedDamage = false;
        //공격 후 제자리로 돌아옴
        while (percent <=1){
            if(percent>=.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2) +percent)*4;
            transform.position = Vector3.Lerp(originalPosition,attackPosition,interpolation); 

            yield return null; 
        }
        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    //player로의 Path 업데이트
    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        while(hasTarget){
            if(currentState == State.Chasing){
                Vector3 dirToTarget = (target.position-transform.position).normalized;
                Vector3 targetPosition =target.position - dirToTarget*(myCollisionRadius+targetCollisionRadius+attackDistanceThreshold/2);
                //Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
