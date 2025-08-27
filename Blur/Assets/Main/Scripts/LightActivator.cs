using UnityEngine;

public class LightActivator : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private float activationDistance = 15f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool shouldEnable = distance < activationDistance;

        foreach (var light in lights)
        {
            if (light != null && light.enabled != shouldEnable)
                light.enabled = shouldEnable;
        }
    }
}
