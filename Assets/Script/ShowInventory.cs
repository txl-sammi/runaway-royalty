using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject myBag;
    private bool BagState = false;

    public GameObject MyBag
    {
        get { return myBag; }
        set { myBag = value; }
    }

    void Update()
    {

        OpenMyBag();
    }

    private void OpenMyBag()
    {
        BagState = MyBag.activeSelf;

        if (Input.GetKeyDown(KeyCode.B))
        {
            BagState = !BagState;
            MyBag.SetActive(BagState);
        }
    }
}
