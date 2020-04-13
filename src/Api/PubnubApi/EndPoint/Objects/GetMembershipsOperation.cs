﻿using System;
using System.Collections.Generic;
using System.Linq;
using PubnubApi.Interface;
using System.Threading;
using System.Net;
using System.Threading.Tasks;

namespace PubnubApi.EndPoint
{
    public class GetMembershipsOperation : PubnubCoreBase
    {
        private readonly PNConfiguration config;
        private readonly IJsonPluggableLibrary jsonLibrary;
        private readonly IPubnubUnitTest unit;
        private readonly IPubnubLog pubnubLog;
        private readonly EndPoint.TelemetryManager pubnubTelemetryMgr;
        private readonly EndPoint.TokenManager pubnubTokenMgr;

        private string usrId = "";
        private int limit = -1;
        private bool includeCount;
        private string commandDelimitedIncludeOptions = "";
        private string membershipsFilter;
        private PNPage page;
        private List<string> sortField;

        private PNCallback<PNGetMembershipsResult> savedCallback;
        private Dictionary<string, object> queryParam;

        public GetMembershipsOperation(PNConfiguration pubnubConfig, IJsonPluggableLibrary jsonPluggableLibrary, IPubnubUnitTest pubnubUnit, IPubnubLog log, EndPoint.TelemetryManager telemetryManager, EndPoint.TokenManager tokenManager, Pubnub instance) : base(pubnubConfig, jsonPluggableLibrary, pubnubUnit, log, telemetryManager, tokenManager, instance)
        {
            config = pubnubConfig;
            jsonLibrary = jsonPluggableLibrary;
            unit = pubnubUnit;
            pubnubLog = log;
            pubnubTelemetryMgr = telemetryManager;
            pubnubTokenMgr = tokenManager;

            if (instance != null)
            {
                if (!ChannelRequest.ContainsKey(instance.InstanceId))
                {
                    ChannelRequest.GetOrAdd(instance.InstanceId, new ConcurrentDictionary<string, HttpWebRequest>());
                }
                if (!ChannelInternetStatus.ContainsKey(instance.InstanceId))
                {
                    ChannelInternetStatus.GetOrAdd(instance.InstanceId, new ConcurrentDictionary<string, bool>());
                }
                if (!ChannelGroupInternetStatus.ContainsKey(instance.InstanceId))
                {
                    ChannelGroupInternetStatus.GetOrAdd(instance.InstanceId, new ConcurrentDictionary<string, bool>());
                }
            }
        }

        public GetMembershipsOperation UserId(string id)
        {
            this.usrId = id;
            return this;
        }

        public GetMembershipsOperation Page(PNPage bookmarkPage)
        {
            this.page = bookmarkPage;
            return this;
        }

        public GetMembershipsOperation Limit(int numberOfObjects)
        {
            this.limit = numberOfObjects;
            return this;
        }

        public GetMembershipsOperation IncludeCount(bool includeTotalCount)
        {
            this.includeCount = includeTotalCount;
            return this;
        }

        public GetMembershipsOperation Include(PNMembershipField[] includeOptions)
        {
            if (includeOptions != null)
            {
                string[] arrayInclude = includeOptions.Select(x => MapEnumValueToEndpoint(x.ToString())).ToArray();
                this.commandDelimitedIncludeOptions = string.Join(",", arrayInclude);
            }
            return this;
        }

        public GetMembershipsOperation Filter(string filterExpression)
        {
            this.membershipsFilter = filterExpression;
            return this;
        }

        public GetMembershipsOperation Sort(List<string> sortByField)
        {
            this.sortField = sortByField;
            return this;
        }

        public GetMembershipsOperation QueryParam(Dictionary<string, object> customQueryParam)
        {
            this.queryParam = customQueryParam;
            return this;
        }

        public void Execute(PNCallback<PNGetMembershipsResult> callback)
        {
#if NETFX_CORE || WINDOWS_UWP || UAP || NETSTANDARD10 || NETSTANDARD11 || NETSTANDARD12
            Task.Factory.StartNew(() =>
            {
                this.savedCallback = callback;
                GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam, savedCallback);
            }, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default).ConfigureAwait(false);
#else
            new Thread(() =>
            {
                this.savedCallback = callback;
                GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam, savedCallback);
            })
            { IsBackground = true }.Start();
#endif
        }

        public async Task<PNResult<PNGetMembershipsResult>> ExecuteAsync()
        {
#if NETFX_CORE || WINDOWS_UWP || UAP || NETSTANDARD10 || NETSTANDARD11 || NETSTANDARD12
            return await GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam).ConfigureAwait(false);
#else
            return await GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam).ConfigureAwait(false);
