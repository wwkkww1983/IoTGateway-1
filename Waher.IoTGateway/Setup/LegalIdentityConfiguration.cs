﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Waher.Content;
using Waher.Content.Html;
using Waher.Content.Markdown;
using Waher.Events;
using Waher.IoTGateway.Setup.Legal;
using Waher.Networking.HTTP;
using Waher.Networking.XMPP;
using Waher.Networking.XMPP.Contracts;
using Waher.Persistence;
using Waher.Persistence.Attributes;
using Waher.Runtime.Language;
using Waher.Script;
using Waher.Security;
using Waher.Security.CallStack;

namespace Waher.IoTGateway.Setup
{
	/// <summary>
	/// Configures legal identity for the gateway.
	/// </summary>
	public class LegalIdentityConfiguration : SystemMultiStepConfiguration
	{
		internal static readonly Regex FromSaveUnsaved = new Regex(@"Waher[.]Persistence[.]Files[.]ObjectBTreeFile[.+]<SaveUnsaved>\w*[.]\w*",
			RegexOptions.Compiled | RegexOptions.Singleline);
		internal static readonly Regex FromUpdateObject = new Regex(@"Waher[.]Persistence[.]Files[.]ObjectBTreeFile[.+]<UpdateObject>\w*[.]\w*",
			RegexOptions.Compiled | RegexOptions.Singleline);
		internal static readonly Regex GatewayStartup = new Regex(@"Waher[.]IoTGateway[.]Gateway([.]Start|[.+]<Start>\w*[.]\w*)",
			RegexOptions.Compiled | RegexOptions.Singleline);
		internal static readonly Regex GetAttachment = new Regex(@"Waher[.]Networking[.]XMPP[.]Contracts[.]ContractsClient[.+]<GetAttachmentAsync>\w*[.]\w*",
			RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly object[] approvedSources = new object[]
		{
			"Waher.Persistence.NeuroLedger.NeuroLedgerProvider",
			typeof(Content.Markdown.Web.MarkdownToHtmlConverter),
			"Waher.IoTGateway.Setup.LegalIdentityConfiguration.UpdateClients",
			FromSaveUnsaved,
			FromUpdateObject,
			GatewayStartup
		};
		private static readonly object[] approvedContractClientSources = new object[]
		{
			"Waher.Service.IoTBroker.Legal.MFA.QuickLogin",
			"Waher.Service.Abc4Io.Model.Actions.Contract.SignContract",
			typeof(LegalIdentityConfiguration),
			GetAttachment
		};

		private static LegalIdentityConfiguration instance = null;
		private static LegalIdentity[] allIdentities = null;
		private static LegalIdentity[] approvedIdentities = null;

		private HttpResource applyLegalIdentity = null;
		private HttpResource contractAction = null;

		private bool useLegalIdentity = false;
		private bool protectWithPassword = false;
		private string firstName = string.Empty;
		private string middleName = string.Empty;
		private string lastName = string.Empty;
		private string personalNumber = string.Empty;
		private string address = string.Empty;
		private string address2 = string.Empty;
		private string postalCode = string.Empty;
		private string area = string.Empty;
		private string city = string.Empty;
		private string region = string.Empty;
		private string country = string.Empty;
		private AlternativeField[] altFields = null;
		private AlternativeField[] passwordHashes = null;

		/// <summary>
		/// Configures legal identity for the gateway.
		/// </summary>
		public LegalIdentityConfiguration()
		{
		}

		/// <summary>
		/// Instance of configuration object.
		/// </summary>
		public static LegalIdentityConfiguration Instance => instance;

		/// <summary>
		/// If the gateway will use a legal identity.
		/// </summary>
		[DefaultValue(false)]
		public bool UseLegalIdentity
		{
			get => this.useLegalIdentity;
			set => this.useLegalIdentity = value;
		}

		/// <summary>
		/// First Name
		/// </summary>
		[DefaultValueStringEmpty]
		public string FirstName
		{
			get => this.firstName;
			set => this.firstName = value;
		}

		/// <summary>
		/// Middle Name
		/// </summary>
		[DefaultValueStringEmpty]
		public string MiddleName
		{
			get => this.middleName;
			set => this.middleName = value;
		}

		/// <summary>
		/// Last Name
		/// </summary>
		[DefaultValueStringEmpty]
		public string LastName
		{
			get => this.lastName;
			set => this.lastName = value;
		}

		/// <summary>
		/// Personal number (or organizational number)
		/// </summary>
		[DefaultValueStringEmpty]
		public string PersonalNumber
		{
			get => this.personalNumber;
			set => this.personalNumber = value;
		}

		/// <summary>
		/// Address
		/// </summary>
		[DefaultValueStringEmpty]
		public string Address
		{
			get => this.address;
			set => this.address = value;
		}

		/// <summary>
		/// Address, 2nd row
		/// </summary>
		[DefaultValueStringEmpty]
		public string Address2
		{
			get => this.address2;
			set => this.address2 = value;
		}

		/// <summary>
		/// Postal Code (or zip code)
		/// </summary>
		[DefaultValueStringEmpty]
		public string PostalCode
		{
			get => this.postalCode;
			set => this.postalCode = value;
		}

		/// <summary>
		/// Area
		/// </summary>
		[DefaultValueStringEmpty]
		public string Area
		{
			get => this.area;
			set => this.area = value;
		}

		/// <summary>
		/// City
		/// </summary>
		[DefaultValueStringEmpty]
		public string City
		{
			get => this.city;
			set => this.city = value;
		}

		/// <summary>
		/// Region
		/// </summary>
		[DefaultValueStringEmpty]
		public string Region
		{
			get => this.region;
			set => this.region = value;
		}

		/// <summary>
		/// Country
		/// </summary>
		[DefaultValueStringEmpty]
		public string Country
		{
			get => this.country;
			set => this.country = value;
		}

		/// <summary>
		/// Alternative fields.
		/// </summary>
		[DefaultValueNull]
		public AlternativeField[] AlternativeFields
		{
			get => this.altFields;
			set => this.altFields = value;
		}

		/// <summary>
		/// If the legal identity should be protected with a password.
		/// </summary>
		[DefaultValue(false)]
		public bool ProtectWithPassword
		{
			get
			{
				return this.protectWithPassword;
			}

			set
			{
				if (this.protectWithPassword != value)
				{
					if (this.protectWithPassword)
						Assert.CallFromSource(approvedSources);

					this.protectWithPassword = value;
				}
			}
		}

		/// <summary>
		/// Password hash, if legal identity is protected by password.
		/// </summary>
		[DefaultValueNull]
		public AlternativeField[] PasswordHashes
		{
			get
			{
				if (!(this.passwordHashes is null))
					Assert.CallFromSource(approvedSources);

				return this.passwordHashes;
			}

			set
			{
				if (!(this.passwordHashes is null))
					Assert.CallFromSource(approvedSources);

				this.passwordHashes = value;
			}
		}

		/// <summary>
		/// Resource to be redirected to, to perform the configuration.
		/// </summary>
		public override string Resource => "/Settings/LegalIdentity.md";

		/// <summary>
		/// Priority of the setting. Configurations are sorted in ascending order.
		/// </summary>
		public override int Priority => 320;

		/// <summary>
		/// Sets the static instance of the configuration.
		/// </summary>
		/// <param name="Configuration">Configuration object</param>
		public override void SetStaticInstance(ISystemConfiguration Configuration)
		{
			instance = Configuration as LegalIdentityConfiguration;
		}

		/// <summary>
		/// Gets a title for the system configuration.
		/// </summary>
		/// <param name="Language">Current language.</param>
		/// <returns>Title string</returns>
		public override Task<string> Title(Language Language)
		{
			return Language.GetStringAsync(typeof(Gateway), 10, "Legal Identity");
		}

		/// <summary>
		/// Is called during startup to configure the system.
		/// </summary>
		public override Task ConfigureSystem()
		{
			if (this.useLegalIdentity && !(Gateway.ContractsClient is null))
				this.AddHandlers();

			return Task.CompletedTask;
		}

		private void AddHandlers()
		{
			if (!this.handlersAdded || Gateway.ContractsClient != this.prevClient)
			{
				this.handlersAdded = true;
				this.prevClient = Gateway.ContractsClient;

				Gateway.XmppClient.OnStateChanged += XmppClient_OnStateChanged;

				Gateway.ContractsClient.ContractDeleted += ContractsClient_ContractDeleted;
				Gateway.ContractsClient.ContractSigned += ContractsClient_ContractSigned;
				Gateway.ContractsClient.ContractUpdated += ContractsClient_ContractUpdated;
				Gateway.ContractsClient.IdentityUpdated += ContractsClient_IdentityUpdated;
				Gateway.ContractsClient.PetitionedIdentityResponseReceived += ContractsClient_PetitionedIdentityResponseReceived;
				Gateway.ContractsClient.PetitionedContractResponseReceived += ContractsClient_PetitionedContractResponseReceived;

				Gateway.ContractsClient.SetAllowedSources(approvedContractClientSources);

				if (Gateway.XmppClient.State == XmppState.Connected)
					this.GetLegalIdentities();
			}
		}

		private bool handlersAdded = false;
		private ContractsClient prevClient = null;

		private Task XmppClient_OnStateChanged(object Sender, XmppState NewState)
		{
			if (NewState == XmppState.Connected)
				this.GetLegalIdentities();

			return Task.CompletedTask;
		}

		private void GetLegalIdentities()
		{
			Gateway.ContractsClient?.GetLegalIdentities((sender, e) =>
			{
				if (e.Ok)
				{
					approvedIdentities = this.SetLegalIdentities(e.Identities, null, false);
					allIdentities = this.SetLegalIdentities(e.Identities, null, true);
				}

				return Task.CompletedTask;

			}, null);
		}

		private LegalIdentity[] SetLegalIdentities(LegalIdentity[] Identities, LegalIdentity Changed, bool All)
		{
			List<LegalIdentity> Result = new List<LegalIdentity>();
			LegalIdentity ID2;
			bool Added = false;

			foreach (LegalIdentity ID in Identities)
			{
				if (Changed is null || ID.Id != Changed.Id)
					ID2 = ID;
				else
				{
					ID2 = Changed;
					Added = true;
				}

				if (All || ID2.State == IdentityState.Approved)
					Result.Add(ID2);
			}

			if (!Added && !(Changed is null) && (All || Changed.State == IdentityState.Approved))
				Result.Add(Changed);

			Result.Sort((i1, i2) => Math.Sign((i2.Created - i1.Created).Ticks));

			return Result.ToArray();
		}

		/// <summary>
		/// All Legal Identities associated with account.
		/// </summary>
		public static LegalIdentity[] AllIdentities
		{
			get
			{
				Assert.CallFromSource(approvedSources);
				return allIdentities;
			}
		}

		/// <summary>
		/// Approved Legal Identities associated with account.
		/// </summary>
		public static LegalIdentity[] ApprovedIdentities
		{
			get
			{
				Assert.CallFromSource(approvedSources);
				return approvedIdentities;
			}
		}

		private Task ContractsClient_IdentityUpdated(object Sender, LegalIdentityEventArgs e)
		{
			LegalIdentity ID = e.Identity;

			List<KeyValuePair<string, object>> Tags = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("State", ID.State),
				new KeyValuePair<string, object>("Provider", ID.Provider),
				new KeyValuePair<string, object>("Created", ID.Created),
				new KeyValuePair<string, object>("Updated", ID.Updated),
				new KeyValuePair<string, object>("From", ID.From),
				new KeyValuePair<string, object>("KeyAlgorithm", ID.ClientKeyName),
				new KeyValuePair<string, object>("PublicKey", Convert.ToBase64String(ID.ClientPubKey))
			};

			foreach (Property P in ID.Properties)
				Tags.Add(new KeyValuePair<string, object>(P.Name, P.Value));

			Log.Notice("Legal Identity updated.", e.Identity.Id, Tags.ToArray());

			this.UpdateClients(ID);

			return Task.CompletedTask;
		}

