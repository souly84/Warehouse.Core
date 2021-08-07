using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Warehouse.Core.Tests
{
    [OdooTableName("res.users")]
    [System.Text.Json.Serialization.JsonConverter(typeof(OdooModelConverter))]
    public class OdooUserDto : IOdooModel
    {

        // res.partner
        // required
        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        // required
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("new_password")]
        public string NewPassword { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("active_partner")]
        public bool? ActivePartner { get; set; }

        // ir.actions.actions
        [JsonProperty("action_id")]
        public long? ActionId { get; set; }

        // res.groups
        [JsonProperty("groups_id")]
        public long[] GroupsId { get; set; }

        // res.users.log
        [JsonProperty("log_ids")]
        public long[] LogIds { get; set; }

        [JsonProperty("login_date")]
        public DateTime? LoginDate { get; set; }

        [JsonProperty("share")]
        public bool? Share { get; set; }

        [JsonProperty("companies_count")]
        public int? CompaniesCount { get; set; }

        [JsonProperty("tz_offset")]
        public string TzOffset { get; set; }

        // res.company
        // required
        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        // res.company
        [JsonProperty("company_ids")]
        public long[] CompanyIds { get; set; }

        [JsonProperty("accesses_count")]
        public int? AccessesCount { get; set; }

        [JsonProperty("rules_count")]
        public int? RulesCount { get; set; }

        [JsonProperty("groups_count")]
        public int? GroupsCount { get; set; }

        // res.users.apikeys
        [JsonProperty("api_key_ids")]
        public long[] ApiKeyIds { get; set; }

        [JsonProperty("state")]
        public StatusResUsersOdooEnum? State { get; set; }

        [JsonProperty("im_status")]
        public string ImStatus { get; set; }

        // required
        [JsonProperty("notification_type")]
        public NotificationResUsersOdooEnum NotificationType { get; set; }

        [JsonProperty("is_moderator")]
        public bool? IsModerator { get; set; }

        [JsonProperty("moderation_counter")]
        public int? ModerationCounter { get; set; }

        // mail.channel
        [JsonProperty("moderation_channel_ids")]
        public long[] ModerationChannelIds { get; set; }

        [JsonProperty("totp_enabled")]
        public bool? TotpEnabled { get; set; }

        // resource.resource
        [JsonProperty("resource_ids")]
        public long[] ResourceIds { get; set; }

        // resource.calendar
        [JsonProperty("resource_calendar_id")]
        public long? ResourceCalendarId { get; set; }

        [JsonProperty("odoobot_state")]
        public OdoobotStatusResUsersOdooEnum? OdoobotState { get; set; }

        [JsonProperty("odoobot_failed")]
        public bool? OdoobotFailed { get; set; }

        [JsonProperty("oauth_uid")]
        public string OauthUid { get; set; }

        [JsonProperty("odoo_com_uid")]
        public string OdooComUid { get; set; }

        // crm.team
        [JsonProperty("sale_team_id")]
        public long? SaleTeamId { get; set; }

        // stock.warehouse
        [JsonProperty("property_warehouse_id")]
        public long? PropertyWarehouseId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        // res.users
        [JsonProperty("create_uid")]
        public long? CreateUid { get; set; }

        [JsonProperty("create_date")]
        public DateTime? CreateDate { get; set; }

        // res.users
        [JsonProperty("write_uid")]
        public long? WriteUid { get; set; }

        [JsonProperty("write_date")]
        public DateTime? WriteDate { get; set; }

        [JsonProperty("__last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        // res.partner.title
        [JsonProperty("title")]
        public long? Title { get; set; }

        // res.partner
        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }

        [JsonProperty("parent_name")]
        public string ParentName { get; set; }

        // res.partner
        [JsonProperty("child_ids")]
        public long[] ChildIds { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("lang")]
        public LanguageResUsersOdooEnum? Lang { get; set; }

        [JsonProperty("active_lang_count")]
        public int? ActiveLangCount { get; set; }

        [JsonProperty("tz")]
        public TimezoneResUsersOdooEnum? Tz { get; set; }

        [JsonProperty("vat")]
        public string Vat { get; set; }

        // res.partner
        [JsonProperty("same_vat_partner_id")]
        public long? SameVatPartnerId { get; set; }

        // res.partner.bank
        [JsonProperty("bank_ids")]
        public long[] BankIds { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        // res.partner.category
        [JsonProperty("category_id")]
        public long[] CategoryId { get; set; }

        [JsonProperty("credit_limit")]
        public double? CreditLimit { get; set; }

        [JsonProperty("employee")]
        public bool? Employee { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("type")]
        public AddressTypeResUsersOdooEnum? Type { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        // res.country.state
        [JsonProperty("state_id")]
        public long? StateId { get; set; }

        // res.country
        [JsonProperty("country_id")]
        public long? CountryId { get; set; }

        [JsonProperty("partner_latitude")]
        public double? PartnerLatitude { get; set; }

        [JsonProperty("partner_longitude")]
        public double? PartnerLongitude { get; set; }

        [JsonProperty("email_formatted")]
        public string EmailFormatted { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("is_company")]
        public bool? IsCompany { get; set; }

        // res.partner.industry
        [JsonProperty("industry_id")]
        public long? IndustryId { get; set; }

        [JsonProperty("company_type")]
        public CompanyTypeResUsersOdooEnum? CompanyType { get; set; }

        [JsonProperty("color")]
        public int? Color { get; set; }

        // res.users
        [JsonProperty("user_ids")]
        public long[] UserIds { get; set; }

        [JsonProperty("partner_share")]
        public bool? PartnerShare { get; set; }

        [JsonProperty("contact_address")]
        public string ContactAddress { get; set; }

        // res.partner
        [JsonProperty("commercial_partner_id")]
        public long? CommercialPartnerId { get; set; }

        [JsonProperty("commercial_company_name")]
        public string CommercialCompanyName { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        // res.partner
        [JsonProperty("self")]
        public long? Self { get; set; }

        [JsonProperty("signup_token")]
        public string SignupToken { get; set; }

        [JsonProperty("signup_type")]
        public string SignupType { get; set; }

        [JsonProperty("signup_expiration")]
        public DateTime? SignupExpiration { get; set; }

        [JsonProperty("signup_valid")]
        public bool? SignupValid { get; set; }

        [JsonProperty("signup_url")]
        public string SignupUrl { get; set; }

        // mail.activity
        [JsonProperty("activity_ids")]
        public long[] ActivityIds { get; set; }

        [JsonProperty("activity_state")]
        public ActivityStateResUsersOdooEnum? ActivityState { get; set; }

        // res.users
        [JsonProperty("activity_user_id")]
        public long? ActivityUserId { get; set; }

        // mail.activity.type
        [JsonProperty("activity_type_id")]
        public long? ActivityTypeId { get; set; }

        [JsonProperty("activity_type_icon")]
        public string ActivityTypeIcon { get; set; }

        [JsonProperty("activity_date_deadline")]
        public DateTime? ActivityDateDeadline { get; set; }

        [JsonProperty("my_activity_date_deadline")]
        public DateTime? MyActivityDateDeadline { get; set; }

        [JsonProperty("activity_summary")]
        public string ActivitySummary { get; set; }

        [JsonProperty("activity_exception_decoration")]
        public ActivityExceptionDecorationResUsersOdooEnum? ActivityExceptionDecoration { get; set; }

        [JsonProperty("activity_exception_icon")]
        public string ActivityExceptionIcon { get; set; }

        [JsonProperty("message_is_follower")]
        public bool? MessageIsFollower { get; set; }

        // mail.followers
        [JsonProperty("message_follower_ids")]
        public long[] MessageFollowerIds { get; set; }

        // res.partner
        [JsonProperty("message_partner_ids")]
        public long[] MessagePartnerIds { get; set; }

        // mail.channel
        [JsonProperty("message_channel_ids")]
        public long[] MessageChannelIds { get; set; }

        // mail.message
        [JsonProperty("message_ids")]
        public long[] MessageIds { get; set; }

        [JsonProperty("message_unread")]
        public bool? MessageUnread { get; set; }

        [JsonProperty("message_unread_counter")]
        public int? MessageUnreadCounter { get; set; }

        [JsonProperty("message_needaction")]
        public bool? MessageNeedaction { get; set; }

        [JsonProperty("message_needaction_counter")]
        public int? MessageNeedactionCounter { get; set; }

        [JsonProperty("message_has_error")]
        public bool? MessageHasError { get; set; }

        [JsonProperty("message_has_error_counter")]
        public int? MessageHasErrorCounter { get; set; }

        [JsonProperty("message_attachment_count")]
        public int? MessageAttachmentCount { get; set; }

        // ir.attachment
        [JsonProperty("message_main_attachment_id")]
        public long? MessageMainAttachmentId { get; set; }

        [JsonProperty("email_normalized")]
        public string EmailNormalized { get; set; }

        [JsonProperty("is_blacklisted")]
        public bool? IsBlacklisted { get; set; }

        [JsonProperty("message_bounce")]
        public int? MessageBounce { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        // mail.channel
        [JsonProperty("channel_ids")]
        public long[] ChannelIds { get; set; }

        // res.users
        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        // mail.message
        [JsonProperty("website_message_ids")]
        public long[] WebsiteMessageIds { get; set; }

        [JsonProperty("credit")]
        public decimal? Credit { get; set; }

        [JsonProperty("debit")]
        public decimal? Debit { get; set; }

        [JsonProperty("debit_limit")]
        public decimal? DebitLimit { get; set; }

        [JsonProperty("total_invoiced")]
        public decimal? TotalInvoiced { get; set; }

        // res.currency
        [JsonProperty("currency_id")]
        public long? CurrencyId { get; set; }

        [JsonProperty("journal_item_count")]
        public int? JournalItemCount { get; set; }

        // account.account
        // required
        [JsonProperty("property_account_payable_id")]
        public long PropertyAccountPayableId { get; set; }

        // account.account
        // required
        [JsonProperty("property_account_receivable_id")]
        public long PropertyAccountReceivableId { get; set; }

        // account.fiscal.position
        [JsonProperty("property_account_position_id")]
        public long? PropertyAccountPositionId { get; set; }

        // account.payment.term
        [JsonProperty("property_payment_term_id")]
        public long? PropertyPaymentTermId { get; set; }

        // account.payment.term
        [JsonProperty("property_supplier_payment_term_id")]
        public long? PropertySupplierPaymentTermId { get; set; }

        // res.company
        [JsonProperty("ref_company_ids")]
        public long[] RefCompanyIds { get; set; }

        [JsonProperty("has_unreconciled_entries")]
        public bool? HasUnreconciledEntries { get; set; }

        [JsonProperty("last_time_entries_checked")]
        public DateTime? LastTimeEntriesChecked { get; set; }

        // account.move
        [JsonProperty("invoice_ids")]
        public long[] InvoiceIds { get; set; }

        // account.analytic.account
        [JsonProperty("contract_ids")]
        public long[] ContractIds { get; set; }

        [JsonProperty("bank_account_count")]
        public int? BankAccountCount { get; set; }

        [JsonProperty("trust")]
        public DegreeOfTrustYouHaveInThisDebtorResUsersOdooEnum? Trust { get; set; }

        [JsonProperty("invoice_warn")]
        public InvoiceResUsersOdooEnum? InvoiceWarn { get; set; }

        [JsonProperty("invoice_warn_msg")]
        public string InvoiceWarnMsg { get; set; }

        [JsonProperty("supplier_rank")]
        public int? SupplierRank { get; set; }

        [JsonProperty("customer_rank")]
        public int? CustomerRank { get; set; }

        [JsonProperty("contact_address_complete")]
        public string ContactAddressComplete { get; set; }

        [JsonProperty("image_medium")]
        public string ImageMedium { get; set; }

        [JsonProperty("phone_sanitized")]
        public string PhoneSanitized { get; set; }

        [JsonProperty("phone_sanitized_blacklisted")]
        public bool? PhoneSanitizedBlacklisted { get; set; }

        [JsonProperty("phone_blacklisted")]
        public bool? PhoneBlacklisted { get; set; }

        [JsonProperty("mobile_blacklisted")]
        public bool? MobileBlacklisted { get; set; }

        [JsonProperty("ocn_token")]
        public string OcnToken { get; set; }

        [JsonProperty("partner_gid")]
        public int? PartnerGid { get; set; }

        [JsonProperty("additional_info")]
        public string AdditionalInfo { get; set; }

        [JsonProperty("message_has_sms_error")]
        public bool? MessageHasSmsError { get; set; }

        // product.pricelist
        [JsonProperty("property_product_pricelist")]
        public long? PropertyProductPricelist { get; set; }

        // crm.team
        [JsonProperty("team_id")]
        public long? TeamId { get; set; }

        [JsonProperty("online_partner_vendor_name")]
        public string OnlinePartnerVendorName { get; set; }

        [JsonProperty("online_partner_bank_account")]
        public string OnlinePartnerBankAccount { get; set; }

        // payment.token
        [JsonProperty("payment_token_ids")]
        public long[] PaymentTokenIds { get; set; }

        [JsonProperty("payment_token_count")]
        public int? PaymentTokenCount { get; set; }

        [JsonProperty("online_partner_information")]
        public string OnlinePartnerInformation { get; set; }

        [JsonProperty("sale_order_count")]
        public int? SaleOrderCount { get; set; }

        // sale.order
        [JsonProperty("sale_order_ids")]
        public long[] SaleOrderIds { get; set; }

        [JsonProperty("sale_warn")]
        public SalesWarningsResUsersOdooEnum? SaleWarn { get; set; }

        [JsonProperty("sale_warn_msg")]
        public string SaleWarnMsg { get; set; }

        [JsonProperty("image_1920")]
        public string Image1920 { get; set; }

        // stock.location
        [JsonProperty("property_stock_customer")]
        public long? PropertyStockCustomer { get; set; }

        // stock.location
        [JsonProperty("property_stock_supplier")]
        public long? PropertyStockSupplier { get; set; }

        [JsonProperty("picking_warn")]
        public StockPickingResUsersOdooEnum? PickingWarn { get; set; }

        [JsonProperty("picking_warn_msg")]
        public string PickingWarnMsg { get; set; }

        // res.currency
        [JsonProperty("property_purchase_currency_id")]
        public long? PropertyPurchaseCurrencyId { get; set; }

        [JsonProperty("purchase_order_count")]
        public int? PurchaseOrderCount { get; set; }

        [JsonProperty("supplier_invoice_count")]
        public int? SupplierInvoiceCount { get; set; }

        [JsonProperty("purchase_warn")]
        public PurchaseOrderResUsersOdooEnum? PurchaseWarn { get; set; }

        [JsonProperty("purchase_warn_msg")]
        public string PurchaseWarnMsg { get; set; }

        [JsonProperty("receipt_reminder_email")]
        public bool? ReceiptReminderEmail { get; set; }

        [JsonProperty("reminder_date_before_receipt")]
        public int? ReminderDateBeforeReceipt { get; set; }

        // purchase.order.line
        [JsonProperty("purchase_line_ids")]
        public long[] PurchaseLineIds { get; set; }

        [JsonProperty("on_time_rate")]
        public double? OnTimeRate { get; set; }

        [JsonProperty("image_1024")]
        public string Image1024 { get; set; }

        [JsonProperty("image_512")]
        public string Image512 { get; set; }

        [JsonProperty("image_256")]
        public string Image256 { get; set; }

        [JsonProperty("image_128")]
        public string Image128 { get; set; }

        [JsonProperty("sel_groups_2_3")]
        public AdministrationResUsersOdooEnum? SelGroups23 { get; set; }

        [JsonProperty("sel_groups_30_31_32")]
        public SalesResUsersOdooEnum? SelGroups303132 { get; set; }

        [JsonProperty("sel_groups_44_45")]
        public InventoryResUsersOdooEnum? SelGroups4445 { get; set; }

        [JsonProperty("sel_groups_23_25")]
        public InvoicingResUsersOdooEnum? SelGroups2325 { get; set; }

        [JsonProperty("sel_groups_38_39")]
        public PurchaseResUsersOdooEnum? SelGroups3839 { get; set; }

        [JsonProperty("sel_groups_1_9_10")]
        public UserTypesResUsersOdooEnum? SelGroups1910 { get; set; }

        [JsonProperty("in_group_26")]
        public bool? InGroup26 { get; set; }

        [JsonProperty("in_group_51")]
        public bool? InGroup51 { get; set; }

        [JsonProperty("in_group_40")]
        public bool? InGroup40 { get; set; }

        [JsonProperty("in_group_35")]
        public bool? InGroup35 { get; set; }

        [JsonProperty("in_group_11")]
        public bool? InGroup11 { get; set; }

        [JsonProperty("in_group_7")]
        public bool? InGroup7 { get; set; }

        [JsonProperty("in_group_34")]
        public bool? InGroup34 { get; set; }

        [JsonProperty("in_group_16")]
        public bool? InGroup16 { get; set; }

        [JsonProperty("in_group_27")]
        public bool? InGroup27 { get; set; }

        [JsonProperty("in_group_13")]
        public bool? InGroup13 { get; set; }

        [JsonProperty("in_group_14")]
        public bool? InGroup14 { get; set; }

        [JsonProperty("in_group_15")]
        public bool? InGroup15 { get; set; }

        [JsonProperty("in_group_19")]
        public bool? InGroup19 { get; set; }

        [JsonProperty("in_group_47")]
        public bool? InGroup47 { get; set; }

        [JsonProperty("in_group_54")]
        public bool? InGroup54 { get; set; }

        [JsonProperty("in_group_53")]
        public bool? InGroup53 { get; set; }

        [JsonProperty("in_group_55")]
        public bool? InGroup55 { get; set; }

        [JsonProperty("in_group_33")]
        public bool? InGroup33 { get; set; }

        [JsonProperty("in_group_50")]
        public bool? InGroup50 { get; set; }

        [JsonProperty("in_group_46")]
        public bool? InGroup46 { get; set; }

        [JsonProperty("in_group_42")]
        public bool? InGroup42 { get; set; }

        [JsonProperty("in_group_12")]
        public bool? InGroup12 { get; set; }

        [JsonProperty("in_group_43")]
        public bool? InGroup43 { get; set; }

        [JsonProperty("in_group_48")]
        public bool? InGroup48 { get; set; }

        [JsonProperty("in_group_17")]
        public bool? InGroup17 { get; set; }

        [JsonProperty("in_group_18")]
        public bool? InGroup18 { get; set; }

        [JsonProperty("in_group_49")]
        public bool? InGroup49 { get; set; }

        [JsonProperty("in_group_36")]
        public bool? InGroup36 { get; set; }

        [JsonProperty("in_group_29")]
        public bool? InGroup29 { get; set; }

        [JsonProperty("in_group_37")]
        public bool? InGroup37 { get; set; }

        [JsonProperty("in_group_52")]
        public bool? InGroup52 { get; set; }

        [JsonProperty("in_group_28")]
        public bool? InGroup28 { get; set; }

        [JsonProperty("in_group_41")]
        public bool? InGroup41 { get; set; }

        [JsonProperty("in_group_22")]
        public bool? InGroup22 { get; set; }

        [JsonProperty("in_group_24")]
        public bool? InGroup24 { get; set; }

        [JsonProperty("in_group_20")]
        public bool? InGroup20 { get; set; }

        [JsonProperty("in_group_21")]
        public bool? InGroup21 { get; set; }

        [JsonProperty("in_group_8")]
        public bool? InGroup8 { get; set; }

        [JsonProperty("in_group_4")]
        public bool? InGroup4 { get; set; }

        [JsonProperty("in_group_5")]
        public bool? InGroup5 { get; set; }

        [JsonProperty("in_group_6")]
        public bool? InGroup6 { get; set; }
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusResUsersOdooEnum
    {
        [EnumMember(Value = "new")]
        NeverConnected = 1,

        [EnumMember(Value = "active")]
        Confirmed = 2,
    }


    // Policy on how to handle Chatter notifications:
    // - Handle by Emails: notifications are sent to your email address
    // - Handle in Odoo: notifications appear in your Odoo Inbox
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationResUsersOdooEnum
    {
        [EnumMember(Value = "email")]
        HandleByEmails = 1,

        [EnumMember(Value = "inbox")]
        HandleInOdoo = 2,
    }


    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum OdoobotStatusResUsersOdooEnum
    {
        [EnumMember(Value = "not_initialized")]
        NotInitialized = 1,

        [EnumMember(Value = "onboarding_emoji")]
        OnboardingEmoji = 2,

        [EnumMember(Value = "onboarding_attachement")]
        OnboardingAttachement = 3,

        [EnumMember(Value = "onboarding_command")]
        OnboardingCommand = 4,

        [EnumMember(Value = "onboarding_ping")]
        OnboardingPing = 5,

        [EnumMember(Value = "idle")]
        Idle = 6,

        [EnumMember(Value = "disabled")]
        Disabled = 7,
    }


    // All the emails and documents sent to this contact will be translated in this language.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageResUsersOdooEnum
    {
        [EnumMember(Value = "en_US")]
        EnglishUS = 1,
    }


    // When printing documents and exporting/importing data, time values are computed according to this timezone.
    // If the timezone is not set, UTC (Coordinated Universal Time) is used.
    // Anywhere else, time values are computed according to the time offset of your web client.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TimezoneResUsersOdooEnum
    {
        [EnumMember(Value = "Africa/Abidjan")]
        AfricaAbidjan = 1,

        [EnumMember(Value = "Africa/Accra")]
        AfricaAccra = 2,

        [EnumMember(Value = "Africa/Addis_Ababa")]
        AfricaAddisAbaba = 3,

        [EnumMember(Value = "Africa/Algiers")]
        AfricaAlgiers = 4,

        [EnumMember(Value = "Africa/Asmara")]
        AfricaAsmara = 5,

        [EnumMember(Value = "Africa/Asmera")]
        AfricaAsmera = 6,

        [EnumMember(Value = "Africa/Bamako")]
        AfricaBamako = 7,

        [EnumMember(Value = "Africa/Bangui")]
        AfricaBangui = 8,

        [EnumMember(Value = "Africa/Banjul")]
        AfricaBanjul = 9,

        [EnumMember(Value = "Africa/Bissau")]
        AfricaBissau = 10,

        [EnumMember(Value = "Africa/Blantyre")]
        AfricaBlantyre = 11,

        [EnumMember(Value = "Africa/Brazzaville")]
        AfricaBrazzaville = 12,

        [EnumMember(Value = "Africa/Bujumbura")]
        AfricaBujumbura = 13,

        [EnumMember(Value = "Africa/Cairo")]
        AfricaCairo = 14,

        [EnumMember(Value = "Africa/Casablanca")]
        AfricaCasablanca = 15,

        [EnumMember(Value = "Africa/Ceuta")]
        AfricaCeuta = 16,

        [EnumMember(Value = "Africa/Conakry")]
        AfricaConakry = 17,

        [EnumMember(Value = "Africa/Dakar")]
        AfricaDakar = 18,

        [EnumMember(Value = "Africa/Dar_es_Salaam")]
        AfricaDarEsSalaam = 19,

        [EnumMember(Value = "Africa/Djibouti")]
        AfricaDjibouti = 20,

        [EnumMember(Value = "Africa/Douala")]
        AfricaDouala = 21,

        [EnumMember(Value = "Africa/El_Aaiun")]
        AfricaElAaiun = 22,

        [EnumMember(Value = "Africa/Freetown")]
        AfricaFreetown = 23,

        [EnumMember(Value = "Africa/Gaborone")]
        AfricaGaborone = 24,

        [EnumMember(Value = "Africa/Harare")]
        AfricaHarare = 25,

        [EnumMember(Value = "Africa/Johannesburg")]
        AfricaJohannesburg = 26,

        [EnumMember(Value = "Africa/Juba")]
        AfricaJuba = 27,

        [EnumMember(Value = "Africa/Kampala")]
        AfricaKampala = 28,

        [EnumMember(Value = "Africa/Khartoum")]
        AfricaKhartoum = 29,

        [EnumMember(Value = "Africa/Kigali")]
        AfricaKigali = 30,

        [EnumMember(Value = "Africa/Kinshasa")]
        AfricaKinshasa = 31,

        [EnumMember(Value = "Africa/Lagos")]
        AfricaLagos = 32,

        [EnumMember(Value = "Africa/Libreville")]
        AfricaLibreville = 33,

        [EnumMember(Value = "Africa/Lome")]
        AfricaLome = 34,

        [EnumMember(Value = "Africa/Luanda")]
        AfricaLuanda = 35,

        [EnumMember(Value = "Africa/Lubumbashi")]
        AfricaLubumbashi = 36,

        [EnumMember(Value = "Africa/Lusaka")]
        AfricaLusaka = 37,

        [EnumMember(Value = "Africa/Malabo")]
        AfricaMalabo = 38,

        [EnumMember(Value = "Africa/Maputo")]
        AfricaMaputo = 39,

        [EnumMember(Value = "Africa/Maseru")]
        AfricaMaseru = 40,

        [EnumMember(Value = "Africa/Mbabane")]
        AfricaMbabane = 41,

        [EnumMember(Value = "Africa/Mogadishu")]
        AfricaMogadishu = 42,

        [EnumMember(Value = "Africa/Monrovia")]
        AfricaMonrovia = 43,

        [EnumMember(Value = "Africa/Nairobi")]
        AfricaNairobi = 44,

        [EnumMember(Value = "Africa/Ndjamena")]
        AfricaNdjamena = 45,

        [EnumMember(Value = "Africa/Niamey")]
        AfricaNiamey = 46,

        [EnumMember(Value = "Africa/Nouakchott")]
        AfricaNouakchott = 47,

        [EnumMember(Value = "Africa/Ouagadougou")]
        AfricaOuagadougou = 48,

        [EnumMember(Value = "Africa/Porto-Novo")]
        AfricaPortoNovo = 49,

        [EnumMember(Value = "Africa/Sao_Tome")]
        AfricaSaoTome = 50,

        [EnumMember(Value = "Africa/Timbuktu")]
        AfricaTimbuktu = 51,

        [EnumMember(Value = "Africa/Tripoli")]
        AfricaTripoli = 52,

        [EnumMember(Value = "Africa/Tunis")]
        AfricaTunis = 53,

        [EnumMember(Value = "Africa/Windhoek")]
        AfricaWindhoek = 54,

        [EnumMember(Value = "America/Adak")]
        AmericaAdak = 55,

        [EnumMember(Value = "America/Anchorage")]
        AmericaAnchorage = 56,

        [EnumMember(Value = "America/Anguilla")]
        AmericaAnguilla = 57,

        [EnumMember(Value = "America/Antigua")]
        AmericaAntigua = 58,

        [EnumMember(Value = "America/Araguaina")]
        AmericaAraguaina = 59,

        [EnumMember(Value = "America/Argentina/Buenos_Aires")]
        AmericaArgentinaBuenosAires = 60,

        [EnumMember(Value = "America/Argentina/Catamarca")]
        AmericaArgentinaCatamarca = 61,

        [EnumMember(Value = "America/Argentina/ComodRivadavia")]
        AmericaArgentinaComodrivadavia = 62,

        [EnumMember(Value = "America/Argentina/Cordoba")]
        AmericaArgentinaCordoba = 63,

        [EnumMember(Value = "America/Argentina/Jujuy")]
        AmericaArgentinaJujuy = 64,

        [EnumMember(Value = "America/Argentina/La_Rioja")]
        AmericaArgentinaLaRioja = 65,

        [EnumMember(Value = "America/Argentina/Mendoza")]
        AmericaArgentinaMendoza = 66,

        [EnumMember(Value = "America/Argentina/Rio_Gallegos")]
        AmericaArgentinaRioGallegos = 67,

        [EnumMember(Value = "America/Argentina/Salta")]
        AmericaArgentinaSalta = 68,

        [EnumMember(Value = "America/Argentina/San_Juan")]
        AmericaArgentinaSanJuan = 69,

        [EnumMember(Value = "America/Argentina/San_Luis")]
        AmericaArgentinaSanLuis = 70,

        [EnumMember(Value = "America/Argentina/Tucuman")]
        AmericaArgentinaTucuman = 71,

        [EnumMember(Value = "America/Argentina/Ushuaia")]
        AmericaArgentinaUshuaia = 72,

        [EnumMember(Value = "America/Aruba")]
        AmericaAruba = 73,

        [EnumMember(Value = "America/Asuncion")]
        AmericaAsuncion = 74,

        [EnumMember(Value = "America/Atikokan")]
        AmericaAtikokan = 75,

        [EnumMember(Value = "America/Atka")]
        AmericaAtka = 76,

        [EnumMember(Value = "America/Bahia")]
        AmericaBahia = 77,

        [EnumMember(Value = "America/Bahia_Banderas")]
        AmericaBahiaBanderas = 78,

        [EnumMember(Value = "America/Barbados")]
        AmericaBarbados = 79,

        [EnumMember(Value = "America/Belem")]
        AmericaBelem = 80,

        [EnumMember(Value = "America/Belize")]
        AmericaBelize = 81,

        [EnumMember(Value = "America/Blanc-Sablon")]
        AmericaBlancSablon = 82,

        [EnumMember(Value = "America/Boa_Vista")]
        AmericaBoaVista = 83,

        [EnumMember(Value = "America/Bogota")]
        AmericaBogota = 84,

        [EnumMember(Value = "America/Boise")]
        AmericaBoise = 85,

        [EnumMember(Value = "America/Buenos_Aires")]
        AmericaBuenosAires = 86,

        [EnumMember(Value = "America/Cambridge_Bay")]
        AmericaCambridgeBay = 87,

        [EnumMember(Value = "America/Campo_Grande")]
        AmericaCampoGrande = 88,

        [EnumMember(Value = "America/Cancun")]
        AmericaCancun = 89,

        [EnumMember(Value = "America/Caracas")]
        AmericaCaracas = 90,

        [EnumMember(Value = "America/Catamarca")]
        AmericaCatamarca = 91,

        [EnumMember(Value = "America/Cayenne")]
        AmericaCayenne = 92,

        [EnumMember(Value = "America/Cayman")]
        AmericaCayman = 93,

        [EnumMember(Value = "America/Chicago")]
        AmericaChicago = 94,

        [EnumMember(Value = "America/Chihuahua")]
        AmericaChihuahua = 95,

        [EnumMember(Value = "America/Coral_Harbour")]
        AmericaCoralHarbour = 96,

        [EnumMember(Value = "America/Cordoba")]
        AmericaCordoba = 97,

        [EnumMember(Value = "America/Costa_Rica")]
        AmericaCostaRica = 98,

        [EnumMember(Value = "America/Creston")]
        AmericaCreston = 99,

        [EnumMember(Value = "America/Cuiaba")]
        AmericaCuiaba = 100,

        [EnumMember(Value = "America/Curacao")]
        AmericaCuracao = 101,

        [EnumMember(Value = "America/Danmarkshavn")]
        AmericaDanmarkshavn = 102,

        [EnumMember(Value = "America/Dawson")]
        AmericaDawson = 103,

        [EnumMember(Value = "America/Dawson_Creek")]
        AmericaDawsonCreek = 104,

        [EnumMember(Value = "America/Denver")]
        AmericaDenver = 105,

        [EnumMember(Value = "America/Detroit")]
        AmericaDetroit = 106,

        [EnumMember(Value = "America/Dominica")]
        AmericaDominica = 107,

        [EnumMember(Value = "America/Edmonton")]
        AmericaEdmonton = 108,

        [EnumMember(Value = "America/Eirunepe")]
        AmericaEirunepe = 109,

        [EnumMember(Value = "America/El_Salvador")]
        AmericaElSalvador = 110,

        [EnumMember(Value = "America/Ensenada")]
        AmericaEnsenada = 111,

        [EnumMember(Value = "America/Fort_Nelson")]
        AmericaFortNelson = 112,

        [EnumMember(Value = "America/Fort_Wayne")]
        AmericaFortWayne = 113,

        [EnumMember(Value = "America/Fortaleza")]
        AmericaFortaleza = 114,

        [EnumMember(Value = "America/Glace_Bay")]
        AmericaGlaceBay = 115,

        [EnumMember(Value = "America/Godthab")]
        AmericaGodthab = 116,

        [EnumMember(Value = "America/Goose_Bay")]
        AmericaGooseBay = 117,

        [EnumMember(Value = "America/Grand_Turk")]
        AmericaGrandTurk = 118,

        [EnumMember(Value = "America/Grenada")]
        AmericaGrenada = 119,

        [EnumMember(Value = "America/Guadeloupe")]
        AmericaGuadeloupe = 120,

        [EnumMember(Value = "America/Guatemala")]
        AmericaGuatemala = 121,

        [EnumMember(Value = "America/Guayaquil")]
        AmericaGuayaquil = 122,

        [EnumMember(Value = "America/Guyana")]
        AmericaGuyana = 123,

        [EnumMember(Value = "America/Halifax")]
        AmericaHalifax = 124,

        [EnumMember(Value = "America/Havana")]
        AmericaHavana = 125,

        [EnumMember(Value = "America/Hermosillo")]
        AmericaHermosillo = 126,

        [EnumMember(Value = "America/Indiana/Indianapolis")]
        AmericaIndianaIndianapolis = 127,

        [EnumMember(Value = "America/Indiana/Knox")]
        AmericaIndianaKnox = 128,

        [EnumMember(Value = "America/Indiana/Marengo")]
        AmericaIndianaMarengo = 129,

        [EnumMember(Value = "America/Indiana/Petersburg")]
        AmericaIndianaPetersburg = 130,

        [EnumMember(Value = "America/Indiana/Tell_City")]
        AmericaIndianaTellCity = 131,

        [EnumMember(Value = "America/Indiana/Vevay")]
        AmericaIndianaVevay = 132,

        [EnumMember(Value = "America/Indiana/Vincennes")]
        AmericaIndianaVincennes = 133,

        [EnumMember(Value = "America/Indiana/Winamac")]
        AmericaIndianaWinamac = 134,

        [EnumMember(Value = "America/Indianapolis")]
        AmericaIndianapolis = 135,

        [EnumMember(Value = "America/Inuvik")]
        AmericaInuvik = 136,

        [EnumMember(Value = "America/Iqaluit")]
        AmericaIqaluit = 137,

        [EnumMember(Value = "America/Jamaica")]
        AmericaJamaica = 138,

        [EnumMember(Value = "America/Jujuy")]
        AmericaJujuy = 139,

        [EnumMember(Value = "America/Juneau")]
        AmericaJuneau = 140,

        [EnumMember(Value = "America/Kentucky/Louisville")]
        AmericaKentuckyLouisville = 141,

        [EnumMember(Value = "America/Kentucky/Monticello")]
        AmericaKentuckyMonticello = 142,

        [EnumMember(Value = "America/Knox_IN")]
        AmericaKnoxIN = 143,

        [EnumMember(Value = "America/Kralendijk")]
        AmericaKralendijk = 144,

        [EnumMember(Value = "America/La_Paz")]
        AmericaLaPaz = 145,

        [EnumMember(Value = "America/Lima")]
        AmericaLima = 146,

        [EnumMember(Value = "America/Los_Angeles")]
        AmericaLosAngeles = 147,

        [EnumMember(Value = "America/Louisville")]
        AmericaLouisville = 148,

        [EnumMember(Value = "America/Lower_Princes")]
        AmericaLowerPrinces = 149,

        [EnumMember(Value = "America/Maceio")]
        AmericaMaceio = 150,

        [EnumMember(Value = "America/Managua")]
        AmericaManagua = 151,

        [EnumMember(Value = "America/Manaus")]
        AmericaManaus = 152,

        [EnumMember(Value = "America/Marigot")]
        AmericaMarigot = 153,

        [EnumMember(Value = "America/Martinique")]
        AmericaMartinique = 154,

        [EnumMember(Value = "America/Matamoros")]
        AmericaMatamoros = 155,

        [EnumMember(Value = "America/Mazatlan")]
        AmericaMazatlan = 156,

        [EnumMember(Value = "America/Mendoza")]
        AmericaMendoza = 157,

        [EnumMember(Value = "America/Menominee")]
        AmericaMenominee = 158,

        [EnumMember(Value = "America/Merida")]
        AmericaMerida = 159,

        [EnumMember(Value = "America/Metlakatla")]
        AmericaMetlakatla = 160,

        [EnumMember(Value = "America/Mexico_City")]
        AmericaMexicoCity = 161,

        [EnumMember(Value = "America/Miquelon")]
        AmericaMiquelon = 162,

        [EnumMember(Value = "America/Moncton")]
        AmericaMoncton = 163,

        [EnumMember(Value = "America/Monterrey")]
        AmericaMonterrey = 164,

        [EnumMember(Value = "America/Montevideo")]
        AmericaMontevideo = 165,

        [EnumMember(Value = "America/Montreal")]
        AmericaMontreal = 166,

        [EnumMember(Value = "America/Montserrat")]
        AmericaMontserrat = 167,

        [EnumMember(Value = "America/Nassau")]
        AmericaNassau = 168,

        [EnumMember(Value = "America/New_York")]
        AmericaNewYork = 169,

        [EnumMember(Value = "America/Nipigon")]
        AmericaNipigon = 170,

        [EnumMember(Value = "America/Nome")]
        AmericaNome = 171,

        [EnumMember(Value = "America/Noronha")]
        AmericaNoronha = 172,

        [EnumMember(Value = "America/North_Dakota/Beulah")]
        AmericaNorthDakotaBeulah = 173,

        [EnumMember(Value = "America/North_Dakota/Center")]
        AmericaNorthDakotaCenter = 174,

        [EnumMember(Value = "America/North_Dakota/New_Salem")]
        AmericaNorthDakotaNewSalem = 175,

        [EnumMember(Value = "America/Ojinaga")]
        AmericaOjinaga = 176,

        [EnumMember(Value = "America/Panama")]
        AmericaPanama = 177,

        [EnumMember(Value = "America/Pangnirtung")]
        AmericaPangnirtung = 178,

        [EnumMember(Value = "America/Paramaribo")]
        AmericaParamaribo = 179,

        [EnumMember(Value = "America/Phoenix")]
        AmericaPhoenix = 180,

        [EnumMember(Value = "America/Port-au-Prince")]
        AmericaPortAuPrince = 181,

        [EnumMember(Value = "America/Port_of_Spain")]
        AmericaPortOfSpain = 182,

        [EnumMember(Value = "America/Porto_Acre")]
        AmericaPortoAcre = 183,

        [EnumMember(Value = "America/Porto_Velho")]
        AmericaPortoVelho = 184,

        [EnumMember(Value = "America/Puerto_Rico")]
        AmericaPuertoRico = 185,

        [EnumMember(Value = "America/Punta_Arenas")]
        AmericaPuntaArenas = 186,

        [EnumMember(Value = "America/Rainy_River")]
        AmericaRainyRiver = 187,

        [EnumMember(Value = "America/Rankin_Inlet")]
        AmericaRankinInlet = 188,

        [EnumMember(Value = "America/Recife")]
        AmericaRecife = 189,

        [EnumMember(Value = "America/Regina")]
        AmericaRegina = 190,

        [EnumMember(Value = "America/Resolute")]
        AmericaResolute = 191,

        [EnumMember(Value = "America/Rio_Branco")]
        AmericaRioBranco = 192,

        [EnumMember(Value = "America/Rosario")]
        AmericaRosario = 193,

        [EnumMember(Value = "America/Santa_Isabel")]
        AmericaSantaIsabel = 194,

        [EnumMember(Value = "America/Santarem")]
        AmericaSantarem = 195,

        [EnumMember(Value = "America/Santiago")]
        AmericaSantiago = 196,

        [EnumMember(Value = "America/Santo_Domingo")]
        AmericaSantoDomingo = 197,

        [EnumMember(Value = "America/Sao_Paulo")]
        AmericaSaoPaulo = 198,

        [EnumMember(Value = "America/Scoresbysund")]
        AmericaScoresbysund = 199,

        [EnumMember(Value = "America/Shiprock")]
        AmericaShiprock = 200,

        [EnumMember(Value = "America/Sitka")]
        AmericaSitka = 201,

        [EnumMember(Value = "America/St_Barthelemy")]
        AmericaStBarthelemy = 202,

        [EnumMember(Value = "America/St_Johns")]
        AmericaStJohns = 203,

        [EnumMember(Value = "America/St_Kitts")]
        AmericaStKitts = 204,

        [EnumMember(Value = "America/St_Lucia")]
        AmericaStLucia = 205,

        [EnumMember(Value = "America/St_Thomas")]
        AmericaStThomas = 206,

        [EnumMember(Value = "America/St_Vincent")]
        AmericaStVincent = 207,

        [EnumMember(Value = "America/Swift_Current")]
        AmericaSwiftCurrent = 208,

        [EnumMember(Value = "America/Tegucigalpa")]
        AmericaTegucigalpa = 209,

        [EnumMember(Value = "America/Thule")]
        AmericaThule = 210,

        [EnumMember(Value = "America/Thunder_Bay")]
        AmericaThunderBay = 211,

        [EnumMember(Value = "America/Tijuana")]
        AmericaTijuana = 212,

        [EnumMember(Value = "America/Toronto")]
        AmericaToronto = 213,

        [EnumMember(Value = "America/Tortola")]
        AmericaTortola = 214,

        [EnumMember(Value = "America/Vancouver")]
        AmericaVancouver = 215,

        [EnumMember(Value = "America/Virgin")]
        AmericaVirgin = 216,

        [EnumMember(Value = "America/Whitehorse")]
        AmericaWhitehorse = 217,

        [EnumMember(Value = "America/Winnipeg")]
        AmericaWinnipeg = 218,

        [EnumMember(Value = "America/Yakutat")]
        AmericaYakutat = 219,

        [EnumMember(Value = "America/Yellowknife")]
        AmericaYellowknife = 220,

        [EnumMember(Value = "Antarctica/Casey")]
        AntarcticaCasey = 221,

        [EnumMember(Value = "Antarctica/Davis")]
        AntarcticaDavis = 222,

        [EnumMember(Value = "Antarctica/DumontDUrville")]
        AntarcticaDumontdurville = 223,

        [EnumMember(Value = "Antarctica/Macquarie")]
        AntarcticaMacquarie = 224,

        [EnumMember(Value = "Antarctica/Mawson")]
        AntarcticaMawson = 225,

        [EnumMember(Value = "Antarctica/McMurdo")]
        AntarcticaMcmurdo = 226,

        [EnumMember(Value = "Antarctica/Palmer")]
        AntarcticaPalmer = 227,

        [EnumMember(Value = "Antarctica/Rothera")]
        AntarcticaRothera = 228,

        [EnumMember(Value = "Antarctica/South_Pole")]
        AntarcticaSouthPole = 229,

        [EnumMember(Value = "Antarctica/Syowa")]
        AntarcticaSyowa = 230,

        [EnumMember(Value = "Antarctica/Troll")]
        AntarcticaTroll = 231,

        [EnumMember(Value = "Antarctica/Vostok")]
        AntarcticaVostok = 232,

        [EnumMember(Value = "Arctic/Longyearbyen")]
        ArcticLongyearbyen = 233,

        [EnumMember(Value = "Asia/Aden")]
        AsiaAden = 234,

        [EnumMember(Value = "Asia/Almaty")]
        AsiaAlmaty = 235,

        [EnumMember(Value = "Asia/Amman")]
        AsiaAmman = 236,

        [EnumMember(Value = "Asia/Anadyr")]
        AsiaAnadyr = 237,

        [EnumMember(Value = "Asia/Aqtau")]
        AsiaAqtau = 238,

        [EnumMember(Value = "Asia/Aqtobe")]
        AsiaAqtobe = 239,

        [EnumMember(Value = "Asia/Ashgabat")]
        AsiaAshgabat = 240,

        [EnumMember(Value = "Asia/Ashkhabad")]
        AsiaAshkhabad = 241,

        [EnumMember(Value = "Asia/Atyrau")]
        AsiaAtyrau = 242,

        [EnumMember(Value = "Asia/Baghdad")]
        AsiaBaghdad = 243,

        [EnumMember(Value = "Asia/Bahrain")]
        AsiaBahrain = 244,

        [EnumMember(Value = "Asia/Baku")]
        AsiaBaku = 245,

        [EnumMember(Value = "Asia/Bangkok")]
        AsiaBangkok = 246,

        [EnumMember(Value = "Asia/Barnaul")]
        AsiaBarnaul = 247,

        [EnumMember(Value = "Asia/Beirut")]
        AsiaBeirut = 248,

        [EnumMember(Value = "Asia/Bishkek")]
        AsiaBishkek = 249,

        [EnumMember(Value = "Asia/Brunei")]
        AsiaBrunei = 250,

        [EnumMember(Value = "Asia/Calcutta")]
        AsiaCalcutta = 251,

        [EnumMember(Value = "Asia/Chita")]
        AsiaChita = 252,

        [EnumMember(Value = "Asia/Choibalsan")]
        AsiaChoibalsan = 253,

        [EnumMember(Value = "Asia/Chongqing")]
        AsiaChongqing = 254,

        [EnumMember(Value = "Asia/Chungking")]
        AsiaChungking = 255,

        [EnumMember(Value = "Asia/Colombo")]
        AsiaColombo = 256,

        [EnumMember(Value = "Asia/Dacca")]
        AsiaDacca = 257,

        [EnumMember(Value = "Asia/Damascus")]
        AsiaDamascus = 258,

        [EnumMember(Value = "Asia/Dhaka")]
        AsiaDhaka = 259,

        [EnumMember(Value = "Asia/Dili")]
        AsiaDili = 260,

        [EnumMember(Value = "Asia/Dubai")]
        AsiaDubai = 261,

        [EnumMember(Value = "Asia/Dushanbe")]
        AsiaDushanbe = 262,

        [EnumMember(Value = "Asia/Famagusta")]
        AsiaFamagusta = 263,

        [EnumMember(Value = "Asia/Gaza")]
        AsiaGaza = 264,

        [EnumMember(Value = "Asia/Harbin")]
        AsiaHarbin = 265,

        [EnumMember(Value = "Asia/Hebron")]
        AsiaHebron = 266,

        [EnumMember(Value = "Asia/Ho_Chi_Minh")]
        AsiaHoChiMinh = 267,

        [EnumMember(Value = "Asia/Hong_Kong")]
        AsiaHongKong = 268,

        [EnumMember(Value = "Asia/Hovd")]
        AsiaHovd = 269,

        [EnumMember(Value = "Asia/Irkutsk")]
        AsiaIrkutsk = 270,

        [EnumMember(Value = "Asia/Istanbul")]
        AsiaIstanbul = 271,

        [EnumMember(Value = "Asia/Jakarta")]
        AsiaJakarta = 272,

        [EnumMember(Value = "Asia/Jayapura")]
        AsiaJayapura = 273,

        [EnumMember(Value = "Asia/Jerusalem")]
        AsiaJerusalem = 274,

        [EnumMember(Value = "Asia/Kabul")]
        AsiaKabul = 275,

        [EnumMember(Value = "Asia/Kamchatka")]
        AsiaKamchatka = 276,

        [EnumMember(Value = "Asia/Karachi")]
        AsiaKarachi = 277,

        [EnumMember(Value = "Asia/Kashgar")]
        AsiaKashgar = 278,

        [EnumMember(Value = "Asia/Kathmandu")]
        AsiaKathmandu = 279,

        [EnumMember(Value = "Asia/Katmandu")]
        AsiaKatmandu = 280,

        [EnumMember(Value = "Asia/Khandyga")]
        AsiaKhandyga = 281,

        [EnumMember(Value = "Asia/Kolkata")]
        AsiaKolkata = 282,

        [EnumMember(Value = "Asia/Krasnoyarsk")]
        AsiaKrasnoyarsk = 283,

        [EnumMember(Value = "Asia/Kuala_Lumpur")]
        AsiaKualaLumpur = 284,

        [EnumMember(Value = "Asia/Kuching")]
        AsiaKuching = 285,

        [EnumMember(Value = "Asia/Kuwait")]
        AsiaKuwait = 286,

        [EnumMember(Value = "Asia/Macao")]
        AsiaMacao = 287,

        [EnumMember(Value = "Asia/Macau")]
        AsiaMacau = 288,

        [EnumMember(Value = "Asia/Magadan")]
        AsiaMagadan = 289,

        [EnumMember(Value = "Asia/Makassar")]
        AsiaMakassar = 290,

        [EnumMember(Value = "Asia/Manila")]
        AsiaManila = 291,

        [EnumMember(Value = "Asia/Muscat")]
        AsiaMuscat = 292,

        [EnumMember(Value = "Asia/Nicosia")]
        AsiaNicosia = 293,

        [EnumMember(Value = "Asia/Novokuznetsk")]
        AsiaNovokuznetsk = 294,

        [EnumMember(Value = "Asia/Novosibirsk")]
        AsiaNovosibirsk = 295,

        [EnumMember(Value = "Asia/Omsk")]
        AsiaOmsk = 296,

        [EnumMember(Value = "Asia/Oral")]
        AsiaOral = 297,

        [EnumMember(Value = "Asia/Phnom_Penh")]
        AsiaPhnomPenh = 298,

        [EnumMember(Value = "Asia/Pontianak")]
        AsiaPontianak = 299,

        [EnumMember(Value = "Asia/Pyongyang")]
        AsiaPyongyang = 300,

        [EnumMember(Value = "Asia/Qatar")]
        AsiaQatar = 301,

        [EnumMember(Value = "Asia/Qostanay")]
        AsiaQostanay = 302,

        [EnumMember(Value = "Asia/Qyzylorda")]
        AsiaQyzylorda = 303,

        [EnumMember(Value = "Asia/Rangoon")]
        AsiaRangoon = 304,

        [EnumMember(Value = "Asia/Riyadh")]
        AsiaRiyadh = 305,

        [EnumMember(Value = "Asia/Saigon")]
        AsiaSaigon = 306,

        [EnumMember(Value = "Asia/Sakhalin")]
        AsiaSakhalin = 307,

        [EnumMember(Value = "Asia/Samarkand")]
        AsiaSamarkand = 308,

        [EnumMember(Value = "Asia/Seoul")]
        AsiaSeoul = 309,

        [EnumMember(Value = "Asia/Shanghai")]
        AsiaShanghai = 310,

        [EnumMember(Value = "Asia/Singapore")]
        AsiaSingapore = 311,

        [EnumMember(Value = "Asia/Srednekolymsk")]
        AsiaSrednekolymsk = 312,

        [EnumMember(Value = "Asia/Taipei")]
        AsiaTaipei = 313,

        [EnumMember(Value = "Asia/Tashkent")]
        AsiaTashkent = 314,

        [EnumMember(Value = "Asia/Tbilisi")]
        AsiaTbilisi = 315,

        [EnumMember(Value = "Asia/Tehran")]
        AsiaTehran = 316,

        [EnumMember(Value = "Asia/Tel_Aviv")]
        AsiaTelAviv = 317,

        [EnumMember(Value = "Asia/Thimbu")]
        AsiaThimbu = 318,

        [EnumMember(Value = "Asia/Thimphu")]
        AsiaThimphu = 319,

        [EnumMember(Value = "Asia/Tokyo")]
        AsiaTokyo = 320,

        [EnumMember(Value = "Asia/Tomsk")]
        AsiaTomsk = 321,

        [EnumMember(Value = "Asia/Ujung_Pandang")]
        AsiaUjungPandang = 322,

        [EnumMember(Value = "Asia/Ulaanbaatar")]
        AsiaUlaanbaatar = 323,

        [EnumMember(Value = "Asia/Ulan_Bator")]
        AsiaUlanBator = 324,

        [EnumMember(Value = "Asia/Urumqi")]
        AsiaUrumqi = 325,

        [EnumMember(Value = "Asia/Ust-Nera")]
        AsiaUstNera = 326,

        [EnumMember(Value = "Asia/Vientiane")]
        AsiaVientiane = 327,

        [EnumMember(Value = "Asia/Vladivostok")]
        AsiaVladivostok = 328,

        [EnumMember(Value = "Asia/Yakutsk")]
        AsiaYakutsk = 329,

        [EnumMember(Value = "Asia/Yangon")]
        AsiaYangon = 330,

        [EnumMember(Value = "Asia/Yekaterinburg")]
        AsiaYekaterinburg = 331,

        [EnumMember(Value = "Asia/Yerevan")]
        AsiaYerevan = 332,

        [EnumMember(Value = "Atlantic/Azores")]
        AtlanticAzores = 333,

        [EnumMember(Value = "Atlantic/Bermuda")]
        AtlanticBermuda = 334,

        [EnumMember(Value = "Atlantic/Canary")]
        AtlanticCanary = 335,

        [EnumMember(Value = "Atlantic/Cape_Verde")]
        AtlanticCapeVerde = 336,

        [EnumMember(Value = "Atlantic/Faeroe")]
        AtlanticFaeroe = 337,

        [EnumMember(Value = "Atlantic/Faroe")]
        AtlanticFaroe = 338,

        [EnumMember(Value = "Atlantic/Jan_Mayen")]
        AtlanticJanMayen = 339,

        [EnumMember(Value = "Atlantic/Madeira")]
        AtlanticMadeira = 340,

        [EnumMember(Value = "Atlantic/Reykjavik")]
        AtlanticReykjavik = 341,

        [EnumMember(Value = "Atlantic/South_Georgia")]
        AtlanticSouthGeorgia = 342,

        [EnumMember(Value = "Atlantic/St_Helena")]
        AtlanticStHelena = 343,

        [EnumMember(Value = "Atlantic/Stanley")]
        AtlanticStanley = 344,

        [EnumMember(Value = "Australia/ACT")]
        AustraliaACT = 345,

        [EnumMember(Value = "Australia/Adelaide")]
        AustraliaAdelaide = 346,

        [EnumMember(Value = "Australia/Brisbane")]
        AustraliaBrisbane = 347,

        [EnumMember(Value = "Australia/Broken_Hill")]
        AustraliaBrokenHill = 348,

        [EnumMember(Value = "Australia/Canberra")]
        AustraliaCanberra = 349,

        [EnumMember(Value = "Australia/Currie")]
        AustraliaCurrie = 350,

        [EnumMember(Value = "Australia/Darwin")]
        AustraliaDarwin = 351,

        [EnumMember(Value = "Australia/Eucla")]
        AustraliaEucla = 352,

        [EnumMember(Value = "Australia/Hobart")]
        AustraliaHobart = 353,

        [EnumMember(Value = "Australia/LHI")]
        AustraliaLHI = 354,

        [EnumMember(Value = "Australia/Lindeman")]
        AustraliaLindeman = 355,

        [EnumMember(Value = "Australia/Lord_Howe")]
        AustraliaLordHowe = 356,

        [EnumMember(Value = "Australia/Melbourne")]
        AustraliaMelbourne = 357,

        [EnumMember(Value = "Australia/NSW")]
        AustraliaNSW = 358,

        [EnumMember(Value = "Australia/North")]
        AustraliaNorth = 359,

        [EnumMember(Value = "Australia/Perth")]
        AustraliaPerth = 360,

        [EnumMember(Value = "Australia/Queensland")]
        AustraliaQueensland = 361,

        [EnumMember(Value = "Australia/South")]
        AustraliaSouth = 362,

        [EnumMember(Value = "Australia/Sydney")]
        AustraliaSydney = 363,

        [EnumMember(Value = "Australia/Tasmania")]
        AustraliaTasmania = 364,

        [EnumMember(Value = "Australia/Victoria")]
        AustraliaVictoria = 365,

        [EnumMember(Value = "Australia/West")]
        AustraliaWest = 366,

        [EnumMember(Value = "Australia/Yancowinna")]
        AustraliaYancowinna = 367,

        [EnumMember(Value = "Brazil/Acre")]
        BrazilAcre = 368,

        [EnumMember(Value = "Brazil/DeNoronha")]
        BrazilDenoronha = 369,

        [EnumMember(Value = "Brazil/East")]
        BrazilEast = 370,

        [EnumMember(Value = "Brazil/West")]
        BrazilWest = 371,

        [EnumMember(Value = "CET")]
        CET = 372,

        [EnumMember(Value = "CST6CDT")]
        CST6CDT = 373,

        [EnumMember(Value = "Canada/Atlantic")]
        CanadaAtlantic = 374,

        [EnumMember(Value = "Canada/Central")]
        CanadaCentral = 375,

        [EnumMember(Value = "Canada/Eastern")]
        CanadaEastern = 376,

        [EnumMember(Value = "Canada/Mountain")]
        CanadaMountain = 377,

        [EnumMember(Value = "Canada/Newfoundland")]
        CanadaNewfoundland = 378,

        [EnumMember(Value = "Canada/Pacific")]
        CanadaPacific = 379,

        [EnumMember(Value = "Canada/Saskatchewan")]
        CanadaSaskatchewan = 380,

        [EnumMember(Value = "Canada/Yukon")]
        CanadaYukon = 381,

        [EnumMember(Value = "Chile/Continental")]
        ChileContinental = 382,

        [EnumMember(Value = "Chile/EasterIsland")]
        ChileEasterisland = 383,

        [EnumMember(Value = "Cuba")]
        Cuba = 384,

        [EnumMember(Value = "EET")]
        EET = 385,

        [EnumMember(Value = "EST")]
        EST = 386,

        [EnumMember(Value = "EST5EDT")]
        EST5EDT = 387,

        [EnumMember(Value = "Egypt")]
        Egypt = 388,

        [EnumMember(Value = "Eire")]
        Eire = 389,

        [EnumMember(Value = "Europe/Amsterdam")]
        EuropeAmsterdam = 390,

        [EnumMember(Value = "Europe/Andorra")]
        EuropeAndorra = 391,

        [EnumMember(Value = "Europe/Astrakhan")]
        EuropeAstrakhan = 392,

        [EnumMember(Value = "Europe/Athens")]
        EuropeAthens = 393,

        [EnumMember(Value = "Europe/Belfast")]
        EuropeBelfast = 394,

        [EnumMember(Value = "Europe/Belgrade")]
        EuropeBelgrade = 395,

        [EnumMember(Value = "Europe/Berlin")]
        EuropeBerlin = 396,

        [EnumMember(Value = "Europe/Bratislava")]
        EuropeBratislava = 397,

        [EnumMember(Value = "Europe/Brussels")]
        EuropeBrussels = 398,

        [EnumMember(Value = "Europe/Bucharest")]
        EuropeBucharest = 399,

        [EnumMember(Value = "Europe/Budapest")]
        EuropeBudapest = 400,

        [EnumMember(Value = "Europe/Busingen")]
        EuropeBusingen = 401,

        [EnumMember(Value = "Europe/Chisinau")]
        EuropeChisinau = 402,

        [EnumMember(Value = "Europe/Copenhagen")]
        EuropeCopenhagen = 403,

        [EnumMember(Value = "Europe/Dublin")]
        EuropeDublin = 404,

        [EnumMember(Value = "Europe/Gibraltar")]
        EuropeGibraltar = 405,

        [EnumMember(Value = "Europe/Guernsey")]
        EuropeGuernsey = 406,

        [EnumMember(Value = "Europe/Helsinki")]
        EuropeHelsinki = 407,

        [EnumMember(Value = "Europe/Isle_of_Man")]
        EuropeIsleOfMan = 408,

        [EnumMember(Value = "Europe/Istanbul")]
        EuropeIstanbul = 409,

        [EnumMember(Value = "Europe/Jersey")]
        EuropeJersey = 410,

        [EnumMember(Value = "Europe/Kaliningrad")]
        EuropeKaliningrad = 411,

        [EnumMember(Value = "Europe/Kiev")]
        EuropeKiev = 412,

        [EnumMember(Value = "Europe/Kirov")]
        EuropeKirov = 413,

        [EnumMember(Value = "Europe/Lisbon")]
        EuropeLisbon = 414,

        [EnumMember(Value = "Europe/Ljubljana")]
        EuropeLjubljana = 415,

        [EnumMember(Value = "Europe/London")]
        EuropeLondon = 416,

        [EnumMember(Value = "Europe/Luxembourg")]
        EuropeLuxembourg = 417,

        [EnumMember(Value = "Europe/Madrid")]
        EuropeMadrid = 418,

        [EnumMember(Value = "Europe/Malta")]
        EuropeMalta = 419,

        [EnumMember(Value = "Europe/Mariehamn")]
        EuropeMariehamn = 420,

        [EnumMember(Value = "Europe/Minsk")]
        EuropeMinsk = 421,

        [EnumMember(Value = "Europe/Monaco")]
        EuropeMonaco = 422,

        [EnumMember(Value = "Europe/Moscow")]
        EuropeMoscow = 423,

        [EnumMember(Value = "Europe/Nicosia")]
        EuropeNicosia = 424,

        [EnumMember(Value = "Europe/Oslo")]
        EuropeOslo = 425,

        [EnumMember(Value = "Europe/Paris")]
        EuropeParis = 426,

        [EnumMember(Value = "Europe/Podgorica")]
        EuropePodgorica = 427,

        [EnumMember(Value = "Europe/Prague")]
        EuropePrague = 428,

        [EnumMember(Value = "Europe/Riga")]
        EuropeRiga = 429,

        [EnumMember(Value = "Europe/Rome")]
        EuropeRome = 430,

        [EnumMember(Value = "Europe/Samara")]
        EuropeSamara = 431,

        [EnumMember(Value = "Europe/San_Marino")]
        EuropeSanMarino = 432,

        [EnumMember(Value = "Europe/Sarajevo")]
        EuropeSarajevo = 433,

        [EnumMember(Value = "Europe/Saratov")]
        EuropeSaratov = 434,

        [EnumMember(Value = "Europe/Simferopol")]
        EuropeSimferopol = 435,

        [EnumMember(Value = "Europe/Skopje")]
        EuropeSkopje = 436,

        [EnumMember(Value = "Europe/Sofia")]
        EuropeSofia = 437,

        [EnumMember(Value = "Europe/Stockholm")]
        EuropeStockholm = 438,

        [EnumMember(Value = "Europe/Tallinn")]
        EuropeTallinn = 439,

        [EnumMember(Value = "Europe/Tirane")]
        EuropeTirane = 440,

        [EnumMember(Value = "Europe/Tiraspol")]
        EuropeTiraspol = 441,

        [EnumMember(Value = "Europe/Ulyanovsk")]
        EuropeUlyanovsk = 442,

        [EnumMember(Value = "Europe/Uzhgorod")]
        EuropeUzhgorod = 443,

        [EnumMember(Value = "Europe/Vaduz")]
        EuropeVaduz = 444,

        [EnumMember(Value = "Europe/Vatican")]
        EuropeVatican = 445,

        [EnumMember(Value = "Europe/Vienna")]
        EuropeVienna = 446,

        [EnumMember(Value = "Europe/Vilnius")]
        EuropeVilnius = 447,

        [EnumMember(Value = "Europe/Volgograd")]
        EuropeVolgograd = 448,

        [EnumMember(Value = "Europe/Warsaw")]
        EuropeWarsaw = 449,

        [EnumMember(Value = "Europe/Zagreb")]
        EuropeZagreb = 450,

        [EnumMember(Value = "Europe/Zaporozhye")]
        EuropeZaporozhye = 451,

        [EnumMember(Value = "Europe/Zurich")]
        EuropeZurich = 452,

        [EnumMember(Value = "GB")]
        GB = 453,

        [EnumMember(Value = "GB-Eire")]
        GBEire = 454,

        [EnumMember(Value = "GMT")]
        GMT = 455,

        [EnumMember(Value = "GMT+0")]
        Gmtplus0 = 456,

        [EnumMember(Value = "GMT-0")]
        GMTDash0 = 457,

        [EnumMember(Value = "GMT0")]
        GMT0 = 458,

        [EnumMember(Value = "Greenwich")]
        Greenwich = 459,

        [EnumMember(Value = "HST")]
        HST = 460,

        [EnumMember(Value = "Hongkong")]
        Hongkong = 461,

        [EnumMember(Value = "Iceland")]
        Iceland = 462,

        [EnumMember(Value = "Indian/Antananarivo")]
        IndianAntananarivo = 463,

        [EnumMember(Value = "Indian/Chagos")]
        IndianChagos = 464,

        [EnumMember(Value = "Indian/Christmas")]
        IndianChristmas = 465,

        [EnumMember(Value = "Indian/Cocos")]
        IndianCocos = 466,

        [EnumMember(Value = "Indian/Comoro")]
        IndianComoro = 467,

        [EnumMember(Value = "Indian/Kerguelen")]
        IndianKerguelen = 468,

        [EnumMember(Value = "Indian/Mahe")]
        IndianMahe = 469,

        [EnumMember(Value = "Indian/Maldives")]
        IndianMaldives = 470,

        [EnumMember(Value = "Indian/Mauritius")]
        IndianMauritius = 471,

        [EnumMember(Value = "Indian/Mayotte")]
        IndianMayotte = 472,

        [EnumMember(Value = "Indian/Reunion")]
        IndianReunion = 473,

        [EnumMember(Value = "Iran")]
        Iran = 474,

        [EnumMember(Value = "Israel")]
        Israel = 475,

        [EnumMember(Value = "Jamaica")]
        Jamaica = 476,

        [EnumMember(Value = "Japan")]
        Japan = 477,

        [EnumMember(Value = "Kwajalein")]
        Kwajalein = 478,

        [EnumMember(Value = "Libya")]
        Libya = 479,

        [EnumMember(Value = "MET")]
        MET = 480,

        [EnumMember(Value = "MST")]
        MST = 481,

        [EnumMember(Value = "MST7MDT")]
        MST7MDT = 482,

        [EnumMember(Value = "Mexico/BajaNorte")]
        MexicoBajanorte = 483,

        [EnumMember(Value = "Mexico/BajaSur")]
        MexicoBajasur = 484,

        [EnumMember(Value = "Mexico/General")]
        MexicoGeneral = 485,

        [EnumMember(Value = "NZ")]
        NZ = 486,

        [EnumMember(Value = "NZ-CHAT")]
        NZCHAT = 487,

        [EnumMember(Value = "Navajo")]
        Navajo = 488,

        [EnumMember(Value = "PRC")]
        PRC = 489,

        [EnumMember(Value = "PST8PDT")]
        PST8PDT = 490,

        [EnumMember(Value = "Pacific/Apia")]
        PacificApia = 491,

        [EnumMember(Value = "Pacific/Auckland")]
        PacificAuckland = 492,

        [EnumMember(Value = "Pacific/Bougainville")]
        PacificBougainville = 493,

        [EnumMember(Value = "Pacific/Chatham")]
        PacificChatham = 494,

        [EnumMember(Value = "Pacific/Chuuk")]
        PacificChuuk = 495,

        [EnumMember(Value = "Pacific/Easter")]
        PacificEaster = 496,

        [EnumMember(Value = "Pacific/Efate")]
        PacificEfate = 497,

        [EnumMember(Value = "Pacific/Enderbury")]
        PacificEnderbury = 498,

        [EnumMember(Value = "Pacific/Fakaofo")]
        PacificFakaofo = 499,

        [EnumMember(Value = "Pacific/Fiji")]
        PacificFiji = 500,

        [EnumMember(Value = "Pacific/Funafuti")]
        PacificFunafuti = 501,

        [EnumMember(Value = "Pacific/Galapagos")]
        PacificGalapagos = 502,

        [EnumMember(Value = "Pacific/Gambier")]
        PacificGambier = 503,

        [EnumMember(Value = "Pacific/Guadalcanal")]
        PacificGuadalcanal = 504,

        [EnumMember(Value = "Pacific/Guam")]
        PacificGuam = 505,

        [EnumMember(Value = "Pacific/Honolulu")]
        PacificHonolulu = 506,

        [EnumMember(Value = "Pacific/Johnston")]
        PacificJohnston = 507,

        [EnumMember(Value = "Pacific/Kiritimati")]
        PacificKiritimati = 508,

        [EnumMember(Value = "Pacific/Kosrae")]
        PacificKosrae = 509,

        [EnumMember(Value = "Pacific/Kwajalein")]
        PacificKwajalein = 510,

        [EnumMember(Value = "Pacific/Majuro")]
        PacificMajuro = 511,

        [EnumMember(Value = "Pacific/Marquesas")]
        PacificMarquesas = 512,

        [EnumMember(Value = "Pacific/Midway")]
        PacificMidway = 513,

        [EnumMember(Value = "Pacific/Nauru")]
        PacificNauru = 514,

        [EnumMember(Value = "Pacific/Niue")]
        PacificNiue = 515,

        [EnumMember(Value = "Pacific/Norfolk")]
        PacificNorfolk = 516,

        [EnumMember(Value = "Pacific/Noumea")]
        PacificNoumea = 517,

        [EnumMember(Value = "Pacific/Pago_Pago")]
        PacificPagoPago = 518,

        [EnumMember(Value = "Pacific/Palau")]
        PacificPalau = 519,

        [EnumMember(Value = "Pacific/Pitcairn")]
        PacificPitcairn = 520,

        [EnumMember(Value = "Pacific/Pohnpei")]
        PacificPohnpei = 521,

        [EnumMember(Value = "Pacific/Ponape")]
        PacificPonape = 522,

        [EnumMember(Value = "Pacific/Port_Moresby")]
        PacificPortMoresby = 523,

        [EnumMember(Value = "Pacific/Rarotonga")]
        PacificRarotonga = 524,

        [EnumMember(Value = "Pacific/Saipan")]
        PacificSaipan = 525,

        [EnumMember(Value = "Pacific/Samoa")]
        PacificSamoa = 526,

        [EnumMember(Value = "Pacific/Tahiti")]
        PacificTahiti = 527,

        [EnumMember(Value = "Pacific/Tarawa")]
        PacificTarawa = 528,

        [EnumMember(Value = "Pacific/Tongatapu")]
        PacificTongatapu = 529,

        [EnumMember(Value = "Pacific/Truk")]
        PacificTruk = 530,

        [EnumMember(Value = "Pacific/Wake")]
        PacificWake = 531,

        [EnumMember(Value = "Pacific/Wallis")]
        PacificWallis = 532,

        [EnumMember(Value = "Pacific/Yap")]
        PacificYap = 533,

        [EnumMember(Value = "Poland")]
        Poland = 534,

        [EnumMember(Value = "Portugal")]
        Portugal = 535,

        [EnumMember(Value = "ROC")]
        ROC = 536,

        [EnumMember(Value = "ROK")]
        ROK = 537,

        [EnumMember(Value = "Singapore")]
        Singapore = 538,

        [EnumMember(Value = "Turkey")]
        Turkey = 539,

        [EnumMember(Value = "UCT")]
        UCT = 540,

        [EnumMember(Value = "US/Alaska")]
        USAlaska = 541,

        [EnumMember(Value = "US/Aleutian")]
        USAleutian = 542,

        [EnumMember(Value = "US/Arizona")]
        USArizona = 543,

        [EnumMember(Value = "US/Central")]
        USCentral = 544,

        [EnumMember(Value = "US/East-Indiana")]
        USEastIndiana = 545,

        [EnumMember(Value = "US/Eastern")]
        USEastern = 546,

        [EnumMember(Value = "US/Hawaii")]
        USHawaii = 547,

        [EnumMember(Value = "US/Indiana-Starke")]
        USIndianaStarke = 548,

        [EnumMember(Value = "US/Michigan")]
        USMichigan = 549,

        [EnumMember(Value = "US/Mountain")]
        USMountain = 550,

        [EnumMember(Value = "US/Pacific")]
        USPacific = 551,

        [EnumMember(Value = "US/Samoa")]
        USSamoa = 552,

        [EnumMember(Value = "UTC")]
        UTC = 553,

        [EnumMember(Value = "Universal")]
        Universal = 554,

        [EnumMember(Value = "W-SU")]
        WSU = 555,

        [EnumMember(Value = "WET")]
        WET = 556,

        [EnumMember(Value = "Zulu")]
        Zulu = 557,

        [EnumMember(Value = "Etc/GMT")]
        EtcGMT = 558,

        [EnumMember(Value = "Etc/GMT+0")]
        EtcGmtplus0 = 559,

        [EnumMember(Value = "Etc/GMT+1")]
        EtcGmtplus1 = 560,

        [EnumMember(Value = "Etc/GMT+10")]
        EtcGmtplus10 = 561,

        [EnumMember(Value = "Etc/GMT+11")]
        EtcGmtplus11 = 562,

        [EnumMember(Value = "Etc/GMT+12")]
        EtcGmtplus12 = 563,

        [EnumMember(Value = "Etc/GMT+2")]
        EtcGmtplus2 = 564,

        [EnumMember(Value = "Etc/GMT+3")]
        EtcGmtplus3 = 565,

        [EnumMember(Value = "Etc/GMT+4")]
        EtcGmtplus4 = 566,

        [EnumMember(Value = "Etc/GMT+5")]
        EtcGmtplus5 = 567,

        [EnumMember(Value = "Etc/GMT+6")]
        EtcGmtplus6 = 568,

        [EnumMember(Value = "Etc/GMT+7")]
        EtcGmtplus7 = 569,

        [EnumMember(Value = "Etc/GMT+8")]
        EtcGmtplus8 = 570,

        [EnumMember(Value = "Etc/GMT+9")]
        EtcGmtplus9 = 571,

        [EnumMember(Value = "Etc/GMT-0")]
        EtcGMT0 = 572,

        [EnumMember(Value = "Etc/GMT-1")]
        EtcGMT1 = 573,

        [EnumMember(Value = "Etc/GMT-10")]
        EtcGMT10 = 574,

        [EnumMember(Value = "Etc/GMT-11")]
        EtcGMT11 = 575,

        [EnumMember(Value = "Etc/GMT-12")]
        EtcGMT12 = 576,

        [EnumMember(Value = "Etc/GMT-13")]
        EtcGMT13 = 577,

        [EnumMember(Value = "Etc/GMT-14")]
        EtcGMT14 = 578,

        [EnumMember(Value = "Etc/GMT-2")]
        EtcGMT2 = 579,

        [EnumMember(Value = "Etc/GMT-3")]
        EtcGMT3 = 580,

        [EnumMember(Value = "Etc/GMT-4")]
        EtcGMT4 = 581,

        [EnumMember(Value = "Etc/GMT-5")]
        EtcGMT5 = 582,

        [EnumMember(Value = "Etc/GMT-6")]
        EtcGMT6 = 583,

        [EnumMember(Value = "Etc/GMT-7")]
        EtcGMT7 = 584,

        [EnumMember(Value = "Etc/GMT-8")]
        EtcGMT8 = 585,

        [EnumMember(Value = "Etc/GMT-9")]
        EtcGMT9 = 586,

        [EnumMember(Value = "Etc/GMT0")]
        EtcGMTDash0 = 587,

        [EnumMember(Value = "Etc/Greenwich")]
        EtcGreenwich = 588,

        [EnumMember(Value = "Etc/UCT")]
        EtcUCT = 589,

        [EnumMember(Value = "Etc/UTC")]
        EtcUTC = 590,

        [EnumMember(Value = "Etc/Universal")]
        EtcUniversal = 591,

        [EnumMember(Value = "Etc/Zulu")]
        EtcZulu = 592,
    }


    // Invoice & Delivery addresses are used in sales orders. Private addresses are only visible by authorized users.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AddressTypeResUsersOdooEnum
    {
        [EnumMember(Value = "contact")]
        Contact = 1,

        [EnumMember(Value = "invoice")]
        InvoiceAddress = 2,

        [EnumMember(Value = "delivery")]
        DeliveryAddress = 3,

        [EnumMember(Value = "other")]
        OtherAddress = 4,

        [EnumMember(Value = "private")]
        PrivateAddress = 5,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyTypeResUsersOdooEnum
    {
        [EnumMember(Value = "person")]
        Individual = 1,

        [EnumMember(Value = "company")]
        Company = 2,
    }


    // Status based on activities
    // Overdue: Due date is already passed
    // Today: Activity date is today
    // Planned: Future activities.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityStateResUsersOdooEnum
    {
        [EnumMember(Value = "overdue")]
        Overdue = 1,

        [EnumMember(Value = "today")]
        Today = 2,

        [EnumMember(Value = "planned")]
        Planned = 3,
    }


    // Type of the exception activity on record.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityExceptionDecorationResUsersOdooEnum
    {
        [EnumMember(Value = "warning")]
        Alert = 1,

        [EnumMember(Value = "danger")]
        Error = 2,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum DegreeOfTrustYouHaveInThisDebtorResUsersOdooEnum
    {
        [EnumMember(Value = "good")]
        GoodDebtor = 1,

        [EnumMember(Value = "normal")]
        NormalDebtor = 2,

        [EnumMember(Value = "bad")]
        BadDebtor = 3,
    }


    // Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoiceResUsersOdooEnum
    {
        [EnumMember(Value = "no-message")]
        NoMessage = 1,

        [EnumMember(Value = "warning")]
        Warning = 2,

        [EnumMember(Value = "block")]
        BlockingMessage = 3,
    }


    // Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SalesWarningsResUsersOdooEnum
    {
        [EnumMember(Value = "no-message")]
        NoMessage = 1,

        [EnumMember(Value = "warning")]
        Warning = 2,

        [EnumMember(Value = "block")]
        BlockingMessage = 3,
    }


    // Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StockPickingResUsersOdooEnum
    {
        [EnumMember(Value = "no-message")]
        NoMessage = 1,

        [EnumMember(Value = "warning")]
        Warning = 2,

        [EnumMember(Value = "block")]
        BlockingMessage = 3,
    }


    // Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PurchaseOrderResUsersOdooEnum
    {
        [EnumMember(Value = "no-message")]
        NoMessage = 1,

        [EnumMember(Value = "warning")]
        Warning = 2,

        [EnumMember(Value = "block")]
        BlockingMessage = 3,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum AdministrationResUsersOdooEnum
    {
        [EnumMember(Value = "False")]

         DummyUser = 1,

        [EnumMember(Value = "2")]
        AccessRights = 2,

        [EnumMember(Value = "3")]
        Settings = 3,
    }


    // User: Own Documents Only: the user will have access to his own data in the sales application.
    // User: All Documents: the user will have access to all records of everyone in the sales application.
    // Administrator: the user will have an access to the sales configuration as well as statistic reports.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SalesResUsersOdooEnum
    {
        [EnumMember(Value = "False")]
        DummyUser = 1,

        [EnumMember(Value = "30")]
        UserOwnDocumentsOnly = 2,

        [EnumMember(Value = "31")]
        UserAllDocuments = 3,

        [EnumMember(Value = "32")]
        Administrator = 4,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum InventoryResUsersOdooEnum
    {
        [EnumMember(Value = "False")]

         DummyUser = 1,

        [EnumMember(Value = "44")]
        User = 2,

        [EnumMember(Value = "45")]
        Administrator = 3,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoicingResUsersOdooEnum
    {
        [EnumMember(Value = "False")]

        User = 1,

        [EnumMember(Value = "23")]
        Billing = 2,

        [EnumMember(Value = "25")]
        BillingAdministrator = 3,
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum PurchaseResUsersOdooEnum
    {
        [EnumMember(Value = "False")]

         DummyUser= 1,

        [EnumMember(Value = "38")]
        User = 2,

        [EnumMember(Value = "39")]
        Administrator = 3,
    }


    // Portal: Portal members have specific access rights (such as record rules and restricted menus).
    //                 They usually do not belong to the usual Odoo groups.
    // Public: Public users have specific access rights (such as record rules and restricted menus).
    //                 They usually do not belong to the usual Odoo groups.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserTypesResUsersOdooEnum
    {
        [EnumMember(Value = "1")]
        InternalUser = 1,

        [EnumMember(Value = "9")]
        Portal = 2,

        [EnumMember(Value = "10")]
        Public = 3,
    }

}
