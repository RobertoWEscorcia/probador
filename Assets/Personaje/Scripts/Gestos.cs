using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestos : MonoBehaviour
{
    public List<Material> materiales;
    private ManejadorGestos gestureListener;

    public List<GameObject> prenda;
    private Usuario user;

    private int cantidadMateriales = 0;
    private int valorMaterial = 1;
    private ConexionBD conn;

    // Start is called before the first frame update
    void Start()
    {
        prenda[1].GetComponent<Renderer>().material = materiales[1];
        gestureListener = ManejadorGestos.GetManejadorGestos();


        cantidadMateriales = materiales.Count;

        conn = (new GameObject("gos")).AddComponent<ConexionBD>();
        user = Usuario.GetUsuario();
        
    }

    
    void Update()
    {
        

        if(gestureListener != null)
		{
            if (gestureListener.IsLeft())
            {
                MaterialAnterior();
            }
            else if (gestureListener.IsRight())
            {
                MaterialSiguiente();
            }
            else if (gestureListener.IsVote())
            {
                Votar(gestureListener.GetVoto());
            }


        }
    }

    public void Votar(int voto)
    {   //Concatenar el usuario / voto / id_prenda
        conn.Agregar("votos", "{\"id_usuarios\" : " + user.idUsuarios +", \"voto\" :" + voto + ", \"id_prendas\" : " + "2" + "}");
        gestureListener.ChangeVote();
    }
    public void MaterialSiguiente()
    {
        valorMaterial++;
        valorMaterial = valorMaterial % cantidadMateriales;
        prenda[1].GetComponent<Renderer>().material = materiales[valorMaterial];
        gestureListener.ChangeRight();
    }

    public void MaterialAnterior()
    {
        valorMaterial--;
        valorMaterial= valorMaterial % cantidadMateriales;
        if(valorMaterial < 0)
        {
            valorMaterial *= -1;
        }
        prenda[1].GetComponent<Renderer>().material = materiales[valorMaterial];
        gestureListener.ChangeLeft();
    }
}
