using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RG_GameCamera.ThirdParty
{
	// Token: 0x020001D3 RID: 467
	public static class Json
	{
		// Token: 0x0600133C RID: 4924 RVA: 0x00055938 File Offset: 0x00053B38
		public static object Deserialize(string json)
		{
			Json.tabCounter = 0;
			Json.lastDecode = json;
			if (json == null)
			{
				return null;
			}
			char[] json2 = json.ToCharArray();
			int num = 0;
			bool flag = true;
			object result = Json.ParseValue(json2, ref num, ref flag);
			if (flag)
			{
				Json.lastErrorIndex = -1;
				return result;
			}
			Json.lastErrorIndex = num;
			return result;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0005597C File Offset: 0x00053B7C
		public static string Serialize(object json)
		{
			StringBuilder stringBuilder = new StringBuilder(2000);
			if (!Json.SerializeValue(json, stringBuilder))
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000559A5 File Offset: 0x00053BA5
		public static bool LastDecodeSuccessful()
		{
			return Json.lastErrorIndex == -1;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000559AF File Offset: 0x00053BAF
		public static int GetLastErrorIndex()
		{
			return Json.lastErrorIndex;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000559B8 File Offset: 0x00053BB8
		public static string GetLastErrorSnippet()
		{
			if (Json.lastErrorIndex == -1)
			{
				return "";
			}
			int num = Json.lastErrorIndex - 5;
			int num2 = Json.lastErrorIndex + 15;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 >= Json.lastDecode.Length)
			{
				num2 = Json.lastDecode.Length - 1;
			}
			return Json.lastDecode.Substring(num, num2 - num + 1);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00055A14 File Offset: 0x00053C14
		private static Dictionary<string, object> ParseObject(char[] json, ref int index)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Json.NextToken(json, ref index);
			for (;;)
			{
				Json.TOKEN token = Json.LookAhead(json, index);
				if (token == Json.TOKEN.NONE)
				{
					break;
				}
				if (token == Json.TOKEN.COMMA)
				{
					Json.NextToken(json, ref index);
				}
				else
				{
					if (token == Json.TOKEN.CURLY_CLOSE)
					{
						goto Block_3;
					}
					string text = Json.ParseString(json, ref index);
					if (text == null)
					{
						goto Block_4;
					}
					token = Json.NextToken(json, ref index);
					if (token != Json.TOKEN.COLON)
					{
						goto Block_5;
					}
					bool flag = true;
					object value = Json.ParseValue(json, ref index, ref flag);
					if (!flag)
					{
						goto Block_6;
					}
					dictionary[text] = value;
				}
			}
			return null;
			Block_3:
			Json.NextToken(json, ref index);
			return dictionary;
			Block_4:
			return null;
			Block_5:
			return null;
			Block_6:
			return null;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00055A90 File Offset: 0x00053C90
		private static List<object> ParseArray(char[] json, ref int index)
		{
			List<object> list = new List<object>();
			Json.NextToken(json, ref index);
			for (;;)
			{
				Json.TOKEN token = Json.LookAhead(json, index);
				if (token == Json.TOKEN.NONE)
				{
					break;
				}
				if (token == Json.TOKEN.COMMA)
				{
					Json.NextToken(json, ref index);
				}
				else
				{
					if (token == Json.TOKEN.SQUARED_CLOSE)
					{
						goto Block_3;
					}
					bool flag = true;
					object item = Json.ParseValue(json, ref index, ref flag);
					if (!flag)
					{
						goto Block_4;
					}
					list.Add(item);
				}
			}
			return null;
			Block_3:
			Json.NextToken(json, ref index);
			return list;
			Block_4:
			return null;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00055AF0 File Offset: 0x00053CF0
		private static object ParseValue(char[] json, ref int index, ref bool success)
		{
			switch (Json.LookAhead(json, index))
			{
			case Json.TOKEN.CURLY_OPEN:
				return Json.ParseObject(json, ref index);
			case Json.TOKEN.SQUARED_OPEN:
				return Json.ParseArray(json, ref index);
			case Json.TOKEN.STRING:
				return Json.ParseString(json, ref index);
			case Json.TOKEN.NUMBER:
				return Json.ParseNumber(json, ref index);
			case Json.TOKEN.TRUE:
				Json.NextToken(json, ref index);
				return true;
			case Json.TOKEN.FALSE:
				Json.NextToken(json, ref index);
				return false;
			case Json.TOKEN.NULL:
				Json.NextToken(json, ref index);
				return null;
			}
			success = false;
			return null;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00055B8C File Offset: 0x00053D8C
		private static string ParseString(char[] json, ref int index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Json.EatWhitespace(json, ref index);
			int num = index;
			index = num + 1;
			char c = json[num];
			bool flag = false;
			while (!flag && index != json.Length)
			{
				num = index;
				index = num + 1;
				c = json[num];
				if (c == '"')
				{
					flag = true;
					break;
				}
				if (c == '\\')
				{
					if (index == json.Length)
					{
						break;
					}
					num = index;
					index = num + 1;
					c = json[num];
					if (c == '"')
					{
						stringBuilder.Append('"');
					}
					else if (c == '\\')
					{
						stringBuilder.Append('\\');
					}
					else if (c == '/')
					{
						stringBuilder.Append('/');
					}
					else if (c == 'b')
					{
						stringBuilder.Append('\b');
					}
					else if (c == 'f')
					{
						stringBuilder.Append('\f');
					}
					else if (c == 'n')
					{
						stringBuilder.Append('\n');
					}
					else if (c == 'r')
					{
						stringBuilder.Append('\r');
					}
					else if (c == 't')
					{
						stringBuilder.Append('\t');
					}
					else if (c == 'u')
					{
						if (json.Length - index < 4)
						{
							break;
						}
						char[] array = new char[4];
						Array.Copy(json, index, array, 0, 4);
						stringBuilder.AppendFormat(string.Format("&#x{0};", array), Array.Empty<object>());
						index += 4;
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (!flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00055CE0 File Offset: 0x00053EE0
		private static object ParseNumber(char[] json, ref int index)
		{
			Json.EatWhitespace(json, ref index);
			int lastIndexOfNumber = Json.GetLastIndexOfNumber(json, index);
			int num = lastIndexOfNumber - index + 1;
			char[] array = new char[num];
			Array.Copy(json, index, array, 0, num);
			index = lastIndexOfNumber + 1;
			string text = new string(array);
			if (text.IndexOf('.') == -1)
			{
				return long.Parse(text);
			}
			return double.Parse(text);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00055D44 File Offset: 0x00053F44
		private static int GetLastIndexOfNumber(char[] json, int index)
		{
			int num = index;
			while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
			{
				num++;
			}
			return num - 1;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00055D72 File Offset: 0x00053F72
		private static void EatWhitespace(char[] json, ref int index)
		{
			while (index < json.Length && " \t\n\r".IndexOf(json[index]) != -1)
			{
				index++;
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00055D94 File Offset: 0x00053F94
		private static Json.TOKEN LookAhead(char[] json, int index)
		{
			int num = index;
			return Json.NextToken(json, ref num);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00055DAC File Offset: 0x00053FAC
		private static Json.TOKEN NextToken(char[] json, ref int index)
		{
			Json.EatWhitespace(json, ref index);
			if (index == json.Length)
			{
				return Json.TOKEN.NONE;
			}
			char c = json[index];
			index++;
			if (c <= '[')
			{
				switch (c)
				{
				case '"':
					return Json.TOKEN.STRING;
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case '.':
				case '/':
					break;
				case ',':
					return Json.TOKEN.COMMA;
				case '-':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return Json.TOKEN.NUMBER;
				case ':':
					return Json.TOKEN.COLON;
				default:
					if (c == '[')
					{
						return Json.TOKEN.SQUARED_OPEN;
					}
					break;
				}
			}
			else
			{
				if (c == ']')
				{
					return Json.TOKEN.SQUARED_CLOSE;
				}
				if (c == '{')
				{
					return Json.TOKEN.CURLY_OPEN;
				}
				if (c == '}')
				{
					return Json.TOKEN.CURLY_CLOSE;
				}
			}
			index--;
			int num = json.Length - index;
			if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
			{
				index += 5;
				return Json.TOKEN.FALSE;
			}
			if (num >= 4)
			{
				if (json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
				{
					index += 4;
					return Json.TOKEN.TRUE;
				}
				if (json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
				{
					index += 4;
					return Json.TOKEN.NULL;
				}
			}
			return Json.TOKEN.NONE;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00055F1C File Offset: 0x0005411C
		private static bool SerializeObject(IDictionary anObject, StringBuilder builder)
		{
			bool flag = true;
			builder.Append('{');
			if (Json.PrettyPrint)
			{
				builder.Append('\n');
				Json.tabCounter++;
			}
			int num = 0;
			foreach (object obj in anObject.Keys)
			{
				int length = obj.ToString().Length;
				if (num < length)
				{
					num = length;
				}
			}
			foreach (object obj2 in anObject.Keys)
			{
				if (!flag)
				{
					builder.Append(',');
					if (Json.PrettyPrint)
					{
						builder.Append('\n');
					}
				}
				Json.PrintPrettyTab(builder, Json.tabCounter, Json.tabSize);
				Json.SerializeString(obj2.ToString(), builder);
				builder.Append(':');
				if (Json.PrettyPrint)
				{
					Json.PrintPrettyTab(builder, num - obj2.ToString().Length + 2, 1);
				}
				if (!Json.SerializeValue(anObject[obj2], builder))
				{
					return false;
				}
				flag = false;
			}
			if (Json.PrettyPrint)
			{
				builder.Append('\n');
				Json.tabCounter--;
			}
			Json.PrintPrettyTab(builder, Json.tabCounter, Json.tabSize);
			builder.Append('}');
			return true;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00056098 File Offset: 0x00054298
		private static void PrintPrettyTab(StringBuilder builder, int count, int tabSize)
		{
			if (Json.PrettyPrint)
			{
				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < tabSize; j++)
					{
						builder.Append(' ');
					}
				}
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000560D0 File Offset: 0x000542D0
		private static bool SerializeArray(IList anArray, StringBuilder builder)
		{
			builder.Append('[');
			bool flag = true;
			foreach (object value in anArray)
			{
				if (!flag)
				{
					builder.Append(',');
					if (Json.PrettyPrint)
					{
						builder.Append(' ');
					}
				}
				if (!Json.SerializeValue(value, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append(']');
			return true;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00056158 File Offset: 0x00054358
		private static bool SerializeValue(object value, StringBuilder builder)
		{
			if (value == null)
			{
				builder.Append("null");
			}
			else if (value.GetType().IsArray)
			{
				Json.SerializeArray((IList)value, builder);
			}
			else if (value is string)
			{
				Json.SerializeString((string)value, builder);
			}
			else if (value is char)
			{
				Json.SerializeString(Convert.ToString((char)value), builder);
			}
			else if (value is IDictionary)
			{
				Json.SerializeObject((IDictionary)value, builder);
			}
			else if (value is IList)
			{
				Json.SerializeArray((IList)value, builder);
			}
			else if (value is bool)
			{
				builder.Append(((bool)value) ? "true" : "false");
			}
			else
			{
				if (!value.GetType().IsPrimitive)
				{
					return false;
				}
				if (value is long)
				{
					Json.SerializeNumber((long)value, builder);
				}
				else
				{
					Json.SerializeNumber(Convert.ToDouble(value), builder);
				}
			}
			return true;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00056258 File Offset: 0x00054458
		private static void SerializeString(string aString, StringBuilder builder)
		{
			builder.Append('"');
			foreach (char c in aString.ToCharArray())
			{
				if (c == '"')
				{
					builder.Append("\\\"");
				}
				else if (c == '\\')
				{
					builder.Append("\\\\");
				}
				else if (c == '\b')
				{
					builder.Append("\\b");
				}
				else if (c == '\f')
				{
					builder.Append("\\f");
				}
				else if (c == '\n')
				{
					builder.Append("\\n");
				}
				else if (c == '\r')
				{
					builder.Append("\\r");
				}
				else if (c == '\t')
				{
					builder.Append("\\t");
				}
				else
				{
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						builder.Append(c);
					}
					else
					{
						builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
					}
				}
			}
			builder.Append('"');
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005635F File Offset: 0x0005455F
		private static void SerializeNumber(double number, StringBuilder builder)
		{
			builder.Append(number.ToString());
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0005636F File Offset: 0x0005456F
		private static void SerializeNumber(long number, StringBuilder builder)
		{
			builder.Append(number.ToString());
		}

		// Token: 0x04000CAD RID: 3245
		private const int BUILDER_CAPACITY = 2000;

		// Token: 0x04000CAE RID: 3246
		private static int lastErrorIndex = -1;

		// Token: 0x04000CAF RID: 3247
		private static string lastDecode;

		// Token: 0x04000CB0 RID: 3248
		public static bool PrettyPrint = true;

		// Token: 0x04000CB1 RID: 3249
		private static int tabCounter = 0;

		// Token: 0x04000CB2 RID: 3250
		private static int tabSize = 2;

		// Token: 0x020003F8 RID: 1016
		private enum TOKEN
		{
			// Token: 0x04001839 RID: 6201
			NONE,
			// Token: 0x0400183A RID: 6202
			CURLY_OPEN,
			// Token: 0x0400183B RID: 6203
			CURLY_CLOSE,
			// Token: 0x0400183C RID: 6204
			SQUARED_OPEN,
			// Token: 0x0400183D RID: 6205
			SQUARED_CLOSE,
			// Token: 0x0400183E RID: 6206
			COLON,
			// Token: 0x0400183F RID: 6207
			COMMA,
			// Token: 0x04001840 RID: 6208
			STRING,
			// Token: 0x04001841 RID: 6209
			NUMBER,
			// Token: 0x04001842 RID: 6210
			TRUE,
			// Token: 0x04001843 RID: 6211
			FALSE,
			// Token: 0x04001844 RID: 6212
			NULL
		}
	}
}
