using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PlayNANOO;
using PlayNANOO.CloudCode;
using PlayNANOO.SimpleJSON;

public class PlayNANOOExample : MonoBehaviour
{
    Plugin plugin;
    void Start()
    {
        plugin = Plugin.GetInstance();

        plugin.SetLanguage(Configure.PN_LANG_EN);

        // Guest SignIn
        plugin.AccountGuestSignIn((status, errorCode, jsonString, values) =>
        {
            //plugin.Postbox.EventAction(OnPostboxActionCallback);
            //plugin.Postbox.SubscribeAction(OnPostboxSubscribeActionCallback);
            //plugin.Invite.Action(OnInviteActionCallback);
        });
    }

    // Update is called once per frame
    void Update()
    {
        plugin.AccountCheckDuplicate(OnCheckAccountDuplicate);
    }

    void OnCheckAccountDuplicate(bool isDuplicate)
    {
        if (isDuplicate)
        {
            Debug.LogError("Duplicate connection has been detected.");
        }
    }

    void OnPostboxActionCallback(string status, string errorCode, string jsonString, Dictionary<string, object> values)
    {
        if (status.Equals(Configure.PN_API_STATE_SUCCESS))
        {
            ArrayList items = (ArrayList)values["Items"];
            foreach (Dictionary<string, object> item in items)
            {
                Debug.Log(item["ItemCode"]);
                Debug.Log(item["ItemCount"]);
            }
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void OnPostboxSubscribeActionCallback(string status, string errorCode, string jsonString, Dictionary<string, object> values)
    {
        if (status.Equals(Configure.PN_API_STATE_SUCCESS))
        {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList items = (ArrayList)values["Items"];
                foreach (Dictionary<string, object> item in items)
                {
                    Debug.Log(item["SubscribeProduct"]);
                    Debug.Log(item["SubscribeTimeUntilExpire"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void OnInviteActionCallback(string status, string errorCode, string jsonString, Dictionary<string, object> values)
    {
        if (status.Equals(Configure.PN_API_STATE_SUCCESS))
        {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList items = (ArrayList)values["Items"];
                foreach (Dictionary<string, object> item in items)
                {
                    Debug.Log(item["ItemCode"]);
                    Debug.Log(item["ItemCount"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    public void OpenBanner()
    {
        plugin.OpenBanner();
    }

    public void OpenForum()
    {
        plugin.OpenForum();
    }

    public void OpenHelpDesk()
    {
        plugin.SetHelpDeskOptional("OptionTest1", "ValueTest1");
        plugin.SetHelpDeskOptional("OptionTest2", "ValueTest2");
        plugin.OpenHelpDesk();
    }

    public void ForumThread()
    {
        plugin.ForumThread(Configure.PN_FORUM_THREAD, 10, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];
                foreach (Dictionary<string, object> thread in list)
                {
                    Debug.Log(thread["seq"]);
                    Debug.Log(thread["title"]);
                    Debug.Log(thread["summary"]);
                    Debug.Log(thread["attach_file"]);
                    Debug.Log(thread["url"]);
                    Debug.Log(thread["post_date"]);

                    foreach (string attach in (ArrayList)thread["attach_file"])
                    {
                        Debug.Log(attach);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    #region Accounts
    public void AccountLink()
    {
        plugin.AccountLink("TEST_UserUniqueID", Configure.PN_ACCOUNT_GOOGLE, (status, errorCode, jsonString, values) =>
        {
            Debug.Log(values["access_token"].ToString());
            Debug.Log(values["refresh_token"].ToString());
            Debug.Log(values["uuid"].ToString());
            Debug.Log(values["openID"].ToString());
            Debug.Log(values["nickname"].ToString());
            Debug.Log(values["linkedID"].ToString());
            Debug.Log(values["linkedType"].ToString());
            Debug.Log(values["country"].ToString());
        });
    }

    public void AccountGuest()
    {
        plugin.AccountGuestSignIn((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountEmailSignIn()
    {
        string email = "test@email.com";
        string password = "password";
        plugin.AccountEmailSignIn(email, password, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountEmailSignUp()
    {
        string email = "test@email.com";
        string password = "password";
        plugin.AccountEmailSignUp(email, password, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknameExists()
    {
        string nickname = "TEST_Nickname";
        plugin.AccountNicknameExists(nickname, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["status"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknameGet()
    {
        plugin.AccountNicknameGet((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["nickname"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountNicknamePut()
    {
        string nickname = "TEST_Nickname";
        plugin.AccountNickanmePut(nickname, true, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["nickname"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenInfo()
    {
        plugin.AccountTokenInfo((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenStatus()
    {
        plugin.AccountTokenStatus((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["status"].ToString());
            }
            else
            {
                Debug.Log("Fail");
                Debug.Log(jsonString.ToString());
            }
        });
    }

    public void AccountTokenRefresh()
    {
        plugin.AccountTokenRefresh((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenSingIn()
    {
        plugin.AccountTokenSignIn((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenSingOut()
    {
        plugin.AccountTokenSignOut((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(jsonString);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountTokenClear()
    {
        plugin.AccountDeviceClear((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(jsonString);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountSocialChange()
    {
        string accountToken = "SERVICE_TOKEN";
        plugin.AccountSocialChange(accountToken, Configure.PN_ACCOUNT_APPLE_ID, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void AccountSocialChangePrevious()
    {
        string accountUserUniqueID = "UserUniqueID";
        string accountToken = "SERVICE_TOKEN";
        plugin.AccountSocialChangePrevious(accountUserUniqueID, accountToken, Configure.PN_ACCOUNT_APPLE_ID, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["access_token"].ToString());
                Debug.Log(values["refresh_token"].ToString());
                Debug.Log(values["uuid"].ToString());
                Debug.Log(values["openID"].ToString());
                Debug.Log(values["nickname"].ToString());
                Debug.Log(values["linkedID"].ToString());
                Debug.Log(values["linkedType"].ToString());
                Debug.Log(values["country"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Server Time
    public void ServerTime()
    {
        plugin.ServerTime((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["timezone"]);
                Debug.Log(dictionary["timestamp"]);
                Debug.Log(dictionary["ISO_8601_date"]);
                Debug.Log(dictionary["date"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Coupon
    public void Coupon()
    {
        plugin.Coupon("COUPON_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["code"]);
                Debug.Log(dictionary["item_code"]);
                Debug.Log(dictionary["item_count"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Inbox(Postbox)
    public void Postbox()
    {
        plugin.PostboxItem((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList items = (ArrayList)dictionary["item"];
                foreach (Dictionary<string, object> item in items)
                {
                    Debug.Log(item["uid"]);
                    Debug.Log(item["message"]);
                    Debug.Log(item["item_code"]);
                    Debug.Log(item["item_count"]);
                    Debug.Log(item["expire_sec"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxCount()
    {
        plugin.Postbox.Count((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["Count"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxItemSend()
    {
        plugin.PostboxItemSend("ITEM_CODE", 1, 7, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxItemUse()
    {
        plugin.PostboxItemUse("ITEM_UID", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["item_code"]);
                Debug.Log(dictionary["item_count"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxMultiItemUse()
    {
        ArrayList items = new ArrayList();
        items.Add("ITEM_UID_1");
        items.Add("ITEM_UID_2");

        plugin.PostboxMultiItemUse(items, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList useItems = (ArrayList)dictionary["item"];
                foreach (Dictionary<string, object> item in useItems)
                {
                    Debug.Log(item["uid"]);
                    Debug.Log(item["item_code"]);
                    Debug.Log(item["item_count"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxClear()
    {
        plugin.PostboxClear((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxSubscriptionRegister()
    {
        plugin.PostboxSubscriptionRegister("PRODUCT_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void PostboxSubscriptionCancel()
    {
        plugin.PostboxSubscriptionCancel("PRODUCT_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Cloud Data(Storage)
    public void StorageSave()
    {
        plugin.StorageSave("TEST_KEY", "TEST_KEY_VALUE", false, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StorageSavePlus()
    {
        plugin.Storage.SavePlus("TEST_KEY", "TEST_KEY_VALUE", true, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["ActionKey"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });

        plugin.Storage.SavePlus("TEST_KEY2", "TEST_KEY_VALUE", true, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["ActionKey"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StorageLoad()
    {
        plugin.StorageLoad("TEST_KEY", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StorageLoadPlus()
    {
        plugin.Storage.LoadPlus("TEST_KEY", (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["StorageKey"]);
                Debug.Log(values["StorageValue"]);
                Debug.Log(values["ActionKey"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StoragePlayerDataLoadPlus()
    {
        plugin.Storage.PlayerDataLoadPlus("TEST_KEY", "TEST_KEY_VALUE", (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["StorageKey"]);
                Debug.Log(values["StorageValue"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void StorageLoadPublic()
    {
        plugin.Storage.LoadPublic("TEST_KEY", (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["StorageKey"]);
                Debug.Log(values["StorageValue"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Leaderboard(Ranking)
    public void Ranking()
    {
        plugin.Ranking("RANKING_CODE", 50, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];
                foreach (Dictionary<string, object> rank in list)
                {
                    Debug.Log(rank["score"]);
                    Debug.Log(rank["data"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingRange()
    {
        plugin.RankingRange("RANKING_CODE", 1, 10, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach(Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["ranking"]);
                    Debug.Log(item["uuid"]);
                    Debug.Log(item["nickname"]);
                    Debug.Log(item["score"]);
                    Debug.Log(item["data"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingPreviousRange()
    {
        plugin.RankingPreviousRange("RANKING_CODE", 1, 1, 10, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["ranking"]);
                    Debug.Log(item["uuid"]);
                    Debug.Log(item["nickname"]);
                    Debug.Log(item["score"]);
                    Debug.Log(item["data"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingRecord()
    {
        plugin.RankingRecord("RANKING_CODE", 100, "TEST_PLAYER_RANKING_VALUE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingPersonal()
    {
        plugin.RankingPersonal("RANKING_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["ranking"]);
                Debug.Log(dictionary["data"]);
                Debug.Log(dictionary["total_player"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void RankingSeason()
    {
        plugin.RankingSeasonInfo("RANKING_CODE", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["season"]);
                Debug.Log(dictionary["expire_sec"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region In App Purchase
    public void IapReceiptionAndroid()
    {
        plugin.IAP.Android("RECEIPT", (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["UserID"]);
                Debug.Log(values["PackageName"]);
                Debug.Log(values["OrderID"]);
                Debug.Log(values["ProductID"]);
                Debug.Log(values["Currency"]);
                Debug.Log(values["Price"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void IapReceiptioniOS()
    {
        plugin.IAP.IOS("RECEIPT", "PRODUCT_ID", "CURRENCY", 100, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["UserID"]);
                Debug.Log(values["PackageName"]);
                Debug.Log(values["OrderID"]);
                Debug.Log(values["ProductID"]);
                Debug.Log(values["Currency"]);
                Debug.Log(values["Price"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void IapReceiptionOneStoreKR()
    {
        plugin.ReceiptVerificationOneStoreKR("PRODUCT_ID", "PURCHASE_ID", "RECEIPT", "CURRENCY", 100, true, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["package"]);
                Debug.Log(dictionary["product_id"]);
                Debug.Log(dictionary["order_id"]);
                Debug.Log("Issue Item");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Invite
    public void Invite(string inviteCode)
    {
        plugin.InviteUrl(inviteCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                string url = dictionary["url"].ToString();

                plugin.OpenShare("Please Enter Invite Message", url);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Cache Data
    public void CacheExists()
    {
        string cacheKey = "TEST001";
        plugin.CacheExists(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheGet()
    {
        string cacheKey = "TEST001";
        plugin.CacheGet(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheMultiGet()
    {
        ArrayList cacheKeys = new ArrayList();
        cacheKeys.Add("TEST001");
        cacheKeys.Add("TEST002");
        cacheKeys.Add("TEST003");

        plugin.CacheMultiGet(cacheKeys, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["values"])
                {
                    Debug.Log(value["key"]);
                    Debug.Log(value["value"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheSet()
    {
        string cacheKey = "TEST001";
        string cacheValue = "TESTValue";
        int cacheTTL = 3600;
        plugin.CacheSet(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheIncrby()
    {
        string cacheKey = "TEST002";
        int cacheValue = 100;
        int cacheTTL = 3600;
        plugin.CacheIncrby(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheDecrby()
    {
        string cacheKey = "TEST003";
        int cacheValue = 100;
        int cacheTTL = 3600;
        plugin.CacheDecrby(cacheKey, cacheValue, cacheTTL, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["value"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CacheDelete()
    {
        string cacheKey = "TEST003";
        plugin.CacheDel(cacheKey, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Currency
    public void CurrencyAll()
    {
        plugin.CurrencyAll((status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach(Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["currency"]);
                    Debug.Log(item["amount"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencySet()
    {
        plugin.CurrencySet("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencyGet()
    {
        plugin.CurrencyGet("CURRENCY_CODE", (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencyCharge()
    {
        plugin.CurrencyCharge("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void CurrencySubtract()
    {
        plugin.CurrencySubtract("CURRENCY_CODE", 1000, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["amount"]);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region CloudeCode
    public void CloudCodeExecution()
    {
        var parameters = new CloudCodeExecution()
        {
            TableCode = "TABLE_CODE",
            FunctionName = "FUNCTION_NAME",
            FunctionArguments = new { InputValue1 = "InputValue1", InputValue2 = "InputValue2", InputValue3 = "InputValue3" }
        };

        plugin.CloudCode.Run(parameters, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                PlayNANOO.SimpleJSON.JSONNode node = PlayNANOO.SimpleJSON.JSONNode.Parse(dictionary["Result"].ToString());
                Debug.Log(node["Function"]["Name"].Value);
                Debug.Log(node["Function"]["Version"].Value);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Friends
    public void FriendInfo()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.FriendInfo(friendCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["relationship_count"].ToString());
                Debug.Log(dictionary["ready_count"].ToString());
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendAll()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.Friend(friendCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["relationship_code"]);
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["access_timezone"]);
                    Debug.Log(value["access_diff"]);
                    Debug.Log(value["access_timestamp"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendReadyAll()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        plugin.FriendReady(friendCode, (state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["relationship_code"]);
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["access_timezone"]);
                    Debug.Log(value["access_diff"]);
                    Debug.Log(value["access_timestamp"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendRequest()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendOpenID = "PLAYER_OPEN_ID";
        plugin.FriendRequest(friendCode, friendOpenID, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendAccept()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendRelationshipCode = "FRIEND_RELATIONSHIP_CODE";
        plugin.FriendAccept(friendCode, friendRelationshipCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendDelete()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        string friendRelationshipCode = "FRIEND_RELATIONSHIP_CODE";
        plugin.FriendDelete(friendCode, friendRelationshipCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void FriendRandomSearch()
    {
        string friendCode = "FRIEND_TABLE_CODE";
        int limit = 10;
        plugin.FriendRandomSearch(friendCode, limit, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)dictionary["friends"])
                {
                    Debug.Log(value["uuid"]);
                    Debug.Log(value["open_id"]);
                    Debug.Log(value["nickname"]);
                    Debug.Log(value["signin_time"]);
                    Debug.Log(value["signin_date"]);
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region BlockReason
    public void BlockReason()
    {
        plugin.BlockReason((status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> value in (ArrayList)values["Items"])
                {
                    Debug.Log(value["Reason"]);
                    Debug.Log(value["Permanent"]);
                    Debug.Log(value["ExpireDate"]);
                    Debug.Log(value["TimeUntilExpire"]);
                    foreach(string service in (string[])value["Services"])
                    {
                        Debug.Log(service);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    #endregion

    #region Event logs
    public void LogWrite()
    {
        var messages = new PlayNANOO.Monitor.LogMessages();
        messages.Add(Configure.PN_LOG_DEBUG, "Message1");
        messages.Add(Configure.PN_LOG_INFO, "Message2");
        messages.Add(Configure.PN_LOG_ERROR, "Message3");

        plugin.LogWrite(new PlayNANOO.Monitor.LogWrite()
        {
            EventCode = "TEST_LOG_20210607001",
            EventMessages = messages
        });
    }
    #endregion

    public void OnApplicationFocus(bool focus)
    {
        if (plugin != null && focus)
        {
            plugin.Postbox.EventAction(OnPostboxActionCallback);
            plugin.Postbox.SubscribeAction(OnPostboxSubscribeActionCallback);
            plugin.Invite.Action(OnInviteActionCallback);
        }
        Debug.Log("Focus");
    }
}
