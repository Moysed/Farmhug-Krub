using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStore : MonoBehaviour
{
    public GameObject m_StorePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Tapped");
                    OnStoreEvent();
                }
            }
        }
    }

    public void OnStoreEvent()
    {
        m_StorePanel.SetActive(true);
    }
}
