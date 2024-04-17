using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace GenshintImpact2
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        public string SaveFiles;
        public GameData gameData = new GameData();


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            SaveFiles = Application.persistentDataPath + "/GameData.json"; //La localizaci�n de la carpeta donde est�n las SaveFiles
        }

        [RuntimeInitializeOnLoadMethod]
        public void LoadData()
        {
            if (File.Exists(SaveFiles))
            {
                string content = File.ReadAllText(SaveFiles);
                Debug.Log("JSON Content: " + content);
                GameData loadedData = JsonUtility.FromJson<GameData>(content);

                //_____________________________________________________________
                gameData.playerTransform = loadedData.playerTransform;
                gameData.playerRotation = loadedData.playerRotation;

                gameData.mainCameraTransform = loadedData.mainCameraTransform;
                gameData.mainCameraRotation = loadedData.mainCameraRotation;

                gameData.TPCameraTransform = loadedData.TPCameraTransform;
                gameData.TPCameraRotation = loadedData.TPCameraRotation;

                gameData.FPCameraTransform = loadedData.FPCameraTransform;
                gameData.FPCameraRotation = loadedData.FPCameraRotation;

                gameData.minimapCameraTransform = loadedData.minimapCameraTransform;
                gameData.minimapCameraRotation = loadedData.minimapCameraRotation;

                gameData.items = loadedData.items;
                gameData.images = loadedData.images;

                //_____________________________________________________________

                Debug.Log("player null" + Player.instance.IsUnityNull());

                if (Camera.main != null)
                {
                    Camera.main.transform.position = gameData.mainCameraTransform;
                    Camera.main.transform.rotation = gameData.mainCameraRotation;
                }

                if (Player.instance != null)
                {
                    Debug.Log("load data");
                    Player.instance.transform.position = gameData.playerTransform;
                    Player.instance.transform.rotation = gameData.playerRotation;

                    if (Player.instance.thirdPersonCam != null)
                    {
                        Debug.Log("thirdCameraLoad");
                        Player.instance.thirdPersonCam.transform.position = gameData.TPCameraTransform;
                        Player.instance.thirdPersonCam.transform.rotation = gameData.TPCameraRotation;
                    }

                    if (Player.instance.firstPersonCam != null)
                    {
                        Player.instance.firstPersonCam.transform.position = gameData.FPCameraTransform;
                        Player.instance.firstPersonCam.transform.rotation = gameData.FPCameraRotation;
                    }

                    if (FindObjectOfType<Minimap>() != null)
                    {
                        FindObjectOfType<Minimap>().transform.position = gameData.minimapCameraTransform;
                    }

                    StartCoroutine(LoadInventoryData());
                }
            }
            else
            {
                Debug.Log("El archivo de guardado no existe");
            }
        }

        public void SaveData()
        {
            GameData newData = new GameData();
            {
                if (Camera.main != null)
                {

                    newData.mainCameraTransform = Camera.main.transform.position;
                    newData.mainCameraRotation = Camera.main.transform.rotation;
                }


                if (Player.instance != null)
                {
                    Debug.Log(Player.instance.transform.position);
                    Debug.Log("save data");
                    newData.playerTransform = Player.instance.transform.position;
                    newData.playerRotation = Player.instance.transform.rotation;
                }

                if (Player.instance.thirdPersonCam != null)
                {
                    newData.TPCameraTransform = Player.instance.thirdPersonCam.transform.position;
                    newData.TPCameraRotation = Player.instance.thirdPersonCam.transform.rotation;

                }

                if (Player.instance.firstPersonCam != null)
                {
                    newData.FPCameraTransform = Player.instance.firstPersonCam.transform.position;
                    newData.FPCameraRotation = Player.instance.firstPersonCam.transform.rotation;
                }

                if (FindObjectOfType<Minimap>() != null)
                {
                    newData.minimapCameraTransform = FindObjectOfType<Minimap>().transform.position;
                }

                if (Inventory.instance != null)
                {
                    newData.items = Inventory.instance.items;
                    newData.images = new List<Sprite>();

                    for (int i = 0; i < InventoryUI.instance.slots.Count; i++)
                    {
                        newData.images.Add(InventoryUI.instance.slots[i].itemIcon.sprite);
                        newData.images[i] = InventoryUI.instance.slots[i].itemIcon.sprite;
                    }


                }
            };

            string JsonString = JsonUtility.ToJson(newData, true);

            File.WriteAllText(SaveFiles, JsonString);

            Debug.Log("Saved File");
        }

        IEnumerator LoadInventoryData()
        {
            while (Inventory.instance == null)
            {
                yield return null;
            }

            if (gameData.items.Length != 0)
            {
                Inventory.instance.items = gameData.items;
            }

            if (InventoryUI.instance != null)
            {
                for (int i = 0; i < InventoryUI.instance.slots.Count; i++)
                {
                    if (InventoryUI.instance.slots[i] != null)
                    {
                        Item currentPowerUp = Inventory.instance.items[i];

                        if (currentPowerUp != null)
                        {
                            InventoryUI.instance.slots[i].UpdateSlot(currentPowerUp, currentPowerUp.amount);
                        }
                    }
                }
            }
        }
    }
}
