using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fondo : MonoBehaviour
{

    //public GameObject cursor;
    //public GameObject cursor2;

    public GUITexture backgroundImage;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public GameObject personaje;

    public GameObject tshirt;
    public Text textShirt;
    public GameObject jeans;
    public Text textJeans;

    public Text textoAltura;
    public GameObject panelEspera;
    public GameObject datosUsuario;

    private ManejadorGestos gesture;

    public float smoothFactor = 5f;

    public GameObject MenuDerecha;
    public GameObject MenuIzquierda;

    private float hombros = 0, codo_muneca_der = 0, codo_muneca_izq = 0, hombro_codo_der = 0, hombro_codo_izq = 0, cadera = 0, altura = 0;

    private float distanceToCamera = 10f;
    private Usuario user;
    private ConexionBD conn;
    private int tiempoIzq1, tiempoIzq2, tiempoIzq3; 
    private int tiempoDer1, tiempoDer2, tiempoDer3;
    private int contMenuDer, contMenuIzq;

    private int tiempoClick = 70;

    // Start is called before the first frame update
    void Start()
    {
        if (personaje)
        {
            distanceToCamera = (personaje.transform.position - Camera.main.transform.position).magnitude;
        }
        gesture = ManejadorGestos.GetManejadorGestos();
        conn = (new GameObject("go")).AddComponent<ConexionBD>();
        
        user = Usuario.GetUsuario();
        
        tiempoIzq1 = 0; tiempoIzq2 = 0; tiempoIzq3 = 0;
        tiempoDer1 = 0; tiempoDer2 = 0; tiempoDer3 = 0;
        contMenuDer = 0; contMenuIzq = 0;
    }


    

    // Update is called once per frame
    void Update()
    {
        
        KinectManager manager = ManejadorKinect.GetKinectManager();
        
        if (manager && manager.IsInitialized())
        {
            
            //backgroundImage.renderer.material.mainTexture = manager.GetUsersClrTex();
            if (backgroundImage && (backgroundImage.texture == null))
            {
                backgroundImage.texture = manager.GetUsersClrTex();
            }
            int iJointIndex = (int)TrackedJoint;

            if (manager.IsUserDetected())
            {
                personaje.SetActive(true);

                //Panel
                panelEspera.SetActive(false);
                //Datos usuario
                datosUsuario.SetActive(true);

                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    
                    Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                    if (posJoint != Vector3.zero)
                    {

                        Vector3 posHombroIzq = manager.GetRawSkeletonJointPos(userId, 4);
                        Vector3 posHombroDer = manager.GetRawSkeletonJointPos(userId, 8);
                        Vector3 posMunecaDer = manager.GetRawSkeletonJointPos(userId, 10);
                        Vector3 posMunecaIzq = manager.GetRawSkeletonJointPos(userId, 6);
                        Vector3 posCodoIzq = manager.GetRawSkeletonJointPos(userId, 5);
                        Vector3 posCodoDer = manager.GetRawSkeletonJointPos(userId, 9);
                        Vector3 posHipDer = manager.GetRawSkeletonJointPos(userId, 16);
                        Vector3 posHipIzq = manager.GetRawSkeletonJointPos(userId, 12);
                        Vector3 posHombroCentro = manager.GetRawSkeletonJointPos(userId, 2);
                        Vector3 posHead = manager.GetRawSkeletonJointPos(userId, 3);
                        Vector3 posPieDer = manager.GetRawSkeletonJointPos(userId, 19);
                        Vector3 posPieIzq = manager.GetRawSkeletonJointPos(userId, 15);
                       

                        if (posMunecaDer.y > posHombroDer.y && posMunecaDer != Vector3.zero)
                        {
                            contMenuDer++;
                            if (contMenuDer > tiempoClick)
                            {
                                MenuDerecha.SetActive(!MenuDerecha.activeSelf);
                                contMenuDer = 0;
                            }
                        }
                        //if (posMunecaDer.y < posHipDer.y || posMunecaDer == Vector3.zero){MenuDerecha.SetActive(!MenuDerecha.activeSelf); }
                        if (posMunecaIzq.y > posHombroIzq.y && posMunecaIzq != Vector3.zero)
                        {
                            contMenuIzq++;
                            if (contMenuIzq > tiempoClick)
                            {
                                MenuIzquierda.SetActive(!MenuIzquierda.activeSelf);
                                contMenuIzq = 0;
                            }
                        }
                        //if (posMunecaIzq.y < posHipIzq.y || posMunecaIzq == Vector3.zero){ MenuIzquierda.SetActive(false); }

                        float espacioDer = posHombroDer.y - posHipDer.y;
                        float espacioIzq = posHombroIzq.y - posHipIzq.y;

                        print(manager.getProfundidadArticulacion(posMunecaIzq));
                        print(manager.getProfundidadArticulacion(posMunecaDer));


                        if (MenuDerecha.activeSelf)
                        {
                            
                            if (posMunecaDer.y > (posHipDer.y + (espacioDer / 3) * 2) && posMunecaDer.y < posHombroDer.y)
                            {
                                tiempoDer2 = 0; tiempoDer3 = 0;
                                print("Menu der 1 " + tiempoDer1);
                                tiempoDer1++;
                                if(tiempoDer1 > tiempoClick)
                                {
                                    gesture.ChangeRight();
                                    tiempoDer1 = 0;
                                }
                            }
                            else if (posMunecaDer.y <= (posHipDer.y + (espacioDer / 3)))
                            {
                                tiempoDer1 = 0; tiempoDer2 = 0;
                                print("Menu der 3 " + tiempoDer3);
                                tiempoDer3++;
                                if (tiempoDer3 > tiempoClick)
                                {
                                    tshirt.SetActive(!tshirt.activeSelf);
                                    textShirt.text = tshirt.activeSelf ? "Quitar Playera" : "Poner Playera";
                                    tiempoDer3 = 0;
                                }
                                
                            }
                            else if (posMunecaDer.y <= (posHipDer.y + (espacioDer / 3) * 2))
                            {
                                tiempoDer1 = 0; tiempoDer3 = 0;
                                print("Menu der 2 " + tiempoDer2);
                                tiempoDer2++;
                                //MenuIzquierda.SetActive(false);
                                if (tiempoDer2 > tiempoClick)
                                {
                                    gesture.SetVoto(1);
                                    gesture.ChangeVote();
                                    tiempoDer2 = 0;
                                }
                            }
                        }
                       

                        if (MenuIzquierda.activeSelf)
                        {
                            //Parte inferior para quitar prenda
                            if(posMunecaIzq.y > (posHipIzq.y + (espacioIzq / 3) * 2) && posMunecaIzq.y < posHombroIzq.y)
                            {
                                tiempoIzq2 = 0; tiempoIzq3 = 0;
                                print("Menu 1 " + tiempoIzq1);
                                tiempoIzq1++;
                                if(tiempoIzq1 > tiempoClick)
                                {
                                    gesture.ChangeLeft();
                                    tiempoIzq1 = 0;
                                }
                            } 
                            else if (posMunecaIzq.y <= (posHipIzq.y + (espacioIzq / 3) ))
                            {
                                tiempoIzq1 = 0; tiempoIzq2 = 0;
                                print("Menu 3 " + tiempoIzq3);
                                tiempoIzq3++;
                                if(tiempoIzq3 > tiempoClick)
                                {
                                    jeans.SetActive(!jeans.activeSelf);
                                    textJeans.text = jeans.activeSelf ? "Quitar Jeans" : "Poner Jeans";
                                    tiempoIzq3 = 0;
                                }
                                
                            }
                            else if(posMunecaIzq.y <= (posHipIzq.y + (espacioIzq / 3) * 2))
                            {
                                tiempoIzq1 = 0; tiempoIzq3 = 0;
                                print("Menu 2 " + tiempoIzq2);
                                tiempoIzq2++;
                                if (tiempoIzq2 > tiempoClick)
                                {
                                    gesture.SetVoto(0);
                                    gesture.ChangeVote();
                                    tiempoIzq2 = 0;
                                }
                                
                            }
                        }

                        if (hombros == 0f)
                        {

                            if (posHombroDer != Vector3.zero && posHombroIzq != Vector3.zero)
                            {
                                hombros = Mathf.Sqrt(Mathf.Pow(posHombroIzq.x - posHombroDer.x, 2) + Mathf.Pow(posHombroDer.z - posHombroIzq.z, 2) + Mathf.Pow(posHombroDer.y - posHombroIzq.y, 2));
                                

                                if (posCodoDer != Vector3.zero)
                                {
                                    hombro_codo_der = Mathf.Sqrt(Mathf.Pow(posCodoDer.x - posHombroDer.x, 2) + Mathf.Pow(posHombroDer.y - posCodoDer.y, 2) + Mathf.Pow(posHombroDer.z - posCodoDer.z, 2));   
                                }

                                if (posCodoIzq != Vector3.zero)
                                {
                                    hombro_codo_izq = Mathf.Sqrt(Mathf.Pow(posCodoIzq.x - posHombroIzq.x, 2) + Mathf.Pow(posHombroIzq.y - posCodoIzq.y, 2) + Mathf.Pow(posHombroIzq.z - posCodoIzq.z, 2));
                                }


                                if (posMunecaIzq != Vector3.zero)
                                {
                                    codo_muneca_izq = Mathf.Sqrt(Mathf.Pow(posMunecaIzq.x - posCodoIzq.x, 2) + Mathf.Pow(posCodoIzq.y - posMunecaIzq.y, 2) + Mathf.Pow(posCodoIzq.z - posMunecaIzq.z, 2));
                                }
                                if (posMunecaDer != Vector3.zero)
                                {
                                    codo_muneca_der = Mathf.Sqrt(Mathf.Pow(posMunecaDer.x - posCodoDer.x, 2) + Mathf.Pow(posCodoDer.y - posMunecaDer.y, 2) + Mathf.Pow(posCodoDer.z - posMunecaDer.z, 2));
                                }


                                if (posHipIzq != Vector3.zero && posHipDer != Vector3.zero)
                                {
                                    cadera = Mathf.Sqrt(Mathf.Pow(posHipIzq.x - posHipDer.x, 2) + Mathf.Pow(posHipIzq.y - posHipDer.y, 2) + Mathf.Pow(posHipIzq.z - posHipDer.z, 2));
                                }
                                

                                if(posPieDer != Vector3.zero && posPieIzq != Vector3.zero)
                                {
                                    altura = Mathf.Sqrt(Mathf.Pow(posPieIzq.x - posHead.x, 2) + Mathf.Pow(posPieIzq.y - posHead.y, 2) + Mathf.Pow(posPieIzq.z - posHead.z, 2)) + (posHombroCentro - posHead).magnitude;
                                }


                                user.SetAll(altura, cadera, hombros, codo_muneca_der, codo_muneca_izq, hombro_codo_der, hombro_codo_izq);

                                string json = JsonUtility.ToJson(user);
                                conn.Agregar("usuarios", json);

                                conn.Consulta("usuarios");

                                textoAltura.text = "Tu altura es " + altura;

                            }
                            else
                            {
                                hombros = 0.01f;
                                print("No se encontraron datos suficientes");
                            }

                        }
                        // 3d position to depth
                        Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

                        // depth pos to color pos
                        Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                        float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                        float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                        if (personaje)
                        {
                            //poner z dinamico y no estable
                            Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                            
                            personaje.transform.position = Vector3.Lerp(personaje.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
                            
                        }
                    }
                }

            }
            else
            {
                personaje.SetActive(false);
                //Panel
                panelEspera.SetActive(true);
                //Datos usuario
                datosUsuario.SetActive(false);
                hombros = 0;
            }
        }
    }
}