#endif
        }

        internal void Retry()
        {
#if NETFX_CORE || WINDOWS_UWP || UAP || NETSTANDARD10 || NETSTANDARD11 || NETSTANDARD12
            Task.Factory.StartNew(() =>
            {
                GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam, savedCallback);
            }, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default).ConfigureAwait(false);
#else
            new Thread(() =>
            {
                GetMembershipsList(this.usrId, this.page, this.limit, this.includeCount, this.commandDelimitedIncludeOptions, this.membershipsFilter, this.sortField, this.queryParam, savedCallback);
            })
            { IsBackground = true }.Start();
#endif
        }

        private void GetMembershipsList(string userId, PNPage page, int limit, bool includeCount, string includeOptions, string filter, List<string> sort, Dictionary<string, object> externalQueryParam, PNCallback<PNGetMembershipsResult> callback)
        {
            if (callback == null)
            {
                throw new ArgumentException("Missing callback");
            }
            PNPage internalPage;
            if (page == null) { internalPage = new PNPage(); }
            else { internalPage = page; }

            IUrlRequestBuilder urlBuilder = new UrlRequestBuilder(config, jsonLibrary, unit, pubnubLog, pubnubTelemetryMgr, pubnubTokenMgr);
            urlBuilder.PubnubInstanceId = (PubnubInstance != null) ? PubnubInstance.InstanceId : "";
            Uri request = urlBuilder.BuildGetAllMembershipsRequest("GET", "", userId, internalPage.Next, internalPage.Prev, limit, includeCount, includeOptions, filter, sort, externalQueryParam);

            RequestState<PNGetMembershipsResult> requestState = new RequestState<PNGetMembershipsResult>();
            requestState.ResponseType = PNOperationType.PNGetMembershipsOperation;
            requestState.PubnubCallback = callback;
            requestState.Reconnect = false;
            requestState.EndPointOperation = this;

            requestState.UsePostMethod = false;
            UrlProcessRequest(request, requestState, false).ContinueWith(r =>
            {
                string json = r.Result.Item1;
                if (!string.IsNullOrEmpty(json))
                {
                    List<object> result = ProcessJsonResponse(requestState, json);
                    ProcessResponseCallbacks(result, requestState);
                }
            }, TaskContinuationOptions.ExecuteSynchronously).Wait();

        }

        private async Task<PNResult<PNGetMembershipsResult>> GetMembershipsList(string userId, PNPage page, int limit, bool includeCount, string includeOptions, string filter, List<string> sort, Dictionary<string, object> externalQueryParam)
        {
            PNResult<PNGetMembershipsResult> ret = new PNResult<PNGetMembershipsResult>();

            PNPage internalPage;
            if (page == null) { internalPage = new PNPage(); }
            else { internalPage = page; }

            IUrlRequestBuilder urlBuilder = new UrlRequestBuilder(config, jsonLibrary, unit, pubnubLog, pubnubTelemetryMgr, pubnubTokenMgr);
            urlBuilder.PubnubInstanceId = (PubnubInstance != null) ? PubnubInstance.InstanceId : "";
            Uri request = urlBuilder.BuildGetAllMembershipsRequest("GET", "", userId, internalPage.Next, internalPage.Prev, limit, includeCount, includeOptions, filter, sort, externalQueryParam);

            RequestState<PNGetMembershipsResult> requestState = new RequestState<PNGetMembershipsResult>();
            requestState.ResponseType = PNOperationType.PNGetMembershipsOperation;
            requestState.Reconnect = false;
            requestState.EndPointOperation = this;

            requestState.UsePostMethod = false;
            Tuple<string, PNStatus> JsonAndStatusTuple = await UrlProcessRequest(request, requestState, false).ConfigureAwait(false);
            ret.Status = JsonAndStatusTuple.Item2;
            string json = JsonAndStatusTuple.Item1;
            if (!string.IsNullOrEmpty(json))
            {
                List<object> resultList = ProcessJsonResponse(requestState, json);
                ResponseBuilder responseBuilder = new ResponseBuilder(config, jsonLibrary, pubnubLog);
                PNGetMembershipsResult responseResult = responseBuilder.JsonToObject<PNGetMembershipsResult>(resultList, true);
                if (responseResult != null)
                {
                    ret.Result = responseResult;
                }
            }

            return ret;
        }

        private static string MapEnumValueToEndpoint(string enumValue)
        {
            string ret = "";
            if (enumValue.ToLowerInvariant() == "custom")
            {
                ret = "custom";
            }
            else if (enumValue.ToLowerInvariant() == "space")
            {
                ret = "space";
            }
            else if (enumValue.ToLowerInvariant() == "space_custom")
            {
                ret = "space.custom";
            }
            return ret;
        }
    }

}
