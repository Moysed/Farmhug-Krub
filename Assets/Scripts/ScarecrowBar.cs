using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScarecrowBar : MonoBehaviour
{
    
    ScarecrowObject scarecrow;
    [SerializeField] private Image uiFill;
    //[SerializeField] private Text uiText;

    public float Duration;

    private float remainingDuration;

    //private bool Pause;

    private void Start()
    {
        scarecrow = GetComponent<ScarecrowObject>();
        //Duration = scarecrow.MaxTime;
        Being(Duration);
    }

    private void Being(float Second)
    {
        remainingDuration = Second;
        
    }

    private async void Update()
    {
        
            if (remainingDuration >= 0)
            {
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration -= Time.deltaTime;

            }

            if (remainingDuration <= 0)
            {
                    OnEnd();
                    Invoke("sth", 1);
          
            }
        
    }


    private void OnEnd()
    {
        remainingDuration = Duration;
        print("End");
    }

    void sth()
    {
        scarecrow.gameObject.SetActive(false);
    }
}
