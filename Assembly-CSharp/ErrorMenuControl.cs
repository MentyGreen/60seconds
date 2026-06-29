using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class ErrorMenuControl : MonoBehaviour
{
	// Token: 0x06000BF9 RID: 3065 RVA: 0x00034BDB File Offset: 0x00032DDB
	private void Start()
	{
		dfControl component = base.GetComponent<dfControl>();
		component.BringToFront();
		component.Focus();
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00034BEE File Offset: 0x00032DEE
	private void Update()
	{
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00034BF0 File Offset: 0x00032DF0
	public void Exit()
	{
		base.gameObject.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00034C10 File Offset: 0x00032E10
	public void SendMessage()
	{
		MailMessage mailMessage = new MailMessage();
		mailMessage.From = new MailAddress("bugs@robotgentleman.com");
		mailMessage.To.Add("domx@robotgentleman.com");
		mailMessage.Subject = "60 Seconds! Report ";
		if (this._bugIssues)
		{
			MailMessage mailMessage2 = mailMessage;
			mailMessage2.Subject += "[BUG]";
		}
		if (this._visualIssues)
		{
			MailMessage mailMessage3 = mailMessage;
			mailMessage3.Subject += "[VISUALS]";
		}
		if (this._techIssues)
		{
			MailMessage mailMessage4 = mailMessage;
			mailMessage4.Subject += "[TECH]";
		}
		if (this._otherIssues)
		{
			MailMessage mailMessage5 = mailMessage;
			mailMessage5.Subject += "[OTHER]";
		}
		mailMessage.Body = this._body;
		SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
		smtpClient.Port = 587;
		smtpClient.Credentials = new NetworkCredential("bot@robotgentleman.com", "termopile237");
		smtpClient.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = ((object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
		smtpClient.Send(mailMessage);
		this.Exit();
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00034D33 File Offset: 0x00032F33
	// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00034D3B File Offset: 0x00032F3B
	public bool BugIssues
	{
		get
		{
			return this._bugIssues;
		}
		set
		{
			this._bugIssues = value;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00034D44 File Offset: 0x00032F44
	// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00034D4C File Offset: 0x00032F4C
	public bool VisualIssues
	{
		get
		{
			return this._visualIssues;
		}
		set
		{
			this._visualIssues = value;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00034D55 File Offset: 0x00032F55
	// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00034D5D File Offset: 0x00032F5D
	public bool TechIssues
	{
		get
		{
			return this._techIssues;
		}
		set
		{
			this._techIssues = value;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00034D66 File Offset: 0x00032F66
	// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00034D6E File Offset: 0x00032F6E
	public bool OtherIssues
	{
		get
		{
			return this._otherIssues;
		}
		set
		{
			this._otherIssues = value;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00034D77 File Offset: 0x00032F77
	// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00034D7F File Offset: 0x00032F7F
	public string Body
	{
		get
		{
			return this._body;
		}
		set
		{
			this._body = value;
		}
	}

	// Token: 0x04000638 RID: 1592
	private bool _bugIssues;

	// Token: 0x04000639 RID: 1593
	private bool _visualIssues;

	// Token: 0x0400063A RID: 1594
	private bool _techIssues;

	// Token: 0x0400063B RID: 1595
	private bool _otherIssues;

	// Token: 0x0400063C RID: 1596
	private string _body = string.Empty;
}
