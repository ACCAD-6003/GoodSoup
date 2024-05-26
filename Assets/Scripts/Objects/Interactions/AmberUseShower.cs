using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    public class AmberUseShower : Interaction
    {
        [SerializeField] private GameObject _tempHierarchy, _particleSystem;
        public enum ShowerState { RAISING_TEMP, SHOWERING, NOT_USING }
        bool _showering = false;
        public ShowerState _showerState = ShowerState.NOT_USING;
        private StoryData<float> _showerTemp;
        private StoryData<bool> _showered;
        private void Awake()
        {
            _tempHierarchy.SetActive(false);
        }
        public override void LoadData(StoryDatastore data)
        {
            _showerTemp = data.ShowerTemperature;
            _showered = data.DoneShowering;
        }

        public override void SaveData(StoryDatastore data)
        {

        }
        private void Update()
        {
            if (!_showering) {
                return;
            }

            switch (_showerState) {
                case ShowerState.RAISING_TEMP:
                    if (_showerTemp.Value < Globals.AMBER_PREFERABLE_SHOWER_TEMP) {
                        _showerTemp.Value += (Time.deltaTime * Globals.TEMP_INCREASE_MODIFIER);
                        _showerTemp.Value = Mathf.Min(_showerTemp.Value, Globals.AMBER_PREFERABLE_SHOWER_TEMP);
                    }
                    if (_showerTemp.Value == Globals.AMBER_PREFERABLE_SHOWER_TEMP) {
                        _showerState = ShowerState.SHOWERING;
                        //UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.SHOWERING);
                    }
                    break;
                case ShowerState.SHOWERING:
                    if (_showerTemp.Value <= Globals.AMBER_GETS_COLD_TEMP)
                    {
                        _showerState = ShowerState.RAISING_TEMP;
                        UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.COLD, 2f);
                    }
                    else {
                        StoryDatastore.Instance.HotShowerDuration.Value += Time.deltaTime;
                    }
                    break;
            }
            if (StoryDatastore.Instance.PlayerTurnedShowerOff.Value) {
                _showerState = ShowerState.NOT_USING;
                UIManager.Instance.ClearBubble();
                _showered.Value = true;
                StoryDatastore.Instance.DoneShowering.Value = true;
                _tempHierarchy.SetActive(false);
                _showering = false;
            }
        }
        public void TurnOffShower() {
            _particleSystem.SetActive(false);        
        }
        public override void DoAction()
        {
            _showering = true;
            _particleSystem.SetActive(true);
            _showerState = ShowerState.RAISING_TEMP;
            UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.COLD, 2f);
            _tempHierarchy.SetActive(true);
            StoryDatastore.Instance.AmberHairOption.Value = AmberVisual.HairOption.MESSY;
            GameObject.FindObjectOfType<GridCharacter>().SetArbitraryRot(Vector3.back);
            EndAction();
        }
    }
}
