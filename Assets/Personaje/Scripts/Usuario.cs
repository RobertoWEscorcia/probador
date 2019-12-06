using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Usuario
{
    private static Usuario user = null;

    public float altura;
    public float cadera;
    public float hombro_hombro;
    public float codo_muneca_der;
    public float codo_muneca_izq;
    public float hombro_codo_izq;
    public float hombro_codo_der;
    public int id_usuarios;

    
    public float GetAltura() { return altura; }
    public float GetCadera() { return cadera; }
    public float GetHombro_hombro() { return hombro_hombro; }
    public float GetCodo_muneca_der() { return codo_muneca_der; }
    public float GetCodo_muneca_izq() { return codo_muneca_izq; }
    public float GetHombro_codo_izq() { return hombro_codo_izq; }
    public float GetHombro_codo_der() { return hombro_codo_der; }
    public float GetBrazo_der() { return hombro_codo_der + codo_muneca_der; }
    public float GetBrazo_izq() { return hombro_codo_izq + codo_muneca_izq; }
    public int GetId() { return id_usuarios; }

    public void SetAltura(float _altura) { altura = _altura; }
    public void SetCadera(float _cadera) { cadera = _cadera; }
    public void SetHombro_hombro(float _hombro_hombro) { hombro_hombro = _hombro_hombro; }
    public void SetCod_muneca_der(float _codo_muneca_der) { codo_muneca_der = _codo_muneca_der; }
    public void SetCodo_muneca_izq(float _codo_muneca_izq) { codo_muneca_izq = _codo_muneca_izq; }
    public void SetHombro_codo_der(float _hombro_codo_der) { hombro_codo_der = _hombro_codo_der; }
    public void SetHombro_codo_izq(float _hombro_codo_izq) { hombro_codo_izq = _hombro_codo_izq; }
    public void SetId(int _id) { id_usuarios = _id; }

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
        hombro_hombro = _hombro_hombro;
        hombro_codo_der = _hombro_codo_der;
        hombro_codo_izq = _hombro_codo_izq;
        codo_muneca_der = _codo_muneca_der;
        codo_muneca_izq = _codo_muneca_izq;
    }
    

}

[System.Serializable]
public class ClassUsuario
{
    public Usuario[] listaUsuario;
}



