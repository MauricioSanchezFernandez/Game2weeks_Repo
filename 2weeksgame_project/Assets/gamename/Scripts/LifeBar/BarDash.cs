using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BarDash : MonoBehaviour
{
    [SerializeField] Slider bar;
    [SerializeField] float originalbarValue;
    [SerializeField] float cooldown;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            StartCoroutine(BAR());

        }

        


   
    
    
    
    }

    IEnumerator BAR()
    {

        do
        {
            bar.value = bar.value - 0.1f;
        }
        while (bar.value == 0.1);
        yield return new WaitForSeconds(cooldown);
        bar.value = originalbarValue;


    }
}
