using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;
   

    public float borda = 1.0f;

	public GameObject goods;
	public GameObject bads;

	public float tempoParaGoods = 1.0f;
	public float tempoParaBads = 2.0f;

	public bool podeAparecerGoods = true;
	public bool podeAparecerBads = true;

	// Use this for initialization
	void Start ()
    {

        PlaySpace();

        StartCoroutine(aparecerGoods());
        StartCoroutine(aparecerBads());

    }


    // Update is called once per frame
    void Update()
    {



    }

	IEnumerator aparecerGoods ()
	{
		while (podeAparecerGoods == true) {
			criarGoods ();
			yield return new WaitForSeconds (tempoParaGoods);
		}

	}

	IEnumerator aparecerBads ()
	{

		while (podeAparecerBads == true) {
			criarBads ();
			yield return new WaitForSeconds (tempoParaBads);
		}

	}

    private void PlaySpace()
    {

        float distancia = transform.position.z - Camera.main.transform.position.z;

        Vector3 limiteEsquerdo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distancia));
        Vector3 limiteDireito = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distancia));

        Vector3 limiteBaixo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distancia));
        Vector3 limiteCima = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distancia));

        xMin = limiteEsquerdo.x + borda;
        xMax = limiteDireito.x - borda;

        yMin = limiteBaixo.y + borda;
        yMax = limiteCima.y - borda;
       
    }

    void criarGoods ()
	{
		//restringe area de jogo.
		float areadeJogo = Mathf.Clamp (transform.position. x, xMin, xMax);
		float areadeJogo2 = Mathf.Clamp (transform.position. y, yMin, yMax);
		Vector3 posicao = new Vector3(Random.Range (areadeJogo,-areadeJogo),Random.Range (areadeJogo2,-areadeJogo2),0);

		Instantiate (goods, posicao,Quaternion.identity);

	}

	void criarBads ()
	{ 
		float areaDeJogo = Mathf.Clamp (transform.position.x, xMin, xMax); 
		float areaDeJogo2 = Mathf.Clamp (transform.position.y, yMin, yMax);

		Vector3 posicao2 = new Vector3(Random.Range (areaDeJogo, -areaDeJogo),Random.Range (areaDeJogo2,-areaDeJogo2),0);

		Instantiate (bads,posicao2,Quaternion.identity);


	}
}
