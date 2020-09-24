using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public float velocidade;
  public float forcaPulo;
  public Rigidbody2D rig;
  public Animator anim;

  bool estaAgachado;
  bool estaPulando;
  float baixo;

  void Start() {
    
  }

  void Update() {
    Movimentar();
    Agachar();
    Pular();
  }

  void Movimentar() {
    float direcao = Input.GetAxis("Horizontal");
    if(estaAgachado == false) {
      rig.velocity = new Vector2(direcao * velocidade, rig.velocity.y);
      if(direcao > 0f) {
        transform.eulerAngles = new Vector2(0f, 0f);
        if(estaPulando == false) {
          anim.SetInteger("transition", 1);
        }
      }

      if(direcao < 0f) {
        transform.eulerAngles = new Vector2(0f, 180f);
        if(estaPulando == false) {
          anim.SetInteger("transition", 1);
        }
      }

      if(direcao == 0) {
        if(estaPulando == false) {
          anim.SetInteger("transition", 0);
        }
      }
    }
  }

  void Pular() {
    if(estaAgachado == false) {
      if(Input.GetButtonDown("Jump") && estaPulando == false) {
        rig.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
        estaPulando = true;
        anim.SetInteger("transition", 2);
      }
    }
  }

  void Agachar() {
    float duck = Input.GetAxis("Vertical");
    if(duck < 0) {
      anim.SetInteger("transition", 3);
      estaAgachado = true;
    } else {
      estaAgachado = false;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if(collision.gameObject.layer == 8) {
      estaPulando = false;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.CompareTag("enemyBody")) {

    }

    if(collision.CompareTag("enemyHead")) {
      rig.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
      Destroy(collision.transform.parent.gameObject);
    }
  }
}
