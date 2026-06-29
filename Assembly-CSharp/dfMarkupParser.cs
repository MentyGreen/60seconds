using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

// Token: 0x0200008A RID: 138
public class dfMarkupParser
{
	// Token: 0x060008A5 RID: 2213 RVA: 0x000262CC File Offset: 0x000244CC
	static dfMarkupParser()
	{
		RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant;
		dfMarkupParser.TAG_PATTERN = new Regex("(\\<\\/?)(?<tag>[a-zA-Z0-9$_]+)(\\s(?<attr>.+?))?([\\/]*\\>)", options);
		dfMarkupParser.ATTR_PATTERN = new Regex("(?<key>[a-zA-Z0-9$_]+)=(?<value>(\"((\\\\\")|\\\\\\\\|[^\"\\n])*\")|('((\\\\')|\\\\\\\\|[^'\\n])*')|\\d+|\\w+)", options);
		dfMarkupParser.STYLE_PATTERN = new Regex("(?<key>[a-zA-Z0-9\\-]+)(\\s*\\:\\s*)(?<value>[^;]+)", options);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00026331 File Offset: 0x00024531
	public static dfList<dfMarkupElement> Parse(dfRichTextLabel owner, string source)
	{
		dfMarkupParser.parserInstance.owner = owner;
		return dfMarkupParser.parserInstance.parseMarkup(source);
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0002634C File Offset: 0x0002454C
	private dfList<dfMarkupElement> parseMarkup(string source)
	{
		Queue<dfMarkupElement> queue = new Queue<dfMarkupElement>();
		MatchCollection matchCollection = dfMarkupParser.TAG_PATTERN.Matches(source);
		int num = 0;
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			if (match.Index > num)
			{
				dfMarkupString item = new dfMarkupString(source.Substring(num, match.Index - num));
				queue.Enqueue(item);
			}
			num = match.Index + match.Length;
			queue.Enqueue(this.parseTag(match));
		}
		if (num < source.Length)
		{
			dfMarkupString item2 = new dfMarkupString(source.Substring(num));
			queue.Enqueue(item2);
		}
		return this.processTokens(queue);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x000263F4 File Offset: 0x000245F4
	private dfList<dfMarkupElement> processTokens(Queue<dfMarkupElement> tokens)
	{
		dfList<dfMarkupElement> dfList = dfList<dfMarkupElement>.Obtain();
		while (tokens.Count > 0)
		{
			dfList.Add(this.parseElement(tokens));
		}
		for (int i = 0; i < dfList.Count; i++)
		{
			if (dfList[i] is dfMarkupTag)
			{
				((dfMarkupTag)dfList[i]).Owner = this.owner;
			}
		}
		return dfList;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00026458 File Offset: 0x00024658
	private dfMarkupElement parseElement(Queue<dfMarkupElement> tokens)
	{
		dfMarkupElement dfMarkupElement = tokens.Dequeue();
		if (dfMarkupElement is dfMarkupString)
		{
			return ((dfMarkupString)dfMarkupElement).SplitWords();
		}
		dfMarkupTag dfMarkupTag = (dfMarkupTag)dfMarkupElement;
		if (dfMarkupTag.IsClosedTag || dfMarkupTag.IsEndTag)
		{
			return this.refineTag(dfMarkupTag);
		}
		while (tokens.Count > 0)
		{
			dfMarkupElement dfMarkupElement2 = this.parseElement(tokens);
			if (dfMarkupElement2 is dfMarkupTag)
			{
				dfMarkupTag dfMarkupTag2 = (dfMarkupTag)dfMarkupElement2;
				if (dfMarkupTag2.IsEndTag)
				{
					dfMarkupTag2.TagName == dfMarkupTag.TagName;
					return this.refineTag(dfMarkupTag);
				}
			}
			dfMarkupTag.AddChildNode(dfMarkupElement2);
		}
		return this.refineTag(dfMarkupTag);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000264F0 File Offset: 0x000246F0
	private dfMarkupTag refineTag(dfMarkupTag original)
	{
		if (original.IsEndTag)
		{
			return original;
		}
		if (dfMarkupParser.tagTypes == null)
		{
			dfMarkupParser.tagTypes = new Dictionary<string, Type>();
			foreach (Type type in this.getAssemblyTypes())
			{
				if (typeof(dfMarkupTag).IsAssignableFrom(type))
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(dfMarkupTagInfoAttribute), true);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						for (int j = 0; j < customAttributes.Length; j++)
						{
							string tagName = ((dfMarkupTagInfoAttribute)customAttributes[j]).TagName;
							dfMarkupParser.tagTypes[tagName] = type;
						}
					}
				}
			}
		}
		if (dfMarkupParser.tagTypes.ContainsKey(original.TagName))
		{
			return (dfMarkupTag)Activator.CreateInstance(dfMarkupParser.tagTypes[original.TagName], new object[]
			{
				original
			});
		}
		return original;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000265C3 File Offset: 0x000247C3
	private Type[] getAssemblyTypes()
	{
		return Assembly.GetExecutingAssembly().GetExportedTypes();
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x000265D0 File Offset: 0x000247D0
	private dfMarkupElement parseTag(Match tag)
	{
		string text = tag.Groups["tag"].Value.ToLowerInvariant();
		if (tag.Value.StartsWith("</"))
		{
			return new dfMarkupTag(text)
			{
				IsEndTag = true
			};
		}
		dfMarkupTag dfMarkupTag = new dfMarkupTag(text);
		string value = tag.Groups["attr"].Value;
		MatchCollection matchCollection = dfMarkupParser.ATTR_PATTERN.Matches(value);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string value2 = match.Groups["key"].Value;
			string text2 = dfMarkupEntity.Replace(match.Groups["value"].Value);
			if (text2.StartsWith("\""))
			{
				text2 = text2.Trim(new char[]
				{
					'"'
				});
			}
			else if (text2.StartsWith("'"))
			{
				text2 = text2.Trim(new char[]
				{
					'\''
				});
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (value2 == "style")
				{
					this.parseStyleAttribute(dfMarkupTag, text2);
				}
				else
				{
					dfMarkupTag.Attributes.Add(new dfMarkupAttribute(value2, text2));
				}
			}
		}
		if (tag.Value.EndsWith("/>") || text == "br" || text == "img")
		{
			dfMarkupTag.IsClosedTag = true;
		}
		return dfMarkupTag;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00026748 File Offset: 0x00024948
	private void parseStyleAttribute(dfMarkupTag element, string text)
	{
		MatchCollection matchCollection = dfMarkupParser.STYLE_PATTERN.Matches(text);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string name = match.Groups["key"].Value.ToLowerInvariant();
			string value = match.Groups["value"].Value;
			element.Attributes.Add(new dfMarkupAttribute(name, value));
		}
	}

	// Token: 0x04000422 RID: 1058
	private static Regex TAG_PATTERN = null;

	// Token: 0x04000423 RID: 1059
	private static Regex ATTR_PATTERN = null;

	// Token: 0x04000424 RID: 1060
	private static Regex STYLE_PATTERN = null;

	// Token: 0x04000425 RID: 1061
	private static Dictionary<string, Type> tagTypes = null;

	// Token: 0x04000426 RID: 1062
	private static dfMarkupParser parserInstance = new dfMarkupParser();

	// Token: 0x04000427 RID: 1063
	private dfRichTextLabel owner;
}
