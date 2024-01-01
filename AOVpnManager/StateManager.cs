﻿using Microsoft.Win32;

namespace AOVpnManager
{
    public class StateManager : IStateManager
    {
        private const string LastConnectionName = nameof(LastConnectionName);

        private readonly RegistryKey root;
        private readonly string path;

        public StateManager(RegistryKey root, string path)
        {
            this.root = root;
            this.path = path;
        }

        public void Clean()
        {
            root.DeleteSubKey(path, false);
        }

        public string ReadLastConnectionName()
        {
            using (RegistryKey key = root.OpenSubKey(path))
            {
                return (string)key?.GetValue(LastConnectionName);
            }
        }

        public void UpdateLastConnectionName(string connectionName)
        {
            using (RegistryKey key = OpenOrCreateKey())
            {
                if (connectionName == null)
                {
                    key.DeleteValue(LastConnectionName);
                }
                else
                {
                    key.SetValue(LastConnectionName, connectionName);
                }
            }
        }

        private RegistryKey OpenOrCreateKey()
        {
            return root.OpenSubKey(path, true) ?? root.CreateSubKey(path, true);
        }
    }
}