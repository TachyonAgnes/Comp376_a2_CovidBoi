using UnityEngine;
using System.Collections;

public class TimedExpiration : MonoBehaviour
{
    [SerializeField]
    float mExpirationTime;
    float mTimer;
    public static bool isbullettime = false;

    void Update ()
    {
        isbullettime = Target.isBulletTime;
        mTimer += Time.deltaTime;
        if(mTimer >= mExpirationTime)
        {
            Destroy (gameObject);
        }
    }
}
