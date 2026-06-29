using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
[dfMarkupTagInfo("img")]
public class dfMarkupTagImg : dfMarkupTag
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x00027E9B File Offset: 0x0002609B
	public dfMarkupTagImg() : base("img")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00027EAF File Offset: 0x000260AF
	public dfMarkupTagImg(dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00027EC0 File Offset: 0x000260C0
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.Owner == null)
		{
			Debug.LogError("Tag has no parent: " + ((this != null) ? this.ToString() : null));
			return;
		}
		style = this.applyStyleAttributes(style);
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"src"
		});
		if (dfMarkupAttribute == null)
		{
			return;
		}
		string value = dfMarkupAttribute.Value;
		dfMarkupBox dfMarkupBox = this.createImageBox(base.Owner.Atlas, value, style);
		if (dfMarkupBox == null)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"height"
		});
		if (dfMarkupAttribute2 != null)
		{
			vector.y = (float)dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, (int)dfMarkupBox.Size.y);
		}
		dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"width"
		});
		if (dfMarkupAttribute3 != null)
		{
			vector.x = (float)dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, (int)dfMarkupBox.Size.x);
		}
		if (vector.sqrMagnitude <= 1E-45f)
		{
			vector = dfMarkupBox.Size;
		}
		else if (vector.x <= 1E-45f)
		{
			vector.x = vector.y * (dfMarkupBox.Size.x / dfMarkupBox.Size.y);
		}
		else if (vector.y <= 1E-45f)
		{
			vector.y = vector.x * (dfMarkupBox.Size.y / dfMarkupBox.Size.x);
		}
		dfMarkupBox.Size = vector;
		dfMarkupBox.Baseline = (int)vector.y;
		container.AddChild(dfMarkupBox);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0002804C File Offset: 0x0002624C
	private dfMarkupStyle applyStyleAttributes(dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"valign"
		});
		if (dfMarkupAttribute != null)
		{
			style.VerticalAlign = dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute.Value);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute2 != null)
		{
			Color color = dfMarkupStyle.ParseColor(dfMarkupAttribute2.Value, base.Owner.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		return style;
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000280D0 File Offset: 0x000262D0
	private dfMarkupBox createImageBox(dfAtlas atlas, string source, dfMarkupStyle style)
	{
		if (source.ToLowerInvariant().StartsWith("http://"))
		{
			return null;
		}
		if (atlas != null && atlas[source] != null)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.inline, style);
			dfMarkupBoxSprite.LoadImage(atlas, source);
			return dfMarkupBoxSprite;
		}
		Texture texture = dfMarkupImageCache.Load(source);
		if (texture != null)
		{
			dfMarkupBoxTexture dfMarkupBoxTexture = new dfMarkupBoxTexture(this, dfMarkupDisplayType.inline, style);
			dfMarkupBoxTexture.LoadTexture(texture);
			return dfMarkupBoxTexture;
		}
		return null;
	}
}
