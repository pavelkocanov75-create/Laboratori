using System.Collections.Generic;
using ARLaboratory.Scriptable_Object;
using TMPro;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class EquipmentSelectionMenu : AbstractLoadable
    {
        [SerializeField] private SelectionButton _selectionButton;
    
        [Space(10)]
        [SerializeField] private ExperimentManager _experimentManager;
        [SerializeField] private SelectionType _selectionType;
    
        [Space(10)]
        [SerializeField] private EquipmentUI _uiEquipment;
        [SerializeField] private GameObject _scrollViewContent;
    
        private Equipment[] _selectedEquipment;
        private bool _isSelectionMenuCreated = false;

        private enum SelectionType
        {
            Reagents,
            Equipments,
        }

        private void Awake()
        {
            GetSelectedEquipment();
        }

        private void GetSelectedEquipment()
        {
            _selectedEquipment = _selectionType switch
            {
                SelectionType.Reagents => _experimentManager.CurrentExperiment.Reagents,
                SelectionType.Equipments => _experimentManager.CurrentExperiment.Equipment,
                _ => _selectedEquipment
            };
        }

        private void Start()
        {
            DisableEmptySelectionPoint(_selectedEquipment);
            CreateSelectionMenu(_selectedEquipment);
            _isSelectionMenuCreated = true;
        }
    
        public override bool IsDone() => _isSelectionMenuCreated;

        private void CreateSelectionMenu(IEnumerable<Equipment> equipments)
        {
            foreach (var equipment in equipments)
            {
                bool isObjectActive = equipment.gameObject.activeSelf;
                if (!isObjectActive)
                    continue;
            
                if (HasUnassignedFields(equipment))
                {
                    Debug.LogError($"{equipment.gameObject.name} has unassigned fields. Check the object and try again.");
                    continue;
                }
                CreateSelectionMenuButton(equipment);
            }
        }

        private void CreateSelectionMenuButton(Equipment equipment)
        {
            var buttonObject = Instantiate(_selectionButton.gameObject, _scrollViewContent.transform, false);
            var selectionButton = buttonObject.GetComponent<SelectionButton>();

            TextMeshProUGUI label = selectionButton.Label;
            AssignButtonName(label, equipment);
        
            EquipmentDataViewer dataViewer = selectionButton.GetComponent<EquipmentDataViewer>();
            AssignSelectionButtonFields(dataViewer, equipment);
        }

        private void DisableEmptySelectionPoint(IReadOnlyCollection<Equipment> equipment)
        {
            if (equipment.Count != 0) 
                return;
        
            gameObject.SetActive(false);
            _isSelectionMenuCreated = true;
        }
    
        private void AssignSelectionButtonFields(EquipmentDataViewer dataViewer, Equipment equipment)
        {
            dataViewer.UIEquipment = _uiEquipment;
            dataViewer.Equipment = equipment;
        }
    
        private bool HasUnassignedFields(Equipment equipment) => equipment.EquipmentData == null;
    
        private void AssignButtonName(TextMeshProUGUI label, Equipment equipment) => label.text = equipment.EquipmentData.Header;
    }
}