		private void UpdateClients(LegalIdentity ID)
		{
			allIdentities = this.SetLegalIdentities(allIdentities, ID, true);
			approvedIdentities = this.SetLegalIdentities(approvedIdentities, ID, false);

			this.UpdateClients();
		}

		private void UpdateClients()
		{
			string[] TabIDs;

			if (Gateway.Configuring)
				TabIDs = ClientEvents.GetTabIDs();
			else
				TabIDs = ClientEvents.GetTabIDsForLocation("/Settings/LegalIdentity.md");

			if (TabIDs.Length > 0)
			{
				string FileName = Path.Combine(Gateway.RootFolder, "Settings", "LegalIdentities.md");
				if (File.Exists(FileName))
				{
					string Markdown = File.ReadAllText(FileName);
					Variables v = new Variables(new Variable("Config", this));
					MarkdownDocument Doc = new MarkdownDocument(Markdown, new MarkdownSettings(Gateway.Emoji1_24x24, true, v));
					string HTML = Doc.GenerateHTML();
					HTML = HtmlDocument.GetBody(HTML);

					ClientEvents.PushEvent(TabIDs, "UpdateIdentityTable", HTML);
				}
			}
		}

		private Task ContractsClient_ContractSigned(object Sender, ContractSignedEventArgs e)
		{
			Log.Notice("Smart contract signed.", e.ContractId, e.LegalId);
			return Task.CompletedTask;
		}

