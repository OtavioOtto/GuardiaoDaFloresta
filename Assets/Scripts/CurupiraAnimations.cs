using UnityEngine;

public class CurupiraAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private SpearShooter shooter;
    [SerializeField] private Animator anim;

    void Update()
    {
        if (movement.isMoving)
            anim.SetBool("andando", true);

        if (!movement.isMoving)
            anim.SetBool("andando", false);

        if (Input.GetMouseButton(0) && shooter.playerHasSpear && Time.timeScale != 0)
            anim.SetBool("lancou", true);

        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    public void ResetAnim() 
    {
        anim.SetBool("lancou", false);
    }

    public void ShootingMethod()
    {
        shooter.ShootingMethod();
    }
}
