using UnityEngine;

public class showdescription : MonoBehaviour
{
    public GameObject desc;

    public void TurnDesc(bool on){
        desc.SetActive(on);
}
}
