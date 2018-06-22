using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    CircleCollider2D m_Collider;
    Vector3 m_Size;
    BadCollectableLogic badCollectableLogic;

    CollectableLogic collectableLogic;

    float valor;
    float badValor;

    float xmin;
	float xmax;
	float yMin;
	float yMax;

	// Use this for initialization
	void Start ()
    {
        collectableLogic = FindObjectOfType<CollectableLogic>();
        badCollectableLogic = FindObjectOfType<BadCollectableLogic>();
        //refencia o collider do object.
        m_Collider = GetComponent<CircleCollider2D>();

        Cursor.visible = false;

        PlaySpace();

    }

  

    // Update is called once per frame
    void Update () {
        
        moverPlayer ();

	}
    

    private void PlaySpace()
    {
        //definir o tamanho do play espace.
        float distancia = transform.position.z - Camera.main.transform.position.z;

        Vector3 limiteEsquerdo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distancia));
        Vector3 limiteDireito = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distancia));

        Vector3 limiteBaixo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distancia));
        Vector3 limiteCima = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distancia));
        m_Size = m_Collider.bounds.size;
        xmin = limiteEsquerdo.x + (m_Size.x / 2);
        xmax = limiteDireito.x - (m_Size.x / 2);

        yMin = limiteBaixo.y + (m_Size.y / 2);
        yMax = limiteCima.y - (m_Size.y / 2);
    }

    void moverPlayer ()
	{
        PlaySpace();
		var playerPosition = Input.mousePosition;

		//posicao do jogador em ralacao a camera.
		playerPosition.z = transform.position.z - Camera.main.transform.position.z;
		playerPosition = Camera.main.ScreenToWorldPoint (playerPosition);
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, 2000 * Time.deltaTime);
        //restringe area de jogo.
        //pega o tamanho do collider.
      
        float areadeJogo = Mathf.Clamp (transform.position. x, xmin , xmax);
		float areadeJogo2 = Mathf.Clamp (transform.position. y, yMin , yMax);

        transform.position = new Vector3 ( areadeJogo, areadeJogo2, transform.position.z);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        valor = collectableLogic.points;
        badValor = badCollectableLogic.badPoint;

        if (collision.gameObject.tag == "badColect")
        {
            Debug.Log(badValor);
        }else if(collision.gameObject.tag == "goodColect")
        {
            Debug.Log(valor);
        }
      
        Grow();
        Destroy(collision.gameObject);
        
       
    }

    private void Grow() {

         
        transform.localScale += new Vector3(1, 1, 1);
        
    }
}