		private Task ContractsClient_ContractUpdated(object Sender, ContractReferenceEventArgs e)
		{
			Log.Notice("Smart contract updated.", e.ContractId);
			return Task.CompletedTask;
		}

		private Task ContractsClient_ContractDeleted(object Sender, ContractReferenceEventArgs e)
		{
			Log.Notice("Smart contract deleted.", e.ContractId);
			return Task.CompletedTask;
		}

		/// <summary>
		/// Initializes the setup object.
		/// </summary>
		/// <param name="WebServer">Current Web Server object.</param>
		public override Task InitSetup(HttpServer WebServer)
		{
			this.applyLegalIdentity = WebServer.Register("/Settings/ApplyLegalIdentity", null, this.ApplyLegalIdentity, true, false, true);
			this.contractAction = WebServer.Register("/Settings/ContractAction", null, this.ContractAction, true, false, true);

			return base.InitSetup(WebServer);
		}

		/// <summary>
		/// Unregisters the setup object.
		/// </summary>
		/// <param name="WebServer">Current Web Server object.</param>
		public override Task UnregisterSetup(HttpServer WebServer)
		{
			WebServer.Unregister(this.applyLegalIdentity);
			WebServer.Unregister(this.contractAction);

			return base.UnregisterSetup(WebServer);
		}

