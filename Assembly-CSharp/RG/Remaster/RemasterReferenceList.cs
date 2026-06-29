using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.Remaster
{
	// Token: 0x02000215 RID: 533
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New reference object list", fileName = "New Object list")]
	[Serializable]
	public class RemasterReferenceList : ObjectsReferenceList
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0005CAF2 File Offset: 0x0005ACF2
		public List<TextJournalGroupId> TextJournalGroupId
		{
			get
			{
				return this._textJournalGroupId;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0005CAFA File Offset: 0x0005ACFA
		public List<BaseActionCondition> BaseActionConditions
		{
			get
			{
				return this._baseActionConditions;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0005CB02 File Offset: 0x0005AD02
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0005CB0A File Offset: 0x0005AD0A
		public List<SystemStatusEvent> SystemStatusEvents
		{
			get
			{
				return this._systemStatusEvents;
			}
			set
			{
				this._systemStatusEvents = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0005CB13 File Offset: 0x0005AD13
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0005CB1B File Offset: 0x0005AD1B
		public List<SurvivalEvent> SurvivalEvents
		{
			get
			{
				return this._survivalEvents;
			}
			set
			{
				this._survivalEvents = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0005CB24 File Offset: 0x0005AD24
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x0005CB2C File Offset: 0x0005AD2C
		public List<ReportEvent> ReportEvents
		{
			get
			{
				return this._reportEvents;
			}
			set
			{
				this._reportEvents = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0005CB35 File Offset: 0x0005AD35
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x0005CB3D File Offset: 0x0005AD3D
		public List<SystemEvent> SystemEvents
		{
			get
			{
				return this._systemEvents;
			}
			set
			{
				this._systemEvents = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0005CB46 File Offset: 0x0005AD46
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0005CB4E File Offset: 0x0005AD4E
		public List<ExpeditionEvent> ExpeditionEvents
		{
			get
			{
				return this._expeditionEvents;
			}
			set
			{
				this._expeditionEvents = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0005CB57 File Offset: 0x0005AD57
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0005CB5F File Offset: 0x0005AD5F
		public List<ExpeditionDestination> ExpeditionDestinations
		{
			get
			{
				return this._expeditionDestinations;
			}
			set
			{
				this._expeditionDestinations = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0005CB68 File Offset: 0x0005AD68
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0005CB70 File Offset: 0x0005AD70
		public List<VisualId> VisualIds
		{
			get
			{
				return this._visualIds;
			}
			set
			{
				this._visualIds = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0005CB79 File Offset: 0x0005AD79
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0005CB81 File Offset: 0x0005AD81
		public List<ExpeditionThreat> ExpeditionThreats
		{
			get
			{
				return this._expeditionThreats;
			}
			set
			{
				this._expeditionThreats = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0005CB8A File Offset: 0x0005AD8A
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0005CB92 File Offset: 0x0005AD92
		public List<CharacterStatus> CharacterStatuses
		{
			get
			{
				return this._characterStatuses;
			}
			set
			{
				this._characterStatuses = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0005CB9B File Offset: 0x0005AD9B
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0005CBA3 File Offset: 0x0005ADA3
		public List<Deck> Decks
		{
			get
			{
				return this._decks;
			}
			set
			{
				this._decks = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0005CBAC File Offset: 0x0005ADAC
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		public List<Character> Characters
		{
			get
			{
				return this._characters;
			}
			set
			{
				this._characters = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0005CBBD File Offset: 0x0005ADBD
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0005CBC5 File Offset: 0x0005ADC5
		public List<IItem> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0005CBCE File Offset: 0x0005ADCE
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0005CBD6 File Offset: 0x0005ADD6
		public List<EventTag> Tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				this._tags = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0005CBDF File Offset: 0x0005ADDF
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0005CBE7 File Offset: 0x0005ADE7
		public List<Goal> Goals
		{
			get
			{
				return this._goals;
			}
			set
			{
				this._goals = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0005CBF8 File Offset: 0x0005ADF8
		public List<Resource> Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0005CC01 File Offset: 0x0005AE01
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0005CC09 File Offset: 0x0005AE09
		public List<ShipSystem> ShipSystems
		{
			get
			{
				return this._shipSystems;
			}
			set
			{
				this._shipSystems = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0005CC12 File Offset: 0x0005AE12
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0005CC1A File Offset: 0x0005AE1A
		public List<Mission> Missions
		{
			get
			{
				return this._missions;
			}
			set
			{
				this._missions = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0005CC23 File Offset: 0x0005AE23
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0005CC2B File Offset: 0x0005AE2B
		public List<Challenge> Challenges
		{
			get
			{
				return this._challenges;
			}
			set
			{
				this._challenges = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0005CC34 File Offset: 0x0005AE34
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0005CC3C File Offset: 0x0005AE3C
		public List<CurrentChallengeData> CurrentChallengeData
		{
			get
			{
				return this._currentChallengeData;
			}
			set
			{
				this._currentChallengeData = value;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0005CC48 File Offset: 0x0005AE48
		public override void Build()
		{
			this._objects = new Dictionary<string, Object>();
			for (int i = 0; i < this._visualIds.Count; i++)
			{
				if (this._objects.ContainsKey(this._visualIds[i].Id))
				{
					Debug.LogErrorFormat(this._visualIds[i], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._visualIds[i].Id].name,
						this._visualIds[i].name
					});
				}
				else
				{
					this._objects.Add(this._visualIds[i].Id, this._visualIds[i]);
				}
			}
			for (int j = 0; j < this._expeditionThreats.Count; j++)
			{
				if (this._objects.ContainsKey(this._expeditionThreats[j].ID))
				{
					Debug.LogErrorFormat(this._expeditionThreats[j], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._expeditionThreats[j].ID].name,
						this._expeditionThreats[j].name
					});
				}
				else
				{
					this._objects.Add(this._expeditionThreats[j].ID, this._expeditionThreats[j]);
				}
			}
			for (int k = 0; k < this._characterStatuses.Count; k++)
			{
				if (this._objects.ContainsKey(this._characterStatuses[k].Id))
				{
					Debug.LogErrorFormat(this._characterStatuses[k], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._characterStatuses[k].Id].name,
						this._characterStatuses[k].name
					});
				}
				else
				{
					this._objects.Add(this._characterStatuses[k].Id, this._characterStatuses[k]);
				}
			}
			for (int l = 0; l < this._decks.Count; l++)
			{
				if (this._objects.ContainsKey(this._decks[l].ID))
				{
					Debug.LogErrorFormat(this._decks[l], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._decks[l].ID].name,
						this._decks[l].name
					});
				}
				else
				{
					this._objects.Add(this._decks[l].ID, this._decks[l]);
				}
			}
			for (int m = 0; m < this._characters.Count; m++)
			{
				if (this._objects.ContainsKey(this._characters[m].ID))
				{
					Debug.LogErrorFormat(this._characters[m], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._characters[m].ID].name,
						this._characters[m].name
					});
				}
				else
				{
					this._objects.Add(this._characters[m].ID, this._characters[m]);
				}
			}
			for (int n = 0; n < this._items.Count; n++)
			{
				if (this._objects.ContainsKey(this._items[n].BaseStaticData.ItemId))
				{
					Debug.LogErrorFormat(this._items[n], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._items[n].BaseStaticData.ItemId].name,
						this._items[n].name
					});
				}
				else
				{
					this._objects.Add(this._items[n].BaseStaticData.ItemId, this._items[n]);
				}
			}
			for (int num = 0; num < this._expeditionDestinations.Count; num++)
			{
				if (this._objects.ContainsKey(this._expeditionDestinations[num].ID))
				{
					Debug.LogErrorFormat(this._expeditionDestinations[num], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._expeditionDestinations[num].ID].name,
						this._expeditionDestinations[num].name
					});
				}
				else
				{
					this._objects.Add(this._expeditionDestinations[num].ID, this._expeditionDestinations[num]);
				}
			}
			for (int num2 = 0; num2 < this._tags.Count; num2++)
			{
				if (this._objects.ContainsKey("etag_" + this._tags[num2].name))
				{
					Debug.LogError(string.Format("Duplicate ID of tag when adding to SurvivalReferenceList dictionary. Key: {0}", "etag_" + this._tags[num2].name));
				}
				else
				{
					this._objects.Add("etag_" + this._tags[num2].name, this._tags[num2]);
				}
			}
			for (int num3 = 0; num3 < this._survivalEvents.Count; num3++)
			{
				if (this._objects.ContainsKey(this._survivalEvents[num3].ID))
				{
					Debug.LogErrorFormat(this._survivalEvents[num3], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._survivalEvents[num3].ID].name,
						this._survivalEvents[num3].name
					});
				}
				else
				{
					this._objects.Add(this._survivalEvents[num3].ID, this._survivalEvents[num3]);
				}
			}
			for (int num4 = 0; num4 < this._expeditionEvents.Count; num4++)
			{
				if (this._objects.ContainsKey(this._expeditionEvents[num4].ID))
				{
					Debug.LogErrorFormat(this._expeditionEvents[num4], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._expeditionEvents[num4].ID].name,
						this._expeditionEvents[num4].name
					});
				}
				else
				{
					this._objects.Add(this._expeditionEvents[num4].ID, this._expeditionEvents[num4]);
				}
			}
			for (int num5 = 0; num5 < this._reportEvents.Count; num5++)
			{
				if (this._objects.ContainsKey(this._reportEvents[num5].ID))
				{
					Debug.LogErrorFormat(this._reportEvents[num5], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._reportEvents[num5].ID].name,
						this._reportEvents[num5].name
					});
				}
				else
				{
					this._objects.Add(this._reportEvents[num5].ID, this._reportEvents[num5]);
				}
			}
			for (int num6 = 0; num6 < this._systemEvents.Count; num6++)
			{
				if (this._objects.ContainsKey(this._systemEvents[num6].ID))
				{
					Debug.LogErrorFormat(this._systemEvents[num6], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._systemEvents[num6].ID].name,
						this._systemEvents[num6].name
					});
				}
				else
				{
					this._objects.Add(this._systemEvents[num6].ID, this._systemEvents[num6]);
				}
			}
			for (int num7 = 0; num7 < this._goals.Count; num7++)
			{
				if (this._objects.ContainsKey(this._goals[num7].ID))
				{
					Debug.LogErrorFormat(this._goals[num7], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._goals[num7].ID].name,
						this._goals[num7].name
					});
				}
				else
				{
					this._objects.Add(this._goals[num7].ID, this._goals[num7]);
				}
			}
			for (int num8 = 0; num8 < this._resources.Count; num8++)
			{
				if (this._objects.ContainsKey(this._resources[num8].ID))
				{
					Debug.LogErrorFormat(this._resources[num8], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._resources[num8].ID].name,
						this._resources[num8].name
					});
				}
				else
				{
					this._objects.Add(this._resources[num8].ID, this._resources[num8]);
				}
			}
			for (int num9 = 0; num9 < this._shipSystems.Count; num9++)
			{
				if (this._objects.ContainsKey(this._shipSystems[num9].Id))
				{
					Debug.LogErrorFormat(this._shipSystems[num9], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._shipSystems[num9].Id].name,
						this._shipSystems[num9].name
					});
				}
				else
				{
					this._objects.Add(this._shipSystems[num9].Id, this._shipSystems[num9]);
				}
			}
			for (int num10 = 0; num10 < this._systemStatusEvents.Count; num10++)
			{
				if (this._objects.ContainsKey(this._systemStatusEvents[num10].ID))
				{
					Debug.LogErrorFormat(this._systemStatusEvents[num10], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._systemStatusEvents[num10].ID].name,
						this._systemStatusEvents[num10].name
					});
				}
				else
				{
					this._objects.Add(this._systemStatusEvents[num10].ID, this._systemStatusEvents[num10]);
				}
			}
			for (int num11 = 0; num11 < this._missions.Count; num11++)
			{
				if (this._objects.ContainsKey(this._missions[num11].ID))
				{
					Debug.LogErrorFormat(this._missions[num11], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._missions[num11].ID].name,
						this._missions[num11].name
					});
				}
				else
				{
					this._objects.Add(this._missions[num11].ID, this._missions[num11]);
				}
			}
			for (int num12 = 0; num12 < this._textJournalGroupId.Count; num12++)
			{
				if (this._objects.ContainsKey(this._textJournalGroupId[num12].Guid))
				{
					Debug.LogErrorFormat(this._textJournalGroupId[num12], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._textJournalGroupId[num12].Guid].name,
						this._textJournalGroupId[num12].name
					});
				}
				else
				{
					this._objects.Add(this._textJournalGroupId[num12].Guid, this._textJournalGroupId[num12]);
				}
			}
			for (int num13 = 0; num13 < this._baseActionConditions.Count; num13++)
			{
				if (this._objects.ContainsKey(this._baseActionConditions[num13].Guid))
				{
					Debug.LogErrorFormat(this._baseActionConditions[num13], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._baseActionConditions[num13].Guid].name,
						this._baseActionConditions[num13].name
					});
				}
				else
				{
					this._objects.Add(this._baseActionConditions[num13].Guid, this._baseActionConditions[num13]);
				}
			}
			for (int num14 = 0; num14 < this._challenges.Count; num14++)
			{
				if (this._objects.ContainsKey(this._challenges[num14].Guid))
				{
					Debug.LogErrorFormat(this._challenges[num14], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this._challenges[num14].Guid].name,
						this._challenges[num14].name
					});
				}
				else
				{
					this._objects.Add(this._challenges[num14].Guid, this._challenges[num14]);
				}
			}
			for (int num15 = 0; num15 < this.CurrentChallengeData.Count; num15++)
			{
				if (this._objects.ContainsKey(this.CurrentChallengeData[num15].Guid))
				{
					Debug.LogErrorFormat(this.CurrentChallengeData[num15], "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.", new object[]
					{
						this._objects[this.CurrentChallengeData[num15].Guid].name,
						this.CurrentChallengeData[num15].name
					});
				}
				else
				{
					this._objects.Add(this.CurrentChallengeData[num15].Guid, this.CurrentChallengeData[num15]);
				}
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005DC19 File Offset: 0x0005BE19
		public override void Destroy()
		{
			if (this._objects != null)
			{
				this._objects.Clear();
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0005DC2E File Offset: 0x0005BE2E
		public override Object GetReferenceObjectByGUID(string id)
		{
			if (this._objects.ContainsKey(id))
			{
				return this._objects[id];
			}
			throw new Exception(string.Format("There is no {0} in dictionary", id));
		}

		// Token: 0x04000DD7 RID: 3543
		[SerializeField]
		private List<SurvivalEvent> _survivalEvents;

		// Token: 0x04000DD8 RID: 3544
		[SerializeField]
		private List<ReportEvent> _reportEvents;

		// Token: 0x04000DD9 RID: 3545
		[SerializeField]
		private List<SystemEvent> _systemEvents;

		// Token: 0x04000DDA RID: 3546
		[SerializeField]
		private List<ExpeditionEvent> _expeditionEvents;

		// Token: 0x04000DDB RID: 3547
		[SerializeField]
		private List<ExpeditionDestination> _expeditionDestinations;

		// Token: 0x04000DDC RID: 3548
		[SerializeField]
		private List<VisualId> _visualIds;

		// Token: 0x04000DDD RID: 3549
		[SerializeField]
		private List<ExpeditionThreat> _expeditionThreats;

		// Token: 0x04000DDE RID: 3550
		[SerializeField]
		private List<CharacterStatus> _characterStatuses;

		// Token: 0x04000DDF RID: 3551
		[SerializeField]
		private List<Deck> _decks;

		// Token: 0x04000DE0 RID: 3552
		[SerializeField]
		private List<Character> _characters;

		// Token: 0x04000DE1 RID: 3553
		[SerializeField]
		private List<IItem> _items;

		// Token: 0x04000DE2 RID: 3554
		[SerializeField]
		private List<EventTag> _tags;

		// Token: 0x04000DE3 RID: 3555
		[SerializeField]
		private List<Goal> _goals;

		// Token: 0x04000DE4 RID: 3556
		[SerializeField]
		private List<Resource> _resources;

		// Token: 0x04000DE5 RID: 3557
		[SerializeField]
		private List<ShipSystem> _shipSystems;

		// Token: 0x04000DE6 RID: 3558
		[SerializeField]
		private List<SystemStatusEvent> _systemStatusEvents;

		// Token: 0x04000DE7 RID: 3559
		[SerializeField]
		private List<Mission> _missions;

		// Token: 0x04000DE8 RID: 3560
		[SerializeField]
		private List<Challenge> _challenges;

		// Token: 0x04000DE9 RID: 3561
		[SerializeField]
		private List<CurrentChallengeData> _currentChallengeData;

		// Token: 0x04000DEA RID: 3562
		[SerializeField]
		private List<TextJournalGroupId> _textJournalGroupId;

		// Token: 0x04000DEB RID: 3563
		[SerializeField]
		private List<BaseActionCondition> _baseActionConditions;

		// Token: 0x04000DEC RID: 3564
		private const string DUPLICATE_ID_ERROR_MESSAGE = "Duplicate ID was found in dictionary '{0}'. Object which was attempted to be added: '{1}'.";

		// Token: 0x04000DED RID: 3565
		private Dictionary<string, Object> _objects;
	}
}
