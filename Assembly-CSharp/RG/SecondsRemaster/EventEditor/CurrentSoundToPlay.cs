using System;
using RG.Core.Base;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.SecondsRemaster.EventEditor
{
	// Token: 0x02000349 RID: 841
	[CreateAssetMenu(fileName = "Current Sound To Play", menuName = "60 Parsecs!/Current Sound To Play")]
	public class CurrentSoundToPlay : RGScriptableObject, ISaveable
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0007752B File Offset: 0x0007572B
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00077533 File Offset: 0x00075733
		public string EventName
		{
			get
			{
				return this._eventName;
			}
			set
			{
				this._eventName = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0007753C File Offset: 0x0007573C
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x00077544 File Offset: 0x00075744
		public int EventPriority
		{
			get
			{
				return this._eventPriority;
			}
			set
			{
				this._eventPriority = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0007754D File Offset: 0x0007574D
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x00077555 File Offset: 0x00075755
		public float Volume
		{
			get
			{
				return this._volume;
			}
			set
			{
				this._volume = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0007755E File Offset: 0x0007575E
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00077566 File Offset: 0x00075766
		public float Pan
		{
			get
			{
				return this._pan;
			}
			set
			{
				this._pan = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0007756F File Offset: 0x0007576F
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x00077577 File Offset: 0x00075777
		public float Pitch
		{
			get
			{
				return this._pitch;
			}
			set
			{
				this._pitch = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00077580 File Offset: 0x00075780
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00077588 File Offset: 0x00075788
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00077591 File Offset: 0x00075791
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00077599 File Offset: 0x00075799
		public bool OffsetCheck
		{
			get
			{
				return this._offsetCheck;
			}
			set
			{
				this._offsetCheck = value;
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000775A2 File Offset: 0x000757A2
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000775AC File Offset: 0x000757AC
		public string Serialize()
		{
			return JsonUtility.ToJson(new CurrentSoundToPlayWrapper
			{
				EventName = this.EventName,
				EventPriority = this.EventPriority,
				Volume = this.Volume,
				Pan = this.Pan,
				Pitch = this.Pitch,
				Offset = this.Offset,
				OffsetCheck = this.OffsetCheck
			});
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00077628 File Offset: 0x00075828
		public void Deserialize(string jsonData)
		{
			CurrentSoundToPlayWrapper currentSoundToPlayWrapper = JsonUtility.FromJson<CurrentSoundToPlayWrapper>(jsonData);
			this.EventName = currentSoundToPlayWrapper.EventName;
			this.EventPriority = currentSoundToPlayWrapper.EventPriority;
			this.Volume = currentSoundToPlayWrapper.Volume;
			this.Pan = currentSoundToPlayWrapper.Pan;
			this.Pitch = currentSoundToPlayWrapper.Pitch;
			this.Offset = currentSoundToPlayWrapper.Offset;
			this.OffsetCheck = currentSoundToPlayWrapper.OffsetCheck;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00077690 File Offset: 0x00075890
		public void Register()
		{
			if (this._saveEvent)
			{
				this._saveEvent.RegisterListener(this);
				return;
			}
			Debug.LogFormat(this, "{0} has not set Save Event", new object[]
			{
				base.name
			});
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000776C6 File Offset: 0x000758C6
		public void Unregister()
		{
			if (this._saveEvent)
			{
				this._saveEvent.UnregisterListener(this);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000776E1 File Offset: 0x000758E1
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000776EC File Offset: 0x000758EC
		public void ResetData()
		{
			this.EventName = string.Empty;
			this.EventPriority = -1;
			this.Volume = 0.5f;
			this.Pan = 0f;
			this.Pitch = 0f;
			this.Offset = 0;
			this.OffsetCheck = false;
		}

		// Token: 0x0400159B RID: 5531
		[SerializeField]
		private SaveEvent _saveEvent;

		// Token: 0x0400159C RID: 5532
		[SerializeField]
		private string _eventName = string.Empty;

		// Token: 0x0400159D RID: 5533
		[SerializeField]
		private int _eventPriority = -1;

		// Token: 0x0400159E RID: 5534
		[SerializeField]
		[Range(0f, 1f)]
		private float _volume = 0.5f;

		// Token: 0x0400159F RID: 5535
		[SerializeField]
		[Range(-1f, 1f)]
		private float _pan;

		// Token: 0x040015A0 RID: 5536
		[SerializeField]
		[Range(-1f, 1f)]
		private float _pitch;

		// Token: 0x040015A1 RID: 5537
		[SerializeField]
		private int _offset;

		// Token: 0x040015A2 RID: 5538
		[SerializeField]
		private bool _offsetCheck;
	}
}