		/// <summary>
		/// Waits for the user to provide configuration.
		/// </summary>
		/// <param name="WebServer">Current Web Server object.</param>
		/// <returns>If all system configuration objects must be reloaded from the database.</returns>
		public override Task<bool> SetupConfiguration(HttpServer WebServer)
		{
			if (!this.Complete && Gateway.XmppClient.State == XmppState.Offline)
				Gateway.XmppClient?.Connect();

			this.AddHandlers();

			return base.SetupConfiguration(WebServer);
		}

		/// <summary>
		/// Minimum required privilege for a user to be allowed to change the configuration defined by the class.
		/// </summary>
		protected override string ConfigPrivilege => "Admin.Legal.ID";

		private async Task ApplyLegalIdentity(HttpRequest Request, HttpResponse Response)
		{
			Gateway.AssertUserAuthenticated(Request, this.ConfigPrivilege);

			if (!Request.HasData)
				throw new BadRequestException();

			object Obj = Request.DecodeData();
			if (!(Obj is Dictionary<string, object> Parameters))
				throw new BadRequestException();

			if (!Parameters.TryGetValue("protectWithPassword", out Obj) || !(Obj is bool ProtectWithPassword) ||
				!Parameters.TryGetValue("password", out Obj) || !(Obj is string Password) ||
				!Parameters.TryGetValue("password2", out Obj) || !(Obj is string Password2) ||
				!Parameters.TryGetValue("firstName", out Obj) || !(Obj is string FirstName) ||
				!Parameters.TryGetValue("middleName", out Obj) || !(Obj is string MiddleName) ||
				!Parameters.TryGetValue("lastName", out Obj) || !(Obj is string LastName) ||
				!Parameters.TryGetValue("pNr", out Obj) || !(Obj is string PNr) ||
				!Parameters.TryGetValue("address", out Obj) || !(Obj is string Address) ||
				!Parameters.TryGetValue("address2", out Obj) || !(Obj is string Address2) ||
				!Parameters.TryGetValue("postalCode", out Obj) || !(Obj is string PostalCode) ||
				!Parameters.TryGetValue("area", out Obj) || !(Obj is string Area) ||
				!Parameters.TryGetValue("city", out Obj) || !(Obj is string City) ||
				!Parameters.TryGetValue("region", out Obj) || !(Obj is string Region) ||
				!Parameters.TryGetValue("country", out Obj) || !(Obj is string Country))
			{
				throw new BadRequestException();
			}

			if (ProtectWithPassword)
			{
				if (string.IsNullOrEmpty(Password))
					throw new BadRequestException("Enter a password and try again.");

				if (Password != Password2)
					throw new BadRequestException("Passwords do not match. Retype, and try again.");
			}

			List<AlternativeField> AlternativeFields = new List<AlternativeField>();

			if (Parameters.TryGetValue("alternative", out Obj) && Obj is Dictionary<string, object> Alternative)
			{
				foreach (KeyValuePair<string, object> P in Alternative)
				{
					switch (P.Key.ToUpper())
					{
						case "FIRST":
						case "MIDDLE":
						case "LAST":
						case "PNR":
						case "ADDR":
						case "ADDR2":
						case "ZIP":
						case "AREA":
						case "CITY":
						case "REGION":
						case "COUNTRY":
							throw new BadRequestException("The following alternative field name is not allowed: " + P.Key);

						default:
							AlternativeFields.Add(new AlternativeField(P.Key, P.Value.ToString()));
							break;
					}
				}
			}

			string TabID = Request.Header["X-TabID"];
			if (string.IsNullOrEmpty(TabID))
				throw new BadRequestException();

			this.useLegalIdentity = true;
			this.firstName = FirstName;
			this.middleName = MiddleName;
			this.lastName = LastName;
			this.personalNumber = PNr;
			this.address = Address;
			this.address2 = Address2;
			this.postalCode = PostalCode;
			this.area = Area;
			this.city = City;
			this.region = Region;
			this.country = Country;
			this.altFields = AlternativeFields.ToArray();

			Response.StatusCode = 200;

			await Gateway.ContractsClient.GenerateNewKeys();
			await Gateway.ContractsClient.Apply(this.GetProperties(), this.ApplyResponse, new object[] { Password, TabID, ProtectWithPassword });
		}

