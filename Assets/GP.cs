using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

public class ARLocationHandler : MonoBehaviour
{
    private const double TsukubaLat = 36.0835;
    private const double TsukubaLon = 140.0766;

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return new WaitForSeconds(1);
        }

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location service is not enabled");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        float userLat = Input.location.lastData.latitude;
        float userLon = Input.location.lastData.longitude;

        double distance = Haversine(TsukubaLat, TsukubaLon, userLat, userLon);

        if (distance <= 100)
        {
            ActivateAR();
        }

        Input.location.Stop();
    }

    private double Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371.0;
        double dLat = Mathf.Deg2Rad * (lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (lon2 - lon1);
        double a = Mathf.Sin((float)(dLat / 2)) * Mathf.Sin((float)(dLat / 2)) +
                   Mathf.Cos((float)(Mathf.Deg2Rad * lat1)) * Mathf.Cos((float)(Mathf.Deg2Rad * lat2)) *
                   Mathf.Sin((float)(dLon / 2)) * Mathf.Sin((float)(dLon / 2));
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
        return R * c * 1000;
    }

    private void ActivateAR()
    {
        // Activate AR functionality here
    }
}
