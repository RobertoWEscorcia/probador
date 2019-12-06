using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class ConexionBD : MonoBehaviour
{
    private const string link = "http://172.16.25.54/probador/";
    private Usuario user = Usuario.GetUsuario();
    public ConexionBD() { }

    public void Agregar(string url,string data)
    {
        StartCoroutine(PostRequest(link + url, data));
    }
    public void Consulta(string url)
    {
        StartCoroutine(GetRequest(link + url));  
    }

    IEnumerator PostRequest(string url, string data)
    {
        //Se incluyen los datos a enviar
        WWWForm form = new WWWForm();
        form.AddField("data", data);
        //Se establece el método
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            //Solicitud de la página
            yield return www.SendWebRequest();

            
        }
    }

    IEnumerator GetRequest(string url)
    {
        //Se establece el método
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Solicitud de la página
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                print("{\"data\": \"error\"}");
            }
            else
            {
                string datos = webRequest.downloadHandler.text;
                ClassUsuario users = JsonUtility.FromJson<ClassUsuario>("{\"listaUsuario\": " + datos + "}");
                user.id_usuarios = users.listaUsuario[0].id_usuarios;  
            }

        }
    }



}
