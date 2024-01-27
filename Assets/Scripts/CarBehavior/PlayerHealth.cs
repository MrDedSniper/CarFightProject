using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //[SerializeField] private CarControls _carControls;

    public int healthPoints = 100;
    
    
    /*private float damageAmount;

    private void FixedUpdate()
    {
        damageAmount = _carControls._currentSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagingEnvironment"))
        {
            if (healthPoints != null)
            {
                healthPoints =- damageAmount;
            }
        }
    }*/
}
