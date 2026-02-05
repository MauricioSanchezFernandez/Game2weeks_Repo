using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    [SerializeField] Transform controlAttack; //controlador
    [SerializeField] float radiusAttack; //rango de ataque


    private void Update()
    {

        Attack();




    }



    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Collider2D [] gameObjectTouch = Physics2D.OverlapCircleAll(controlAttack.position, radiusAttack);
            //enemigos que va a matar/dañar

            foreach (Collider2D gameObject in gameObjectTouch)
            { 
                
            
            }


        }
    }
}


