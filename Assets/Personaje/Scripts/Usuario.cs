using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Usuario
{
    private static Usuario user = null;


    //Probar si funciona con private, en caso sí cambiar diagrama de clases
    public float altura;
    public float cadera;
    public float hombroHombro;
    public float codoMunecaDer;
    public float codoMunecaIzq;
    public float hombroCodoIzq;
    public float hombroCodoDer;
    public int idUsuarios;

    
    public float GetAltura() { return altura; }
    public float GetCadera() { return cadera; }
    public float GetHombroHombro() { return hombroHombro; }
    public float GetCodoMunecaDer() { return codoMunecaDer; }
    public float GetCodoMunecaIzq() { return codoMunecaIzq; }
    public float GetHombroCodoIzq() { return hombroCodoIzq; }
    public float GetHombroCodoDer() { return hombroCodoDer; }
    public float GetBrazoDer() { return hombroCodoDer + codoMunecaDer; }
    public float GetBrazoIzq() { return hombroCodoIzq + codoMunecaIzq; }
    public int GetId() { return idUsuarios; }

    public void SetAltura(float _altura) { altura = _altura; }
    public void SetCadera(float _cadera) { cadera = _cadera; }
    public void SetHombroHombro(float _hombro_hombro) { hombroHombro = _hombro_hombro; }
    public void SetCodoMunecaDer(float _codo_muneca_der) { codoMunecaDer = _codo_muneca_der; }
    public void SetCodoMunecaIzq(float _codo_muneca_izq) { codoMunecaIzq = _codo_muneca_izq; }
    public void SetHombroCodoDer(float _hombro_codo_der) { hombroCodoDer = _hombro_codo_der; }
    public void SetHombroCodoIzq(float _hombro_codo_izq) { hombroCodoIzq = _hombro_codo_izq; }
    public void SetId(int _id) { idUsuarios = _id; }

    private Usuario()
    {

    }

    public static Usuario GetUsuario()
    {
        if(user == null)
        {
            user = new Usuario();
        }
        return user;
    }


    public void SetAll(float _altura, float _cadera, float _hombro_hombro, float _codo_muneca_der, float _codo_muneca_izq, float _hombro_codo_der, float _hombro_codo_izq)
    {
        altura = _altura;
        cadera = _cadera;
        hombroHombro = _hombro_hombro;
        hombroCodoDer = _hombro_codo_der;
        hombroCodoIzq = _hombro_codo_izq;
        codoMunecaDer = _codo_muneca_der;
        codoMunecaIzq = _codo_muneca_izq;
    }
    

}

[System.Serializable]
public class ClassUsuario
{
    public Usuario[] listaUsuario;
}



