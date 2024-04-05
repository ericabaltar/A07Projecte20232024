using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public new Camera camera;
    public Transform spawner;
    public GameObject ArrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
        CheckFiring();
    }

    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();

        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipY = angle >= 90 && angle <= 270;
    }

    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

            return angle;
    }

    private void CheckFiring()
    {
        if (Input.GetMouseButtonDown(0)&& !GetComponentInParent<CombatePJ>().melee)
        {
            GameObject Arrow = Instantiate(ArrowPrefab);
            Arrow.transform.position = spawner.position;
            Arrow.transform.rotation = spawner.rotation;
            Destroy(Arrow, 50f);
        }
    }
}
