using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    // Start is called before the first frame update

    // private Transform _currentHighlight;


    // private void Update(){
    //     if(_currentHighlight != null){
    //         _currentHighlight.GetComponent<Renderer>().material = defaultMaterial;
    //     }
    // }

    public void HighlightObject()
    {
        this.transform.GetChild(0).GetComponent<Renderer>().material = highlightMaterial;
    }

    public void UnHighlight()
    {
        this.transform.GetChild(0).GetComponent<Renderer>().material = defaultMaterial;
    }
}
