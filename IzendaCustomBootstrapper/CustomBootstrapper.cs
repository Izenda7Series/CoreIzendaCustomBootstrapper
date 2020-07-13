using Izenda.BI.API.Bootstrappers;
using Izenda.BI.Framework.Models;
using Izenda.BI.Framework.Models.Common;
using Izenda.BI.Framework.Models.Paging;
using IzendaCustomBootstrapper.Extensions;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IzendaCustomBootstrapper
{
    /// <summary>
    /// Custom Bootstrapper to hook into the NancyFx API pipeline
    /// </summary>
    public class CustomBootstrapper : IzendaBootstraper
    {
        const string ApiPrefix = "api";

        private JsonSerializerSettings _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomBootstrapper"/> class.
        /// </summary>
        public CustomBootstrapper()
            : base()
        {
            _serializer = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
        }

        /// <summary>
        /// Allows for pre/post request hooks into the API endpoints
        /// </summary>
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                this.ModifyExportResponse(ctx);
            });

            base.RequestStartup(container, pipelines, context);
        }

        /// <summary>
        /// Modifies the reponse of the export endpoints
        /// </summary>
        /// <param name="ctx">The Nancy Context</param>
        private void ModifyExportResponse(NancyContext ctx)
        {
            if (!(ctx.Request.Url.Path.Contains($"/{ApiPrefix}/export/pdf") || ctx.Request.Url.Path.Contains($"/{ApiPrefix}/export/word") || ctx.Request.Url.Path.Contains($"/{ApiPrefix}/export/excel")))
                return;

            if (ctx?.Response?.Headers != null && ctx.Response.Headers.Any())
            {
                var headerValue = ctx.Response.Headers.First().Value;
                var ogFileName = headerValue.Contains("filename") ? headerValue.Substring(headerValue.LastIndexOf("filename=") + 9) : null;
                if (!string.IsNullOrWhiteSpace(ogFileName))
                {
                    // Updates file name for export document's response
                    var lastIndex = ogFileName.LastIndexOf('.');
                    var newFileName = $"{ogFileName.Substring(0, lastIndex)}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}{ogFileName.Substring(lastIndex)}";
                    ctx.Response.AsAttachment(string.IsNullOrWhiteSpace(newFileName) ? ogFileName : newFileName);
                }
            }
        }
    }
}
