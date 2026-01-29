using UnityEngine;

public class Experiment : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject ObjectLi;
    public GameObject ObjectNa;
    public GameObject ObjectK;
    public float waterTemperature = 20f;

    public void StartLi()
    {
        ResetAndActivate(ObjectLi);
    }

    public void StartNa()
    {
        ResetAndActivate(ObjectNa);
    }

    public void StartK()
    {
        ResetAndActivate(ObjectK);
    }

    private void ResetAndActivate(GameObject metalObj)
    {
        ObjectLi.SetActive(false);
        ObjectNa.SetActive(false);
        ObjectK.SetActive(false);

        metalObj.transform.position = spawnPoint.position;
        metalObj.SetActive(true);

        waterTemperature = 20f;
    }
}