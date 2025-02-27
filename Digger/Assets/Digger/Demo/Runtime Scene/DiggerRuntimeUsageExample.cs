using Digger.Modules.Core.Sources;
using Digger.Modules.Runtime.Sources;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Digger
{
    public class DiggerRuntimeUsageExample : MonoBehaviour
    {
        [Header("Async parameters")]
        public bool editAsynchronously = true;

        [Header("Modification parameters")]
        public BrushType brush = BrushType.Sphere;
        public ActionType action = ActionType.Dig;
        [Range(0, 7)] public int textureIndex;

        private float opacity = 1f;
        private float size = 0.7f;

        public void SetToolProperties(float newOpacity, float newSize)
        {
            opacity = newOpacity;
            size = newSize;
        }

        [Header("Persistence parameters")]
        public Button digButton;
        public Button persistButton;
        public Button deleteButton;

        public EnergyManager energyManager;

        private DiggerMasterRuntime diggerMasterRuntime;
        public float rayLength = 50f;

        private void Start()
        {
            diggerMasterRuntime = FindObjectOfType<DiggerMasterRuntime>();
            if (!diggerMasterRuntime)
            {
                enabled = false;
                Debug.LogWarning("DiggerRuntimeUsageExample component requires DiggerMasterRuntime component to be set up in the scene.");
                return;
            }

            digButton.onClick.AddListener(OnDigButtonClick);
            persistButton.onClick.AddListener(OnPersistButtonClick);
            deleteButton.onClick.AddListener(OnDeleteButtonClick);
        }

        private void OnDigButtonClick()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, rayLength))
            {
                energyManager.OnDigButtonClick();

                if (energyManager.isEnergyDepleted)
                {
                    Debug.Log("Energy depleted. Cannot dig.");
                    return;
                }

                //AudioManager.Instance.PlaySFX("dig");

                Vector3 modificationPoint = hit.point;

                if (editAsynchronously)
                {
                    diggerMasterRuntime.ModifyAsyncBuffured(modificationPoint, brush, action, textureIndex, opacity, size);
                }
                else
                {
                    diggerMasterRuntime.Modify(modificationPoint, brush, action, textureIndex, opacity, size);
                }

                OnPersistButtonClick();
            }
        }


        private void OnPersistButtonClick()
        {
            diggerMasterRuntime.PersistAll();
        }

        private void OnDeleteButtonClick()
        {
            diggerMasterRuntime.DeleteAllPersistedData();
        }
    }
}


