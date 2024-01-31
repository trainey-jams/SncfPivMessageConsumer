﻿namespace PIV_POC_Client.Models.Config.Openwire
{
    public class OpenWireConnectionConfig
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string BrokerUrl { get; set; } = string.Empty;
    }
}