		private async Task ApplyResponse(object Sender, LegalIdentityEventArgs e)
		{
			object[] P = (object[])e.State;
			string Password = (string)P[0];
			string TabID = (string)P[1];
			bool ProtectWithPassword = (bool)P[2];

			if (e.Ok)
			{
				this.protectWithPassword = ProtectWithPassword;

				if (ProtectWithPassword)
				{
					Dictionary<string, AlternativeField> ById = new Dictionary<string, AlternativeField>();

					if (!(this.passwordHashes is null))
					{
						foreach (AlternativeField H in this.passwordHashes)
							ById[H.Key] = H;
					}

					ById[e.Identity.Id] = new AlternativeField(e.Identity.Id, this.CalcPasswordhash(e.Identity, Password));

					AlternativeField[] Hashes = new AlternativeField[ById.Count];
					ById.Values.CopyTo(Hashes, 0);

					this.passwordHashes = Hashes;
				}

				this.Step = 1;
				await Database.Update(this);

				await ClientEvents.PushEvent(new string[] { TabID }, "ApplicationOK", string.Empty);

				this.UpdateClients(e.Identity);
			}
			else
				await ClientEvents.PushEvent(new string[] { TabID }, "ApplicationError", e.ErrorText);
		}

		private string CalcPasswordhash(LegalIdentity ID, string Password)
		{
			StringBuilder sb = new StringBuilder();
			SortedDictionary<string, string> Sorted = new SortedDictionary<string, string>();

			foreach (Property P in ID.Properties)
			{
				if (Sorted.TryGetValue(P.Name, out string s))
				{
					s += ";" + P.Value;
					Sorted[P.Name] = s;
				}
				else
					Sorted[P.Name] = P.Value;
			}

			bool First = true;

			foreach (KeyValuePair<string, string> P in Sorted)
			{
				if (First)
					First = false;
				else
					sb.Append(',');

				sb.Append(P.Key);
				sb.Append('=');
				sb.Append(P.Value);
			}

			byte[] Digest = Hashes.ComputeHMACSHA256Hash(Encoding.UTF8.GetBytes(Password), Encoding.UTF8.GetBytes(sb.ToString()));

			return Convert.ToBase64String(Digest);
		}

