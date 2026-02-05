using UnityEngine;
using UnityEngine.UI;

public class BarLife : MonoBehaviour
{


    Slider sliderLife;

    private void Start()
    {
        sliderLife = GetComponent<Slider>();
    }

    public void ChangeLifeMax(float maxlife) //vidamaxima
    { 
        
        sliderLife.value = maxlife;
    }

    public void ChangeCurrentLife(float quantityLife) 
    {

        sliderLife.value = quantityLife;
    }


    public void Inicializarbarradevida(float quantityLife) 
    {
        ChangeLifeMax(quantityLife);
        ChangeCurrentLife(quantityLife);
        
    }


}
