using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [System.Serializable]
    public class GameData
    {
        public Vector3 playerTransform;
        public Quaternion playerRotation;

        public Vector3 mainCameraTransform;
        public Quaternion mainCameraRotation;

        public Vector3 TPCameraTransform;
        public Quaternion TPCameraRotation;

        public Vector3 FPCameraTransform;
        public Quaternion FPCameraRotation;

        public Vector3 minimapCameraTransform;
        public Quaternion minimapCameraRotation;

        // public List<Slot> slots;
        public Item[] items;
        public List<Sprite> images;
        public float sliderMusicValue;
        public float sliderSFXValue;
        public bool logroPuntos;
        public bool logroMatar;
    }
}