		private Property[] GetProperties()
		{
			List<Property> Properties = new List<Property>();

			if (!string.IsNullOrEmpty(this.firstName))
				Properties.Add(new Property("FIRST", this.firstName));

			if (!string.IsNullOrEmpty(this.middleName))
				Properties.Add(new Property("MIDDLE", this.middleName));

			if (!string.IsNullOrEmpty(this.lastName))
				Properties.Add(new Property("LAST", this.lastName));

			if (!string.IsNullOrEmpty(this.firstName))
				Properties.Add(new Property("PNR", this.personalNumber));

			if (!string.IsNullOrEmpty(this.address))
				Properties.Add(new Property("ADDR", this.address));

			if (!string.IsNullOrEmpty(this.address2))
				Properties.Add(new Property("ADDR2", this.address2));

			if (!string.IsNullOrEmpty(this.postalCode))
				Properties.Add(new Property("ZIP", this.postalCode));

			if (!string.IsNullOrEmpty(this.area))
				Properties.Add(new Property("AREA", this.area));

			if (!string.IsNullOrEmpty(this.city))
				Properties.Add(new Property("CITY", this.city));

			if (!string.IsNullOrEmpty(this.region))
				Properties.Add(new Property("REGION", this.region));

			if (!string.IsNullOrEmpty(this.country))
				Properties.Add(new Property("COUNTRY", this.country));

			if (!(this.altFields is null))
			{
				foreach (AlternativeField F in this.altFields)
					Properties.Add(new Property(F.Key, F.Value));
			}

			return Properties.ToArray();
		}

		private async Task ContractAction(HttpRequest Request, HttpResponse Response)
		{
			Gateway.AssertUserAuthenticated(Request, this.ConfigPrivilege);

			if (!Request.HasData)
				throw new BadRequestException("No content.");

			string Password;
			object Obj = Request.DecodeData();
			if (!(Obj is Dictionary<string, object> Parameters))
				throw new BadRequestException("Invalid content.");

			if (!Parameters.TryGetValue("requestId", out Obj) || !(Obj is string RequestId) ||
				!Parameters.TryGetValue("sign", out Obj) || !(Obj is bool Sign) ||
				!Parameters.TryGetValue("protect", out Obj) || !(Obj is bool Protect))
			{
				throw new BadRequestException("Invalid request.");
			}

			if (Protect)
			{
				if (!Parameters.TryGetValue("password", out Obj) || !(Obj is string s))
					throw new BadRequestException("No password.");

				Password = s;
			}
			else
				Password = string.Empty;

			ContractSignatureRequest SignatureRequest = await Database.TryLoadObject<ContractSignatureRequest>(RequestId);
			if (SignatureRequest is null)
				throw new NotFoundException("Content Signature Request not found.");

			if (SignatureRequest.Signed.HasValue)
				throw new BadRequestException("Contract has already been signed.");

			if (Protect)
			{
				if (!HasApprovedLegalIdentities)
					throw new BadRequestException("No approved legal identity found with which to sign the contract.");

				string Id = this.GetPasswordLegalId(Password);
				if (string.IsNullOrEmpty(Id))
					throw new BadRequestException("Invalid password.");
			}
			else if (this.protectWithPassword)
				throw new BadRequestException("Legal identities protected with password.");

			if (Sign)
			{
				SignatureRequest.Contract = await Gateway.ContractsClient.SignContractAsync(SignatureRequest.Contract, SignatureRequest.Role, false);
				SignatureRequest.Signed = DateTime.Now;
				await Database.Update(SignatureRequest);
			}
			else
				await Database.Delete(SignatureRequest);

			Response.StatusCode = 200;
			Response.ContentType = "application/json";
			await Response.Write(JSON.Encode(Sign, false));
			await Response.SendResponse();
		}

		/// <summary>
		/// If there are approved legal identities configured.
		/// </summary>
		public static bool HasApprovedLegalIdentities
		{
			get => !(approvedIdentities is null) && approvedIdentities.Length > 0;
		}

		/// <summary>
		/// Latest approved Legal Identity ID.
		/// </summary>
		public static string LatestApprovedLegalIdentityId
		{
			get
			{
				if (!HasApprovedLegalIdentities)
					throw new NotFoundException("Gateway has no approved legal identity.");

				LegalIdentity Latest = null;

				foreach (LegalIdentity Identity in approvedIdentities)
				{
					if (Latest is null || Identity.Created > Latest.Created)
						Latest = Identity;
				}

				return Latest?.Id;
			}
		}

