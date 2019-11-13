using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    public GUITexture backgroundImage;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public GameObject OverlayObject;
    public float smoothFactor = 5f;

    private float medida = 0f;


    private float distanceToCamera = 10f;


    // Start is called before the first frame update
    void Start()
    {
        if (OverlayObject)
        {
            distanceToCamera = (OverlayObject.transform.position - Camera.main.transform.position).magnitude;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        KinectManager manager = KinectManager.Instance;

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
                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                    if (posJoint != Vector3.zero)
                    {
                        if (medida == 0f)
                        {
                            Vector3 posHombroIzq = manager.GetRawSkeletonJointPos(userId, 4);
                            Vector3 posHombroDer = manager.GetRawSkeletonJointPos(userId, 8);
                            Vector3 posMunecaDer = manager.GetRawSkeletonJointPos(userId, 10);
                            Vector3 posMunecaIzq = manager.GetRawSkeletonJointPos(userId, 6);
                            Vector3 posHipDer = manager.GetRawSkeletonJointPos(userId, 16);
                            Vector3 posHipIzq = manager.GetRawSkeletonJointPos(userId, 12);
                            
                            
                            medida = Mathf.Sqrt(Mathf.Abs(posHombroIzq.x - posHombroDer.x) + Mathf.Abs(posHombroDer.z - posHombroIzq.z));
                            print("Distancia espalda: " + medida);

                            if (posMunecaDer != Vector3.zero)
                            {
                                float medida2 = Mathf.Sqrt(Mathf.Abs(posMunecaDer.x - posHombroDer.x) + Mathf.Abs(posHombroDer.y - posMunecaDer.y));
                                print("Muñeca - Hombro Derecha: " + medida2);
                            }
                            else
                            {
                                print("No se encontraron suficientes datos para medir: Muñeca - Hombro Derecha");
                            }

                            if (posMunecaIzq != Vector3.zero)
                            {
                                float medida2 = Mathf.Sqrt(Mathf.Abs(posMunecaIzq.x - posHombroIzq.x) + Mathf.Abs(posHombroIzq.y - posMunecaIzq.y));
                                print("Muñeca - Hombro Izquierda: " + medida2);
                            }
                            else
                            {
                                print("No se encontraron suficientes datos para medir: Muñeca - Hombro Izquierda");
                            }

                            if (posHipIzq != Vector3.zero || posHipDer != Vector3.zero)
                            {
                                float medida2 = Mathf.Sqrt(Mathf.Abs(posHipIzq.x - posHipDer.x) + Mathf.Abs(posHipIzq.z - posHipDer.z));
                                print("Cintura: " + medida2);
                            }
                            else
                            {
                                print("No se encontraron suficientes datos para medir: cintura");
                            }

                        }
                        // 3d position to depth
                        Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

                        // depth pos to color pos
                        Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                        float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                        float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                       

                        

                        //						Vector3 localPos = new Vector3(scaleX * 10f - 5f, 0f, scaleY * 10f - 5f); // 5f is 1/2 of 10f - size of the plane
                        //						Vector3 vPosOverlay = backgroundImage.transform.TransformPoint(localPos);
                        //Vector3 vPosOverlay = BottomLeft + ((vRight * scaleX) + (vUp * scaleY));


                        if (OverlayObject)
                        {
                            Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                            OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
                        }
                    }
                }

            }
        }
    }
}
