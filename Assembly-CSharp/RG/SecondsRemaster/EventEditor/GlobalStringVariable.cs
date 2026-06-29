using System;
using RG.Core.SaveSystem;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.EventEditor
{
	// Token: 0x0200034C RID: 844
	[CreateAssetMenu(fileName = "New String Variable", menuName = "60 Parsecs!/Event Editor/Variables/Global/New String Variable")]
	public class GlobalStringVariable : Variable<string>, ISaveable
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00077923 File Offset: 0x00075B23
		public override string TypeConnection
		{
			get
			{
				return this._typeConnection;
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0007792B File Offset: 0x00075B2B
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00077933 File Offset: 0x00075B33
		public override Type GetVariableType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0007793F File Offset: 0x00075B3F
		public string GetInitialValue()
		{
			return this._initialValue;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00077947 File Offset: 0x00075B47
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0007794F File Offset: 0x00075B4F
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.Unregister();
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00077960 File Offset: 0x00075B60
		public string Serialize()
		{
			return JsonUtility.ToJson(new StringWrapper
			{
				Value = this.Value
			});
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00077990 File Offset: 0x00075B90
		public void Deserialize(string jsonData)
		{
			StringWrapper stringWrapper = JsonUtility.FromJson<StringWrapper>(jsonData);
			this.Value = stringWrapper.Value;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000779B0 File Offset: 0x00075BB0
		public void Register()
		{
			if (!this._isStatic)
			{
				if (this._saveEvent)
				{
					this._saveEvent.RegisterListener(this);
					return;
				}
				Debug.Log(string.Format("Global string variable {0} has not set Save Event", base.name));
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000779E9 File Offset: 0x00075BE9
		public void Unregister()
		{
			if (!this._isStatic && this._saveEvent)
			{
				this._saveEvent.UnregisterListener(this);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00077A0C File Offset: 0x00075C0C
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00077A14 File Offset: 0x00075C14
		public void ResetData()
		{
			this.Value = this._initialValue;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00077A22 File Offset: 0x00075C22
		public override string GuidPrefix
		{
			get
			{
				return "GlobalStringVar";
			}
		}

		// Token: 0x040015BB RID: 5563
		private string _typeConnection = "String";

		// Token: 0x040015BC RID: 5564
		private const string GLOBAL_STRING_VAR_PREFIX = "GlobalStringVar";
	}
}
