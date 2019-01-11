using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float x;
    public float z;
    public float rotation_angle;

    public int isDead;


    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator> ();        
    }
    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, 0,z), step); // 이동
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0,rotation_angle, 0), Time.deltaTime*10); // 회전
        Animating(x, z);
        if(isDead == 1)
        {
            anim.SetTrigger("Die");
            Destroy(gameObject,2);
        }
    }

    void Animating(float x, float z)
    {
        bool walking = x != 0f || z != 0f; //walking? trigger 받기
        anim.SetBool("IsWalking", walking);
    }
}