using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class WordWrapper
{
	// Token: 0x06000F56 RID: 3926 RVA: 0x0003F996 File Offset: 0x0003DB96
	public static WordWrapper GetInstance()
	{
		if (WordWrapper.Instance == null)
		{
			WordWrapper.Instance = new WordWrapper();
		}
		return WordWrapper.Instance;
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003F9AE File Offset: 0x0003DBAE
	// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003F9B6 File Offset: 0x0003DBB6
	public bool RecalculateLatinCharacters
	{
		get
		{
			return this._recalculateLatinCharacters;
		}
		set
		{
			this._recalculateLatinCharacters = value;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003F9BF File Offset: 0x0003DBBF
	// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0003F9C7 File Offset: 0x0003DBC7
	public string WordSeparator
	{
		get
		{
			return this._wordSeparator;
		}
		set
		{
			this._wordSeparator = value;
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
	// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0003F9D8 File Offset: 0x0003DBD8
	public char DefaultDelimiter
	{
		get
		{
			return this._defaultDelimiter;
		}
		set
		{
			this._defaultDelimiter = value;
			this._defaultDelimiterStr = this._defaultDelimiter.ToString();
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0003F9F2 File Offset: 0x0003DBF2
	public StringBuilder ClearDelimiters(StringBuilder sb)
	{
		return sb.Replace(this._defaultDelimiterStr, "");
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0003FA05 File Offset: 0x0003DC05
	public string ClearDelimiters(string s)
	{
		return s.Replace(this._defaultDelimiterStr, "");
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0003FA18 File Offset: 0x0003DC18
	public string WrapText(string text, int lineWidth, WordWrapper.EWrapAlgorithm algorithm = WordWrapper.EWrapAlgorithm.Dynamic)
	{
		return this.WrapText(text, lineWidth, this._defaultDelimiter, algorithm);
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0003FA2C File Offset: 0x0003DC2C
	public string WrapText(string text, int lineWidth, char delimiter = ' ', WordWrapper.EWrapAlgorithm algorithm = WordWrapper.EWrapAlgorithm.Dynamic)
	{
		if (!text.Contains(delimiter.ToString()))
		{
			return text;
		}
		string[] array = text.Split(new char[]
		{
			delimiter
		});
		if (text.Length - array.Length - 1 < lineWidth)
		{
			return text.Replace(delimiter.ToString(), "");
		}
		if (algorithm == WordWrapper.EWrapAlgorithm.Greedy)
		{
			return this.GreedyAlgorithm(array, lineWidth);
		}
		if (algorithm != WordWrapper.EWrapAlgorithm.Dynamic)
		{
			return string.Empty;
		}
		return this.DynamicAlgorithm(array, lineWidth);
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
	private string GreedyAlgorithm(string[] words, int lineWidth)
	{
		int num = lineWidth;
		string text = string.Empty;
		foreach (string text2 in words)
		{
			if (text2.Length + this._wordSeparator.Length > num)
			{
				text += Environment.NewLine;
				text = text + text2 + this._wordSeparator;
				num = lineWidth - (text2.Length + this._wordSeparator.Length);
			}
			else
			{
				text = text + text2 + this._wordSeparator;
				num -= text2.Length + this._wordSeparator.Length;
			}
		}
		return text;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x0003FB33 File Offset: 0x0003DD33
	private int calculateIndex(int row, int column, int matrixSize)
	{
		return matrixSize * row + column - row * (row + 1) / 2;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x0003FB42 File Offset: 0x0003DD42
	private int calculateMatrixSize(int size)
	{
		return size * (size + 1) / 2;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x0003FB4C File Offset: 0x0003DD4C
	private void CheckMatrices(int wordsNumber)
	{
		if (this._wordsLineCounterMatrix == null || this._wordsLineCounterMatrix.Length < this.calculateMatrixSize(wordsNumber))
		{
			this._wordsLineCounterMatrix = new int[this.calculateMatrixSize(wordsNumber)];
		}
		if (this._costMatrix == null || this._costMatrix.Length < this.calculateMatrixSize(wordsNumber))
		{
			this._costMatrix = new int[this.calculateMatrixSize(wordsNumber)];
		}
		if (this._costArrangement == null || this._costArrangement.Length < wordsNumber + 1)
		{
			this._costArrangement = new int[wordsNumber + 1];
		}
		if (this._result == null || this._result.Length < wordsNumber + 1)
		{
			this._result = new int[wordsNumber + 1];
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
	private void CleanTables()
	{
		Array.Clear(this._wordsLineCounterMatrix, 0, this._wordsLineCounterMatrix.Length);
		Array.Clear(this._costMatrix, 0, this._costMatrix.Length);
		Array.Clear(this._costArrangement, 0, this._costArrangement.Length);
		Array.Clear(this._result, 0, this._result.Length);
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0003FC58 File Offset: 0x0003DE58
	private string DynamicAlgorithm(string[] words, int lineWidth)
	{
		int[] array = new int[words.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = words[i].Length;
			if (this._recalculateLatinCharacters)
			{
				int num = words[i].ToLower().Count((char c) => (c >= 'a' && c <= 'z') || c == ' ');
				int num2 = Mathf.CeilToInt((float)num / 3f);
				array[i] = array[i] - num + num2;
			}
			lineWidth = Math.Max(lineWidth, words[i].Length);
		}
		int num3 = array.Length;
		this.CheckMatrices(num3);
		for (int j = 0; j < num3; j++)
		{
			this._wordsLineCounterMatrix[this.calculateIndex(j, j, num3)] = lineWidth - array[j];
			for (int k = j + 1; k < num3; k++)
			{
				this._wordsLineCounterMatrix[this.calculateIndex(j, k, num3)] = this._wordsLineCounterMatrix[this.calculateIndex(j, k - 1, num3)] - array[k] - this._wordSeparator.Length;
			}
		}
		for (int l = 0; l < num3; l++)
		{
			for (int m = l; m < num3; m++)
			{
				if (this._wordsLineCounterMatrix[this.calculateIndex(l, m, num3)] < 0)
				{
					this._costMatrix[this.calculateIndex(l, m, num3)] = int.MaxValue;
				}
				else if (m == num3 - 1 && this._wordsLineCounterMatrix[this.calculateIndex(l, m, num3)] >= 0)
				{
					this._costMatrix[this.calculateIndex(l, m, num3)] = 0;
				}
				else
				{
					this._costMatrix[this.calculateIndex(l, m, num3)] = this._wordsLineCounterMatrix[this.calculateIndex(l, m, num3)] * this._wordsLineCounterMatrix[this.calculateIndex(l, m, num3)];
				}
			}
		}
		this._costArrangement[0] = 0;
		for (int n = 1; n <= num3; n++)
		{
			this._costArrangement[n] = int.MaxValue;
			for (int num4 = 1; num4 <= n; num4++)
			{
				if (this._costArrangement[num4 - 1] != 2147483647 && this._costMatrix[this.calculateIndex(num4 - 1, n - 1, num3)] != 2147483647 && this._costArrangement[num4 - 1] + this._costMatrix[this.calculateIndex(num4 - 1, n - 1, num3)] < this._costArrangement[n])
				{
					this._costArrangement[n] = this._costArrangement[num4 - 1] + this._costMatrix[this.calculateIndex(num4 - 1, n - 1, num3)];
					this._result[n] = num4;
				}
			}
		}
		string empty = string.Empty;
		this.GetWrappedText(num3, words, ref empty);
		this.CleanTables();
		return empty.TrimEnd(Array.Empty<char>());
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x0003FF24 File Offset: 0x0003E124
	private int GetWrappedText(int n, string[] words, ref string message)
	{
		int result;
		if (this._result[n] == 1)
		{
			result = 1;
		}
		else
		{
			result = this.GetWrappedText(this._result[n] - 1, words, ref message) + 1;
		}
		for (int i = this._result[n] - 1; i < n; i++)
		{
			message = message + words[i] + this._wordSeparator;
		}
		message += Environment.NewLine;
		return result;
	}

	// Token: 0x04000948 RID: 2376
	private const int INFINITY = 2147483647;

	// Token: 0x04000949 RID: 2377
	private static WordWrapper Instance;

	// Token: 0x0400094A RID: 2378
	private bool _recalculateLatinCharacters;

	// Token: 0x0400094B RID: 2379
	private string _wordSeparator = " ";

	// Token: 0x0400094C RID: 2380
	private char _defaultDelimiter = ' ';

	// Token: 0x0400094D RID: 2381
	private string _defaultDelimiterStr = " ";

	// Token: 0x0400094E RID: 2382
	private int[] _wordsLineCounterMatrix;

	// Token: 0x0400094F RID: 2383
	private int[] _costMatrix;

	// Token: 0x04000950 RID: 2384
	private int[] _costArrangement;

	// Token: 0x04000951 RID: 2385
	private int[] _result;

	// Token: 0x020003CA RID: 970
	public enum EWrapAlgorithm
	{
		// Token: 0x040017AC RID: 6060
		Greedy,
		// Token: 0x040017AD RID: 6061
		Dynamic
	}
}
