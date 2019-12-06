using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorGestos 
{
    private static ManejadorGestos manejadorGestos = null;
    private bool right = false, left =  false, voto = false;
    private int valorVoto;

    public static ManejadorGestos GetManejadorGestos()
    {
        if(manejadorGestos == null)
        {
            manejadorGestos = new ManejadorGestos();
        }
        return manejadorGestos;
    }

    public bool IsRight()
    {
        return right;
    }
    public bool IsVote()
    {
        return voto;
    }

    public bool IsLeft()
    {
        return left;
    }

    public void ChangeRight()
    {
        right = !right;
    }

    public void ChangeLeft()
    {
        left = !left;
    }
    public void ChangeVote()
    {
        voto = !voto;
    }

    public int GetVoto()
    {
        return valorVoto;
    }
    public void SetVoto(int _voto)
    {
        valorVoto = _voto;
    }
    private ManejadorGestos()
    {

    }
}
