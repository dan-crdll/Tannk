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


    public float mouseSensitivity = 5;
    private float rightLeftRotation = 0.0f;

    ParticleSystem _groundShootParticle;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(Move());
        StartCoroutine(UpdateCoroutine());

        _groundShootParticle = Instantiate(GroundShootParticle);
    }

    private void Update()
    {
        Vector3 newRotation = new Vector3(0, rightLeftRotation, 0);
        rightLeftRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        newRotation = new Vector3(0, rightLeftRotation, 0);
        cannonHead.localRotation = Quaternion.Euler(newRotation);
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

        yield return new WaitForSeconds(0.5f);
        _groundShootParticle.Stop();
        _groundShootParticle.gameObject.SetActive(false);
    }

    public void Stop()
    {
        throw new System.NotImplementedException();
    }
}
