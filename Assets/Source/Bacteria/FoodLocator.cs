
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FoodLocator : MonoBehaviour
{
    private const string FOOD_TAG = "Food";
    public Vector3 NearestFood
    {
        get;
        private set;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == FOOD_TAG)
        {
            if(NearestFood == null)
            {
                NearestFood = collision.transform.position;
                return;
            }
            
            var nearestFoodPosition = NearestFood;
            var collisionPosition = collision.transform.position;

            NearestFood = 
                Vector3.Max
                (
                nearestFoodPosition, 
                collisionPosition
                ) == nearestFoodPosition ? NearestFood : collision.transform.position;
        }
    }
}
