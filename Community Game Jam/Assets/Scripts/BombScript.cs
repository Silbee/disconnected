using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombScript : MonoBehaviour {

    public float BombRadius = 8f;

    public float ExplodeDuration = 0.5f;
    public Ease ExplodeEase;

    public GameObject BombMesh;
    public MeshRenderer ExplodeCircle;

    List<EntityBehaviour> hitEntities = new List<EntityBehaviour>();

    void Update()
    {
        if (ExplodeCircle.transform.localScale.x == 0)
            return;

        Collider[] entities = Physics.OverlapSphere(transform.position, ExplodeCircle.transform.localScale.x, LayerMask.GetMask("Entities"));
        for (int i = 0; i < entities.Length; i++)
        {
            EntityBehaviour newEntity = entities[i].GetComponent<EntityBehaviour>();
            if (!newEntity || hitEntities.Contains(newEntity)) continue;

            entities[i].GetComponent<EntityBehaviour>().EntityHit();
            hitEntities.Add(newEntity);
        }
    }

    void Explode()
    {
        BombMesh.SetActive(false);

        BombMesh.transform.localScale = Vector3.one;
        ExplodeCircle.transform.localScale = Vector3.zero;
        ExplodeCircle.material.color = Color.white;

        Sequence ExplodeSequence = DOTween.Sequence();

        ExplodeSequence.Append(ExplodeCircle.transform.DOScale(Vector3.one * BombRadius, ExplodeDuration).SetEase(ExplodeEase));
        ExplodeSequence.Join(ExplodeCircle.material.DOFade(0f, ExplodeDuration).SetEase(ExplodeEase));

        ExplodeSequence.AppendCallback(() => {
            gameObject.SetActive(false);
        });
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Explode();
        }
    }
}
