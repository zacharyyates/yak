using System;
using System.Configuration;

namespace Yak.Configuration {
    public static class Config {
        public static int Get(string key, int defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return int.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static string Get(string key, string defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return ConfigurationManager.AppSettings.Get(key);
            } catch {
                return defaultValue;
            }
        }
        public static float Get(string key, float defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return float.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static decimal Get(string key, decimal defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return decimal.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static double Get(string key, double defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return double.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static bool Get(string key, bool defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return bool.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static TimeSpan Get(string key, TimeSpan defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return TimeSpan.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static DateTime Get(string key, DateTime defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");

            try {
                return DateTime.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
    }
}