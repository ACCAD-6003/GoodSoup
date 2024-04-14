using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    public class Shower : Interaction
    {
        [SerializeField] private GameObject _tempHierarchy, _particleSystem;
        [SerializeField] private TextMeshProUGUI temp;
        public enum ShowerState { RAISING_TEMP, SHOWERING, NOT_USING }
        bool _showering = false;
        private ShowerState _showerState = ShowerState.NOT_USING;
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
                        UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.SHOWERING);
                    }
                    break;
                case ShowerState.SHOWERING:
                    if (_showerTemp.Value <= Globals.AMBER_GETS_COLD_TEMP)
                    {
                        _showerState = ShowerState.RAISING_TEMP;
                        UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.COLD);
                    }
                    else {
                        StoryDatastore.Instance.HotShowerDuration.Value += Time.deltaTime;
                    }
                    break;
            }
            if (StoryDatastore.Instance.HotShowerDuration.Value >= Globals.SECONDS_AMBER_NEEDS_TO_SHOWER_IN_WARM_WATER) {
                _showerState = ShowerState.NOT_USING;
                UIManager.Instance.ClearBubble();
                _showered.Value = true;
                StoryDatastore.Instance.DoneShowering.Value = true;
                _tempHierarchy.SetActive(false);
                _showering = false;
                EndAction();
            }
            temp.text = MathF.Floor(_showerTemp.Value) + "F";
        }
        public void TurnOffShower() {
            _particleSystem.SetActive(false);        
        }
        public override void DoAction()
        {
            _showering = true;
            _particleSystem.SetActive(true);
            _showerState = ShowerState.RAISING_TEMP;
            UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.COLD);
            _tempHierarchy.SetActive(true);
            temp.text = _showerTemp.Value.ToString();
            StoryDatastore.Instance.AmberHairOption.Value = AmberVisual.HairOption.MESSY;
            GameObject.FindObjectOfType<GridCharacter>().SetArbitraryRot(Vector3.back);
        }
    }
}
