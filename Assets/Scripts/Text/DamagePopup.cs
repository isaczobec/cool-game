using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] private TextMeshPro textMeshPro;

    [Header("Animation Variables")]
    [SerializeField] float initialVelocity;
    [SerializeField] float velocityDamping;
    [SerializeField] float randomSpawnLocationRadius;

    private Vector3 velocity = Vector3.zero;

    public void Setup(HitInfo hitInfo) {
        textMeshPro.text = hitInfo.damage.ToString();

        float randomAngle = Random.Range(0f,6.283185f);
        velocity = new Vector3(Mathf.Sin(randomAngle),Mathf.Cos(randomAngle),0f) * Random.Range(0f,1f);
        transform.position += new Vector3(randomSpawnLocationRadius * Random.Range(0f,1f),randomSpawnLocationRadius * Random.Range(0f,1f),0f); // randomizes the number's spawn location.
        transform.localScale *= hitInfo.damage/hitInfo.baseDamage; //change the scale based on the damage variance
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        transform.position += velocity * Time.deltaTime;
        velocity = Vector3.Lerp(velocity,Vector3.zero, Time.deltaTime * velocityDamping);
    }
}
