using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private float range;

    private Transform ob;
    [SerializeField] private Transform mirilla;

    private Transform m_camera;
    private bool grabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (ob != null)
        {
            ob.position = Vector3.Lerp(ob.position, transform.forward + transform.position, Time.deltaTime * 5);

            if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)) && !grabbed)
            {
                ob.GetComponent<Collider>().enabled = true;
                if (ob.TryGetComponent<Rigidbody>(out Rigidbody rbob))
                {
                    rbob.useGravity = true;
                }
                ob = null;
                StartCoroutine(GrabColldown());
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, m_camera.forward, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);

            Debug.Log(hit.transform.name);
            if (Vector3.Distance(transform.position, hit.transform.position) > range && hit.transform.tag != "Pickable")
            {
                mirilla.position = new Vector3(100, 100, 100);
                return;
            }

            mirilla.position = hit.point;
            if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)) && ob == null && !grabbed)
            {
                ob = hit.transform;
                ob.GetComponent<Collider>().enabled = false;
                if (ob.TryGetComponent<Rigidbody>(out Rigidbody rbob))
                {
                    rbob.useGravity = false;
                }
                StartCoroutine(GrabColldown());
            }
        }
    }

    private IEnumerator GrabColldown()
    {
        grabbed = true;
        yield return new WaitForSeconds(0.2f);
        grabbed = false;
    }
}