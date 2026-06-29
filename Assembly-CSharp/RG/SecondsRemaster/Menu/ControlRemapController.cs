using System;
using System.Collections.Generic;
using I2.Loc;
using Rewired;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Inputs;
using RG.VirtualInput;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A4 RID: 676
	public class ControlRemapController : MonoBehaviour
	{
		// Token: 0x0600185B RID: 6235 RVA: 0x0006A2CC File Offset: 0x000684CC
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
			ReInput.ControllerConnectedEvent += this.ReInput_ControllerConnectedEvent;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0006A2F0 File Offset: 0x000684F0
		public void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.ReInput_ControllerConnectedEvent;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0006A303 File Offset: 0x00068503
		private void ReInput_ControllerConnectedEvent(ControllerStatusChangedEventArgs obj)
		{
			this._valueText.text = this.GetElementIdentifierName();
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0006A316 File Offset: 0x00068516
		private void OnEnable()
		{
			this._valueText.text = this.GetElementIdentifierName();
			if (this._virtualInputButton != null)
			{
				this._virtualInputButton.SelectionPriority = 0;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0006A344 File Offset: 0x00068544
		private string GetElementIdentifierName()
		{
			ActionElementMap actionElementMapForThisControl = this.GetActionElementMapForThisControl();
			if (actionElementMapForThisControl == null)
			{
				return "";
			}
			return actionElementMapForThisControl.elementIdentifierName;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0006A368 File Offset: 0x00068568
		private ActionElementMap GetActionElementMapForThisControl()
		{
			List<ActionElementMap> list = new List<ActionElementMap>();
			this._player.controllers.maps.GetElementMapsWithAction(this._actionId, false, list);
			for (int i = 0; i < list.Count; i++)
			{
				ActionElementMap actionElementMap = list[i];
				if (actionElementMap.controllerMap.categoryId == (int)this._mapCategory && (actionElementMap.controllerMap.controllerType != ControllerType.Keyboard || ((Application.systemLanguage != SystemLanguage.French || actionElementMap.controllerMap.layoutId == 1) && (Application.systemLanguage == SystemLanguage.French || actionElementMap.controllerMap.layoutId != 1))))
				{
					if (actionElementMap.elementType == ControllerElementType.Axis)
					{
						return actionElementMap;
					}
					if (actionElementMap.elementType == ControllerElementType.Button && actionElementMap.axisContribution == this._axisContribution)
					{
						return actionElementMap;
					}
				}
			}
			return null;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0006A428 File Offset: 0x00068628
		public void ReAssignButton()
		{
			if (this._assigningInputs)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this._allowedControllerTypes.Length; i++)
			{
				if (this._allowedControllerTypes[i] != ControllerType.Joystick || this._player.controllers.joystickCount != 0)
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			this._controlsPanel.SetCloseActionBlocked(true);
			if (this._mappers == null)
			{
				this._mappers = new List<InputMapper>();
			}
			this._valueText.text = this._waitingForButtonPressTerm;
			this._currentElementMap = this.GetActionElementMapForThisControl();
			for (int j = 0; j < this._allowedControllerTypes.Length; j++)
			{
				if (this._allowedControllerTypes[j] != ControllerType.Joystick || this._player.controllers.joystickCount != 0)
				{
					this._mappers.Add(this.StartMapperForControl(this._allowedControllerTypes[j]));
				}
			}
			this._assigningInputs = true;
			Singleton<VirtualInputManager>.Instance.IsGamepadInputBlocked = true;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0006A514 File Offset: 0x00068714
		private InputMapper StartMapperForControl(ControllerType controllerType)
		{
			InputMapper inputMapper = new InputMapper();
			inputMapper.options.checkForConflicts = true;
			inputMapper.options.allowButtons = (this._currentElementMap.elementType == ControllerElementType.Button);
			inputMapper.options.allowAxes = (this._currentElementMap.elementType == ControllerElementType.Axis);
			inputMapper.options.timeout = 0f;
			inputMapper.InputMappedEvent += this.OnControlMapped;
			inputMapper.CanceledEvent += this.OnControlMappingCanceled;
			inputMapper.ConflictFoundEvent += this.MapperOnConflictFoundEvent;
			InputMapper.Context context = new InputMapper.Context();
			context.actionId = this._currentElementMap.actionId;
			if (this._currentElementMap.elementType == ControllerElementType.Axis)
			{
				context.actionRange = this._currentElementMap.axisRange;
			}
			else if (this._currentElementMap.elementType == ControllerElementType.Button)
			{
				context.actionRange = ((this._currentElementMap.axisContribution == Pole.Positive) ? AxisRange.Positive : AxisRange.Negative);
			}
			foreach (ControllerMap controllerMap in this._player.controllers.maps.GetAllMapsInCategory((int)this._mapCategory))
			{
				if (controllerMap.controllerType == controllerType)
				{
					if (controllerType == ControllerType.Keyboard)
					{
						if (controllerMap.layoutId == ((Application.systemLanguage == SystemLanguage.French) ? 1 : 0))
						{
							context.controllerMap = controllerMap;
						}
					}
					else
					{
						context.controllerMap = controllerMap;
					}
				}
			}
			if (ControlRemapController._lastUsedButton != null)
			{
				ControlRemapController._lastUsedButton.SelectionPriority = 0;
			}
			if (this._virtualInputButton != null)
			{
				this._virtualInputButton.SelectionPriority = 1;
				ControlRemapController._lastUsedButton = this._virtualInputButton;
			}
			inputMapper.Start(context);
			return inputMapper;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0006A6CC File Offset: 0x000688CC
		private void MapperOnConflictFoundEvent(InputMapper.ConflictFoundEventData obj)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo in obj.conflicts)
			{
				if (elementAssignmentConflictInfo.controllerMap.categoryId == obj.assignment.controllerMap.categoryId && elementAssignmentConflictInfo.controllerMap.layoutId == obj.assignment.controllerMap.layoutId)
				{
					obj.responseCallback(InputMapper.ConflictResponse.Cancel);
					flag = true;
					break;
				}
			}
			if (this._additionalConfictsActionId.Count > 0)
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo2 in obj.conflicts)
				{
					if (this._additionalConfictsActionId.Contains(elementAssignmentConflictInfo2.actionId))
					{
						obj.responseCallback(InputMapper.ConflictResponse.Cancel);
						flag2 = true;
						break;
					}
				}
			}
			if (!flag && !flag2)
			{
				obj.responseCallback(InputMapper.ConflictResponse.Add);
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0006A7DC File Offset: 0x000689DC
		private void ReAllowAssignment()
		{
			this._assigningInputs = false;
			this._controlsPanel.SetCloseActionBlocked(false);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0006A7F1 File Offset: 0x000689F1
		private void OnControlMappingCanceled(InputMapper.CanceledEventData obj)
		{
			if (this._clearingMappers)
			{
				return;
			}
			this._valueText.text = this.GetElementIdentifierName();
			this.ClearAllMappers();
			base.Invoke("ReAllowAssignment", 0.1f);
			Singleton<VirtualInputManager>.Instance.IsGamepadInputBlocked = false;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0006A830 File Offset: 0x00068A30
		private void OnControlMapped(InputMapper.InputMappedEventData obj)
		{
			this._currentElementMap.controllerMap.DeleteElementMap(this._currentElementMap.id);
			this._currentElementMap = null;
			this._valueText.text = this.GetElementIdentifierName();
			this.ClearAllMappers();
			base.Invoke("ReAllowAssignment", 0.1f);
			Singleton<VirtualInputManager>.Instance.IsGamepadInputBlocked = false;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0006A894 File Offset: 0x00068A94
		private void ClearAllMappers()
		{
			this._clearingMappers = true;
			for (int i = 0; i < this._mappers.Count; i++)
			{
				this._mappers[i].Clear();
			}
			this._mappers.Clear();
			this._clearingMappers = false;
		}

		// Token: 0x04001203 RID: 4611
		[SerializeField]
		[Tooltip("TMP field that displays current control assignment.")]
		private TextMeshProUGUI _valueText;

		// Token: 0x04001204 RID: 4612
		[SerializeField]
		[ActionIdProperty(typeof(RewiredConsts.Action))]
		[Tooltip("Rewired Action that will be used for this controller.")]
		private int _actionId;

		// Token: 0x04001205 RID: 4613
		[SerializeField]
		[Tooltip("Map Category to which this Action belongs to.")]
		private EMapCategory _mapCategory;

		// Token: 0x04001206 RID: 4614
		[SerializeField]
		[Tooltip("Axis Contribution for Axis type Actions. For Button type Actions leave 'Positive'.")]
		private Pole _axisContribution;

		// Token: 0x04001207 RID: 4615
		[SerializeField]
		[Tooltip("What types of controllers can be used for mapping the control?")]
		private ControllerType[] _allowedControllerTypes;

		// Token: 0x04001208 RID: 4616
		[SerializeField]
		private LocalizedString _waitingForButtonPressTerm;

		// Token: 0x04001209 RID: 4617
		[SerializeField]
		private ClosePanelOnCancelPress _controlsPanel;

		// Token: 0x0400120A RID: 4618
		[SerializeField]
		[ActionIdProperty(typeof(RewiredConsts.Action))]
		[Tooltip("Rewired Action that will be used for this controller.")]
		private List<int> _additionalConfictsActionId = new List<int>();

		// Token: 0x0400120B RID: 4619
		[SerializeField]
		private VirtualInputButton _virtualInputButton;

		// Token: 0x0400120C RID: 4620
		private static VirtualInputButton _lastUsedButton;

		// Token: 0x0400120D RID: 4621
		private Player _player;

		// Token: 0x0400120E RID: 4622
		private List<InputMapper> _mappers;

		// Token: 0x0400120F RID: 4623
		private ActionElementMap _currentElementMap;

		// Token: 0x04001210 RID: 4624
		private bool _assigningInputs;

		// Token: 0x04001211 RID: 4625
		private bool _clearingMappers;
	}
}
