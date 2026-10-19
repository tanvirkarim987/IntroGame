using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI distanceText;

    private GameObject[] pickups;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Cache once: the PickUp objects exist from the start, we just skip inactive ones later
        pickups = GameObject.FindGameObjectsWithTag("PickUp");

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        // Without a material the line renders pink
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }

    void Update()
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < pickups.Length; i++)
        {
            // Collected pick-ups are deactivated, so ignore them
            if (!pickups[i].activeInHierarchy) continue;

            // Reset to white; the closest one is turned blue below
            pickups[i].GetComponent<Renderer>().material.color = Color.white;

            float d = Vector3.Distance(player.transform.position, pickups[i].transform.position);
            if (d < closestDistance)
            {
                closestDistance = d;
                closest = pickups[i];
            }
        }

        if (closest != null)
        {
            closest.GetComponent<Renderer>().material.color = Color.blue;
            distanceText.text = "Distance: " + closestDistance.ToString("0.00");

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, closest.transform.position);
        }
        else
        {
            // Everything collected
            distanceText.text = "Distance: -";
            lineRenderer.enabled = false;
        }
    }
}