//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayers : MonoBehaviour
{
    public int moveSpeed;

    Animator animator;
    Material characterColor;
    Material trailEffect;
    private Rigidbody _rigidbody;

    bool stopPlayerCollision;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        characterColor = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        trailEffect = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial;
        _rigidbody = gameObject.transform.GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && !stopPlayerCollision)
        {
            stopPlayerCollision = true;
            Material thisPlayer = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
            Material otherplayer = other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;

            if(thisPlayer.color == otherplayer.color)
            {
                trailEffect = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial;
                this.gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = trailEffect;
                AddPlayer();
            }
            else
            {
                KillPlayer(other);
            }
        }

        if(other.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            ChangeCharacterColor(0);
        }

        if (other.gameObject.CompareTag("Green"))
        {
            ChangeCharacterColor(1);
        }

        if (other.gameObject.CompareTag("Red"))
        {
            ChangeCharacterColor(2);
        }

        if(other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.TouchedFinishline();
        }
    }


    void ChangeCharacterColor(int num)
    {
        characterColor = GameManager.instance.characterColors[num];
        trailEffect = GameManager.instance.trailEffects[num];
        this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = characterColor;
        this.gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = trailEffect;
    }

    void AddPlayer()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetInteger("Player", 1);
        this.gameObject.tag = "Player";
        GameManager.instance.playersList.Add(this.gameObject);
        this.transform.parent = GameManager.instance.characterParentGroup.transform;
        GameObject addVFX = Instantiate(GameManager.instance.addEffect, this.transform.position, GameManager.instance.addEffect.transform.rotation);
        addVFX.transform.parent = this.transform;
        GameManager.instance.PlayerCounter();
        GameManager.instance.AddPlayer();
    }

    void KillPlayer(Collision other)
    {
        Destroy(other.gameObject);

        int zrot1 = Random.Range(0, 359);
        GameObject dieFx1 = Instantiate(GameManager.instance.dieEffext, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot1));
        dieFx1.GetComponent<SpriteRenderer>().color = other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;

        GameManager.instance.playersList.Remove(other.gameObject);
        GameManager.instance.PlayerCounter();
        GameManager.instance.CharacterParentCheck();


        int zrot = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.instance.dieEffext, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot));
        dieFx.GetComponent<SpriteRenderer>().color = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        GameManager.instance.DiePlayer();
        Destroy(this.gameObject);
    }

    void Die()
    {
        GameManager.instance.playersList.Remove(this.gameObject);
        GameManager.instance.PlayerCounter();
        GameManager.instance.CharacterParentCheck();
        GameManager.instance.DiePlayer();
        int zrot = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.instance.dieEffext, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot));
        dieFx.GetComponent<SpriteRenderer>().color = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        Destroy(this.gameObject);
    }
}
