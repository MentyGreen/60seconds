using System;
using System.Collections.Generic;
using System.IO;
using RG.SecondsRemaster;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000117 RID: 279
public class Settings : MonoBehaviour
{
	// Token: 0x14000064 RID: 100
	// (add) Token: 0x06000D9B RID: 3483 RVA: 0x00038838 File Offset: 0x00036A38
	// (remove) Token: 0x06000D9C RID: 3484 RVA: 0x00038870 File Offset: 0x00036A70
	public event Settings.Report OnSettingsUpdated;

	// Token: 0x06000D9D RID: 3485 RVA: 0x000388A5 File Offset: 0x00036AA5
	public void Awake()
	{
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000388A7 File Offset: 0x00036AA7
	private void OnEnable()
	{
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x000388A9 File Offset: 0x00036AA9
	private void OnDisable()
	{
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000388AC File Offset: 0x00036AAC
	protected void Initialize()
	{
		if (Settings.Data == null)
		{
			Settings.Data = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		bool flag = this.LoadSettings(this.SettingsFilepath, false);
		this.ForceSteamBigPicture();
		if (!flag)
		{
			string key = "EN";
			string currentGameLanguage = SteamApps.GetCurrentGameLanguage();
			if (currentGameLanguage != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(currentGameLanguage);
				if (num <= 1901528810U)
				{
					if (num <= 599131013U)
					{
						if (num != 380651494U)
						{
							if (num == 599131013U)
							{
								if (currentGameLanguage == "french")
								{
									key = "FR";
								}
							}
						}
						else if (currentGameLanguage == "russian")
						{
							key = "RU";
						}
					}
					else if (num != 1492143887U)
					{
						if (num == 1901528810U)
						{
							if (currentGameLanguage == "japanese")
							{
								key = "JA";
							}
						}
					}
					else if (currentGameLanguage == "brazillian")
					{
						key = "BR";
					}
				}
				else if (num <= 2499415067U)
				{
					if (num != 2471602315U)
					{
						if (num == 2499415067U)
						{
							if (currentGameLanguage == "english")
							{
								key = "EN";
							}
						}
					}
					else if (currentGameLanguage == "italian")
					{
						key = "IT";
					}
				}
				else if (num != 3180870988U)
				{
					if (num != 3405445907U)
					{
						if (num == 3719199419U)
						{
							if (currentGameLanguage == "spanish")
							{
								key = "ES";
							}
						}
					}
					else if (currentGameLanguage == "german")
					{
						key = "DE";
					}
				}
				else if (currentGameLanguage == "polish")
				{
					key = "PL";
				}
			}
			this.SelectLanguage(key);
		}
		this.ForceGamepad(false, false);
		this.SelectLanguage();
		this.UpdateSettings(true);
		this.ForceScreenNativeResolution(flag, true);
		this.UpdateResolutionStr();
		this.SetDelimiter();
		this._profile.Initialize();
		InputHandler.Instance.Reload(this._controls, this.GetControlMode(this._controlMode));
		base.GetComponent<ResolutionHandler>().HandleResolution();
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00038AD0 File Offset: 0x00036CD0
	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		this.ReloadSettings();
		if (this.DoesCurrentLanguageWordwrap())
		{
			dfRichTextLabel[] array = Object.FindObjectsOfType<dfRichTextLabel>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].PreserveWhitespace = true;
			}
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00038B08 File Offset: 0x00036D08
	protected void ReloadSettings()
	{
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00038B0C File Offset: 0x00036D0C
	protected void LoadSupportedLanguages(string bookText)
	{
		List<string> list = new List<string>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < bookText.Length; i++)
		{
			bool flag = bookText[i] == '\n';
			if (bookText[i] == ',' || bookText[i] == ';' || flag)
			{
				num2++;
				if (num2 > 1)
				{
					list.Add(bookText.Substring(num, (flag ? (i - 1) : i) - num));
				}
				num = i + 1;
			}
			if (flag)
			{
				break;
			}
		}
		if (list.Count > 0)
		{
			this._supportedLanguages = list.ToArray();
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00038B9D File Offset: 0x00036D9D
	protected void CreateSettingsFile()
	{
		if (!File.Exists(this.SettingsFilepath))
		{
			File.Create(this.SettingsFilepath);
		}
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x00038BB8 File Offset: 0x00036DB8
	protected void DeleteSetting(string key)
	{
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00038BBA File Offset: 0x00036DBA
	protected void DeleteSettingsFile()
	{
		File.Delete(this.SettingsFilepath);
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00038BC7 File Offset: 0x00036DC7
	protected string LoadSetting(string key)
	{
		return null;
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00038BCA File Offset: 0x00036DCA
	protected bool ParseSetting(string rawVal, ref bool val)
	{
		return !string.IsNullOrEmpty(rawVal) && bool.TryParse(rawVal, out val);
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00038BDD File Offset: 0x00036DDD
	protected bool ParseSetting(string rawVal, ref int val)
	{
		return !string.IsNullOrEmpty(rawVal) && int.TryParse(rawVal, out val);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00038BF0 File Offset: 0x00036DF0
	protected bool ParseSetting(string rawVal, ref float val)
	{
		return !string.IsNullOrEmpty(rawVal) && float.TryParse(rawVal, out val);
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x00038C04 File Offset: 0x00036E04
	protected bool LoadSettings(string filepath, bool update = false)
	{
		bool result = true;
		try
		{
			using (StreamReader streamReader = new StreamReader(filepath))
			{
				while (!streamReader.EndOfStream)
				{
					string text = streamReader.ReadLine();
					try
					{
						int i = 0;
						while (i < text.Length)
						{
							if (text[i] == '=')
							{
								i++;
								string text2 = text.Substring(0, i - 1);
								if (text2.Contains("ControlRotationSensitivity"))
								{
									int index = 0;
									if (int.TryParse(text2.Substring(text2.Length - 1, 1), out index))
									{
										float rotationSensitivity = 1f;
										if (float.TryParse(text.Substring(i, text.Length - i), out rotationSensitivity))
										{
											this.GetControlMode(index).RotationSensitivity = rotationSensitivity;
										}
									}
								}
								bool flag = false;
								if (text2 != null)
								{
									uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
									if (num <= 1856687319U)
									{
										if (num <= 776263287U)
										{
											if (num <= 608091816U)
											{
												if (num <= 268818984U)
												{
													if (num != 13804558U)
													{
														if (num == 268818984U)
														{
															if (text2 == "KM1_GLOBAL_MENU")
															{
																flag = true;
															}
														}
													}
													else if (text2 == "KM2_SCAVENGE_FORWARD")
													{
														flag = true;
													}
												}
												else if (num != 399034222U)
												{
													if (num != 536393765U)
													{
														if (num == 608091816U)
														{
															if (text2 == "JOY_GLOBAL_ALTCHOICE2")
															{
																flag = true;
															}
														}
													}
													else if (text2 == "KM1_SCAVENGE_FORWARD")
													{
														flag = true;
													}
												}
												else if (text2 == "MO_SCAVENGE_FORWARD")
												{
													flag = true;
												}
											}
											else if (num <= 658424673U)
											{
												if (num != 624869435U)
												{
													if (num == 658424673U)
													{
														if (text2 == "JOY_GLOBAL_ALTCHOICE1")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "JOY_GLOBAL_ALTCHOICE3")
												{
													flag = true;
												}
											}
											else if (num != 708757530U)
											{
												if (num != 713426840U)
												{
													if (num == 776263287U)
													{
														if (text2 == "JOY_GLOBAL_NEXT")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "FullScreen")
												{
													bool.TryParse(text.Substring(i, text.Length - i), out this._fullscreen);
												}
											}
											else if (text2 == "JOY_GLOBAL_ALTCHOICE4")
											{
												flag = true;
											}
										}
										else if (num <= 1237878590U)
										{
											if (num <= 973224046U)
											{
												if (num != 947360957U)
												{
													if (num == 973224046U)
													{
														if (text2 == "KM2_SCAVENGE_STRAFE_LEFT")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "JOY_GLOBAL_MENU")
												{
													flag = true;
												}
											}
											else if (num != 1027799589U)
											{
												if (num != 1154920606U)
												{
													if (num == 1237878590U)
													{
														if (text2 == "DataLogging")
														{
															bool.TryParse(text.Substring(i, text.Length - i), out this._dataLogging);
														}
													}
												}
												else if (text2 == "Subtitles")
												{
													bool.TryParse(text.Substring(i, text.Length - i), out this._subtitles);
												}
											}
											else if (text2 == "KM2_GLOBAL_MENU")
											{
												flag = true;
											}
										}
										else if (num <= 1806354462U)
										{
											if (num != 1772680185U)
											{
												if (num != 1772799224U)
												{
													if (num == 1806354462U)
													{
														if (text2 == "JOY_GLOBAL_CHOICE3")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "JOY_GLOBAL_CHOICE1")
												{
													flag = true;
												}
											}
											else if (text2 == "KM1_SCAVENGE_ROTATE_LEFT")
											{
												flag = true;
											}
										}
										else if (num != 1823132081U)
										{
											if (num != 1846622138U)
											{
												if (num == 1856687319U)
												{
													if (text2 == "JOY_GLOBAL_CHOICE4")
													{
														flag = true;
													}
												}
											}
											else if (text2 == "KM1_SCAVENGE_ROTATE_RIGHT")
											{
												flag = true;
											}
										}
										else if (text2 == "JOY_GLOBAL_CHOICE2")
										{
											flag = true;
										}
									}
									else if (num <= 2697917098U)
									{
										if (num <= 1978013866U)
										{
											if (num <= 1902345931U)
											{
												if (num != 1864353169U)
												{
													if (num == 1902345931U)
													{
														if (text2 == "JOY_GLOBAL_ACTION1")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "KM1_SCAVENGE_BACKWARD")
												{
													flag = true;
												}
											}
											else if (num != 1919123550U)
											{
												if (num != 1967974538U)
												{
													if (num == 1978013866U)
													{
														if (text2 == "Gamma")
														{
															float.TryParse(text.Substring(i, text.Length - i), out this._gamma);
															this._gamma = Mathf.Clamp(this._gamma, 0.25f, 2f);
														}
													}
												}
												else if (text2 == "QualityIndex")
												{
													int.TryParse(text.Substring(i, text.Length - i), out this._selectedQualityIndex);
												}
											}
											else if (text2 == "JOY_GLOBAL_ACTION2")
											{
												flag = true;
											}
										}
										else if (num <= 2126684997U)
										{
											if (num != 2080855286U)
											{
												if (num != 2123872711U)
												{
													if (num == 2126684997U)
													{
														if (text2 == "MO_GLOBAL_MENU")
														{
															flag = true;
														}
													}
												}
												else if (text2 == "JOY_SCAVENGE_INTERACTION")
												{
													flag = true;
												}
											}
											else if (text2 == "ScreenHeight")
											{
												int.TryParse(text.Substring(i, text.Length - i), out this._screenHeight);
												this._selectedResY = this._screenHeight;
											}
										}
										else if (num != 2476689583U)
										{
											if (num != 2591284123U)
											{
												if (num == 2697917098U)
												{
													if (text2 == "VolumeSfx")
													{
														float.TryParse(text.Substring(i, text.Length - i), out this._volumeSfx);
													}
												}
											}
											else if (text2 == "Language")
											{
												string b = text.Substring(i, text.Length - i);
												for (int j = 0; j < this._supportedLanguages.Length; j++)
												{
													if (this._supportedLanguages[j] == b)
													{
														this._selectedLanguage = j;
														break;
													}
												}
											}
										}
										else if (text2 == "KM2_SCAVENGE_INTERACTION")
										{
											flag = true;
										}
									}
									else if (num <= 3228690829U)
									{
										if (num <= 2931237260U)
										{
											if (num != 2726844303U)
											{
												if (num == 2931237260U)
												{
													if (text2 == "SkipIntro")
													{
														bool.TryParse(text.Substring(i, text.Length - i), out this._skipIntro);
													}
												}
											}
											else if (text2 == "MO_SCAVENGE_INTERACTION")
											{
												flag = true;
											}
										}
										else if (num != 3090272498U)
										{
											if (num != 3130823408U)
											{
												if (num == 3228690829U)
												{
													if (text2 == "ResearchLogging")
													{
														bool.TryParse(text.Substring(i, text.Length - i), out this._researchLogging);
													}
												}
											}
											else if (text2 == "KM2_SCAVENGE_BACKWARD")
											{
												flag = true;
											}
										}
										else if (text2 == "VolumeMusic")
										{
											float.TryParse(text.Substring(i, text.Length - i), out this._volumeMusic);
										}
									}
									else if (num <= 3506588379U)
									{
										if (num != 3276495552U)
										{
											if (num != 3479948389U)
											{
												if (num == 3506588379U)
												{
													if (text2 == "ScreenWidth")
													{
														int.TryParse(text.Substring(i, text.Length - i), out this._screenWidth);
														this._selectedResX = this._screenWidth;
													}
												}
											}
											else if (text2 == "ControlMode")
											{
												int.TryParse(text.Substring(i, text.Length - i), out this._controlMode);
												if (this._controlMode >= 0)
												{
													this.SetControlMode(this._controlMode);
												}
											}
										}
										else if (text2 == "KM1_SCAVENGE_INTERACTION")
										{
											flag = true;
										}
									}
									else if (num != 3640359011U)
									{
										if (num != 3969092447U)
										{
											if (num == 4245451243U)
											{
												if (text2 == "DisableFlashes")
												{
													bool.TryParse(text.Substring(i, text.Length - i), out this._disableFlashes);
												}
											}
										}
										else if (text2 == "JOY_GLOBAL_PREV")
										{
											flag = true;
										}
									}
									else if (text2 == "KM2_SCAVENGE_STRAFE_RIGHT")
									{
										flag = true;
									}
								}
								if (!flag)
								{
									break;
								}
								string value = text.Substring(i, text.Length - i);
								if (this._controls.ContainsKey(text2))
								{
									this._controls[text2] = value;
									break;
								}
								this._controls.Add(text2, value);
								break;
							}
							else
							{
								i++;
							}
						}
					}
					catch
					{
						result = false;
					}
				}
			}
		}
		catch
		{
			result = false;
		}
		if (update)
		{
			this.UpdateSettings(true);
		}
		return result;
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000396EC File Offset: 0x000378EC
	public void RefreshFullscreen(bool fullscreen = true)
	{
		this.Fullscreen = fullscreen;
		this.SetResolution(true);
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000396FC File Offset: 0x000378FC
	protected void SaveSetting(string key, string val, StreamWriter wr)
	{
		wr.WriteLine(string.Format("{0}={1}", key, val));
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x00039710 File Offset: 0x00037910
	protected void SaveSettings(string filepath = null, bool cleanup = false)
	{
		using (StreamWriter streamWriter = new StreamWriter(filepath))
		{
			this.SaveSetting("Language", this.CurrentLanguageCode, streamWriter);
			this.SaveSetting("Subtitles", this._subtitles.ToString(), streamWriter);
			this._screenWidth = this._selectedResX;
			this._screenHeight = this._selectedResY;
			this.SaveSetting("ScreenWidth", this._screenWidth.ToString(), streamWriter);
			this.SaveSetting("ScreenHeight", this._screenHeight.ToString(), streamWriter);
			this.SaveSetting("QualityIndex", this._selectedQualityIndex.ToString(), streamWriter);
			this.SaveSetting("FullScreen", this._fullscreen.ToString(), streamWriter);
			this.SaveSetting("Gamma", this._gamma.ToString(), streamWriter);
			this.SaveSetting("VolumeSfx", this._volumeSfx.ToString(), streamWriter);
			this.SaveSetting("VolumeMusic", this._volumeMusic.ToString(), streamWriter);
			this.SaveSetting("ControlMode", this._controlMode.ToString(), streamWriter);
			this.SaveSetting("SkipIntro", this._skipIntro.ToString(), streamWriter);
			this.SaveSetting("DisableFlashes", this._disableFlashes.ToString(), streamWriter);
			this.SaveSetting("DataLogging", this._dataLogging.ToString(), streamWriter);
			this.SaveSetting("ResearchLogging", this._researchLogging.ToString(), streamWriter);
			for (int i = 0; i < this._supportedControlModes.Length; i++)
			{
				this.SaveSetting("ControlRotationSensitivity" + i.ToString(), this.GetControlMode(i).RotationSensitivity.ToString(), streamWriter);
			}
			foreach (string key in this._controls.Keys)
			{
				this.SaveSetting(key, this._controls[key].ToString(), streamWriter);
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
		this.SetDelimiter();
		if (cleanup && this.OnSettingsUpdated != null)
		{
			this.OnSettingsUpdated();
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00039970 File Offset: 0x00037B70
	public void ResetSettings()
	{
		this.DeleteSettingsFile();
		if (this.OnSettingsUpdated != null)
		{
			this.OnSettingsUpdated();
		}
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0003998B File Offset: 0x00037B8B
	public void ApplySettingsManually(bool cleanup)
	{
		this.SaveSettings(this.SettingsFilepath, cleanup);
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0003999A File Offset: 0x00037B9A
	public void ApplySettings()
	{
		this.SaveSettings(this.SettingsFilepath, true);
		InputHandler.Instance.Reload(this._controls, this.GetControlMode(this._controlMode));
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x000399C5 File Offset: 0x00037BC5
	public void RejectSettings()
	{
		this.LoadSettings(this.SettingsFilepath, true);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x000399D5 File Offset: 0x00037BD5
	protected void PropagateSettings()
	{
		this.UpdateSettings(true);
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x000399E0 File Offset: 0x00037BE0
	public void UpdateSettings(bool advanced = true)
	{
		this._localization.Bind(this.CurrentLanguageCode);
		this.SelectLanguage();
		this._languageSet = true;
		if (advanced)
		{
			this.SetQuality(true);
			if (!this.FullscreenForce)
			{
				this.SetResolution(true);
			}
		}
		this.SetGamma();
		this.SetSfxVolume();
		this.SetMusicVolume();
		this.TryToAddMissingControl("KM1_SCAVENGE_INTERACTION", KeyCode.Space.ToString());
		this.TryToAddMissingControl("KM1_SCAVENGE_BACKWARD", KeyCode.S.ToString());
		this.TryToAddMissingControl("KM1_SCAVENGE_FORWARD", KeyCode.W.ToString());
		this.TryToAddMissingControl("KM1_GLOBAL_MENU", KeyCode.Escape.ToString());
		this.TryToAddMissingControl("KM1_SCAVENGE_ROTATE_LEFT", KeyCode.A.ToString());
		this.TryToAddMissingControl("KM1_SCAVENGE_ROTATE_RIGHT", KeyCode.D.ToString());
		this.TryToAddMissingControl("KM2_SCAVENGE_BACKWARD", KeyCode.S.ToString());
		this.TryToAddMissingControl("KM2_SCAVENGE_FORWARD", KeyCode.W.ToString());
		this.TryToAddMissingControl("KM2_GLOBAL_MENU", KeyCode.Escape.ToString());
		this.TryToAddMissingControl("KM2_SCAVENGE_STRAFE_LEFT", KeyCode.A.ToString());
		this.TryToAddMissingControl("KM2_SCAVENGE_STRAFE_RIGHT", KeyCode.D.ToString());
		this.TryToAddMissingControl("KM2_SCAVENGE_INTERACTION", "MOUSE_0");
		this.TryToAddMissingControl("MO_SCAVENGE_INTERACTION", "MOUSE_0");
		this.TryToAddMissingControl("MO_SCAVENGE_FORWARD", "MOUSE_1");
		this.TryToAddMissingControl("MO_GLOBAL_MENU", "MOUSE_2");
		this.TryToAddMissingControl("JOY_SCAVENGE_INTERACTION", "JOY_" + InputHandler.GetJoyButtonCode(0).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_MENU", "JOY_" + InputHandler.GetJoyButtonCode(7).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_ACTION1", "JOY_" + InputHandler.GetJoyButtonCode(4).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_ACTION2", "JOY_" + InputHandler.GetJoyButtonCode(5).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_ALTCHOICEX", "JoyAxis" + InputHandler.GetJoyAxis(6).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_ALTCHOICEY", "JoyAxis" + InputHandler.GetJoyAxis(7).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_CHOICE1", "JOY_" + InputHandler.GetJoyButtonCode(0).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_CHOICE2", "JOY_" + InputHandler.GetJoyButtonCode(1).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_CHOICE3", "JOY_" + InputHandler.GetJoyButtonCode(3).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_CHOICE4", "JOY_" + InputHandler.GetJoyButtonCode(2).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_NEXT", "JoyAxis" + InputHandler.GetJoyAxis(10).ToString());
		this.TryToAddMissingControl("JOY_GLOBAL_PREV", "JoyAxis" + InputHandler.GetJoyAxis(9).ToString());
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00039D45 File Offset: 0x00037F45
	private void TryToAddMissingControl(string key, string val)
	{
		if (!this._controls.ContainsKey(key))
		{
			this._controls.Add(key, val);
		}
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00039D64 File Offset: 0x00037F64
	private void ChangeControlMode(bool next)
	{
		bool flag = false;
		int num = this._controlMode;
		while (!flag)
		{
			int num2 = num + (next ? 1 : -1);
			if (num2 < 0)
			{
				num2 = this._supportedControlModes.Length - 1;
			}
			else if (num2 >= this._supportedControlModes.Length)
			{
				num2 = 0;
			}
			num = num2;
			if (this._supportedControlModes[num2].Enabled && !this._supportedControlModes[num2].Mobile && (this._supportedControlModes[num2].ScavengeControl != EPlayerInput.GAMEPAD || this.GetValidGamepad() >= 0))
			{
				this.SetControlMode(num2);
				flag = true;
			}
		}
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00039DE9 File Offset: 0x00037FE9
	private void SetControlMode(int index)
	{
		this._controlMode = index;
		if (this.ControlMode.IsGamepad())
		{
			this._activeGamepad = this.GetValidGamepad();
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00039E0C File Offset: 0x0003800C
	public void ChangeControlMode(string key)
	{
		if (this._supportedControlModes != null)
		{
			for (int i = 0; i < this._supportedControlModes.Length; i++)
			{
				if (this._supportedControlModes[i].Key == key)
				{
					this.SetControlMode(i);
					return;
				}
			}
		}
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00039E51 File Offset: 0x00038051
	public void NextControl()
	{
		this.ChangeControlMode(true);
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00039E5A File Offset: 0x0003805A
	public void PrevControl()
	{
		this.ChangeControlMode(false);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00039E63 File Offset: 0x00038063
	public ControlMode GetControlMode(int index)
	{
		if (this._supportedControlModes != null && index >= 0 && this._supportedControlModes.Length > index)
		{
			return this._supportedControlModes[index];
		}
		return null;
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00039E88 File Offset: 0x00038088
	public ControlMode GetControlMode(EPlayerInput moveType)
	{
		if (this._supportedControlModes != null)
		{
			for (int i = 0; i < this._supportedControlModes.Length; i++)
			{
				if (this._supportedControlModes[i].ScavengeControl == moveType)
				{
					return this._supportedControlModes[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00039ECC File Offset: 0x000380CC
	public void SetQuality(bool commit)
	{
		this.FindCurrentQuality();
		QualitySetting currentQualitySetting = this.CurrentQualitySetting;
		if (commit && currentQualitySetting != null)
		{
			currentQualitySetting.Set();
		}
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00039EF3 File Offset: 0x000380F3
	public void SetResolution(bool commit)
	{
		this.FindCurrentResolution();
		this.SelectResolution();
		if (commit)
		{
			if (this._windowedForce)
			{
				this._fullscreen = false;
			}
			Screen.SetResolution(this._screenWidth, this._screenHeight, this._fullscreen);
		}
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00039F2C File Offset: 0x0003812C
	public void SetGamma()
	{
		GammaCorrectionEffect[] array = Object.FindObjectsOfType<GammaCorrectionEffect>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gamma = this._gamma;
		}
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00039F5B File Offset: 0x0003815B
	public void NextQualitySetting()
	{
		this.FindViewedQuality(true);
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00039F64 File Offset: 0x00038164
	public void PrevQualitySetting()
	{
		this.FindViewedQuality(false);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00039F6D File Offset: 0x0003816D
	public void NextResolution()
	{
		this.FindViewedRes(true);
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x00039F76 File Offset: 0x00038176
	public void PrevResolution()
	{
		this.FindViewedRes(false);
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00039F7F File Offset: 0x0003817F
	private bool SelectQuality()
	{
		if (this._viewedQualityIndex >= 0 && this._viewedQualityIndex < this._supportedQualitySettings.Length)
		{
			this._selectedQualityIndex = this._viewedQualityIndex;
			return true;
		}
		return false;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00039FAC File Offset: 0x000381AC
	private bool SelectResolution()
	{
		Resolution[] resolutions = base.GetComponent<ResolutionHandler>().Resolutions;
		if (this._viewedResIndex >= 0 && this._viewedResIndex < resolutions.Length)
		{
			this._selectedResX = resolutions[this._viewedResIndex].width;
			this._selectedResY = resolutions[this._viewedResIndex].height;
			this.UpdateResolutionStr();
			return true;
		}
		return false;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0003A010 File Offset: 0x00038210
	private void UpdateResolutionStr()
	{
		this._selectedResStr = string.Format("{0} x {1}", this._selectedResX, this._selectedResY);
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0003A038 File Offset: 0x00038238
	private bool FindCurrentQuality()
	{
		if (this._supportedQualitySettings != null && this._selectedQualityIndex >= 0 && this._supportedQualitySettings.Length != 0 && this._supportedQualitySettings.Length > this._selectedQualityIndex)
		{
			this._viewedQualityIndex = this._selectedQualityIndex;
			return true;
		}
		this._viewedQualityIndex = -1;
		return false;
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0003A088 File Offset: 0x00038288
	private int GetValidGamepad()
	{
		string[] joystickNames = Input.GetJoystickNames();
		if (joystickNames.Length != 0)
		{
			for (int i = 0; i < joystickNames.Length; i++)
			{
				if (!string.IsNullOrEmpty(joystickNames[i]))
				{
					return i + 1;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0003A0BC File Offset: 0x000382BC
	private bool IsGamepadValid(int index)
	{
		string[] joystickNames = Input.GetJoystickNames();
		return joystickNames.Length > index && !string.IsNullOrEmpty(joystickNames[index]);
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x0003A0E4 File Offset: 0x000382E4
	private void ForceGamepad(bool force = true, bool skipSafetyCheck = false)
	{
		bool flag = false;
		Input.GetJoystickNames();
		if (force)
		{
			if (this.GetValidGamepad() >= 0 || skipSafetyCheck)
			{
				this.ChangeControlMode("JOY");
				flag = true;
			}
		}
		else if (this.GetValidGamepad() < 0 && this.ControlMode.ScavengeControl == EPlayerInput.GAMEPAD)
		{
			this.ChangeControlMode("KM2");
			flag = true;
		}
		if (flag)
		{
			this.SaveSettings(this.SettingsFilepath, false);
		}
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0003A150 File Offset: 0x00038350
	private void ForceSteamBigPicture()
	{
		if (SteamManager.Initialized)
		{
			int num = 0;
			if (int.TryParse(Environment.GetEnvironmentVariable("SteamTenfoot"), out num) && num > 0)
			{
				this.ForceGamepad(true, false);
			}
		}
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0003A188 File Offset: 0x00038388
	private void ForceScreenNativeResolution(bool fileLoaded, bool maxResolution)
	{
		if (!fileLoaded)
		{
			int num = 0;
			int num2 = 0;
			if (maxResolution)
			{
				Resolution[] resolutions = base.GetComponent<ResolutionHandler>().Resolutions;
				if (resolutions.Length != 0)
				{
					num = resolutions[resolutions.Length - 1].width;
					num2 = resolutions[resolutions.Length - 1].height;
				}
			}
			else
			{
				num = Screen.width;
				num2 = Screen.height;
			}
			if (num > 0 && num2 > 0)
			{
				this._screenWidth = num;
				this._selectedResX = num;
				this._screenHeight = num2;
				this._selectedResY = num2;
				this.SetResolution(true);
			}
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0003A20C File Offset: 0x0003840C
	private void FindCurrentResolution()
	{
		Resolution[] resolutions = base.GetComponent<ResolutionHandler>().Resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			if (this._screenWidth == resolutions[i].width && this._screenHeight == resolutions[i].height)
			{
				this._viewedResIndex = i;
				return;
			}
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0003A264 File Offset: 0x00038464
	public void FindViewedQuality(bool next)
	{
		if (next)
		{
			this._viewedQualityIndex++;
		}
		else
		{
			this._viewedQualityIndex--;
		}
		this._viewedQualityIndex = Mathf.Clamp(this._viewedQualityIndex, 0, this._supportedQualitySettings.Length - 1);
		this.SelectQuality();
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0003A2B8 File Offset: 0x000384B8
	public void FindViewedRes(bool next)
	{
		Resolution[] resolutions = base.GetComponent<ResolutionHandler>().Resolutions;
		if (next)
		{
			this._viewedResIndex++;
		}
		else
		{
			this._viewedResIndex--;
		}
		this._viewedResIndex = Mathf.Clamp(this._viewedResIndex, 0, resolutions.Length - 1);
		this.SelectResolution();
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0003A310 File Offset: 0x00038510
	public void SetSfxVolume()
	{
		SoundManager.Instance.VolumeSfx = this._volumeSfx;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0003A322 File Offset: 0x00038522
	public void SetMusicVolume()
	{
		SoundManager.Instance.VolumeMusic = this._volumeMusic;
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003A334 File Offset: 0x00038534
	public string SettingsFilepath
	{
		get
		{
			return Application.persistentDataPath + "/Settings.dat";
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0003A345 File Offset: 0x00038545
	public string DataFilepath
	{
		get
		{
			return Application.persistentDataPath + "/Logs/";
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0003A356 File Offset: 0x00038556
	public void NextLanguage()
	{
		this._languageSet = false;
		this._selectedLanguage++;
		if (this._selectedLanguage >= this._supportedLanguages.Length)
		{
			this._selectedLanguage = 0;
		}
		this.SelectLanguage();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0003A38A File Offset: 0x0003858A
	public void PrevLanguage()
	{
		this._languageSet = false;
		this._selectedLanguage--;
		if (this._selectedLanguage < 0)
		{
			this._selectedLanguage = this._supportedLanguages.Length - 1;
		}
		this.SelectLanguage();
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0003A3C0 File Offset: 0x000385C0
	public bool SelectLanguage(string key)
	{
		if (this._supportedLanguages != null)
		{
			for (int i = 0; i < this._supportedLanguages.Length; i++)
			{
				if (this._supportedLanguages[i] == key)
				{
					this._selectedLanguage = i;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0003A404 File Offset: 0x00038604
	public void SelectLanguage()
	{
		string text = null;
		if (this._supportedLanguages != null && this._selectedLanguage >= 0 && this._selectedLanguage < this._supportedLanguages.Length)
		{
			text = this._supportedLanguages[this._selectedLanguage];
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "EN";
		}
		text = "lang_" + text;
		this._selectedLanguageStr = text;
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0003A463 File Offset: 0x00038663
	public bool DoesCurrentLanguageWordwrap()
	{
		return this.CurrentLanguageCode == "JA" || this.CurrentLanguageCode == "ZH";
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0003A490 File Offset: 0x00038690
	private void SetDelimiter()
	{
		if (this.DoesCurrentLanguageWordwrap())
		{
			WordWrapper instance = WordWrapper.GetInstance();
			instance.WordSeparator = string.Empty;
			instance.DefaultDelimiter = '`';
			if (this.CurrentLanguageCode == "ZH")
			{
				instance.RecalculateLatinCharacters = true;
			}
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003A4E0 File Offset: 0x000386E0
	// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0003A4D7 File Offset: 0x000386D7
	public string VersionInfo
	{
		get
		{
			return this._versionInfo;
		}
		set
		{
			this._versionInfo = value;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003A4F1 File Offset: 0x000386F1
	// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0003A4E8 File Offset: 0x000386E8
	public string VersionDate
	{
		get
		{
			return this._versionDate;
		}
		set
		{
			this._versionDate = value;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003A4F9 File Offset: 0x000386F9
	// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0003A501 File Offset: 0x00038701
	public float VolumeSfx
	{
		get
		{
			return this._volumeSfx;
		}
		set
		{
			this._volumeSfx = value;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003A50A File Offset: 0x0003870A
	// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0003A512 File Offset: 0x00038712
	public float VolumeMusic
	{
		get
		{
			return this._volumeMusic;
		}
		set
		{
			this._volumeMusic = value;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003A51B File Offset: 0x0003871B
	// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0003A523 File Offset: 0x00038723
	public float Gamma
	{
		get
		{
			return this._gamma;
		}
		set
		{
			this._gamma = value;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003A52C File Offset: 0x0003872C
	// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0003A534 File Offset: 0x00038734
	public int ResX
	{
		get
		{
			return this._screenWidth;
		}
		set
		{
			this._screenWidth = value;
		}
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003A53D File Offset: 0x0003873D
	// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0003A545 File Offset: 0x00038745
	public int ResY
	{
		get
		{
			return this._screenHeight;
		}
		set
		{
			this._screenHeight = value;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0003A54E File Offset: 0x0003874E
	// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0003A556 File Offset: 0x00038756
	public bool Fullscreen
	{
		get
		{
			return this._fullscreen;
		}
		set
		{
			this._fullscreen = value;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003A55F File Offset: 0x0003875F
	// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0003A567 File Offset: 0x00038767
	public bool FullscreenForce
	{
		get
		{
			return this._fullscreenForce;
		}
		set
		{
			this._fullscreenForce = value;
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0003A570 File Offset: 0x00038770
	// (set) Token: 0x06000DED RID: 3565 RVA: 0x0003A578 File Offset: 0x00038778
	public bool WindowedForce
	{
		get
		{
			return this._windowedForce;
		}
		set
		{
			this._windowedForce = value;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003A581 File Offset: 0x00038781
	// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0003A589 File Offset: 0x00038789
	public string Language
	{
		get
		{
			return this._language;
		}
		set
		{
			this._language = value;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0003A59B File Offset: 0x0003879B
	// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0003A592 File Offset: 0x00038792
	public bool Subtitles
	{
		get
		{
			return this._subtitles;
		}
		set
		{
			this._subtitles = value;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003A5A3 File Offset: 0x000387A3
	public bool LanguageSet
	{
		get
		{
			return this._languageSet;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0003A5AB File Offset: 0x000387AB
	// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x0003A5B3 File Offset: 0x000387B3
	public string CurrentResolution
	{
		get
		{
			return this._selectedResStr;
		}
		set
		{
			this._selectedResStr = value;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003A5BC File Offset: 0x000387BC
	public string CurrentLanguage
	{
		get
		{
			return this._selectedLanguageStr;
		}
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003A5C4 File Offset: 0x000387C4
	public string CurrentLanguageCode
	{
		get
		{
			if (this._supportedLanguages == null || this._supportedLanguages.Length <= this._selectedLanguage || this._selectedLanguage < 0)
			{
				return "EN";
			}
			return this._supportedLanguages[this._selectedLanguage];
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003A5FA File Offset: 0x000387FA
	public QualitySetting CurrentQualitySetting
	{
		get
		{
			if (this._supportedQualitySettings == null || this._supportedQualitySettings.Length <= this._viewedQualityIndex || this._viewedQualityIndex < 0)
			{
				return null;
			}
			return this._supportedQualitySettings[this._viewedQualityIndex];
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003A62C File Offset: 0x0003882C
	public Localization LocalizationManager
	{
		get
		{
			return this._localization;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003A634 File Offset: 0x00038834
	public Profile PlayerProfile
	{
		get
		{
			return this._profile;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0003A63C File Offset: 0x0003883C
	public ControlMode ControlMode
	{
		get
		{
			return this._supportedControlModes[this._controlMode];
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0003A64B File Offset: 0x0003884B
	public string ControlModeName
	{
		get
		{
			return this.ControlMode.Name;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0003A658 File Offset: 0x00038858
	public string ControlModeIcon
	{
		get
		{
			return this.ControlMode.Icon;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0003A665 File Offset: 0x00038865
	// (set) Token: 0x06000DFE RID: 3582 RVA: 0x0003A672 File Offset: 0x00038872
	public float ControlModeRotationSensitivity
	{
		get
		{
			return this.ControlMode.RotationSensitivity;
		}
		set
		{
			this.ControlMode.RotationSensitivity = value;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0003A680 File Offset: 0x00038880
	// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0003A688 File Offset: 0x00038888
	public CursorLockMode LockCursor
	{
		get
		{
			return this._lockCursor;
		}
		set
		{
			this._lockCursor = value;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003A691 File Offset: 0x00038891
	// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0003A699 File Offset: 0x00038899
	public bool ShowCursor
	{
		get
		{
			return this._showCursor;
		}
		set
		{
			this._showCursor = value;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003A6A2 File Offset: 0x000388A2
	// (set) Token: 0x06000E04 RID: 3588 RVA: 0x0003A6AA File Offset: 0x000388AA
	public bool SkipIntro
	{
		get
		{
			return this._skipIntro;
		}
		set
		{
			this._skipIntro = value;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0003A6B3 File Offset: 0x000388B3
	// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0003A6BB File Offset: 0x000388BB
	public bool DisableFlashes
	{
		get
		{
			return this._disableFlashes;
		}
		set
		{
			this._disableFlashes = value;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003A6C4 File Offset: 0x000388C4
	// (set) Token: 0x06000E08 RID: 3592 RVA: 0x0003A6CC File Offset: 0x000388CC
	public bool ResearchLogging
	{
		get
		{
			return this._researchLogging;
		}
		set
		{
			this._researchLogging = value;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0003A6D5 File Offset: 0x000388D5
	// (set) Token: 0x06000E0A RID: 3594 RVA: 0x0003A6DD File Offset: 0x000388DD
	public bool DataLogging
	{
		get
		{
			return this._dataLogging;
		}
		set
		{
			this._dataLogging = value;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0003A6E6 File Offset: 0x000388E6
	public Dictionary<string, string> Controls
	{
		get
		{
			return this._controls;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0003A6EE File Offset: 0x000388EE
	public int ActiveGamepad
	{
		get
		{
			return this._activeGamepad;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0003A6F6 File Offset: 0x000388F6
	public string Version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x04000761 RID: 1889
	public static Settings Data;

	// Token: 0x04000762 RID: 1890
	public static string VersionPlatform = "STEAM ";

	// Token: 0x04000763 RID: 1891
	public static int VersionId = 1402;

	// Token: 0x04000764 RID: 1892
	public static int SaveVersion = 13001;

	// Token: 0x04000765 RID: 1893
	protected const string DEFAULT_SETTINGS_FILENAME = "Settings.dat";

	// Token: 0x04000766 RID: 1894
	protected const string DEFAULT_DATA_FOLDERNAME = "Logs";

	// Token: 0x04000767 RID: 1895
	protected const string FALLBACK_LANGUAGE = "EN";

	// Token: 0x04000768 RID: 1896
	protected const string LANGUAGE_STR = "Language";

	// Token: 0x04000769 RID: 1897
	protected const string SUBTITLES_STR = "Subtitles";

	// Token: 0x0400076A RID: 1898
	protected const string QUALITY_INDEX_STR = "QualityIndex";

	// Token: 0x0400076B RID: 1899
	protected const string SCREEN_WIDTH_STR = "ScreenWidth";

	// Token: 0x0400076C RID: 1900
	protected const string SCREEN_HEIGHT_STR = "ScreenHeight";

	// Token: 0x0400076D RID: 1901
	protected const string FULL_SCREEN_STR = "FullScreen";

	// Token: 0x0400076E RID: 1902
	protected const string GAMMA_STR = "Gamma";

	// Token: 0x0400076F RID: 1903
	protected const string VOLUME_SFX_STR = "VolumeSfx";

	// Token: 0x04000770 RID: 1904
	protected const string VOLUME_MUSIC_STR = "VolumeMusic";

	// Token: 0x04000771 RID: 1905
	protected const string CONTROL_MODE_STR = "ControlMode";

	// Token: 0x04000772 RID: 1906
	protected const string CONTROL_ROTATION_SENSITIVITY_STR = "ControlRotationSensitivity";

	// Token: 0x04000773 RID: 1907
	protected const string SKIP_INTRO_STR = "SkipIntro";

	// Token: 0x04000774 RID: 1908
	protected const string DISABLE_FLASHES_STR = "DisableFlashes";

	// Token: 0x04000775 RID: 1909
	protected const string DATA_LOGGING_STR = "DataLogging";

	// Token: 0x04000776 RID: 1910
	protected const string RESEARCH_LOGGING_STR = "ResearchLogging";

	// Token: 0x04000777 RID: 1911
	protected const string DAD_HAT_STR = "DadHat";

	// Token: 0x04000778 RID: 1912
	protected const string MOM_HAT_STR = "MomHat";

	// Token: 0x04000779 RID: 1913
	protected const string SON_HAT_STR = "SonHat";

	// Token: 0x0400077A RID: 1914
	protected const string DAUGHTER_HAT_STR = "DaughterHat";

	// Token: 0x0400077B RID: 1915
	protected const string RESOLUTION_FORMAT = "{0} x {1}";

	// Token: 0x0400077C RID: 1916
	protected const string SETTING_FORMAT = "{0}={1}";

	// Token: 0x0400077D RID: 1917
	protected const char DEFAULT_SETTINGS_DIVIDER = '=';

	// Token: 0x0400077E RID: 1918
	protected const char DEFAULT_TIME_DIVIDER = ':';

	// Token: 0x0400077F RID: 1919
	protected const char DEFAULT_DATA_SEPARATOR = ';';

	// Token: 0x04000780 RID: 1920
	protected const char DEFAULT_BOOK_SEPARATOR = ',';

	// Token: 0x04000782 RID: 1922
	[SerializeField]
	private string _versionInfo = string.Empty;

	// Token: 0x04000783 RID: 1923
	[SerializeField]
	private string _versionDate = DateTime.Now.ToShortDateString();

	// Token: 0x04000784 RID: 1924
	[SerializeField]
	private Profile _profile = new Profile();

	// Token: 0x04000785 RID: 1925
	[SerializeField]
	protected float _volumeSfx = 1f;

	// Token: 0x04000786 RID: 1926
	[SerializeField]
	protected float _volumeMusic = 1f;

	// Token: 0x04000787 RID: 1927
	[SerializeField]
	protected float _gamma = 0.85f;

	// Token: 0x04000788 RID: 1928
	[SerializeField]
	protected int _screenWidth = 1920;

	// Token: 0x04000789 RID: 1929
	[SerializeField]
	protected int _screenHeight = 1080;

	// Token: 0x0400078A RID: 1930
	[SerializeField]
	protected bool _fullscreen = true;

	// Token: 0x0400078B RID: 1931
	[SerializeField]
	protected string _language = "EN";

	// Token: 0x0400078C RID: 1932
	[SerializeField]
	protected bool _subtitles;

	// Token: 0x0400078D RID: 1933
	[SerializeField]
	protected int _controlMode;

	// Token: 0x0400078E RID: 1934
	[SerializeField]
	protected bool _skipIntro;

	// Token: 0x0400078F RID: 1935
	[SerializeField]
	protected bool _disableFlashes;

	// Token: 0x04000790 RID: 1936
	[SerializeField]
	protected bool _dataLogging;

	// Token: 0x04000791 RID: 1937
	[SerializeField]
	protected bool _researchLogging;

	// Token: 0x04000792 RID: 1938
	[SerializeField]
	protected ControlMode[] _supportedControlModes;

	// Token: 0x04000793 RID: 1939
	[SerializeField]
	protected string[] _supportedLanguages;

	// Token: 0x04000794 RID: 1940
	[SerializeField]
	protected QualitySetting[] _supportedQualitySettings;

	// Token: 0x04000795 RID: 1941
	protected Dictionary<string, string> _controls = new Dictionary<string, string>();

	// Token: 0x04000796 RID: 1942
	protected string _selectedResStr = string.Empty;

	// Token: 0x04000797 RID: 1943
	protected int _viewedResIndex;

	// Token: 0x04000798 RID: 1944
	protected int _selectedResX;

	// Token: 0x04000799 RID: 1945
	protected int _selectedResY;

	// Token: 0x0400079A RID: 1946
	protected string _selectedLanguageStr = string.Empty;

	// Token: 0x0400079B RID: 1947
	protected int _selectedLanguage = -1;

	// Token: 0x0400079C RID: 1948
	protected int _viewedQualityIndex;

	// Token: 0x0400079D RID: 1949
	protected int _selectedQualityIndex;

	// Token: 0x0400079E RID: 1950
	protected bool _fullscreenForce;

	// Token: 0x0400079F RID: 1951
	protected bool _firstSetup = true;

	// Token: 0x040007A0 RID: 1952
	protected bool _languageSet = true;

	// Token: 0x040007A1 RID: 1953
	protected int _activeGamepad = -1;

	// Token: 0x040007A2 RID: 1954
	protected string _version = string.Empty;

	// Token: 0x040007A3 RID: 1955
	protected bool _windowedForce;

	// Token: 0x040007A4 RID: 1956
	[SerializeField]
	private Localization _localization = new Localization();

	// Token: 0x040007A5 RID: 1957
	private CursorLockMode _lockCursor = CursorLockMode.Confined;

	// Token: 0x040007A6 RID: 1958
	private bool _showCursor;

	// Token: 0x020003AD RID: 941
	// (Invoke) Token: 0x06001DBE RID: 7614
	public delegate void Report();
}
