using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UsingWeapon : MonoBehaviour
{
    [SerializeField] GameObject mMaskPrefab;
    [SerializeField] GameObject mVaccinePrefab;
    [SerializeField] GameObject mWaterGunPrefab;
    [SerializeField] GameObject mMarkerPrefab;
    [SerializeField] GameObject mPortalProjectilePrefab;
    [SerializeField] private Text targetInPortalText;

    GameObject usingObj;
    public static bool isPortalGun = false;
    public static Queue<string> objName = new Queue<string>();
    public static bool isPortalCharged {
        get => objName.Count != 0;
    }

    void Start ()
    {
        usingObj = mMaskPrefab;
    }

    void Update ()
    {
        targetInPortalText.text = objName.Count.ToString(); ;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.0f));
        if (Input.GetKeyDown(KeyCode.Alpha1))
            usingObj = mMaskPrefab;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            usingObj = mVaccinePrefab;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            usingObj = mWaterGunPrefab;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            usingObj = mMarkerPrefab;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            usingObj = mPortalProjectilePrefab;

        isPortalGun = usingObj == mPortalProjectilePrefab;
        if (Input.GetMouseButtonDown(0))
        {
            if ((isPortalGun && !isPortalCharged) || !isPortalGun)
            {
                GameObject bulletObj = Instantiate(usingObj);
                bulletObj.transform.position = transform.position;
                Bullet bullet = bulletObj.GetComponent<Bullet>();

                // TODO: Set the direction of the bullet
                //       Use the SetDirection() function from the Bullet class
                Vector2 direction = new Vector2(mousePosWorld.x - transform.position.x, mousePosWorld.y - transform.position.y);
                bullet.SetDirection(direction.normalized);
                float Angle = Mathf.Atan2(direction.normalized.x, direction.normalized.y) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, -Angle);
            }
        }

        //UpdateAnimator();
    }


}
