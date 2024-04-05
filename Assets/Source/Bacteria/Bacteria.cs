
using UnityEngine;

[RequireComponent(typeof(AliveThing))]
public class Bacteria : MonoBehaviour
{

    [SerializeField] private int EatBeforeDuplicate;

    [SerializeField] private int Mutations;

    [SerializeField] private float RotationForce;

    [SerializeField] private float HealthFoodIncrease;
    
    [SerializeField] private float Speed;

    [SerializeField] private NetworkConfigurator Configurator;

    private AliveThing AliveThing;
    private int Ate;
    private const string FOOD_TAG = "Food";

    [SerializeField] private FoodLocator Locator;

    private SpriteRenderer Renderer;

    private Network Network;

    public void SetColor(Color color)
    {
        Renderer.color = color;
    }

    private void OnEnable()
    {
        AliveThing = GetComponent<AliveThing>();
        Network = new Network(Configurator);
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == FOOD_TAG)
        {

             AliveThing.Increase(HealthFoodIncrease);

            if(Ate >= EatBeforeDuplicate)
            {
                Duplicate();
                Ate = 0;
            }
            Ate++;
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    { 
        transform.Translate(new Vector3(0,Speed,0));
        Vector3 foodPosition = Locator.NearestFood;
        Vector3 foodDirection = foodPosition - transform.position;

        float[] networkOutput = Network.Process(foodDirection.x, foodDirection.y, foodDirection.z);

        float rotation = networkOutput[0];

        transform.Rotate(new Vector3(0, 0, RotationForce * rotation));
    }

    private void Duplicate()
    {
        var kid = Instantiate(gameObject).GetComponent<Bacteria>();
        kid.Mutate();
    }

    private void Mutate()
    {
        for(int i = 0; i < Mutations; i++)
        {
            Network.Mutate();
        }
        Color color = Renderer.color;

        color[Random.Range(0,3)] += Random.Range(-0.2f, 0.2f);
        Renderer.color = color;
    }

}
