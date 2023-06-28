using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, ICharacter
{
    NavMeshAgent navMeshAgent;
    [SerializeField] Transform cannonHead;
    [SerializeField] AudioSource cannonShootAudioSource;
    [SerializeField] ParticleSystem GroundShootParticle;

    ParticleSystem _groundShootParticle;

    public float reloadTime = 1f;

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X";

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(Move());
        StartCoroutine(UpdateCoroutine());

        _groundShootParticle = Instantiate(GroundShootParticle);
    }


    private void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

        cannonHead.localRotation = xQuat;
    }

    private IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            if (Input.GetMouseButtonDown(1))
                yield return StartCoroutine(Attack());

            yield return 0;
        }
    }

    public IEnumerator Move()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.CompareTag("ground"))
                    {
                        navMeshAgent.ResetPath();
                        navMeshAgent.SetDestination(hit.point);
                        navMeshAgent.isStopped = false;
                    }
                }
            }
            yield return 0;
        }
    }

    public IEnumerator Attack()
    {
        cannonShootAudioSource.Play();

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "ground" || hit.collider.tag == "block")
            {
                _groundShootParticle.transform.position = hit.point;
                _groundShootParticle.gameObject.SetActive(true);
                _groundShootParticle.Play();
            }
        }

        yield return new WaitForSeconds(reloadTime);
        _groundShootParticle.Stop();
        _groundShootParticle.gameObject.SetActive(false);
    }

    public void Stop()
    {
        throw new System.NotImplementedException();
    }
}
