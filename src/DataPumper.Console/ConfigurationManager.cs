﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DataPumper.Console
{
    public static class ConfigurationManager
    {
        public static T GetRequired<T>(string key)
        {
            return Configuration.GetRequired<T>(key);
        }

        public static T Get<T>(string key, T defaultValue = default(T))
        {
            return Configuration.Get(key, defaultValue);
        }

        public static IConfigurationRoot Configuration { get; set; }
    }

    public static class ConfigurationMixin
    {
        public static string Get(this IConfiguration config, string key, string defaultValue = null)
        {
            return Get<string>(config, key, defaultValue);
        }

        public static List<T> GetList<T>(this IConfiguration config, string key, char prefix = ',')
        {
            try
            {
                var itemList = config[key].Split(new char[] { prefix });
                return itemList.OfType<T>().ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Wrong configuration parameter '{key}'", ex);
            }
        }

        public static T Get<T>(this IConfiguration config, string key, T defaultValue = default(T))
        {
            string value = null;
            if (config != null)
            {
                value = config[key];
            }

            if (string.IsNullOrEmpty(value))
                return defaultValue;
            try
            {
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), value);

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Wrong configuration parameter '{key}' value '{value}'", ex);
            }
        }

        public static T? GetNullable<T>(this IConfiguration config, string key) where T : struct
        {
            var strVal = Get<string>(config, key);
            try
            {
                if (string.IsNullOrEmpty(strVal))
                    return null;
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), strVal);
                return (T)Convert.ChangeType(strVal, typeof(T));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Wrong configuration parameter '{key}' value '{strVal}'", ex);
            }
        }

        public static T GetRequired<T>(this IConfiguration config, string key)
        {
            var res = config.Get<T>(key);
            if (Equals(res, default(T)))
                throw new ApplicationException($"Required configuration parameter '{key}' is missing");
            return res;
        }
    }
}
