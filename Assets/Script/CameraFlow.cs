using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    Vector3 Dir;
    public GameObject m_Carriage;

    void Start () {
        Dir = m_Carriage.transform.position - transform.position;
    }

    void LateUpdate () {
        transform.position = m_Carriage.transform.position - Dir;
        
    }
}