		/// <summary>
		/// Gets the legal identity that corresponds to a given password, from the corresponding hash digests.
		/// </summary>
		/// <param name="Password">Password</param>
		/// <returns>Legal Identity ID, if found, or null otherwise</returns>
		public string GetPasswordLegalId(string Password)
		{
			if (approvedIdentities is null || approvedIdentities.Length == 0)
				return null;

			foreach (LegalIdentity ID in approvedIdentities)
			{
				string H = this.CalcPasswordhash(ID, Password);

				foreach (AlternativeField F in passwordHashes)
				{
					if (F.Key == ID.Id)
					{
						if (F.Value == H)
							return F.Key;

						break;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Petitions information about a legal identity from its owner.
		/// </summary>
		/// <param name="LegalId">ID of petitioned legal identity.</param>
		/// <param name="PetitionId">A petition ID string used to identity request when response is returned.</param>
		/// <param name="Purpose">String containing purpose of petition. Can be seen by owner, as well as the legal identity of the current machine.</param>
		/// <param name="Callback">Method to call when response is returned. If timed out, or declined, identity will be null.</param>
		/// <param name="Timeout">Maximum time to wait for a response.</param>
		/// <returns>If a legal identity was found that could be used to sign the petition.</returns>
		public Task<bool> PetitionLegalIdentity(string LegalId, string PetitionId, string Purpose,
			LegalIdentityPetitionResponseEventHandler Callback, TimeSpan Timeout)
		{
			if (this.protectWithPassword)
				throw new ForbiddenException("Petitioning legal identities is protected using passwords on this machine.");

			return this.PetitionLegalIdentity(LegalId, PetitionId, Purpose, string.Empty, Callback, Timeout);
		}

		/// <summary>
		/// Petitions information about a legal identity from its owner.
		/// </summary>
		/// <param name="LegalId">ID of petitioned legal identity.</param>
		/// <param name="PetitionId">A petition ID string used to identity request when response is returned.</param>
		/// <param name="Purpose">String containing purpose of petition. Can be seen by owner, as well as the legal identity of the current machine.</param>
		/// <param name="Password">Password of legal identity on the current machine used to sign the petition.</param>
		/// <param name="Callback">Method to call when response is returned. If timed out, or declined, identity will be null.</param>
		/// <param name="Timeout">Maximum time to wait for a response.</param>
		/// <returns>If a legal identity was found that could be used to sign the petition, and the password matched (if protected by password).</returns>
		public async Task<bool> PetitionLegalIdentity(string LegalId, string PetitionId, string Purpose, string Password,
			LegalIdentityPetitionResponseEventHandler Callback, TimeSpan Timeout)
		{
			if (!HasApprovedLegalIdentities)
				return false;

			if (this.protectWithPassword)
			{
				string Id = this.GetPasswordLegalId(Password);
				if (string.IsNullOrEmpty(Id))
					return false;
			}

			lock (this.identityPetitionCallbackMethods)
			{
				if (this.identityPetitionCallbackMethods.ContainsKey(PetitionId))
					throw new ArgumentException("Petition ID already used.", nameof(PetitionId));

				this.identityPetitionCallbackMethods[PetitionId] = Callback;
			}

			try
			{
				await Gateway.ContractsClient.PetitionIdentityAsync(LegalId, PetitionId, Purpose);

				Gateway.ScheduleEvent((P) =>
				{
					LegalIdentityPetitionResponseEventArgs e = new LegalIdentityPetitionResponseEventArgs(null, null, (string)P, false);
					this.ContractsClient_PetitionedIdentityResponseReceived(Gateway.ContractsClient, e);
				}, DateTime.Now.Add(Timeout), PetitionId);
			}
			catch (Exception ex)
			{
				lock (this.identityPetitionCallbackMethods)
				{
					this.identityPetitionCallbackMethods.Remove(PetitionId);
				}

				ExceptionDispatchInfo.Capture(ex).Throw();
			}

			return true;
		}

		/// <summary>
		/// Petitions information about a smart contract from its owner.
		/// </summary>
		/// <param name="ContractId">ID of petitioned smart contract.</param>
		/// <param name="PetitionId">A petition ID string used to contract request when response is returned.</param>
		/// <param name="Purpose">String containing purpose of petition. Can be seen by owner, as well as the smart contract of the current machine.</param>
		/// <param name="Callback">Method to call when response is returned. If timed out, or declined, identity will be null.</param>
		/// <param name="Timeout">Maximum time to wait for a response.</param>
		/// <returns>If a smart contract was found that could be used to sign the petition.</returns>
		public Task<bool> PetitionContract(string ContractId, string PetitionId, string Purpose,
			ContractPetitionResponseEventHandler Callback, TimeSpan Timeout)
		{
			if (this.protectWithPassword)
				throw new ForbiddenException("Petitioning legal identities is protected using passwords on this machine.");

			return this.PetitionContract(ContractId, PetitionId, Purpose, string.Empty, Callback, Timeout);
		}

		/// <summary>
		/// Petitions information about a smart contract from its owner.
		/// </summary>
		/// <param name="ContractId">ID of petitioned smart contract.</param>
		/// <param name="PetitionId">A petition ID string used to contract request when response is returned.</param>
		/// <param name="Purpose">String containing purpose of petition. Can be seen by owner, as well as the smart contract of the current machine.</param>
		/// <param name="Password">Password of smart contract on the current machine used to sign the petition.</param>
		/// <param name="Callback">Method to call when response is returned. If timed out, or declined, identity will be null.</param>
		/// <param name="Timeout">Maximum time to wait for a response.</param>
		/// <returns>If a smart contract was found that could be used to sign the petition, and the password matched (if protected by password).</returns>
		public async Task<bool> PetitionContract(string ContractId, string PetitionId, string Purpose, string Password,
			ContractPetitionResponseEventHandler Callback, TimeSpan Timeout)
		{
			if (!HasApprovedLegalIdentities)
				return false;

			if (this.protectWithPassword)
			{
				string Id = this.GetPasswordLegalId(Password);
				if (string.IsNullOrEmpty(Id))
					return false;
			}

			lock (this.contractPetitionCallbackMethods)
			{
				if (this.contractPetitionCallbackMethods.ContainsKey(PetitionId))
					throw new ArgumentException("Petition ID already used.", nameof(PetitionId));

				this.contractPetitionCallbackMethods[PetitionId] = Callback;
			}

			try
			{
				await Gateway.ContractsClient.PetitionContractAsync(ContractId, PetitionId, Purpose);

				Gateway.ScheduleEvent((P) =>
				{
					ContractPetitionResponseEventArgs e = new ContractPetitionResponseEventArgs(null, null, (string)P, false);
					this.ContractsClient_PetitionedContractResponseReceived(Gateway.ContractsClient, e);
				}, DateTime.Now.Add(Timeout), PetitionId);
			}
			catch (Exception ex)
			{
				lock (this.contractPetitionCallbackMethods)
				{
					this.contractPetitionCallbackMethods.Remove(PetitionId);
				}

				ExceptionDispatchInfo.Capture(ex).Throw();
			}

			return true;
		}

		private readonly Dictionary<string, LegalIdentityPetitionResponseEventHandler> identityPetitionCallbackMethods = new Dictionary<string, LegalIdentityPetitionResponseEventHandler>();
		private readonly Dictionary<string, ContractPetitionResponseEventHandler> contractPetitionCallbackMethods = new Dictionary<string, ContractPetitionResponseEventHandler>();

		private Task ContractsClient_PetitionedIdentityResponseReceived(object Sender, LegalIdentityPetitionResponseEventArgs e)
		{
			LegalIdentityPetitionResponseEventHandler Callback;
			string PetitionId = e.PetitionId;

			lock (this.identityPetitionCallbackMethods)
			{
				if (!this.identityPetitionCallbackMethods.TryGetValue(PetitionId, out Callback))
					return Task.CompletedTask;
				else
					this.identityPetitionCallbackMethods.Remove(PetitionId);
			}

			try
			{
				Callback(Sender, e);
			}
			catch (Exception ex)
			{
				Log.Critical(ex);
			}

			return Task.CompletedTask;
		}

		private Task ContractsClient_PetitionedContractResponseReceived(object Sender, ContractPetitionResponseEventArgs e)
		{
			ContractPetitionResponseEventHandler Callback;
			string PetitionId = e.PetitionId;

			lock (this.contractPetitionCallbackMethods)
			{
				if (!this.contractPetitionCallbackMethods.TryGetValue(PetitionId, out Callback))
					return Task.CompletedTask;
				else
					this.contractPetitionCallbackMethods.Remove(PetitionId);
			}

			try
			{
				Callback(Sender, e);
			}
			catch (Exception ex)
			{
				Log.Critical(ex);
			}

			return Task.CompletedTask;
		}

	}
}
