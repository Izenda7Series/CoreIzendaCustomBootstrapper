using Izenda.BI.API.Bootstrappers;
using Izenda.BI.Utility.AppSetting;
using Nancy;
using Nancy.Bootstrapper;
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
        /// Setting up the application startup
        /// </summary>
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            if (AppSettings.Settings != null && AppSettings.Settings.ContainsKey("izendapassphrase"))
                AppSettings.Settings["izendapassphrase"] = "vqL7SF+9c9FIQEKUOhSZapacQgUQh";

            base.ApplicationStartup(container, pipelines);
        }
    }
}