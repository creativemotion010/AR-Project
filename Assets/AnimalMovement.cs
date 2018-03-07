using System.Collections;
using UnityEngine;

public enum CurrentState
{
    Stop, Move
}
public class AnimalMovement : MonoBehaviour
{
    public CurrentState animalState;
    public string[] animationNames;
    public LayerMask HittableMask;
    public float MinX, MaxX, MinZ, MaxZ;
    public float maxRadiusDelta = 0.08f;
    public float maxDistanceDelta = 0.001f;
    public Transform[] OtherAnimals;

    SphereCollider sCollider;

    private Animator animator;
    private bool walking = false;
    private Vector3 seekPoint;

    float fieldOfView = 10;

    private Vector3 SeekPoint
    {
        get
        {
            float xPosition = Random.Range(MinX, MaxX);
            float zPosition = Random.Range(MinZ, MaxZ);

            seekPoint = new Vector3(xPosition, transform.position.y, zPosition);

            return (seekPoint);
        }
    }

    public bool ReachSeekPoint
    {
        get
        {
            if (transform.position == seekPoint)
            {
                walking = false;
                return (true);
            }

            return (false);
        }
        set { }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        sCollider = GetComponent<SphereCollider>();

        StartCoroutine(MakeBehaviour());
    }

    private void FixedUpdate()
    {
        if (walking && !ReachSeekPoint)
            Walk();
    }

    private void Walk()
    {
        Vector3 desired_velocity = (seekPoint - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, desired_velocity, maxRadiusDelta, 0));
        transform.position = Vector3.MoveTowards(transform.position, seekPoint, maxDistanceDelta);
        //transform.GetComponent<Rigidbody>().velocity = desired_velocity;

        if (ReachSeekPoint)
        {
            Stop();
        }

      //  if (CheckAnimalNearby())
        //    Stop();
    }

     void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ColliosonEnter");
        if (collision.gameObject.layer == 8)
        {
            //walking = true;
            ////animator.SetTrigger("Move");
            //ReachSeekPoint = false;

            seekPoint = SeekPoint;

            walking = true;
            animator.SetTrigger("Move");
            ReachSeekPoint = false;

            Debug.Log(collision.gameObject.name +" - "+ gameObject.name);
        }
    }

    private bool CheckAnimalNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.15f, HittableMask);
        if (colliders.Length > 0)
        {
            foreach (var item in colliders)
            {
                if (item.gameObject != this.gameObject)
                {
                    print(this.gameObject.tag + " Seeing: " + item.gameObject.tag);
                    if (CalcFieldOfView(item.transform.position))
                    {
                        return (true);
                    }
                }
            }
        }
        return (false);
    }


    private void Stop()
    {
        walking = false;
        ReachSeekPoint = true;
        animator.SetTrigger("Stop");
        /*seekPoint = Vector3.ClampMagnitude(-seekPoint, 0.4f);*/ // transform.position - new Vector3(5,0,0);
    }

    private void ChangeDirection()
    {
        seekPoint = Vector3.ClampMagnitude(-seekPoint, 0.4f);
    }

    private IEnumerator MakeBehaviour()
    {
        while (true)
        {
            int state = Random.Range(0, 2);

            if (!walking)
            {
                if (state == 0)        // if state is walking.
                {
                    seekPoint = SeekPoint;

                    walking = true;
                    animator.SetTrigger("Move");
                    ReachSeekPoint = false;

                    yield return new WaitUntil(() => ReachSeekPoint == true);
                }
                else
                {
                    walking = false;
                    animalState = (CurrentState)state;
                    animator.SetTrigger("Stop");
                }

            }

            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }
    }

    bool CalcFieldOfView(Vector3 pos)
    {

        bool result = false;
        float angle = Vector3.Angle(transform.forward, pos);
        this.angle = angle;
        if (angle < fieldOfView || angle > -fieldOfView)
            result = true;
        print(angle);
        return result;
    }
    private float angle;
    private void OnDrawGizmos()
    {
        Gizmos.DrawFrustum(transform.position, angle, -fieldOfView, fieldOfView, 30);
    }
}