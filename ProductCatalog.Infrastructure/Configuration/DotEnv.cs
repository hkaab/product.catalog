using System;
using System.IO;

namespace ProductCatalog.Infrastructure.Configuration
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var indexOf = line.IndexOf(value: "=", StringComparison.Ordinal);
                if (indexOf <= 0)
                {
                    continue;
                }

                var key = line.Substring(startIndex: 0, length: indexOf);
                var value = line.Substring(startIndex: indexOf + 1);

                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}