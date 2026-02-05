using JetBrains.Annotations;
using System;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] float life; //vidajugador
    [SerializeField] float maxlife;
    [SerializeField] BarLife barLife;


    private void Start()
    {
        life = maxlife;
        barLife.Inicializarbarradevida(life);
    }


    public void TakeDamage(float damage)
    { 
        life -= damage;
        barLife.ChangeCurrentLife(life);
        if (life <= 0) //matar al jugador
        { 
            Destroy(gameObject);
        }
    
    }

    public void Healh(float heal) //curacion
    {
        if ((life + heal) > maxlife)
        {
            life = maxlife;
        }

        else
        { 
            life += heal;
        }

    }




}





















