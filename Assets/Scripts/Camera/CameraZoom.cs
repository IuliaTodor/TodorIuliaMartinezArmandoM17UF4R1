using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField][Range(0f, 10f)] private float defaultDistance = 0f;
        [SerializeField][Range(0f, 10f)] private float minimumDistance = 1f;
        [SerializeField][Range(0f, 10f)] private float maximumDistance = 6f;

        [SerializeField][Range(0f, 10f)] private float smoothing = 4f;
        [SerializeField][Range(0f, 10f)] private float sensitivty = 1f;

        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;

        //La distancia de la cámara al jugador
        private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();
            //Queremos que empiece a 0
            currentTargetDistance = defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            //2 es el índice para el eje Z
            float zoomValue = inputProvider.GetAxisValue(2) * sensitivty;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);
            float currentDistance = framingTransposer.m_CameraDistance;

            //Si ya estamos en la distancia del objetivo no hará nada
            if(currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
            framingTransposer.m_CameraDistance = lerpedZoomValue;
        }

    }


}
