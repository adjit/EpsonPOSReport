using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsonPOSReport
{
    class UserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("Data Source=METRO-GP1;Integrated Security=SSPI;Initial Catalog=METRO")]
        public string DatabaseConnectionString
        {
            get
            {
                return ((string)this["DatabaseConnectionString"]);
            }
            set
            {
                this["DatabaseConnectionString"] = (string)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string EpsonPriceListFilePath
        {
            get;
            set;
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string EnvisionCustomersFilePath
        {
            get;
            set;
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string SpaListFilePath
        {
            get;
            set;
        }
    }
}
