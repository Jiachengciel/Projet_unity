using UnityEngine;

[RequireComponent(typeof(HandUtils))]
public class Grab : MonoBehaviour
{

    private HandUtils hand;
    private bool grabbed;
    private bool isGrabbing;

    public bool Pinch
    {
        get
        {
            return hand.Pinch;
        }
    }

    private void Awake()
    {
        hand = GetComponent<HandUtils>();

        grabbed = false;
        isGrabbing = false;
    }

    private void Update()
    {
        if (!grabbed && hand.Pinch)
        {
            grabbed = true;
        }
        else if (grabbed && !hand.Pinch)
        {
            grabbed = false;
            isGrabbing = false;
        }

    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (grabbed && ! isGrabbing)
        {
            collision.gameObject.transform.parent = transform;
            isGrabbing = true;

            Rigidbody rigid_body = collision.gameObject.GetComponent<Rigidbody>();

            if (rigid_body != null)
            {
                rigid_body.isKinematic = true;
            }
        }
    }
}
