using System;
using System.Configuration;

namespace Yak.Configuration {
    public static class Config {
        public static string TryGet(string key, string defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return ConfigurationManager.AppSettings.Get(key);
            } catch {
                return defaultValue;
            }
        }
        public static int TryGet(string key, int defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return int.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static float TryGet(string key, float defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return float.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static decimal TryGet(string key, decimal defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return decimal.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static double TryGet(string key, double defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return double.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static bool TryGet(string key, bool defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return bool.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static TimeSpan TryGet(string key, TimeSpan defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return TimeSpan.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }
        public static DateTime TryGet(string key, DateTime defaultValue) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return DateTime.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch {
                return defaultValue;
            }
        }

        public static string GetString(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return ConfigurationManager.AppSettings.Get(key);
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static int GetInt(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return int.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static float GetFloat(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return float.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static decimal GetDecimal(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return decimal.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static double GetDougle(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return double.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static bool GetBool(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return bool.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static TimeSpan GetTimeSpan(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return TimeSpan.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }
        public static DateTime GetDateTime(string key) {
            key.ThrowIfNullOrWhiteSpace("key");
            try {
                return DateTime.Parse(ConfigurationManager.AppSettings.Get(key));
            } catch (Exception ex) {
                throw CreateConfigurationException(key, ex);
            }
        }

        static Exception CreateConfigurationException(string key, Exception inner) {
            return new ConfigurationErrorsException("Could not get appSetting: {0}".Fmt(key), inner);
        }
    }
}